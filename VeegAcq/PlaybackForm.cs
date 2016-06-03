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
        /// <summary>
        /// 绝对时间
        /// wsp
        /// </summary>
        DateTime dt_startTime;        
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

        public NationFile _nfi = null;  // 诺诚头文件  --by zt
        private int sampleRate;  //采样率 --by zt
        private int numberOfSamples; //总点数 --by zt

        int maxPage;
        private List<EegPacket> _packets = new List<EegPacket>();
        /// <summary>
        /// 事件队列
        /// -- by lxl
        /// </summary>
        private List<preDefineEvent> _preDEventsList = new List<preDefineEvent>();
        private List<customEvent> _customEventList = new List<customEvent>();
        private int _Page;
        private int _maxPage;
        public double b;
        private int _totalSeconds;                   
        private DateTime? _LastTime = null;
        public double _CurrentOffset;

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
        private int _currentSeconds;
        public int CurrentSeconds
        {
            get { return _currentSeconds; }
            set { _currentSeconds = value; }
        }
        /// <summary>
        /// 预定义事件窗口
        /// -- by lxl
        /// </summary>
        private predefineEventsForm pdEventForm;
        /// <summary>
        /// 现在是否在添加事件
        /// -- by lxl
        /// </summary>
        private bool _isAddingEvent;
        /// <summary>
        /// 现在的鼠标所在的位于X轴的坐标点,添加事件时用
        /// -- by lxl
        /// </summary>
        private double _mouseValueNow;
        /// <summary>
        /// 所添加的预定义事件的名字
        /// -- by lxl
        /// </summary>
        private preDefineEvent.pdEvents _addedEventName_preD;
        /// <summary>
        /// 所添加的自定义事件名字
        /// -- by lxl
        /// </summary>
        private string _addedEventName_custom;
        /// <summary>
        /// 判断现在是在添加什么事件，true为custom，false为preDefine
        /// -- by lxl
        /// </summary>
        private bool _isAddingPreDefineEvent;
        /// <summary>
        /// 添加事件时的所选事件的COLOR
        /// -- by lxl
        /// </summary>
        private Color _addEventColor;
        public PlaybackForm(NationFile EegFile)
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
            _signalNum = 20;
            _currentTopSignal = 0;
            _isAddingEvent = false;
            setVScrollVisible(false);
            initMenuItems();
            
            //natfileinfo = new NatFile(EegFile.NedFullName);
            //pationinfo = new PationInfo(EegFile.NedFullName, natfileinfo.PatOff);
            _Page = 0;

            try
            {
                _nfi = EegFile;
            }
            catch
            { }

            sampleRate = EegFile.NatInfo.Freq; // --by zt
            numberOfSamples = EegFile.NumberOfSamples; // --by zt
            dt_startTime = EegFile.StartDateTime; // --by zt
            GetHsprogressMax();
            _totalSeconds = (int)EegFile.Duration.TotalSeconds; // 修改 --by zt
            hsProgress.Maximum = _totalSeconds;         //不一定是整数秒 故maximum不需要-1
        }

        private void LoadData(int Offset)
        {
            if (_nfi == null)
            {
                return;
            }
            int loadRec = WINDOW_SECONDS * sampleRate;                           
            DateTime begin = DateTime.Now;
            //加载8导数据
            Parse8LeadsData(Offset, loadRec);
            //FileStream fs = new FileStream(_nfi.NedFullName, FileMode.Open, FileAccess.Read);
            //fs.Seek(0x200, SeekOrigin.Begin);//为什么从512开始？  --by zt
            //fs.Seek(0x6c * Offset * (loadRec - _nfi.SampleRate * (WINDOW_SECONDS-(int)Math.Ceiling(_xMaximum) + 1)), SeekOrigin.Current);//为什么要取108个字节
            //byte[] buf = new byte[0x6c];
            //_packets.Clear();
            //foreach (int x in Enumerable.Range(0, loadRec))
            //{
            //    if (fs.Read(buf, 0, 0x6c) < 0x6c)
            //    {
            //        break;
            //    }
            //    StringBuilder sb = new StringBuilder();
            //    foreach (byte b in buf) 
            //    {
            //        sb.Append(b.ToString("X2"));
            //    }
            //    double ekg = Util.RawToSignal((short)(buf[6] | (buf[7] << 8)));//心电数据，为什么要转化为short   --by zt
            //    List<double> eeg = new List<double>();
            //    foreach (int off in Enumerable.Range(0, 19))
            //    {
            //        eeg.Add(Util.RawToSignal((short)(buf[28 + 2 * off] | (buf[29 + 2 * off] << 8))));
            //    }
            //    EegPacket pkt = new EegPacket(ekg, eeg.ToArray());
            //    _packets.Add(pkt);
            //}
            //fs.Close();
            //fs.Dispose();
            DateTime end = DateTime.Now;
            Debug.WriteLine(string.Format("Read a window of data {0} records in {1} seconds", _packets.Count, (end - begin).TotalSeconds));
        }

        public void Parse8LeadsData(int Offset, int loadRec) 
        {
            FileStream fs = new FileStream(_nfi.NedFileName, FileMode.Open, FileAccess.Read);
            fs.Seek(0x200, SeekOrigin.Begin);//从512开始  --by zt
            //取26个字节
            int byteOfPerData = 0x1A;
            int numberOfData= 8;
            fs.Seek(byteOfPerData * Offset * (loadRec - sampleRate * (WINDOW_SECONDS - (int)Math.Ceiling(_xMaximum) + 1)), SeekOrigin.Current);//  --by zt
            byte[] buf = new byte[byteOfPerData];
            _packets.Clear();
            foreach (int x in Enumerable.Range(0, loadRec))
            {
                if (fs.Read(buf, 0, byteOfPerData) < byteOfPerData)
                {
                    break;
                }
                //测试代码 by zt
                StringBuilder sb = new StringBuilder();
                foreach (byte b in buf)
                {
                    sb.Append(b.ToString("X2"));
                }
                //8导没有心电
                //double ekg = Util.RawToSignal((short)(buf[6] | (buf[7] << 8)));//心电数据，为什么要转化为short   --by zt
                double ekg = 0;
                List<double> eeg = new List<double>();
                //加载脑电数据
                foreach (int off in Enumerable.Range(0, numberOfData))
                {
                    eeg.Add(Util.RawToSignal((short)(buf[6 + 2 * off] | (buf[7 + 2 * off] << 8))));
                }
                //其余的用0补全
                for (int i = numberOfData; i < 19; i++)
                {
                    eeg.Add(0);
                }
                EegPacket pkt = new EegPacket(ekg, eeg.ToArray());
                _packets.Add(pkt);
            }
            fs.Close();
            fs.Dispose();
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
                if (tIdx % 127 == 0) _preDEventsList.Add(new preDefineEvent(preDefineEvent.pdEvents.eyesOpen, tIdx));
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
            dt_relativetime=DateTime.Parse("2016-05-23  00:00:00");
            dt_totaltime = DateTime.Parse("2016-05-23 00:00:00");
            displayStartTime.Text = dt_startTime.ToLongTimeString().ToString();
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
            video.Show();
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
        /// <summary>
        /// 点击上一页时所用，与从头播放分开来写
        /// </summary>
        private void PagePrev_2()
        {
            _currentSeconds -= (int)(Math.Floor(_xMaximum));
            if (_currentSeconds <= 0)
                _currentSeconds = 0;
            _CurrentOffset = _currentSeconds;
            LoadData(_currentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();
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
        /// <summary>
        /// 点击下一页时所用
        /// wsp
        /// </summary>
        private void PageNext_2()
        {
            _currentSeconds += (int)(Math.Floor(_xMaximum));
            if (_currentSeconds > _totalSeconds - hsProgress.LargeChange + 1)
                _currentSeconds = _totalSeconds - hsProgress.LargeChange + 1;
            _CurrentOffset = _currentSeconds;
            LoadData(_currentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();     
        }
        public void SetTimeLine()
        {
    //        chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _CurrentOffset - _Page * chartWave.ChartAreas[0].AxisX.Maximum;////////添加需要拖动的一些位置进行
	//          chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset += 0.1*speed;
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = (_CurrentOffset - _currentSeconds);
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
//            PagePrev();
            PagePrev_2();
            changed();
            _player.Time = _currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            video.player.Time = _currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            //           SyncVideo();
            update_BtnEnable();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {                
            Pause();
  //          PageNext();
            PageNext_2();
            changed();
            _player.Time = _currentSeconds * 1000+(long)_nfi.VideoOffset*1000+(long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset*1000;
            video.player.Time = _currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
 //           SyncVideo();
            update_BtnEnable();

        }
        /// <summary>
        /// Time事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Tick(object sender, EventArgs e)
        {
          DateTime now = DateTime.Now;
           _CurrentOffset += (now - _LastTime.Value).TotalSeconds*speed;
           double l = (now - _LastTime.Value).TotalSeconds * speed;
            _LastTime = now;
 //            if (_currentSeconds+chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)/////////////////
            if(_CurrentOffset>=_nfi.Duration.TotalSeconds)
            {
                Pause();
            //    _CurrentOffset = 0;
                _LastTime = null;
                video.player.Pause();
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                video.btn_play.Enabled = true;
                video.btn_pause.Enabled = false;
            }
            else
             {
                if (chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _xMaximum)
                {
                    PageNext();
                }
                else
                {             
                    SetTimeLine();
     //               dt = dt.AddSeconds(0.1 * speed);
                    dt_startTime = dt_startTime.AddSeconds(l);
                    DateTime a = DateTime.Parse("2016-05-23  00:00:00");
                    displayStartTime.Text = dt_startTime.ToLongTimeString().ToString();
     //               dt_relativetime = dt_relativetime.AddSeconds(0.1 * speed);
                    dt_relativetime = dt_relativetime.AddSeconds(l);
                    b = (dt_relativetime - a).TotalSeconds;
                    displayRecordingTime.Text = dt_relativetime.ToLongTimeString().ToString();
                }
            }
            //为了保证最后的那条线画在正确的位置，所以相对时间和画的那条线最后需要特殊处理一下
            //wsp
   //          if (_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset > _nfi.Duration.TotalSeconds)
            if (_CurrentOffset >= _nfi.Duration.TotalSeconds)
             {
                 chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _nfi.Duration.TotalSeconds - _currentSeconds;
                 DateTime dt_totaltime1;
                 DateTime dt_begin1;
                 dt_begin1 = _nfi.StartDateTime;  // 修改  --by zt
                 dt_totaltime1 = DateTime.Parse("2016-05-23 00:00:00");
                 if ((int)_nfi.Duration.TotalSeconds < _nfi.Duration.TotalSeconds)
                 {
                     dt_totaltime1 = dt_totaltime1.AddSeconds(_nfi.Duration.TotalSeconds + 1);
                     dt_begin1 = dt_begin1.AddSeconds(_nfi.Duration.TotalSeconds + 1);
                 }
                 else
                 {
                     dt_totaltime1 = dt_totaltime1.AddSeconds(_nfi.Duration.TotalSeconds);
                     dt_begin1 = dt_begin1.AddSeconds(_nfi.Duration.TotalSeconds);
                 }
                 displayStartTime.Text = dt_begin1.ToLongTimeString().ToString();
                 displayRecordingTime.Text = dt_totaltime1.ToLongTimeString().ToString();
             //    _player.Time=(long)(_nfi.Duration.TotalSeconds*1000+_nfi.VideoOffset*1000);
             }
        }
        /// <summary>
        /// 播放事件
        /// wsp
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
                // TODO:
               // _player.Time = (long)(_nfi.VideoOffset * 1000);
                if (_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)
                {
                    clear();
                } 
                if (!_player.IsPlaying)
                    _player.Play();         
                //if (_currentSeconds == 0&&chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset==0)
                //    _player.Time = (long)_nfi.VideoOffset * 1000;
                //if (_currentSeconds != 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                //    _player.Time = (long)_nfi.VideoOffset * 1000 + _currentSeconds * 1000;
                //if (_currentSeconds != 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset != 0)
                    _player.Time = (long)(_nfi.VideoOffset * 1000 + _currentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000);
            }
            else
            {
                timer.Enabled = false;
                MessageBox.Show("没有视频", "提示");
            }
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
        }
        /// <summary>
        /// 暂停事件
        ///wsp
        /// </summary>
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
            if (_currentSeconds == 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
            video.player.Time = (long)_nfi.VideoOffset * 1000;
            if(_currentSeconds!=0&&chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
            video.player.Time = (long)_nfi.VideoOffset * 1000 + _currentSeconds * 1000;
         //   video.Hide();  
        }

        public void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
            video.Pause();
        }
        /// <summary>
        /// 仿造尼高利，下一页数据加载时从整数部分加载，画线则是从上一页的_xMaximum开始；
        /// wsp
        /// </summary>
        private void set_hsScrollBarValue()
        {
 //           if (b - _currentSeconds >= 0&&b-_currentSeconds<_xMaximum)
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = b - _currentSeconds;
//            else
//            {
//                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
//                _player.Time = _currentSeconds * 1000+(long)_nfi.VideoOffset*1000;
//            }
            hsProgress.Value = _currentSeconds;
        }
        /// <summary>
        /// 点击pagenext或者pageprev时，画线偏移量要变化；
        /// </summary>
        private void set_hsScrollBarValue_Next()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
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
                _CurrentOffset = _currentSeconds;
                LoadData(_currentSeconds);
                ShowData();
                update_BtnEnable();
                _player.Time = _currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
   //             SyncVideo();
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

        #region 跟病人有关的信息 待修改

        private void pationInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set_PationInfoPanel(_nfi.PatInfo);   //  --by zt
            this.PationInfoPanel.Visible = true;
            this.DetectionInfoPanel.Visible = false;
        }

        private void detectionInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set_DetectionInfoPanel(_nfi.PatInfo);  //  --by zt
            this.PationInfoPanel.Visible = false;
            this.DetectionInfoPanel.Visible = true;
        }

        private void hideingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PationInfoPanel.Visible = false;
            this.DetectionInfoPanel.Visible = false;
        }

        private void Set_PationInfoPanel(PatInfo patientinfo)
        {
            this.PatNameTextBt.Text = patientinfo.Name;
            this.PatGenderCB.Text = patientinfo.Gender;
            this.PatIDTextBt.Text = patientinfo.ID;
            this.PatAgeTextBt.Text = patientinfo.Age;
            this.SingleHandAdvanCB.Text = patientinfo.Handedness;
        }

        private void Set_DetectionInfoPanel(PatInfo patientinfo)
        {
            this.DetectionTextBt.Text = patientinfo.ID;
            this.RequesterTextBt.Text = patientinfo.Name;
            this.TechnicianTextBt.Text = patientinfo.ResidentDoctor;
            this.PhysicianTextBT.Text = patientinfo.OperateDoctor;
            this.PationStateTextBt.Text = patientinfo.State;
            this.PharmacyTextBt.Text = patientinfo.Medicine;
            this.DetectionRemarksTextBt.Text = patientinfo.Diagnosis;
            //this.FilePathTextBt.Text = _pationinfo.FilePath;
        }


        #endregion

        /// <summary>
        ///  拖动移动病人属性和检查属性   
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
            _player.Time = _currentSeconds * 1000;
            video.player.Time = _currentSeconds * 1000;
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
        /// <summary>
        /// 进度条，PagePrev,PageNext变化时，对应的时间也要发生变化；
        /// wsp
        /// </summary>
        private void changed()
        {
            
            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt_startTime = dt_startTime.AddSeconds(_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            displayStartTime.Text = dt_startTime.ToLongTimeString().ToString();
            dt_relativetime = dt_relativetime.AddSeconds(_currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            displayRecordingTime.Text = dt_relativetime.ToLongTimeString().ToString();
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
            maxPage = (numberOfSamples + ((int)(10 * 30D / Convert.ToDouble(_timeStandard)) * sampleRate) - 1) / ((int)(10 * 30D / Convert.ToDouble(_timeStandard)) * sampleRate);  // 修改 --by zt
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
            //SolidBrush rectBrush = new SolidBrush(Color.FromArgb(200, Color.Red));
            SolidBrush strBrush = new SolidBrush(Color.White);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Bold);
            Pen dotPen = new Pen(Color.Red, 2);            //虚线画笔     
            dotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            if (_pageWidth == 0)                            //计算图表的宽与高          -- by lxl
            {
                _pageWidth = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisX.Maximum) - this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(0);
                _X_totalMM = _pageWidth / _pixelPerMM;
                setAxisXMaximum(_X_totalMM / _timeStandard);
                LoadData(_currentSeconds);
                ShowData();
                _pageHeight = Math.Abs(this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisY.Maximum) - this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0));
                _Y_totalMM = _pageHeight / _pixelPerMM;
            }
            foreach(preDefineEvent p in _preDEventsList)                  //画事件                   -- by lxl
            {
                dotPen.Color = p.Color;
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(p.PointPosition / sampleRate);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, p.Color)), new Rectangle((int)drawPosition - 40, 5, 80, 15));
                switch (p.Event) {
                    case preDefineEvent.pdEvents.eyesOpen: e.Graphics.DrawString("睁眼", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                    case preDefineEvent.pdEvents.eyesClose: e.Graphics.DrawString("闭眼", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                    case preDefineEvent.pdEvents.deepBreath: e.Graphics.DrawString("深呼吸", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                    case preDefineEvent.pdEvents.calibrate: e.Graphics.DrawString("定标", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
            }
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
            if (_isAddingEvent)
            {
                e.Graphics.DrawLine(Pens.Red, new Point(Control.MousePosition.X - this.chartWave.Location.X, 0), new Point(Control.MousePosition.X - this.chartWave.Location.X, this.chartWave.Height));
                _mouseValueNow = this.chartWave.ChartAreas[0].AxisX.PixelPositionToValue(Control.MousePosition.X - this.chartWave.Location.X);
            }
            //画图表上的秒数
            double time1Pos=this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition((int)Math.Floor(_xMaximum) / 2);
            if (_xMaximum >= 2)
            {
                e.Graphics.DrawString((_currentSeconds + (int)Math.Floor(_xMaximum) / 2).ToString(), strFont, new SolidBrush(Color.Black), new RectangleF((int)time1Pos, 25, 80, 15));
                e.Graphics.DrawString((_currentSeconds + (int)Math.Floor(_xMaximum) / 2 * 2).ToString(), strFont, new SolidBrush(Color.Black), new RectangleF((int)time1Pos * 2, 25, 80, 15));
            }
            else
            {
                e.Graphics.DrawString((_currentSeconds + 1).ToString(), strFont, new SolidBrush(Color.Black), new RectangleF((int)(this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(1)), 25, 80, 15));
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
        public void clear()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0.0;
            _currentSeconds = 0;
            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt_totaltime = DateTime.Parse("2016-05-23 00:00:00");
//            _player.Time = (long)_nfi.VideoOffset * 1000;
            _CurrentOffset = 0;
            _player.Time =0;
            hsProgress.Value = 0;
            LoadData(_currentSeconds);
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
        /// 事件点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void events_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem a = (ToolStripMenuItem)sender;
            if (a.Name.Substring(0, 2) == "pr")
            {
                if (pdEventForm == null)
                    pdEventForm = new predefineEventsForm(this);
                pdEventForm.Show();
                pdEventForm.initList();
            }
            else 
            {
                MessageBox.Show("cu");
            }
        }
        /// <summary>
        /// 获得事件列表
        /// -- by lxl
        /// </summary>
        /// <returns></returns>
        public List<preDefineEvent> getEventList()
        {
            return _preDEventsList;
        }

        /// <summary>
        /// 获取采样频率
        /// </summary>
        /// <returns></returns>
        public double getSampleRate()
        {
            return sampleRate;
        }

        /// <summary>
        /// 获取文件开始时间
        /// -- by lxl
        /// </summary>
        /// <returns></returns>
        public DateTime getStartTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 开始添加事件
        /// -- by lxl
        /// </summary>
        /// <param name="flag">true为预定义事件，false为自定义事件</param>
        public void startAddEvents(bool flag,Color clr,object name)
        {
            _addEventColor = clr;
            _isAddingPreDefineEvent = flag;
            _isAddingEvent = true;
            if (flag)
                _addedEventName_preD = (preDefineEvent.pdEvents)name;
            else
                _addedEventName_custom = (string)name;
        }

        /// <summary>
        /// 鼠标在chart上的移动事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseMoveOnChart(object sender, MouseEventArgs e)
        {
            if (_isAddingEvent)
            {
                System.Diagnostics.Debug.WriteLine(Control.MousePosition);
                this.chartWave.Invalidate();
            }
        }

        /// <summary>
        /// form的重绘事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formPaint(object sender, PaintEventArgs e)
        {
            if (_isAddingEvent)
            {
                System.Diagnostics.Debug.WriteLine("in it");
                //e.Graphics.DrawLine(Pens.Red, new Point(Control.MousePosition.X, this.chartWave.Location.Y), new Point(Control.MousePosition.X, this.chartWave.Location.Y + this.chartWave.Height));
                Graphics.FromHwnd(chartWave.Handle).DrawLine(Pens.Red, new Point(Control.MousePosition.X - this.chartWave.Location.X, 0), new Point(Control.MousePosition.X - this.chartWave.Location.X, this.chartWave.Height));
            }
        }

        /// <summary>
        /// chart上的点击事件，只有在添加事件时才有用
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartwave_Click(object sender, EventArgs e)
        {
            if (_isAddingEvent)
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Left)
                {
                    if (_isAddingPreDefineEvent)
                    {
                        _preDEventsList.Add(new preDefineEvent(_addedEventName_preD, _mouseValueNow));
                        pdEventForm.updateListView();
                    }
                    else
                    {
                        _customEventList.Add(new customEvent(_addedEventName_custom, _mouseValueNow, _addEventColor));
                    }
                }
                _isAddingEvent = !_isAddingEvent;
            }
        }
        /// <summary>
        /// 当回放Form关闭时，弹出视频Form也要关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaybackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            video.Close();
        }

        /// <summary>
        /// 图表大小改变触发事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartSizeChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("sizechanged"+DateTime.Now);
            this.labelPanel.Invalidate();
        }
    }
}
