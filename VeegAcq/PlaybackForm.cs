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
using System.Collections;

namespace VeegStation
{
    public partial class PlaybackForm : Form
    {

        #region 声明属性

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
        private int byteOfPerData;   //每个数据块占用的字节数  --by zt
        private int numberOfPerData; //数据块中脑电数据占用的字节数   --by zt
        private int indexOfData;     //数据的起始位置
        List<double> testList = new List<double>();  //测数值,调试用  --by zt
        public ArrayList myLeadSource;
        
        //public ArrayList defaultLeadArrayList = new ArrayList();
        //    string[] leadname = new string[19] { "FP1", "FP2", "F3", "F4", "F7", "F8", "C3", "C4", "T3", "T4", "P3", "P4", "T5", "T6", "O1","O2","Fz", "Cz", "Pz" };
        //    for (int i = 0; i < 19;i++ )
        //        defaultLeadSource.Add(i+1, leadname[i]);            
        //    for (int i = 0; i < 19; i++)
        //    {
        //        if (leadname[i] != "")
        //             defaultLeadArrayList.Add(leadname[i] + "-GND");
        //    } 
        
        //public ArrayList leadSource = new ArrayList() { "1","2"};


        int maxPage;
        private List<EegPacket> _packets = new List<EegPacket>();
        /// <summary>
        /// 事件队列
        /// -- by lxl
        /// </summary>
        private List<PreDefineEvent> preDEventsList = new List<PreDefineEvent>();
        private List<CustomEvent> customEventList = new List<CustomEvent>();
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
        private int[] sensitivityArray = { 10, 20, 30, 50, 70, 100, 150, 200, 300, 500, 700, 1000, 2000, 5000 };
        /// <summary>
        /// 当前灵敏度的取值 
        /// -- by lxl
        /// </summary>
        private int sensitivity;
        /// <summary>
        /// 时间基准选择范围 
        /// -- by lxl
        /// </summary>
        private int[] timeStandardArray = { 6, 10, 15, 30, 60, 100, 200 };
        /// <summary>
        /// 当前选择时间基准,毫米每秒  
        /// -- by lxl
        /// </summary>
        private int timeStandard;
        /// <summary>
        /// 信道显示选择范围
        /// -- by lxl
        /// </summary>
        private int[] signalNumArray = { 1, 2, 4, 8, 16, 20 };
        /// <summary>
        /// 当前显示的信道数据
        /// -- by lxl
        /// </summary>
        private int signalNum;
        /// <summary>
        /// 当前显示的最上面的信道,当选择显示的通道小鱼20时拖动滑动条时需要用到
        /// -- by lxl
        /// </summary>
        private int currentTopSignal;
        /// <summary>
        /// 校准Y轴窗口            
        /// - by lxl
        /// </summary>
        private CalibrateYForm calibForm;
        /// <summary>
        /// 校验X轴窗口            
        /// -- by lxl
        /// </summary>
        private calibrateXForm calibXForm;
        /// <summary>
        /// 屏幕的宽度,单位为像素点
        /// -- by lxl
        /// </summary>
        private double pageWidthInPixel = 0;
        /// <summary>
        /// 屏幕的高度,单位为像素点
        /// -- by lxl
        /// </summary>
        private double pageHeightInPixel = 0;
        /// <summary>
        /// 图表宽度,单位为毫米        
        /// -- by lxl
        /// </summary>
        private double pageWidthInMM;
        /// <summary>
        /// 图标高度，单位为毫米
        /// -- by lxl
        /// </summary>
        private double pageHeightInMM;
        /// <summary>
        /// 一毫米有多少个像素         
        /// -- by lxl
        /// </summary>
        private double pixelPerMM;
        /// <summary>
        /// 纵轴一格多少毫米
        /// -- by lxl
        /// </summary>
        private double mmPerYGrid;
        /// <summary>
        /// 判断面板此时是否显示
        /// -- by lxl
        /// </summary>
        private bool isBoardShow;
        /// <summary>
        /// X轴当前的最大值为多少
        /// -- by lxl
        /// </summary>
        private double xMaximum;
        /// <summary>
        /// 标识此时是否刚更换面板显示状态，防止不停调用画图函数里的计算
        /// -- by lxl
        /// </summary>
        private bool isChangingBoardShow;
        /// <summary>
        /// 当前图表的开头为第几秒
        /// -- by lxl
        /// </summary>
        private int currentSeconds;
        public int CurrentSeconds
        {
            get { return currentSeconds; }
            set { currentSeconds = value; }
        }
        /// <summary>
        /// 预定义事件窗口
        /// -- by lxl
        /// </summary>
        private predefineEventsForm myPreDefineEventFormEventForm;
        /// <summary>
        /// 自定义事件窗口
        /// -- by lxl
        /// </summary>
        private CustomEventForm myCustomEventForm;
        /// <summary>
        /// 现在是否在添加事件
        /// -- by lxl
        /// </summary>
        private bool isAddingEvent;
        /// <summary>
        /// 现在的鼠标所在的位于X轴的坐标点,添加病人事件时用
        /// -- by lxl
        /// </summary>
        private double mouseValueNow;
        /// <summary>
        /// 所添加的预定义事件的名字
        /// -- by lxl
        /// </summary>
        private PreDefineEvent.PreDefineEventsName addedEventNameForPDEvent;
        /// <summary>
        /// 所添加的自定义事件名字
        /// -- by lxl
        /// </summary>
        private string addedEventNameForCustomEvent;
        /// <summary>
        /// 判断现在是在添加什么事件，true为custom，false为preDefine
        /// -- by lxl
        /// </summary>
        private bool isAddingPreDefineEvent;
        /// <summary>
        /// 添加事件时的所选事件的COLOR
        /// -- by lxl
        /// </summary>
        private Color addingEventColor;
        #endregion
        public PlaybackForm(NationFile EegFile)
        {
            InitializeComponent();
            sensitivity = 100;
            isBoardShow = true;
            this.boardToolStripMenuItem.Checked = isBoardShow;
            this.boardPanel.Visible = isBoardShow;
            timeStandard = 30;
            pixelPerMM = 3.8;
            mmPerYGrid = 1;
            isChangingBoardShow = true;
            signalNum = 20;
            currentTopSignal = 0;
            isAddingEvent = false;
            SetVScrollVisible(false);
            InitMenuItems();

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
            this.myLeadSource = EegFile.Montage.LeadSource;
        }

        
        private void LoadData(int Offset)
        {
            if (_nfi == null)
            {
                return;
            }
            int loadRec = WINDOW_SECONDS * sampleRate;
            DateTime begin = DateTime.Now;
            #region 加载数据
            switch (_nfi.Montage.SzSetting)
            {
                case "P10":                 //8导脑电
                    byteOfPerData = 26;
                    numberOfPerData = 8;
                    indexOfData = 6;
                    break;
                case "P11":                 //8导脑电+多参数
                    byteOfPerData = 48;
                    numberOfPerData = 8;
                    indexOfData = 28;
                    break;
                case "P20":                 //16导脑电
                    byteOfPerData = 46;
                    numberOfPerData = 16;
                    indexOfData = 6;
                    break;
                case "P21":                 //16导脑电+多参数
                    byteOfPerData = 68;
                    numberOfPerData = 16;
                    indexOfData = 28;
                    break;
                case "P30":                 //24导脑电
                    byteOfPerData = 86;
                    numberOfPerData = 19;
                    indexOfData = 6;
                    break;
                case "P40":                 //32导脑电
                    byteOfPerData = 86;
                    numberOfPerData = 19;
                    indexOfData = 6;
                    break;
                case "P41":                 //32导脑电+多参数
                    byteOfPerData = 108;
                    numberOfPerData = 19;
                    indexOfData = 28;
                    break;
                default:
                    break;
            }

            ParseData(byteOfPerData, numberOfPerData, indexOfData, Offset, loadRec);
            #endregion

            DateTime end = DateTime.Now;
            Debug.WriteLine(string.Format("Read a window of data {0} records in {1} seconds", _packets.Count, (end - begin).TotalSeconds));
        }

        /// <summary>
        /// 解析数据，按秒数加载  --by zt
        /// </summary>
        /// <param name="byteOfPerData"></param>
        /// <param name="numberOfPerData"></param>
        /// <param name="indexOfData"></param>
        /// <param name="Offset"></param>
        /// <param name="loadRec"></param>
        public void ParseData(int byteOfPerData, int numberOfPerData, int indexOfData, int Offset, int loadRec)
        {
            FileStream fs = new FileStream(_nfi.NedFileName, FileMode.Open, FileAccess.Read);
            fs.Seek(0x200, SeekOrigin.Begin);//从512开始  --by zt
            fs.Seek(byteOfPerData * Offset * (loadRec - sampleRate * (WINDOW_SECONDS - (int)Math.Ceiling(xMaximum) + 1)), SeekOrigin.Current);//  --by zt
            byte[] buf = new byte[byteOfPerData];
            _packets.Clear();
            testList.Clear();

            foreach (int x in Enumerable.Range(0, loadRec))
            {
                if (fs.Read(buf, 0, byteOfPerData) < byteOfPerData)
                {
                    break;
                }
                ////测试代码 by zt
                //StringBuilder sb = new StringBuilder();
                //foreach (byte b in buf)
                //{
                //    sb.Append(b.ToString("X2"));
                //}
                //8导没有心电
                //double ekg = Util.RawToSignal((short)(buf[6] | (buf[7] << 8)));//心电数据，为什么要转化为short   --by zt
                double ekg = 0;//不知道心电数据，暂时为0
                List<double> eeg = new List<double>();

                //加载脑电数据
                foreach (int off in Enumerable.Range(0, numberOfPerData))
                {
                    eeg.Add(Util.RawToSignal((short)(buf[indexOfData + 2 * off] | (buf[indexOfData + 1 + 2 * off] << 8))));
                    if (off == 1)
                    {
                        testList.Add(eeg[off]);
                        //Console.WriteLine(eeg[1]);
                    }
                }
                //其余的用0补全
                for (int i = numberOfPerData; i < 19; i++)
                {
                    eeg.Add(0);
                }
                EegPacket pkt = new EegPacket(ekg, eeg.ToArray());
                _packets.Add(pkt);
            }

            //Console.WriteLine("最大值 {0}，最小值 {1}", testList.Max(), testList.Min());  //测试用到  --by zt
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// 解析8导联数据  --by zt
        /// </summary>
        /// <param name="Offset"></param>
        /// <param name="loadRec"></param>
        public void Parse8LeadsData(int Offset, int loadRec)
        {
            FileStream fs = new FileStream(_nfi.NedFileName, FileMode.Open, FileAccess.Read);
            fs.Seek(0x200, SeekOrigin.Begin);//从512开始  --by zt
            //取26个字节
            int byteOfPerData = 0x1A;
            int numberOfData = 8;
            fs.Seek(byteOfPerData * Offset * (loadRec - sampleRate * (WINDOW_SECONDS - (int)Math.Ceiling(xMaximum) + 1)), SeekOrigin.Current);//  --by zt
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
            double interval = 2000D / signalNum;
            chartWave.SuspendLayout();
            foreach (int sIdx in Enumerable.Range(0, 20))
            {
                int a = col[sIdx].Points.Count;
                col[sIdx].Points.Clear();
            }
            foreach (int tIdx in Enumerable.Range(0, _packets.Count))
            {
                //没有事件先手动填充事件
                //if (tIdx % 127 == 0) _preDEventsList.Add(new preDefineEvent(preDefineEvent.pdEvents.eyesOpen, tIdx));
                if (tIdx % 127 == 0) customEventList.Add(new CustomEvent("你好", tIdx, Color.Blue));
                foreach (int sIdx in Enumerable.Range(currentTopSignal, signalNum))
                {
                    if (sIdx == 19)
                    {
                        col[19].Points.AddXY(tIdx / 128.0, _packets[tIdx].EKG * interval * 10 / sensitivity / mmPerYGrid + (2000D - interval * (19 - currentTopSignal) - interval / 2));
                        continue;
                    }
                    double val = _packets[tIdx].EEG[sIdx];
                    val /= 20;
                    val = val * interval * 10 / sensitivity / mmPerYGrid;              //根据所校准的单位与灵敏度调整Y轴值-- by lxl
                    val += (2000D - interval * (sIdx - currentTopSignal) - interval / 2);
                    col[sIdx].Points.AddXY(tIdx / 128.0, val);
                }
            }
            chartWave.ResumeLayout();
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
            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt_totaltime = DateTime.Parse("2016-05-23 00:00:00");
            displayStartTime.Text = dt_startTime.ToLongTimeString().ToString();
            if ((int)_nfi.Duration.TotalSeconds < _nfi.Duration.TotalSeconds)
                dt_totaltime = dt_totaltime.AddSeconds(_nfi.Duration.TotalSeconds + 1);
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
            //chartWave.ChartAreas[0].AxisX.Maximum = WINDOW_SECONDS;               //注释掉，因为画表的有些数据此时还不知道，故将画表放在别处 -- by lxl
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
            currentSeconds -= (int)(Math.Floor(xMaximum));
            if (currentSeconds <= 0)
                currentSeconds = 0;
            LoadData(currentSeconds);
            ShowData();
            set_hsScrollBarValue();
        }
        /// <summary>
        /// 点击上一页时所用，与从头播放分开来写
        /// </summary>
        private void PagePrev_2()
        {
            currentSeconds -= (int)(Math.Floor(xMaximum));
            if (currentSeconds <= 0)
                currentSeconds = 0;
            _CurrentOffset = currentSeconds;
            LoadData(currentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();
        }
        private void PageNext()
        {
            currentSeconds += (int)(Math.Floor(xMaximum));
            if (currentSeconds > _totalSeconds - hsProgress.LargeChange + 1)
                currentSeconds = _totalSeconds - hsProgress.LargeChange + 1;
            LoadData(currentSeconds);
            ShowData();
            set_hsScrollBarValue();
        }
        /// <summary>
        /// 点击下一页时所用
        /// wsp
        /// </summary>
        private void PageNext_2()
        {
            currentSeconds += (int)(Math.Floor(xMaximum));
            if (currentSeconds > _totalSeconds - hsProgress.LargeChange + 1)
                currentSeconds = _totalSeconds - hsProgress.LargeChange + 1;
            _CurrentOffset = currentSeconds;
            LoadData(currentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();
        }
        public void SetTimeLine()
        {
            //        chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _CurrentOffset - _Page * chartWave.ChartAreas[0].AxisX.Maximum;////////添加需要拖动的一些位置进行
            //          chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset += 0.1*speed;
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = (_CurrentOffset - currentSeconds);
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
            _player.Time = currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            video.player.Time = currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            //           SyncVideo();
            UpdateBtnEnable();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Pause();
            //          PageNext();
            PageNext_2();
            changed();
            _player.Time = currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            video.player.Time = currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            //           SyncVideo();
            UpdateBtnEnable();

        }
        /// <summary>
        /// Time事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            _CurrentOffset += (now - _LastTime.Value).TotalSeconds * speed;
            double l = (now - _LastTime.Value).TotalSeconds * speed;
            _LastTime = now;
            //            if (_currentSeconds+chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)/////////////////
            if (_CurrentOffset >= _nfi.Duration.TotalSeconds)
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
                if (chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= xMaximum)
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
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = _nfi.Duration.TotalSeconds - currentSeconds;
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
                if (currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= _nfi.Duration.TotalSeconds)
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
                _player.Time = (long)(_nfi.VideoOffset * 1000 + currentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000);
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
            if (currentSeconds == 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                video.player.Time = (long)_nfi.VideoOffset * 1000;
            if (currentSeconds != 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                video.player.Time = (long)_nfi.VideoOffset * 1000 + currentSeconds * 1000;
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
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = b - currentSeconds;
            //            else
            //            {
            //                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
            //                _player.Time = _currentSeconds * 1000+(long)_nfi.VideoOffset*1000;
            //            }
            hsProgress.Value = currentSeconds;
        }
        /// <summary>
        /// 点击pagenext或者pageprev时，画线偏移量要变化；
        /// </summary>
        private void set_hsScrollBarValue_Next()
        {
            chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
            hsProgress.Value = currentSeconds;
        }
        private void hsProgress_MouseCaptureChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Format("Scroll mouse cap changed {0}", e));
            Pause();
            if (currentSeconds != hsProgress.Value)
            {
                currentSeconds = hsProgress.Value;
                changed();
                _CurrentOffset = currentSeconds;
                LoadData(currentSeconds);
                ShowData();
                UpdateBtnEnable();
                _player.Time = currentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
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
        private void UpdateBtnEnable()
        {
            //不能让秒数为负数
            if (currentSeconds <= 0)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                return;
            }
            else if (currentSeconds > _totalSeconds - hsProgress.LargeChange)       //也不能让秒数大过最大值
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                return;
            }
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
        }

        #region 跟病人有关的信息
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
                _Page = hsProgress.Value;
                changed();
                LoadData(_Page);
                ShowData();
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = 0;
            }
            Debug.WriteLine(string.Format("Scroll changed to {0}", hsProgress.Value));
            //            SetTimeLine();
            //            SyncVideo();
            _player.Time = currentSeconds * 1000;
            video.player.Time = currentSeconds * 1000;
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
        private void SensivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string num = sender.ToString().Substring(0, sender.ToString().Length - 5);
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            //将位选中的checked改为false，把选中的item的checked改为true
            foreach (ToolStripMenuItem i in this.sensitivityToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;

            sensitivity = int.Parse(num);
            //根据修改的值再重新显示图表
            ShowData();
        }

        /// <summary>
        /// 时间基准菜单选项点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerStandartMenuItem_Click(object sender, EventArgs e)
        {
            string num = sender.ToString().Substring(0, sender.ToString().Length - 6);
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            //将位选中的checked改为false，把选中的item的checked改为true
            foreach (ToolStripMenuItem i in this.timeStandartToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;

            timeStandard = int.Parse(num);

            //修改X轴的最大值
            SetAxisXMaximum(pageWidthInMM / timeStandard);

            //让画图函数重新计算一下当前的_xMaximum是多少，（因为若面板是显示的状态则只能再根据坐标与像素点来计算X轴显示的最大值）
            isChangingBoardShow = true; 
            
            //重新加载数据与显示数据
            LoadData(currentSeconds);
            ShowData();
        }

        /// <summary>
        /// 设置可显示的X轴最大值
        /// -- by lxl
        /// </summary>
        /// <param name="max"></param>
        private void SetXMaximum(double max)
        {
            xMaximum = max;

            //让滚动条的取值最大为总秒数-一页秒数-2 ，-2是为了保证留出2格的空格，让用户知道后面没有数据了
            this.hsProgress.LargeChange = (int)Math.Ceiling(xMaximum) - 2;
        }

        /// <summary>
        /// 设置一页有多少秒
        /// -- by lxl
        /// </summary>
        /// <param name="wins"></param>
        private void UpdateWindowSeconds()//int wins)
        {
            WINDOW_SECONDS = (int)Math.Ceiling(chartWave.ChartAreas[0].AxisX.Maximum);            //保证取的数据不小于当前窗口应该显示的数据,因为window_seconds为int，而秒数可能为小数

        }

        /// <summary>
        /// 设置X轴最大值，同时也会更新window_seconds的值以适应显示
        /// -- by lxl
        /// </summary>
        /// <param name="max"></param>
        private void SetAxisXMaximum(double max)
        {
            chartWave.ChartAreas[0].AxisX.Maximum = max;
            UpdateWindowSeconds();
        }
        /// <summary>
        /// 进度条，PagePrev,PageNext变化时，对应的时间也要发生变化；
        /// wsp
        /// </summary>
        private void changed()
        {

            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt_startTime = dt_startTime.AddSeconds(currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            displayStartTime.Text = dt_startTime.ToLongTimeString().ToString();
            dt_relativetime = dt_relativetime.AddSeconds(currentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            displayRecordingTime.Text = dt_relativetime.ToLongTimeString().ToString();
        }
        /// <summary>
        /// 初始化时间基准与灵敏度的选项
        /// -- by lxl
        /// </summary>
        private void InitMenuItems()
        {
            System.Windows.Forms.ToolStripMenuItem item;//= new ToolStripMenuItem();

            //初始化时间基准选项
            foreach (int t in timeStandardArray)
            {
                item = new ToolStripMenuItem();
                item.Name = "timeStandardMenuItem_" + t;
                item.Size = new Size(140, 22);
                item.Text = t + "mm/sec";
                item.Click += new EventHandler(this.TimerStandartMenuItem_Click);
                if (t == timeStandard)
                    item.Checked = true;
                this.timeStandartToolStripMenuItem.DropDownItems.Add(item);
            }

            //初始化灵敏度选项
            foreach (int s in sensitivityArray)
            {
                item = new ToolStripMenuItem();
                item.Name = "vcmToolStripMenuItem_" + s;
                item.Size = new Size(140, 22);
                item.Text = s + "μv/cm";
                item.Click += new EventHandler(this.SensivityToolStripMenuItem_Click);
                if (s == sensitivity)
                    item.Checked = true;
                this.sensitivityToolStripMenuItem.DropDownItems.Add(item);
            }

            //初始化通道显示数据选项
            foreach (int si in signalNumArray)
            {
                item = new ToolStripMenuItem();
                item.Name = "signalToolStripMenuItem_" + si;
                item.Size = new Size(140, 22);
                item.Text = si.ToString();
                item.Click += new EventHandler(this.SignalToolStripMenuItem_Click);
                if (si == signalNum)
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
        private void GetHsprogressMax()
        {
            SecPerPage();
            hsProgress.Maximum = maxPage;
        }
        #endregion
        #region 数据有多少页
        private void SecPerPage()
        {
            maxPage = (numberOfSamples + ((int)(10 * 30D / Convert.ToDouble(timeStandard)) * sampleRate) - 1) / ((int)(10 * 30D / Convert.ToDouble(timeStandard)) * sampleRate);  // 修改 --by zt
        }
        #endregion
        /// <summary>
        /// Y轴校准点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibrateYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calibForm == null)
                calibForm = new CalibrateYForm(this);
            this.calibForm.Show();
            this.calibForm.BringToFront();
        }
        /// <summary>
        /// Y轴校准
        /// -- by lxl
        /// </summary>
        /// <param name="height">一厘米多少像素点</param>
        public void CalibrateY(double height)
        {
            //一毫米多少像素点
            pixelPerMM = height / 10D;

            //表格高度有多少毫米
            pageHeightInMM = pageHeightInPixel / pixelPerMM;

            //一格应该有多少毫米
            mmPerYGrid = pageHeightInMM / 20;

            ShowData();
        }
        /// <summary>
        /// X轴校准点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibrateXToolStripMenuItem_Click(object sender, EventArgs e)
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
        public void CalibrateX(double width)
        {
            //一毫米多少像素点
            pixelPerMM = width / 10D;

            //表格高度有多少毫米
            pageWidthInMM = pageWidthInPixel / pixelPerMM;

            //根据表格宽度设置X轴的最大值
            SetAxisXMaximum(pageWidthInMM / timeStandard);

            //由于修改了X轴最大值，故重新加载、重新显示数据
            LoadData(currentSeconds);
            ShowData();
        }
        /// <summary>
        /// chart的重绘函数
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartPaint(object sender, PaintEventArgs e)
        {
            if (pageWidthInPixel == 0)                            //计算图表的宽与高          -- by lxl
            {
                pageWidthInPixel = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisX.Maximum) - this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(0);
                pageWidthInMM = pageWidthInPixel / pixelPerMM;
                SetAxisXMaximum(pageWidthInMM / timeStandard);
                LoadData(currentSeconds);
                ShowData();
                pageHeightInPixel = Math.Abs(this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisY.Maximum) - this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0));
                pageHeightInMM = pageHeightInPixel / pixelPerMM;
            }
            DrawEvents(e.Graphics);
            if (isChangingBoardShow)                       //计算面板显示与否的宽度    -- by lxl
            {
                if (isBoardShow)
                {
                    SetXMaximum(this.chartWave.ChartAreas[0].AxisX.PixelPositionToValue(this.chartWave.Width - this.boardPanel.Width));
                }
                else
                {
                    SetXMaximum(this.chartWave.ChartAreas[0].AxisX.Maximum);
                }
                isChangingBoardShow = false;
            }
            if (isAddingEvent)
            {
                e.Graphics.DrawLine(new Pen(addingEventColor), new Point(Control.MousePosition.X - this.chartWave.Location.X, 0), new Point(Control.MousePosition.X - this.chartWave.Location.X, this.chartWave.Height));
                mouseValueNow = this.chartWave.ChartAreas[0].AxisX.PixelPositionToValue(Control.MousePosition.X - this.chartWave.Location.X) * sampleRate;
            }
            //画图表上的秒数
            double time1Pos = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition((int)Math.Floor(xMaximum) / 2);
            if (xMaximum >= 2)
            {
                e.Graphics.DrawString((currentSeconds + (int)Math.Floor(xMaximum) / 2).ToString(), new System.Drawing.Font("黑体", 10, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF((int)time1Pos, 25, 80, 15));
                e.Graphics.DrawString((currentSeconds + (int)Math.Floor(xMaximum) / 2 * 2).ToString(), new System.Drawing.Font("黑体", 10, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF((int)time1Pos * 2, 25, 80, 15));
            }
            else
            {
                e.Graphics.DrawString((currentSeconds + 1).ToString(), new System.Drawing.Font("黑体", 10, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF((int)(this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(1)), 25, 80, 15));
            }
        }

        /// <summary>
        /// 画病人事件
        /// -- by lxl
        /// </summary>
        /// <param name="g">画笔</param>
        private void DrawEvents(Graphics g)
        {
            //设置画画所需要的变量
            Pen dotPen = new Pen(Color.Red, 2);            //虚线画笔    
            dotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            SolidBrush strBrush = new SolidBrush(Color.White);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Bold);

            //事件该画的位置
            double drawPosition;

            //画预定义事件                   
            foreach (PreDefineEvent p in preDEventsList)                  
            {
                //只画当前页面能显示的事件
                if (p.EventPosition / sampleRate < currentSeconds)                 
                    continue;
                if (p.EventPosition / sampleRate > currentSeconds + xMaximum)
                    break;
                dotPen.Color = p.EventColor;
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(p.EventPosition / sampleRate);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, p.EventColor)), new Rectangle((int)drawPosition - 40, 5, 80, 15));
                switch (p.EventName)
                {
                    case PreDefineEvent.PreDefineEventsName.eyesOpen: g.DrawString("睁眼", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                    case PreDefineEvent.PreDefineEventsName.eyesClose: g.DrawString("闭眼", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                    case PreDefineEvent.PreDefineEventsName.deepBreath: g.DrawString("深呼吸", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                    case PreDefineEvent.PreDefineEventsName.calibrate: g.DrawString("定标", strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15)); break;
                }
                g.DrawLine(dotPen, new Point((int)drawPosition, 5), new Point((int)drawPosition, (int)this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0)));
            }

            //画自定义事件                  
            foreach (CustomEvent p in customEventList)                  
            {
                //只画当前页面能显示的事件
                if (p.EventPosition / sampleRate < currentSeconds)                 
                    continue;
                if (p.EventPosition / sampleRate > currentSeconds + xMaximum)
                    break;
                dotPen.Color = p.EventColor;
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(p.EventPosition / sampleRate);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, p.EventColor)), new Rectangle((int)drawPosition - 40, 5, 80, 15));
                g.DrawString(p.EventName, strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15));
                g.DrawLine(dotPen, new Point((int)drawPosition, 5), new Point((int)drawPosition, (int)this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0)));
            }
        }
        /// <summary>
        /// labelPanel的重绘函数,若要更新label则需调用labelPanel.Invalidate()函数
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawLabelPanel(object sender, PaintEventArgs e)
        {
            SolidBrush strBrush = new SolidBrush(Color.Black);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Regular);
            double topSigPos = this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(2000D - 2000D / signalNum / 2);
            double intervalPos = this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(2000D - 2000D / signalNum / 2 * 3) - topSigPos;
            foreach (int i in Enumerable.Range(currentTopSignal, signalNum))
            {
                e.Graphics.DrawString((i + 1).ToString(), strFont, strBrush, new RectangleF(0, (int)(topSigPos + (i - currentTopSignal) * intervalPos), this.labelPanel.Width, 15));
            }
        }

        /// <summary>
        /// 面板点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isBoardShow = !isBoardShow;
            this.boardPanel.Visible = isBoardShow;
            this.boardToolStripMenuItem.Checked = isBoardShow;

            //重新计算X轴显示的最大值
            isChangingBoardShow = true;

            //修改竖直滚动条的位置
            if (isBoardShow)
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
            currentSeconds = 0;
            dt_relativetime = DateTime.Parse("2016-05-23  00:00:00");
            dt_totaltime = DateTime.Parse("2016-05-23 00:00:00");
            //            _player.Time = (long)_nfi.VideoOffset * 1000;
            _CurrentOffset = 0;
            _player.Time = 0;
            hsProgress.Value = 0;
            LoadData(currentSeconds);
            ShowData();
        }
        /// <summary>
        /// 通道数目显示点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in this.signalToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;
            signalNum = int.Parse(sender.ToString());
            this.chartWave.ChartAreas[0].AxisY.MajorGrid.Interval = 2000D / signalNum;
            this.chartWave.ChartAreas[0].AxisY.MajorTickMark.Interval = 2000D / signalNum;
            if (currentTopSignal + signalNum > 20)
                currentTopSignal = 20 - signalNum;
            ShowData();
            if (signalNum < 20)
                SetVScrollVisible(true);
            else
                SetVScrollVisible(false);
            this.vScroll.LargeChange = signalNum + 1;
            this.labelPanel.Invalidate();
        }
        /// <summary>
        /// 设置竖直滚动条是否可用
        /// -- by lxl
        /// </summary>
        /// <param name="flag"></param>
        private void SetVScrollVisible(bool flag)
        {
            this.vScroll.Visible = flag;
        }

        /// <summary>
        /// 竖直滚动条鼠标状态改变事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBar_mouseCapture(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.vScroll.Value);

            //若值相同就没必要再重绘表了
            if (this.currentTopSignal != this.vScroll.Value)
            {
                //更改显示在最上方的信道，并重新画图
                this.currentTopSignal = this.vScroll.Value;
                ShowData();

                //更改对应的label值
                this.labelPanel.Invalidate();
            }
        }
        /// <summary>
        /// 事件按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem a = (ToolStripMenuItem)sender;
            
            //若名字以pr开头则是preDefine（预定义）事件
            if (a.Name.Substring(0, 2) == "pr")
            {
                if (myPreDefineEventFormEventForm == null)
                    myPreDefineEventFormEventForm = new predefineEventsForm(this);
                myPreDefineEventFormEventForm.Show();
                myPreDefineEventFormEventForm.BringToFront();
                myPreDefineEventFormEventForm.initList();
            }
            else//若名字以pr开头则是custom（自定义）事件
            {
                if (myCustomEventForm == null)
                    myCustomEventForm = new CustomEventForm(this);
                myCustomEventForm.Show();
                myCustomEventForm.BringToFront();
                myCustomEventForm.initList();
            }
        }
        /// <summary>
        /// 获得预定义事件列表
        /// -- by lxl
        /// </summary>
        /// <returns>预定义事件列表</returns>
        public List<PreDefineEvent> GetPreEventList()
        {
            return preDEventsList;
        }

        /// <summary>
        /// 获得自定义事件列表
        /// -- by lxl
        /// </summary>
        /// <returns>自定义事件列表</returns>
        public List<CustomEvent> GetCustomEventList()
        {
            return customEventList;
        }

        /// <summary>
        /// 获取采样频率
        /// -- by lxl
        /// </summary>
        /// <returns></returns>
        public double GetSampleRate()
        {
            return sampleRate;
        }

        /// <summary>
        /// 获取文件开始时间
        /// -- by lxl
        /// </summary>
        /// <returns></returns>
        public DateTime GetStartTime()
        {
            return _nfi.StartDateTime;
        }

        /// <summary>
        /// 开始添加事件
        /// -- by lxl
        /// </summary>
        /// <param name="flag">true为预定义事件，false为自定义事件</param>
        /// <param name="clr">事件颜色</param>
        /// <param name="name">事件名称</param>
        public void StartAddEvents(bool flag, Color clr, object name)
        {
            //设置添加事件过程中所要画的直线的颜色
            addingEventColor = clr;

            //设置现在是添加的什么事件
            isAddingPreDefineEvent = flag;

            isAddingEvent = true;
            
            //根据现在是在添加什么事件来确定事件名称
            if (flag)
                addedEventNameForPDEvent = (PreDefineEvent.PreDefineEventsName)name;
            else
                addedEventNameForCustomEvent = (string)name;
        }

        /// <summary>
        /// 鼠标在chart上的移动事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseMoveOnChart(object sender, MouseEventArgs e)
        {
            //重绘整个表格（因为确定位置的直线改变位置，而且目前没有找到重绘表格一部分的方法），只在添加事件的过程中用
            if (isAddingEvent)
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
        private void FormPaint(object sender, PaintEventArgs e)
        {
            if (isAddingEvent)
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
        private void Chartwave_Click(object sender, EventArgs e)
        {
            if (isAddingEvent)
            {
                //左键才添加事件，右键则不添加事件
                if (((MouseEventArgs)e).Button == MouseButtons.Left)
                {
                    if (isAddingPreDefineEvent)
                    {
                        preDEventsList.Add(new PreDefineEvent(addedEventNameForPDEvent, mouseValueNow));

                        //事件添加后更新添加事件的form里的列表
                        myPreDefineEventFormEventForm.updateListView(true);
                    }
                    else
                    {
                        customEventList.Add(new CustomEvent(addedEventNameForCustomEvent, mouseValueNow, addingEventColor));

                        //事件添加后更新添加事件的form里的列表
                        myCustomEventForm.UpdateListView(true);
                    }
                }
                isAddingEvent = !isAddingEvent;
                this.chartWave.Invalidate();
            }
        }

        /// <summary>
        /// 删除指定索引的事件
        /// -- by lxl
        /// </summary>
        /// <param name="flag">是否是预定义事件</param>
        /// <param name="index">索引</param>
        public void RemoveEvent(bool flag, int index)
        {
            if (flag)
            {
                preDEventsList.RemoveAt(index);

                //事件删除后更新添加事件的form里的列表
                myPreDefineEventFormEventForm.updateListView(false);
            }
            else
            {
                customEventList.RemoveAt(index);

                //事件删除后更新添加事件的form里的列表
                myCustomEventForm.UpdateListView(false);
            }
            this.chartWave.Invalidate();
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
        private void ChartSizeChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("sizechanged" + DateTime.Now);

            //重画label，防止图表大小改变后label显示位置不正确
            this.labelPanel.Invalidate();
        }
    }
}
