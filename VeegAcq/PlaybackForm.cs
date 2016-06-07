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
      
        //定义绝对时间--by wsp
        DateTime DT;        
        
        //定义相对时间--by wsp                       
        DateTime DT_RelativeTime;      
        
        //定义总时间--by wsp
        DateTime DT_TotalTime;   
       
        //定义视频播放速率--by wsp
        public float Speed = 1.0f;
        
        //定义视频面板Form--by wsp
        private VideoForm video;

        private /*const*/ int WINDOW_SECONDS = 10;                                                                                      //不同的时间基准会有不同的window_seconds，所以取消掉const -- by lxl
        public NationFileInfo _nfi = null;

        //定义最大页数
        int maxPage;

        private List<EegPacket> _packets = new List<EegPacket>();
        /// <summary>
        /// 事件队列
        /// -- by lxl
        /// </summary>
        private Queue<double> _eventsQueue = new Queue<double>();
        private int _Page;

        //定义走了多少时间
        public double LongTime;

        private int _totalSeconds;      
             
        private DateTime? _LastTime = null;

        //偏移量--by xls
        public double CurrentOffset;

        //定义播放器--by xls
        public IVideoPlayer Player;

        //定义视频media--by xls
        private IMedia media;

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
        private int[] _timeStandardArray = { 6, 10, 15, 30, 60, 100, 200 };                                                            
        /// <summary>
        /// 当前选择时间基准,毫米每秒  
        /// -- by lxl
        /// </summary>
        private int _timeStandard;
        /// <summary>
        /// 信道显示选择范围
        /// -- by lxl
        /// </summary>
        private int[] _signalNumArray = { 1, 2, 4, 8, 16, 20 };
        /// <summary>
        /// 当前显示的信道数据
        /// -- by lxl
        /// </summary>
        private int _signalNum;
        /// <summary>
        /// 当前显示的最上面的信道,当选择显示的通道小鱼20时拖动滑动条时需要用到
        /// -- by lxl
        /// </summary>
        private int _currentTopSignal;                                                                            
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
        public int CurrentSeconds;

        public PlaybackForm(NationFileInfo EegFile)
        {
            InitializeComponent();
            _Sensitivity = 100;
            CurrentOffset = 0;
            if (EegFile.HasVideo)
                _isBoardShow = true;
            else
                _isBoardShow = false;
            this.boardToolStripMenuItem.Checked = _isBoardShow;
            this.boardPanel.Visible = _isBoardShow;
            _timeStandard = 30;
            _pixelPerMM = 3.8;
            _rate_that_ensure_1_cm = 1;
            _isChangingBoardShow = true;
            _signalNum = 20;
            _currentTopSignal = 0;
            setVScrollVisible(false);
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
            double interval = 2000D / _signalNum;
            chartWave.SuspendLayout();
            foreach (int sIdx in Enumerable.Range(0, 20))
            {
                int a = col[sIdx].Points.Count;
                col[sIdx].Points.Clear();
            }
            foreach (int tIdx in Enumerable.Range(0, _packets.Count))
            {
                if (tIdx % 127 == 0) _eventsQueue.Enqueue(tIdx / 128D);
                //col[19].Points.AddXY(tIdx / 128.0, _packets[tIdx].EKG / rate + 50);
                foreach (int sIdx in Enumerable.Range(_currentTopSignal, _signalNum))
                {
                    if (sIdx == 19)
                    {
                        col[19].Points.AddXY(tIdx / 128.0, _packets[tIdx].EKG / rate + (2000D - interval * (19 - _currentTopSignal) - interval / 2));
                        continue;
                    }
                    double val = _packets[tIdx].EEG[sIdx];
                    val /= 20;
                    val /= rate;                                //根据灵敏度调整 -- by lxl
                    val *= _rate_that_ensure_1_cm;              //根据所校准的单位调整-- by lxl
                    //val += (2000 - 100 * sIdx - 50);
                    val += (2000D - interval * (sIdx - _currentTopSignal) - interval / 2);
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

            #region 绝对时间和总时间初始化模块--by wsp
            //初始化绝对时间和总时间，并显示
            DT_RelativeTime=DateTime.Parse("2016-05-23  00:00:00");
            DT_TotalTime = DateTime.Parse("2016-05-23 00:00:00");
            DT = _nfi.StartTime;
            displayStartTime.Text = DT.ToLongTimeString().ToString();
            if ((int)_nfi.Duration.TotalSeconds < _nfi.Duration.TotalSeconds)
            DT_TotalTime = DT_TotalTime.AddSeconds(_nfi.Duration.TotalSeconds+1);
            else
            DT_TotalTime = DT_TotalTime.AddSeconds(_nfi.Duration.TotalSeconds);
            displayRecordingTime.Text = DT_RelativeTime.ToLongTimeString().ToString();
            displayTotalTime.Text = DT_TotalTime.ToLongTimeString().ToString();
            #endregion

            chartWave.Series.Clear();
            foreach (int idx in Enumerable.Range(0, 20))
            {
                Series ser = new Series();
                ser.ChartType = SeriesChartType.FastLine;
                ser.BorderDashStyle = ChartDashStyle.Solid;
                chartWave.Series.Add(ser);
            }         
            if (_nfi.HasVideo)
            {
                IMediaPlayerFactory factory = new MediaPlayerFactory();

                //创建播放器
                Player = factory.CreatePlayer<IVideoPlayer>();

                //将panelvideo句柄赋给播放器窗口句柄，在panelvideo上面播放视频
                Player.WindowHandle = panelVideo.Handle;

                //宽高比例
                Player.AspectRatio = AspectRatioMode.Mode2;
                if (_nfi.HasVideo)
                {
                    //获得需要播放的视频的数据文件
                    media = factory.CreateMedia<IMediaFromFile>(_nfi.VideoFullName);
                    Player.Open(media);
                    Debug.WriteLine(Player.IsSeekable);
                    Debug.WriteLine(Player.Length);

                    //初始阶段，不播放视频
                    Player.Pause();
                }

                //方便该Form与视频弹出Form进行数据交换
                video = new VideoForm(this);
                video.Show();
                video.Hide();
            }
        }
        private void PagePrev()
        {
            CurrentSeconds -= (int)(Math.Floor(_xMaximum));
            if (CurrentSeconds <= 0)
                CurrentSeconds = 0;
            LoadData(CurrentSeconds);
            ShowData();
            set_hsScrollBarValue();
        }
        /// <summary>
        /// 点击上一页时所用，与从头播放分开来写
        ///  --by wsp
        /// </summary>
        private void PagePrev2()
        {
            CurrentSeconds -= (int)(Math.Floor(_xMaximum));
            if (CurrentSeconds <= 0)
                CurrentSeconds = 0;
            CurrentOffset = CurrentSeconds;
            LoadData(CurrentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();
        }

        private void PageNext()
        {
                CurrentSeconds += (int)(Math.Floor(_xMaximum));
                if (CurrentSeconds > _totalSeconds - hsProgress.LargeChange + 1)
                    CurrentSeconds = _totalSeconds - hsProgress.LargeChange + 1;
                LoadData(CurrentSeconds);
                ShowData();
                set_hsScrollBarValue();              
        }

        /// <summary>
        /// 点击下一页时所用,与重头播放分开
        /// --by wsp
        /// </summary>
        private void PageNext2()
        {
            CurrentSeconds += (int)(Math.Floor(_xMaximum));
            if (CurrentSeconds > _totalSeconds - hsProgress.LargeChange + 1)
                CurrentSeconds = _totalSeconds - hsProgress.LargeChange + 1;
            CurrentOffset = CurrentSeconds;
            LoadData(CurrentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();     
        }

        /// <summary>       
        /// 画竖线
        /// --by wsp
        /// </summary>
        public void SetTimeLine()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = (CurrentOffset - CurrentSeconds);
        }

        private void SyncVideo()
        {
            Player.Time = (long)CurrentOffset * 1000;
        }

        /// <summary>
        /// 该Form与videoForm视频变化
        /// --by wsp
        /// </summary>
        private void TimeChange()
        {
            //视频同步变化
            Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;

            //videoForm视频同步变化
            video.Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Pause();
            PagePrev2();

            //点击了上一页，进度条，时间要变化
            Changed();

            ////视频同步变化
            //Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
          
            ////videoForm视频同步变化
            //video.Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            if (_nfi.HasVideo)
                TimeChange();
            update_BtnEnable();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {                
            Pause();
            PageNext2();

            //点击了下一页，进度条，时间要变化
            Changed();

            ////视频同步变化
            //Player.Time = CurrentSeconds * 1000+(long)_nfi.VideoOffset*1000+(long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset*1000;
            
            ////videoForm视频同步变化
            //video.Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            if (_nfi.HasVideo)
                TimeChange();
            update_BtnEnable();

        }

        #region  Time时间--by wsp
        /// <summary>
        /// Time事件
        /// --by wsp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Tick(object sender, EventArgs e)
        {
           DateTime Now = DateTime.Now;

            //保证偏移量要与视频时间是正常的秒数
           double remainTime = (Now - _LastTime.Value).TotalSeconds * Speed;
           CurrentOffset += remainTime;
            _LastTime = Now;

            //脑电播放结束处理
            if(CurrentOffset>=_nfi.Duration.TotalSeconds)
            {
                Pause();
                _LastTime = null;            
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                if (_nfi.HasVideo)
                {
                    video.Player.Pause();
                    video.btn_play.Enabled = true;
                    video.btn_pause.Enabled = false;
                }
            }
            else
             {

                //播放本页结束，下一页处理
                if (chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _xMaximum)
                {
                    PageNext();
                }
                else
                {             
                    SetTimeLine();
                    DT = DT.AddSeconds(remainTime);
                    DateTime vTime = DateTime.Parse("2016-05-23  00:00:00");
                //    displayStartTime.Text = DT.ToLongTimeString().ToString();
                    DT_RelativeTime = DT_RelativeTime.AddSeconds(remainTime);
                    LongTime = (DT_RelativeTime - vTime).TotalSeconds;
                //    displayRecordingTime.Text = DT_RelativeTime.ToLongTimeString().ToString();
                    AllShowTime(GetStarTotalSecond(), CurrentSeconds, this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                    ShowTime(CurrentSeconds, this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                }
            }
            //为了保证竖线画在正确的位置，所以相对时间和竖线最后需要特殊处理一下--by wsp
            if (CurrentOffset >= _nfi.Duration.TotalSeconds)
             {
                 chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _nfi.Duration.TotalSeconds - CurrentSeconds;
                 DateTime dt_TotalTime1;
                 DateTime dt_Begin1;
                 dt_Begin1 = _nfi.StartTime;
                 dt_TotalTime1 = DateTime.Parse("2016-05-23 00:00:00");

                //由于脑电数据总时间可能为小数，但是时间显示上作为整数显示，因此要特殊处理（不满一秒的也按照一秒来计算）
                 if ((int)_nfi.Duration.TotalSeconds < _nfi.Duration.TotalSeconds)
                 {
                     dt_TotalTime1 = dt_TotalTime1.AddSeconds(_nfi.Duration.TotalSeconds + 1);
                     dt_Begin1 = dt_Begin1.AddSeconds(_nfi.Duration.TotalSeconds + 1);
                 }
                 else
                 {
                     dt_TotalTime1 = dt_TotalTime1.AddSeconds(_nfi.Duration.TotalSeconds);
                     dt_Begin1 = dt_Begin1.AddSeconds(_nfi.Duration.TotalSeconds);
                 }
                 displayStartTime.Text = dt_Begin1.ToLongTimeString().ToString();
                 displayRecordingTime.Text = dt_TotalTime1.ToLongTimeString().ToString();
             }
        }
        #endregion

        /// <summary>
        /// 播放
        ///--by  wsp
        /// </summary>
        public void Play()
        {
            if (!_LastTime.HasValue)
            {
                _LastTime = DateTime.Now;
            }
            timer.Enabled = true;
            if (_nfi.HasVideo)
            {

                //如果播放完毕后再次点击播放按钮，则重头开始播放
                if (CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)
                {
                    Clear();
                } 
                if (!Player.IsPlaying)
                    Player.Play();         
                    Player.Time = (long)(_nfi.VideoOffset * 1000 + CurrentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000);
            }
            else
            {
                timer.Enabled = true;
                //MessageBox.Show("没有视频", "提示");
            }
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
        }

        /// <summary>
        /// 暂停
        ///--by wsp
        /// </summary>
        public void Pause()
        {
            timer.Enabled = false;
            _LastTime = null;
            if (_nfi.HasVideo)
            {
                if (Player.IsPlaying)
                    Player.Pause();
            }
            btnPause.Enabled = false;
            btnPlay.Enabled = true;
        }

        /// <summary>
        /// 播放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnPlay_Click(object sender, EventArgs e)
        {
            Play();
            if (_nfi.HasVideo)
            {
                video.Play();
                if (CurrentSeconds == 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                    video.Player.Time = (long)_nfi.VideoOffset * 1000;
                if (CurrentSeconds != 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                    video.Player.Time = (long)_nfi.VideoOffset * 1000 + CurrentSeconds * 1000;
            }
        }

        /// <summary>
        /// 暂停事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
            if(_nfi.HasVideo)
            video.Pause();
        }

        /// <summary>
        /// 仿造尼高利，下一页数据加载时从整数部分加载，画线则是从上一页的_xMaximum开始；
        /// --by wsp
        /// </summary>
        private void set_hsScrollBarValue()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = LongTime - CurrentSeconds;
            hsProgress.Value = CurrentSeconds;
        }

        /// <summary>
        /// 点击pagenext或者pageprev时，画线偏移量要变化
        /// --by wsp
        /// </summary>
        private void set_hsScrollBarValue_Next()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
            hsProgress.Value = CurrentSeconds;
        }

        /// <summary>
        /// 鼠标点击进度条改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsProgress_MouseCaptureChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Format("Scroll mouse cap changed {0}", e));
            Pause();
            if (CurrentSeconds != hsProgress.Value)
            {
                CurrentSeconds = hsProgress.Value;
                Changed();
                CurrentOffset = CurrentSeconds;
                LoadData(CurrentSeconds);
                ShowData();
                update_BtnEnable();
                if(_nfi.HasVideo)
                Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
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
            if (CurrentSeconds <= 0)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                return;
            }
            else if (CurrentSeconds > _totalSeconds - hsProgress.LargeChange)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                return;
            }
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
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

        #region 鼠标拖动--by xcg&wsp
        /// <summary>
        ///  拖动移动病人属性和检查属性   
        ///  --by xcg&wsp
        /// </summary>
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

        /// <summary>
        /// 改变进度条值
        /// --by wsp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsProgress_ValueChanged(object sender, ScrollEventArgs e)
        {
            if (_Page < maxPage)
            {
			    _Page=hsProgress.Value;
                Changed();
                LoadData(_Page);
                ShowData();
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
            }
            Debug.WriteLine(string.Format("Scroll changed to {0}", hsProgress.Value));
            if (_nfi.HasVideo)
            {
                Player.Time = CurrentSeconds * 1000;
                video.Player.Time = CurrentSeconds * 1000; 
                video.btn_play.Enabled = true;
            }
           
        }

        /// <summary>
        /// 测试点击事件是否响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            LoadData(CurrentSeconds);
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

        /// <summary>
        /// --by wsp
        /// 进度条，PagePrev,PageNext变化时，对应的时间也要发生变化；
        /// </summary>
        private void Changed()
        {
            DT = _nfi.StartTime;
            DT_RelativeTime = DateTime.Parse("2016-05-23  00:00:00");
            DT = DT.AddSeconds(CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            AllShowTime(GetStarTotalSecond(), CurrentSeconds, chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
      //      displayStartTime.Text = DT.ToLongTimeString().ToString();
            DT_RelativeTime = DT_RelativeTime.AddSeconds(CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
      //      displayRecordingTime.Text = DT_RelativeTime.ToLongTimeString().ToString();
            ShowTime(CurrentSeconds, chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
        }

        /// <summary>
        /// 初始化时间基准与灵敏度的选项
        /// -- by lxl
        /// </summary>
        private void initMenuItems()
        {
            System.Windows.Forms.ToolStripMenuItem item ;//= new ToolStripMenuItem();
            foreach (int t in _timeStandardArray)
            {
                item = new ToolStripMenuItem();
                item.Name = "timeStandardMenuItem_" + t;
                item.Size = new Size(140, 22);
                item.Text = t + "mm/sec";
                item.Click += new EventHandler(this.timerStandartMenuItem_Click);
                if (t == _timeStandard)
                    item.Checked = true;
                this.timeStandartToolStripMenuItem.DropDownItems.Add(item);
            }
            foreach (int s in _sensitivityArray)
            {
                item = new ToolStripMenuItem();
                item.Name = "vcmToolStripMenuItem_" + s;
                item.Size = new Size(140, 22);
                item.Text = s + "μv/cm";
                item.Click += new EventHandler(this.μvcmToolStripMenuItem_Click);
                if (s == _Sensitivity)
                    item.Checked = true;
                this.sensitivityToolStripMenuItem.DropDownItems.Add(item);
            }
            foreach (int si in _signalNumArray)
            {
                item = new ToolStripMenuItem();
                item.Name = "signalToolStripMenuItem_" + si;
                item.Size = new Size(140, 22);
                item.Text = si.ToString();
                item.Click += new EventHandler(this.signalToolStripMenuItem_Click);
                if (si == _signalNum)
                    item.Checked = true;
                this.signalToolStripMenuItem.DropDownItems.Add(item);
            }
        }
       /// <summary>
       /// 视频加速
       /// --by wsp
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btn_accelerate_Click(object sender, EventArgs e)
        {
            //倍速过快时，会使得视频花屏，因此对倍速做了限制
            if (Speed <= 4)
            {
                btn_accelerate.Enabled = true;
                Player.PlaybackRate = Player.PlaybackRate * 2;

                //设置Speed是为了使得脑电数据也要加倍
                Speed = Player.PlaybackRate;

                //videoForm的倍速要与该Form倍速保持一致
                video.Player.PlaybackRate = Player.PlaybackRate;
                video.btn_accelerate.Enabled = true;
            }
            else
            {
                //加速到最大后，加速按钮不可用，减速按钮可用
                btn_accelerate.Enabled = false;
                video.btn_accelerate.Enabled = false;
            }
        }

        /// <summary>
        /// 视频减速
        /// --by wsp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_decelerate_Click(object sender, EventArgs e)
        {
            if (Speed >= 0.5)
            {
                //倍速过慢时，视频也会花屏，因此对最低倍速也做了限制
                btn_accelerate.Enabled = true;
                btn_decelerate.Enabled = true;
                Player.PlaybackRate = Player.PlaybackRate / 2;

                //设置Speed是为了使得脑电数据也要减速
                Speed = Player.PlaybackRate;

                //videoForm的倍速要与该Form倍速保持一致
                video.Player.PlaybackRate = Player.PlaybackRate;
                video.btn_decelerate.Enabled = true;
            }
            else
            {
                //加速到最大后，加速按钮不可用，减速按钮可用
                btn_decelerate.Enabled = false;
                video.btn_decelerate.Enabled = false;
            }
        }

        /// <summary>
        /// 视频面板弹出按键设置是否弹出或者隐藏
        /// --by wsp
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

         /// <summary>
        ///  进度条最大值
        /// </summary>
        private void  GetHsprogressMax()
        {
            SecPerPage();
            hsProgress.Maximum = maxPage;
        }

        /// <summary>
        /// 数据有多少页
        /// </summary>
        private void SecPerPage()
        {
            maxPage = (_nfi.SampleCount + ((int)(10 * 30D / Convert.ToDouble(_timeStandard)) * _nfi.SampleRate) - 1) / ((int)(10 * 30D / Convert.ToDouble(_timeStandard)) * _nfi.SampleRate);
        }

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
            LoadData(CurrentSeconds);
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
            Pen dotPen = new Pen(Color.Red, 2);            //虚线画笔     
            dotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            if (_pageWidth == 0)                            //计算图表的宽与高          -- by lxl
            {
                _pageWidth = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisX.Maximum) - this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(0);
                _X_totalMM = _pageWidth / _pixelPerMM;
                setAxisXMaximum(_X_totalMM / _timeStandard);
                LoadData(CurrentSeconds);
                ShowData();
                _pageHeight = Math.Abs(this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisY.Maximum) - this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0));
                _Y_totalMM = _pageHeight / _pixelPerMM;
            }
            while (_eventsQueue.Count > 0)                  //画事件                   -- by lxl
            {
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(_eventsQueue.Dequeue());
                e.Graphics.FillRectangle(rectBrush, new Rectangle((int)drawPosition - 40, 5, 80, 15));
                e.Graphics.DrawString("病人事件", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15));
                e.Graphics.DrawLine(dotPen, new Point((int)drawPosition, 5), new Point((int)drawPosition, (int)this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0)));
            }
            if (_isChangingBoardShow)                       //计算面板显示与否的宽度    -- by lxl
            {
                if (_isBoardShow)
                {
                    setXMaximum(this.chartWave.ChartAreas[0].AxisX.PixelPositionToValue(this.chartWave.Width-this.boardPanel.Width));
                }
                else
                {
                    setXMaximum(this.chartWave.ChartAreas[0].AxisX.Maximum);
                }
                _isChangingBoardShow = false;
            }
            //画图表上的秒数
            double time1Pos=this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition((int)Math.Floor(_xMaximum) / 2);
            if (_xMaximum >= 2)
            {
                e.Graphics.DrawString((CurrentSeconds + (int)Math.Floor(_xMaximum) / 2).ToString(), strFont, new SolidBrush(Color.Black), new RectangleF((int)time1Pos, 25, 80, 15));
                e.Graphics.DrawString((CurrentSeconds + (int)Math.Floor(_xMaximum) / 2 * 2).ToString(), strFont, new SolidBrush(Color.Black), new RectangleF((int)time1Pos * 2, 25, 80, 15));
            }
            else
            {
                e.Graphics.DrawString((CurrentSeconds + 1).ToString(), strFont, new SolidBrush(Color.Black), new RectangleF((int)(this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(1)), 25, 80, 15));
            }
        }

        /// <summary>
        /// labelPanel的重绘函数,若要更新label则需调用labelPanel.Invalidate()函数
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawLabelPanel(object sender, PaintEventArgs e)
        {
            SolidBrush strBrush = new SolidBrush(Color.Black);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Regular);
            double topSigPos = this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(2000D - 2000D / _signalNum / 2);
            double intervalPos = this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(2000D - 2000D / _signalNum / 2 * 3) - topSigPos;
            foreach (int i in Enumerable.Range(_currentTopSignal,_signalNum))
            {
                e.Graphics.DrawString((i+1).ToString(), strFont, strBrush, new RectangleF(0, (int)(topSigPos + (i - _currentTopSignal) * intervalPos), this.labelPanel.Width, 15));
            }
        }

        /// <summary>
        /// 面板点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isBoardShow = !_isBoardShow;
            this.boardPanel.Visible = _isBoardShow;
            this.boardToolStripMenuItem.Checked = _isBoardShow;
            _isChangingBoardShow = true;
            if (_isBoardShow)
                this.vScroll.Location = new Point(this.boardPanel.Location.X - this.vScroll.Width, this.vScroll.Location.Y);
            else
                this.vScroll.Location = new Point(this.chartWave.Location.X + this.chartWave.Width - this.vScroll.Width, this.vScroll.Location.Y);
            this.chartWave.Invalidate();
        }
       
        /// <summary>
        /// 重头播放，对数据进行初始化处理
        /// --wsp
        /// </summary>
        public void Clear()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0.0;
            CurrentSeconds = 0;
            DT_RelativeTime = DateTime.Parse("2016-05-23  00:00:00");
            DT_TotalTime = DateTime.Parse("2016-05-23 00:00:00");
            CurrentOffset = 0;
            Player.Time =0;
            DT = _nfi.StartTime;
            hsProgress.Value = 0;
            LoadData(CurrentSeconds);
            ShowData();
        }

        /// <summary>
        /// 通道数目显示点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in this.signalToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;
            _signalNum = int.Parse(sender.ToString());
            this.chartWave.ChartAreas[0].AxisY.MajorGrid.Interval = 2000D / _signalNum;
            this.chartWave.ChartAreas[0].AxisY.MajorTickMark.Interval = 2000D / _signalNum;
            if (_currentTopSignal + _signalNum > 20)
                _currentTopSignal = 20 - _signalNum;
            ShowData();
            if (_signalNum < 20)
                setVScrollVisible(true);
            else
                setVScrollVisible(false);
            this.vScroll.LargeChange = _signalNum + 1;
            this.labelPanel.Invalidate();
        }
        /// <summary>
        /// 设置竖直滚动条是否可用
        /// -- by lxl
        /// </summary>
        /// <param name="flag"></param>
        private void setVScrollVisible(bool flag)
        {
            this.vScroll.Visible = flag;
        }

        /// <summary>
        /// 竖直滚动条滚动事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vScrollBar_mouseCapture(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.vScroll.Value);
            if (this._currentTopSignal != this.vScroll.Value)
            {
                this._currentTopSignal = this.vScroll.Value;
                ShowData();
                this.labelPanel.Invalidate();
            }
        }
        /// <summary>
        /// 当回放Form关闭时，弹出视频Form也要关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaybackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(_nfi.HasVideo)
            video.Close();
        }

        /// <summary>
        /// 显示相对时间
        /// --by wsp
        /// </summary>
        /// <param name="CurrentPosition">偏移量</param>
        /// <param name="line">竖线所处的位置</param>
        private void ShowTime(double CurrentPosition, double line)
        {
            int h, s, m;
            h = (int)((CurrentPosition + line)) / 3600;
            int l = (int)(CurrentPosition + line) % 3600;
            m = l / 60;
            s = l % 60;
            if (h > 9 && s > 9 && m > 9)
                displayRecordingTime.Text = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
            if (h > 9 && s <= 9 && m > 9)
                displayRecordingTime.Text = h.ToString() + ":" + m.ToString() + ":0" + s.ToString();
            if (h > 9 && s > 9 && m <= 9)
                displayRecordingTime.Text = h.ToString() + ":0" + m.ToString() + ":" + s.ToString();
            if (h <= 9 && s > 9 && m > 9)
                displayRecordingTime.Text = "0" + h.ToString() + ":" + m.ToString() + ":" + s.ToString();
            if (h <= 9 && s <= 9 && m > 9)
                displayRecordingTime.Text = "0" + h.ToString() + ":" + m.ToString() + ":0" + s.ToString();
            if (h <= 9 && s > 9 && m <= 9)
                displayRecordingTime.Text = "0" + h.ToString() + ":0" + m.ToString() + ":" + s.ToString();
            if (h <= 9 && s <= 9 && m <= 9)
                displayRecordingTime.Text = "0" + h.ToString() + ":0" + m.ToString() + ":0" + s.ToString();
            if (h > 9 && s <= 9 && m <= 9)
                displayRecordingTime.Text = h.ToString() + ":0" + m.ToString() + ":0" + s.ToString();
        }

      /// <summary>
      /// 显示绝对时间
      /// --by wsp
      /// </summary>
      /// <param name="Starttime"></param>
      /// <param name="CurrentPosition"></param>
      /// <param name="Line"></param>
        private void AllShowTime(double Starttime,double CurrentPosition, double Line)
        {
            int h, s, m;
            if (Starttime + CurrentPosition + Line >= 86400)
            {
                h = 0;
                s = 0;
                m = 0;
            }
            else
            {
                h = (int)((Starttime + CurrentPosition + Line)) / 3600;
                int l = (int)(Starttime + CurrentPosition + Line) % 3600;
                m = l / 60;
                s = l % 60;
            }
            if (h > 9 && s > 9 && m > 9)
                displayStartTime.Text = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
            if (h > 9 && s <= 9 && m > 9)
                displayStartTime.Text = h.ToString() + ":" + m.ToString() + ":0" + s.ToString();
            if (h > 9 && s > 9 && m <= 9)
                displayStartTime.Text = h.ToString() + ":0" + m.ToString() + ":" + s.ToString();
            if (h <= 9 && s > 9 && m > 9)
                displayStartTime.Text = "0" + h.ToString() + ":" + m.ToString() + ":" + s.ToString();
            if (h <= 9 && s <= 9 && m > 9)
                displayStartTime.Text = "0" + h.ToString() + ":" + m.ToString() + ":0" + s.ToString();
            if (h <= 9 && s > 9 && m <= 9)
                displayStartTime.Text = "0" + h.ToString() + ":0" + m.ToString() + ":" + s.ToString();
            if (h <= 9 && s <= 9 && m <= 9)
                displayStartTime.Text = "0" + h.ToString() + ":0" + m.ToString() + ":0" + s.ToString();
            if (h > 9 && s <= 9 && m <= 9)
                displayStartTime.Text = h.ToString() + ":0" + m.ToString() + ":0" + s.ToString();
        }

        /// <summary>
        /// 获取开始时刻的总秒数
        /// --by wsp
        /// </summary>
        /// <returns></returns>
        private int GetStarTotalSecond()
        {
            return _nfi.StartTime.Hour * 3600 + _nfi.StartTime.Minute * 60 + _nfi.StartTime.Second;
        }
    }
}
