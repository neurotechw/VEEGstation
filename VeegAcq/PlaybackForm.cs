using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// nVLC, strange namespaces :(
using Declarations;
using Declarations.Players;
using Declarations.Media;
using Implementation;

namespace VeegStation
{
    public partial class PlaybackForm : Form
    {
        //////////////////////////////
        NatFileInfo natfileinfo;
        PationInfo pationinfo;
        /// <summary>
        /// 绝对时间
        /// wsp
        /// </summary>
        DateTime dt;        
        /// <summary>
        /// 相对时间
        /// wsp
        /// </summary>                                   
        DateTime dt_relativetime;      
        /// <summary>
        /// 总时间
        /// wsp
        /// </summary>                  
        DateTime dt_totaltime;   
        /// <summary>
        /// 调整视频速率时，脑电播放速率也要同步
        /// wsp
        /// </summary>                          
        public float speed = 1.0f;
        /// <summary>
        /// 与弹出的视频面板进行数据和状态传递
        /// wsp
        /// </summary>
        private VideoForm video;
        private /*const*/ int WINDOW_SECONDS = 10;                                                                                      //不同的时间基准会有不同的window_seconds，所以取消掉const -- by lxl
        public NationFileInfo _nfi = null;
        int maxPage;
        private List<EegPacket> _packets = new List<EegPacket>();
        /// <summary>
        /// 事件队列
        /// -- by lxl
        /// </summary>
        private Queue<double> _eventsQueue = new Queue<double>();
        private int _Page;
        private int _maxPage;
        public double b;
        //private int _Page;
        private int _totalSeconds;                   
        private DateTime? _LastTime = null;
        private double _CurrentOffset;

        public IVideoPlayer _player;
        private IMedia _media;

        /// <summary>
        /// 灵敏度的选择范围 
        /// -- by lxl
        /// </summary>
        private int[] _sensitivityArray = { 10, 20, 30, 50, 70, 100, 150, 200, 300, 500, 700, 1000, 2000, 5000 };        
        /// <summary>
        /// 当前灵敏度的取值 
        /// -- by lxl
        /// </summary>
        private int _Sensitivity;                                                                                                      
        /// <summary>
        /// 时间基准选择范围 
        /// -- by lxl
        /// </summary>
        private int[] _timeStandardArray = { 6, 10, 15, 30, 60, 100, 300 };                                                            
        /// <summary>
        /// 当前选择时间基准,毫米每秒  
        /// -- by lxl
        /// </summary>
        private int _timeStandard;                                                                                                          
        /// <summary>
        /// 校准Y轴窗口            
        /// - by lxl
        /// </summary>
        private CalibrateForm calibForm;                                                                                               
        /// <summary>
        /// 校验X轴窗口            
        /// -- by lxl
        /// </summary>
        private calibrateXForm calibXForm;                                                                                            
        /// <summary>
        /// 屏幕的宽度,单位为像素点
        /// -- by lxl
        /// </summary>
        private double _pageWidth = 0;
        /// <summary>
        /// 屏幕的高度,单位为像素点
        /// -- by lxl
        /// </summary>
        private double _pageHeight = 0;
        /// <summary>
        /// 图表宽度,单位为毫米        
        /// -- by lxl
        /// </summary>
        private double _X_totalMM;
        /// <summary>
        /// 图标高度，单位为毫米
        /// -- by lxl
        /// </summary>
        private double _Y_totalMM;
        /// <summary>
        /// 一毫米有多少个像素         
        /// -- by lxl
        /// </summary>
        private double _pixelPerMM;
        /// <summary>
        /// 坐标数据显示的比例，用于校准屏幕宽度时用
        /// -- by lxl
        /// </summary>
        private double _rate_that_ensure_1_cm;
        /// <summary>
        /// 判断面板此时是否显示
        /// -- by lxl
        /// </summary>
        private bool _isBoardShow;
        /// <summary>
        /// X轴当前的最大值为多少
        /// -- by lxl
        /// </summary>
        private double _xMaximum;
        /// <summary>
        /// 标识此时是否刚更换面板显示状态，防止不停调用画图函数里的计算
        /// -- by lxl
        /// </summary>
        private bool _isChangingBoardShow;
        /// <summary>
        /// 当前图表的开头为第几秒
        /// -- by lxl
        /// </summary>
        public int _currentSeconds;
        public PlaybackForm(NationFileInfo EegFile)
        {
            InitializeComponent();
            _Sensitivity = 100;
            _isBoardShow = true;
            this.boardToolStripMenuItem.Checked = _isBoardShow;
            this.boardPanel.Visible = _isBoardShow;
            _timeStandard = 30;
            _pixelPerMM = 3.8;
            _rate_that_ensure_1_cm = 1;
            _isChangingBoardShow = true;
            initMenuItems();
            natfileinfo = new NatFileInfo(EegFile.NedFullName);
            pationinfo = new PationInfo(EegFile.NedFullName, natfileinfo.PatOff);
            try
            {
                _nfi = EegFile;
            }
            catch
            { }
            GetHsprogressMax();
            _totalSeconds = EegFile.SampleCount / EegFile.SampleRate;
            hsProgress.Maximum = _totalSeconds;         //不一定是整数秒 故maximum不需要-1
        }

        private void LoadData(int Offset)
        {
            if (_nfi == null)
            {
                return;
            }
            int loadRec = WINDOW_SECONDS * _nfi.SampleRate;                           
            DateTime begin = DateTime.Now;
            FileStream fs = new FileStream(_nfi.NedFullName, FileMode.Open, FileAccess.Read);
            fs.Seek(0x200, SeekOrigin.Begin);
            //fs.Seek(0x6c * Offset * (loadRec - _nfi.SampleRate * (WINDOW_SECONDS-(int)Math.Ceiling(_xMaximum) + 1)), SeekOrigin.Current);
            fs.Seek(0x6c * Offset * _nfi.SampleRate, SeekOrigin.Current);
            byte[] buf = new byte[0x6c];
            _packets.Clear();
            foreach (int x in Enumerable.Range(0, loadRec))
            {
                if (fs.Read(buf, 0, 0x6c) < 0x6c)
                {
                    break;
                }
                double ekg = Util.RawToSignal((short)(buf[6] | (buf[7] << 8)));
                List<double> eeg = new List<double>();
                foreach (int off in Enumerable.Range(0, 19))
                {
                    eeg.Add(Util.RawToSignal((short)(buf[28 + 2 * off] | (buf[29 + 2 * off] << 8))));
                }
                EegPacket pkt = new EegPacket(ekg, eeg.ToArray());
                _packets.Add(pkt);
            }
            fs.Close();
            fs.Dispose();
            DateTime end = DateTime.Now;
            Debug.WriteLine(string.Format("Read a window of data {0} records in {1} seconds", _packets.Count, (end - begin).TotalSeconds));
        }

        private void ShowData()
        {
            DateTime begin = DateTime.Now;
            SeriesCollection col = chartWave.Series;
            double rate = Convert.ToDouble(_Sensitivity) / 100D;                                          //灵敏度比例 -- by lxl
            chartWave.SuspendLayout();
            foreach (int sIdx in Enumerable.Range(0, 20))
            {
                int a = col[sIdx].Points.Count;
                col[sIdx].Points.Clear();
            }
            foreach (int tIdx in Enumerable.Range(0, _packets.Count))
            {
                if (tIdx % 127 == 0) _eventsQueue.Enqueue(tIdx / 128D);
                col[19].Points.AddXY(tIdx / 128.0, _packets[tIdx].EKG / rate + 50);
                foreach (int sIdx in Enumerable.Range(0, 19))
                {
                    double val = _packets[tIdx].EEG[sIdx];
                    val /= 20;
                    val /= rate;                                //根据灵敏度调整 -- by lxl
                    val *= _rate_that_ensure_1_cm;              //根据所校准的单位调整-- by lxl
                    val += (2000 - 100 * sIdx - 50);
                    col[sIdx].Points.AddXY(tIdx / 128.0, val);
                }
            }
            chartWave.ResumeLayout();
            //labelPage.Text = (_Page + 1).ToString() + "/" + _maxPage.ToString();
            DateTime end = DateTime.Now;
            Debug.WriteLine(string.Format("Show a window of data in {0} seconds", (end - begin).TotalSeconds));
        }
        private void PlaybackForm_Load(object sender, EventArgs e)
        {
            if (_nfi == null)
            {
                Close();
                return;
            }
            dt_relativetime=DateTime.Parse("2016-05-23  00:00:00");
            dt_totaltime = DateTime.Parse("2016-05-23 00:00:00");
            dt = _nfi.StartTime;
            displayStartTime.Text = dt.ToLongTimeString().ToString();
            if ((int)_nfi.Duration.TotalSeconds < _nfi.Duration.TotalSeconds)
            dt_totaltime = dt_totaltime.AddSeconds(_nfi.Duration.TotalSeconds+1);
            else
            dt_totaltime = dt_totaltime.AddSeconds(_nfi.Duration.TotalSeconds);
            displayRecordingTime.Text = dt_relativetime.ToLongTimeString().ToString();
            displayTotalTime.Text = dt_totaltime.ToLongTimeString().ToString();
            chartWave.Series.Clear();
            foreach (int idx in Enumerable.Range(0, 20))
            {
                Series ser = new Series();
                ser.ChartType = SeriesChartType.FastLine;
                ser.BorderDashStyle = ChartDashStyle.Solid;
                //ser.Color = Color.Black;
                chartWave.Series.Add(ser);
            }
            //chartWave.ChartAreas[0].AxisX.Maximum = WINDOW_SECONDS;
            //hsProgress.
            //if (_nfi != null)
            //{
            //    LoadData(0);
            //    ShowData();
            //}
            IMediaPlayerFactory factory = new MediaPlayerFactory();
            _player = factory.CreatePlayer<IVideoPlayer>();
            _player.WindowHandle = panelVideo.Handle;
            _player.AspectRatio = AspectRatioMode.Mode2; // fill?
            if (_nfi.HasVideo)
            {
                _media = factory.CreateMedia<IMediaFromFile>(_nfi.VideoFullName);
                _player.Open(_media);
                Debug.WriteLine(_player.IsSeekable);
                Debug.WriteLine(_player.Length);
                _player.Time = (long)_nfi.VideoOffset * 1000;
               // _player.Play();
               _player.Pause();
            }
            video = new VideoForm(this);
            video.Hide();
        }

        private void PagePrev()
        {
            _currentSeconds -= (int)(Math.Floor(_xMaximum));
            if (_currentSeconds <= 0)
                _currentSeconds = 0;
            LoadData(_currentSeconds);
            ShowData();
            set_hsScrollBarValue();
        }

        private void PageNext()
        {
                _currentSeconds += (int)(Math.Floor(_xMaximum));
                if (_currentSeconds > _totalSeconds - hsProgress.LargeChange + 1)
                    _currentSeconds = _totalSeconds - hsProgress.LargeChange + 1;
                LoadData(_currentSeconds);
                ShowData();
                set_hsScrollBarValue();
              
        }
        public void SetTimeLine()
        {
    //        chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _CurrentOffset - _Page * chartWave.ChartAreas[0].AxisX.Maximum;////////添加需要拖动的一些位置进行
	          chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset += 0.1*speed;
        }
        private void SyncVideo()
        {
            //_player.Position = (float)_CurrentOffset * 1000;
            _player.Time = (long)_CurrentOffset * 1000;
            //+ hsProgress.Value * 100;//自己添加" + hsProgress.Value*100 "
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            //timer.Enabled = false;
            Pause();
            PagePrev();
            _player.Time = _currentSeconds * 1000;
            video.player.Time = _currentSeconds * 1000;
            SyncVideo();
            update_BtnEnable();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {                
            Pause();
            PageNext();
            changed();
            _player.Time = _currentSeconds * 1000;
            video.player.Time =_currentSeconds * 1000;
            SyncVideo();
            update_BtnEnable();
        }
        /// <summary>
        /// Time 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Tick(object sender, EventArgs e)
        {
          DateTime now = DateTime.Now;
           _CurrentOffset += (now - _LastTime.Value).TotalSeconds*speed;
            _LastTime = now;
     //       if (_CurrentOffset > _nfi.Duration.TotalSeconds+0.3)/////////////////
             if (_currentSeconds+chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)/////////////////
            {
                Pause();
                _CurrentOffset = 0;
                _LastTime = null;
                video.player.Pause();
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                video.btn_play.Enabled = true;
                video.btn_pause.Enabled = false;
            }
            else
             {
                //          if (_CurrentOffset > (_Page + 1) * chartWave.ChartAreas[0].AxisX.Maximum)
                 if (chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _xMaximum)
                {
                    PageNext();
                }
                else
                {             
                    SetTimeLine();          
                    dt = dt.AddSeconds(0.1 * speed);
                    DateTime a = DateTime.Parse("2016-05-23  00:00:00");
                    displayStartTime.Text = dt.ToLongTimeString().ToString();
                    dt_relativetime = dt_relativetime.AddSeconds(0.1 * speed);
                    b = (dt_relativetime - a).TotalSeconds;
                    displayRecordingTime.Text = dt_relativetime.ToLongTimeString().ToString();
                }
            }
            //为了保证最后的那条线画在正确的位置，所以相对时间和画的那条线最后需要特殊处理一下
            //wsp
             if (_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)
             {
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _nfi.Duration.TotalSeconds - _currentSeconds;
                 displayRecordingTime.Text = dt_totaltime.ToLongTimeString().ToString();
             }
        }

        public void Play()
        {
            if (!_LastTime.HasValue)
            {
                _LastTime = DateTime.Now;
            }
            timer.Enabled = true;
            if (_nfi.HasVideo)
            {
                // TODO:
               // _player.Time = (long)(_nfi.VideoOffset * 1000);
                if (_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)
                {
                    clear();
                } 
                if (!_player.IsPlaying)
                    _player.Play();
                if (_currentSeconds == 0)
                    _player.Time = (long)_nfi.VideoOffset * 1000;
            }
            else
            {
                timer.Enabled = false;
                MessageBox.Show("没有视频", "提示");
            }
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
        }

        public void Pause()
        {
            timer.Enabled = false;
            _LastTime = null;
            if (_nfi.HasVideo)
            {
                if (_player.IsPlaying)
                    _player.Pause();
            }
            btnPause.Enabled = false;
            btnPlay.Enabled = true;
        }

        public void btnPlay_Click(object sender, EventArgs e)
        {
            Play();
            video.Play();
         //   video.Hide();  
        }

        public void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
            video.Pause();
        }

        private void set_hsScrollBarValue()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = b-_currentSeconds;
            hsProgress.Value = _currentSeconds;
        }

        private void hsProgress_MouseCaptureChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Format("Scroll mouse cap changed {0}", e));
            Pause();
            if (_currentSeconds != hsProgress.Value)
            {
                _currentSeconds = hsProgress.Value;
                changed();
                LoadData(_currentSeconds);
                ShowData();
                update_BtnEnable();
                SyncVideo();
            }
        }

        private void panel_next_click(object sender, EventArgs e)
        {
            btnNext_Click(sender, e);
        }

        private void panel_prev_click(object sender, EventArgs e)
        {
            btnPrev_Click(sender, e);
        }

        /// <summary>
        /// 更新翻页按钮是否可用
        /// -- by lxl
        /// </summary>
        private void update_BtnEnable()
        {
            if (_currentSeconds <= 0)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                return;
            }
            else if (_currentSeconds > _totalSeconds - hsProgress.LargeChange)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                return;
            }
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
        }

        private void btn_enlarge_Click(object sender, EventArgs e)
        {
            if (_player.VideoScale <= 2.0f)
                _player.VideoScale += 0.5f;
            else MessageBox.Show("已放大到最大化");
        }

        private void btn_shrink_Click(object sender, EventArgs e)
        {
            if (_player.VideoScale >= 0)
            {
                _player.VideoScale -= 0.5f;
            }
            else MessageBox.Show("已经缩小到最小");
        }
        private void pationInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set_PationInfoPanel(pationinfo);
            this.PationInfoPanel.Visible = true;
            this.DetectionInfoPanel.Visible = false;
        }

        private void detectionInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set_DetectionInfoPanel(natfileinfo, pationinfo);
            this.PationInfoPanel.Visible = false;
            this.DetectionInfoPanel.Visible = true;
        }

        private void hideingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PationInfoPanel.Visible = false;
            this.DetectionInfoPanel.Visible = false;
        }

        private void Set_PationInfoPanel(PationInfo _pationinfo)
        {
            this.PatNameTextBt.Text = _pationinfo.Name;
            this.PatGenderCB.Text = _pationinfo.Gender;
            this.PatIDTextBt.Text = _pationinfo.ID;
            this.PatAgeTextBt.Text = _pationinfo.Age;
            this.SingleHandAdvanCB.Text = _pationinfo.Handedness;
        }

        private void Set_DetectionInfoPanel(NatFileInfo _nationfileinfo, PationInfo _pationinfo)
        {
            this.DetectionTextBt.Text = _pationinfo.ID;
            this.RequesterTextBt.Text = _pationinfo.Name;
            this.TechnicianTextBt.Text = _pationinfo.ResidentDoctor;
            this.PhysicianTextBT.Text = _pationinfo.OperateDoctor;
            this.PationStateTextBt.Text = _pationinfo.State;
            this.PharmacyTextBt.Text = _pationinfo.Medicine;
            this.DetectionRemarksTextBt.Text = _pationinfo.Diagnosis;
            this.FilePathTextBt.Text = _pationinfo.FilePath;
        }
        #region  拖动移动病人属性和检查属性   
        Point pt;
        private void PationInfoPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = System.Windows.Forms.Control.MousePosition.X - pt.X;
                int py = System.Windows.Forms.Control.MousePosition.Y - pt.Y;
                PationInfoPanel.Location = new Point(PationInfoPanel.Location.X + px, PationInfoPanel.Location.Y + py);

                pt = System.Windows.Forms.Control.MousePosition;
            }
        }

        private void PationInfoPanel_MouseDown(object sender, MouseEventArgs e)
        {
            pt = System.Windows.Forms.Control.MousePosition;
        }

        Point _pt;
        private void DetectionInfoPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = System.Windows.Forms.Control.MousePosition.X - _pt.X;
                int py = System.Windows.Forms.Control.MousePosition.Y - _pt.Y;
                DetectionInfoPanel.Location = new Point(DetectionInfoPanel.Location.X + px, DetectionInfoPanel.Location.Y + py);

                _pt = System.Windows.Forms.Control.MousePosition;
            }
        }

        private void DetectionInfoPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _pt = System.Windows.Forms.Control.MousePosition;
        }
        #endregion
        private void hsProgress_ValueChanged(object sender, ScrollEventArgs e)
        {
            if (_Page < maxPage)
            {
			    _Page=hsProgress.Value;
                changed();
                LoadData(_Page);
                ShowData();
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
            }
            Debug.WriteLine(string.Format("Scroll changed to {0}", hsProgress.Value));
//            SetTimeLine();
//            SyncVideo();
            _player.Time = _Page * WINDOW_SECONDS * 1000;
            video.player.Time = _Page * WINDOW_SECONDS * 1000;
            video.btn_play.Enabled = true;
           // _player.
            //_player.Play();
            //_player.Pause();
        }

        private void panelVideo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("********");
        }

        /// <summary>
        /// 灵敏度点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void μvcmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string num = sender.ToString().Substring(0,sender.ToString().Length-5);
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in this.sensitivityToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;
            _Sensitivity = int.Parse(num);
            ShowData();
        }

        /// <summary>
        /// 时间基准菜单选项点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerStandartMenuItem_Click(object sender, EventArgs e)
        {
            string num = sender.ToString().Substring(0, sender.ToString().Length - 6);
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in this.timeStandartToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;
            _timeStandard = int.Parse(num);
            setAxisXMaximum(_X_totalMM / _timeStandard);
            _isChangingBoardShow = true;                        //让画图函数重新计算一下当前的_xMaximum是多少
            LoadData(_currentSeconds);
            ShowData();
        }

        /// <summary>
        /// 设置可显示的X轴最大值
        /// -- by lxl
        /// </summary>
        /// <param name="max"></param>
        private void setXMaximum(double max)
        {
            _xMaximum=max;
            this.hsProgress.LargeChange = (int)Math.Ceiling(_xMaximum) - 2;
        }

        /// <summary>
        /// 设置一页有多少秒
        /// -- by lxl
        /// </summary>
        /// <param name="wins"></param>
        private void updateWindowSeconds()//int wins)
        {
            WINDOW_SECONDS = (int)Math.Ceiling(chartWave.ChartAreas[0].AxisX.Maximum);            //保证取的数据不小于当前窗口应该显示的数据,因为window_seconds为int，而秒数可能为小数
        }

        /// <summary>
        /// 设置X轴最大值，同时也会更新window_seconds的值以适应显示
        /// -- by lxl
        /// </summary>
        /// <param name="max"></param>
        private void setAxisXMaximum(double max)
        {
            chartWave.ChartAreas[0].AxisX.Maximum = max;
            updateWindowSeconds();
        }
        #region 进度条，PagePrev,PageNext变化时，对应的时间也要发生变化；  ----wsp
        private void changed()
        {
            //_CurrentOffset = _Page * chartWave.ChartAreas[0].AxisX.Maximum;
            dt = _nfi.StartTime;
            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt = dt.AddSeconds(_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            displayStartTime.Text = dt.ToLongTimeString().ToString();
            dt_relativetime = dt_relativetime.AddSeconds(_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            displayRecordingTime.Text = dt_relativetime.ToLongTimeString().ToString();
        }
        #endregion
        /// <summary>
        /// 初始化时间基准与灵敏度的选项
        /// -- by lxl
        /// </summary>
        private void initMenuItems()
        {
            foreach (int t in _timeStandardArray)
            {
                System.Windows.Forms.ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = "timeStandardMenuItem" + t;
                item.Size = new Size(140, 22);
                item.Text = t + "mm/sec";
                item.Click += new EventHandler(this.timerStandartMenuItem_Click);
                if (t == _timeStandard)
                    item.Checked = true;
                this.timeStandartToolStripMenuItem.DropDownItems.Add(item);
            }
            foreach (int s in _sensitivityArray)
            {
                System.Windows.Forms.ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = "vcmToolStripMenuItem_100" + s;
                item.Size = new Size(140, 22);
                item.Text = s + "μv/cm";
                item.Click += new EventHandler(this.μvcmToolStripMenuItem_Click);
                if (s == _Sensitivity)
                    item.Checked = true;
                this.sensitivityToolStripMenuItem.DropDownItems.Add(item);
            }
        }
       /// <summary>
       /// 视频加速
       /// wsp
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btn_accelerate_Click(object sender, EventArgs e)
        {
            _player.PlaybackRate = _player.PlaybackRate * 2;
            speed = _player.PlaybackRate;
            video.player.PlaybackRate = _player.PlaybackRate;
        }
        /// <summary>
        /// 视频减速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_decelerate_Click(object sender, EventArgs e)
        {
            _player.PlaybackRate = _player.PlaybackRate / 2;
            speed = _player.PlaybackRate;
            video.player.PlaybackRate = _player.PlaybackRate;
        }
        /// <summary>
        /// 视频面板弹出按键设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (panelVideo.Visible == true)
            {
                btn_hide.Text = "显示";
                panelVideo.Visible = false;
                video.Show();
            }
            else
            {
                btn_hide.Text = "隐藏";
                panelVideo.Visible = true;
                video.Hide();
            }
        }      
        #region 进度条最大值
        private void  GetHsprogressMax()
        {
            SecPerPage();
            hsProgress.Maximum = maxPage;
        }
        #endregion
        #region 数据有多少页
        private void SecPerPage()
        {
            maxPage = (_nfi.SampleCount + ((int)(10 * 30D / Convert.ToDouble(_timeStandard)) * _nfi.SampleRate) - 1) / ((int)(10 * 30D / Convert.ToDouble(_timeStandard)) * _nfi.SampleRate);
        }
        #endregion
        /// <summary>
        /// Y轴校准点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrateYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calibForm == null)
                calibForm = new CalibrateForm(this);
            this.calibForm.Show();
            this.calibForm.BringToFront();
        }      
        /// <summary>
        /// Y轴校准
        /// -- by lxl
        /// </summary>
        /// <param name="height">一厘米多少像素点</param>
        public void calibrateY(double height)
        {
            _pixelPerMM = height / 10D;
            _Y_totalMM = _pageHeight / _pixelPerMM;
            double mmPerGrid = _Y_totalMM / 20;
            _rate_that_ensure_1_cm = 10 / mmPerGrid;
            ShowData();
        }
        /// <summary>
        /// X轴校准点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrateXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calibXForm == null)
                calibXForm = new calibrateXForm(this);
            calibXForm.Show();
            calibXForm.BringToFront();
        }
        /// <summary>
        /// X轴校准函数
        /// </summary>
        /// <param name="width">一厘米多少个像素点</param>
        public void calibrateX(double width)
        {
            _pixelPerMM = width / 10D;
            _X_totalMM = _pageWidth / _pixelPerMM;
            setAxisXMaximum(_X_totalMM / _timeStandard);
            LoadData(_currentSeconds);
            ShowData();
        }
        /// <summary>
        /// chart的重绘函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartPaint(object sender, PaintEventArgs e)
        {
            double drawPosition;
            SolidBrush rectBrush = new SolidBrush(Color.FromArgb(200, Color.Red));
            SolidBrush strBrush = new SolidBrush(Color.White);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Bold);
            Pen dotPen = new Pen(Color.Red, 2);
            dotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            if (_pageWidth == 0)
            {
                _pageWidth = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisX.Maximum) - this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(0);
                _X_totalMM = _pageWidth / _pixelPerMM;
                //this.chartWave.ChartAreas[0].AxisX.Maximum = _X_totalMM / _timeStandard;
                setAxisXMaximum(_X_totalMM / _timeStandard);
                LoadData(_currentSeconds);
                ShowData();
                _pageHeight = Math.Abs(this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisY.Maximum) - this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0));
                _Y_totalMM = _pageHeight / _pixelPerMM;
            }
            while (_eventsQueue.Count > 0)
            {
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(_eventsQueue.Dequeue());
                e.Graphics.FillRectangle(rectBrush, new Rectangle((int)drawPosition - 40, 5, 80, 15));
                e.Graphics.DrawString("病人事件", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15));
                e.Graphics.DrawLine(dotPen, new Point((int)drawPosition, 5), new Point((int)drawPosition, (int)this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0)));
            }
            if (_isChangingBoardShow)
            {
                if (_isBoardShow)
                {
                    setXMaximum(this.chartWave.ChartAreas[0].AxisX.PixelPositionToValue(this.boardPanel.Location.X));
                }
                else
                {
                    setXMaximum(this.chartWave.ChartAreas[0].AxisX.Maximum);
                }
                _isChangingBoardShow = false;
            }
        }
        /// <summary>
        /// 面板点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isBoardShow = !_isBoardShow;
            this.boardPanel.Visible = _isBoardShow;
            this.boardToolStripMenuItem.Checked = _isBoardShow;
            _isChangingBoardShow = true;
            this.chartWave.Invalidate();
        }
        /// <summary>
        /// 重头播放，对数据进行初始化处理
        /// wsp
        /// </summary>
        private void clear()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0.0;
            _currentSeconds = 0;
            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt_totaltime = DateTime.Parse("2016-05-23 00:00:00");
            _player.Time = (long)_nfi.VideoOffset * 1000;
            dt = _nfi.StartTime;
            hsProgress.Value = 0;
            LoadData(_currentSeconds);
            ShowData();
        }
    }
}
