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
using System.Threading;

// nVLC, strange namespaces :(
using Declarations;
using Declarations.Players;
using Declarations.Media;
using Implementation;
using System.Collections;

namespace VeegStation
{
    /// <summary>
    /// 回放文件Form
    /// </summary>
    public partial class PlaybackForm : Form
    { 
        #region 声明变量
        /// <summary>
        /// Controller  --by zt
        /// </summary>
        private VeegControl controller;

        /// <summary>
        /// 公共数据池   --by zt
        /// </summary>
        private CommonData commonDataPool;

       /// <summary>
       /// 定义绝对时间
       /// --by wsp
       /// </summary>
        DateTime DT;        
        
        /// <summary>
        /// 定义相对时间
        /// --by wsp  
        /// </summary>  
        DateTime DT_RelativeTime;      
        
        /// <summary>
        /// //定义总时间
        /// --by wsp
        /// </summary>
        DateTime DT_TotalTime;   
       
        /// <summary>
        /// 定义视频播放速率
        /// --by wsp
        /// </summary>
        public float Speed = 1.0f;
        
        /// <summary>
        /// 定义视频面板Form
        /// --by wsp
        /// </summary>
        private VideoForm video;

        /// <summary>
        /// 获得中断之间的时差
        /// --by wsp
        /// </summary>
        private double getDvalue=0.0;

        /// <summary>
        /// 中断的时间数组
        /// --by wsp
        /// </summary>
        private double[] timeSignal;

        /// <summary>
        /// 标示作为video是否弹出
        /// --by wsp
        /// </summary>
        private int  isPop=0;

        /// <summary>
        /// 标示isPop执行一次
        /// --by wsp
        /// </summary>
       private  int  isIdent= 0;

        private /*const*/ int WINDOW_SECONDS = 10;                                                                                      //不同的时间基准会有不同的window_seconds，所以取消掉const -- by lxl
        
        /// <summary>
        /// 诺诚头文件  --by zt
        /// </summary>
        public NationFile nfi = null;  
  
        /// <summary>
        /// 采样率 --by zt
        /// </summary>
        private int sampleRate;

        /// <summary>
        /// 总点数 --by zt
        /// </summary>
        private int numberOfSamples; 

        /// <summary>
        /// 每个数据块占用的字节数  --by zt
        /// </summary>
        private int byteOfPerData;   

        /// <summary>
        /// 数据块中脑电数据占用的字节数   --by zt
        /// </summary>
        private int numberOfPerData; 

        /// <summary>
        /// 数据的起始位置  --by zt
        /// </summary>
        private int indexOfData;     

        /// <summary>
        /// 测数值,调试用  --by zt
        /// </summary>
        List<double> testList = new List<double>();

        #region 导联参数 --by zt
        /// <summary>
        /// 导联配置Form  --by zt
        /// </summary>
        private LeadConfigForm myLeadConfigForm;

        /// <summary>
        /// 导联配置
        /// </summary>
        private Hashtable leadConfigList;

        /// <summary>
        /// 导联源
        /// </summary>
        private Hashtable leadSource;

        /// <summary>
        /// 当前导联名称
        /// </summary>
        private string currentLeadConfigName;

        /// <summary>
        /// 存放当前导联配置
        /// </summary>
        private ArrayList leadConfigArrayList;

        /// <summary>
        /// 该数据的硬件配置名称
        /// </summary>
        private string hardwareConfigName;
        #endregion

        #region 滤波参数  --by zt
        /// <summary>
        /// 是否50Hz滤波  --by zt
        /// </summary>
        private bool is50HzFilter;
 
        /// <summary>
        /// 是否带通滤波  --by zt
        /// </summary>
        private bool isBandFilter;
 
        /// <summary>
        /// 采样点数      --by zt
        /// </summary>
        private int Ns;   
        
        /// <summary>
        /// 采样时间      --by zt
        /// </summary>
        private double tc;   
      
        /// <summary>
        /// 采样频率      --by zt
        /// </summary>
        private int fs;           

        /// <summary>
        /// 低频参数      --by zt
        /// </summary>
        private int lowFrequency;

        /// <summary>
        /// 高频参数      --by zt
        /// </summary>
        private int highFrequency;

        /// <summary>
        /// 滤波实例  --by zt
        /// </summary>
        private FreqAnalyzer freFilter = new FreqAnalyzer();
        
        /// <summary>
        /// 带通滤波Form  --by zt
        /// </summary>
        private BandFilterForm myBandFilterForm;
        #endregion

        int maxPage;

        /// <summary>
        /// 脑电数据list  --by xjg
        /// </summary>
        private List<EegPacket> packets = new List<EegPacket>();

        #region 事件相关
        /// <summary>
        /// 预定义事件队列
        /// -- by lxl
        /// </summary>
        private List<PreDefineEvent> preDefineEventsList = new List<PreDefineEvent>();

        /// <summary>
        /// 本次添加的预定义事件队列
        /// -- by lxl
        /// </summary>
        private List<PreDefineEvent> preDefineEventsListToBeAdd = new List<PreDefineEvent>();

        /// <summary>
        /// 自定义事件队列
        /// -- by lxl
        /// </summary>
        private List<CustomEvent> customEventList = new List<CustomEvent>();

        /// <summary>
        /// 事件属性，CFG后面除开具体的事件信息外的其余信息
        /// -- by lxl
        /// </summary>
        private EventProperty eventProperty;

        /// <summary>
        /// 删除预定义事件时用来保存删除事件的位置列表
        /// -- by lxl
        /// </summary>
        private List<int> positionInFileOfDeletedFileList = new List<int>();
        #endregion

        private int _Page;

        /// <summary>
        /// 定义走了多少时间
        /// --by wsp
        /// </summary>
        public double LongTime;
        private int _maxPage;
        private int totalSeconds;
        private DateTime? _LastTime = null;

        /// <summary>
        /// 偏移量
        /// --by xls
        /// </summary>
        public double CurrentOffset;

        /// <summary>
        /// 定义播放器
        /// --by xls
        /// </summary>
        public IVideoPlayer Player;

        /// <summary>
        /// 定义视频media
        /// --by xls
        /// </summary>
        private IMedia media;

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
        private CalibrateXForm calibXForm;

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
        private PredefineEventsForm myPreDefineEventFormEventForm;

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
        /// 所添加的预定义事件的编号（即第几号预定义事件）,启事位置从0开始
        /// -- by lxl
        /// </summary>
        private int preDefineEventIndex;
        /// <summary>
        /// 所添加的事件名称
        /// -- by lxl
        /// </summary>
        private string addedEventName;
        /// <summary>
        /// 判断现在是在添加什么事件，true为custom，false为preDefine
        /// -- by lxl
        /// </summary>
        private bool isAddingPreDefineEvent;

        /// <summary>
        /// 添加事件时的所选事件的COLOR的索引
        /// -- by lxl
        /// </summary>
        private int addingEventColorIndex;
        #endregion

        #region 访问器

        #region 滤波参数访问器
        public bool Is50HzFilter
        {
            get { return is50HzFilter; }
            set { is50HzFilter = value; }
        }

        public bool IsBandFilter
        {
            get { return isBandFilter; }
            set { isBandFilter = value; }
        }

        public int LowFrequency
        {
            get { return lowFrequency; }
            set { lowFrequency = value; }
        }

        public int HighFrequency
        {
            get { return highFrequency; }
            set { highFrequency = value; }
        } 
        #endregion


        public int SampleRate
        {
            get { return sampleRate; }
        }  

        #endregion

        public PlaybackForm(NationFile EegFile)
        {
            InitializeComponent();

            //构造导联配置Form实例  --by zt
            myLeadConfigForm = new LeadConfigForm();

            //构造滤波Form实例  --by zt
            myBandFilterForm = new BandFilterForm(this);

            try
            {
                this.nfi = EegFile;
            }
            catch(Exception ex)
            {
                MessageBox.Show("文件格式错误");
                LogHelper.WriteLog(typeof(PlaybackForm), ex.Message + "文件格式错误");
                return;
            }

            //根据是否有视频判定面板是否显示--by wsp
            if (this.nfi.HasVideo)
                isBoardShow = true;
            else
                isBoardShow = false;

            this.boardToolStripMenuItem.Checked = isBoardShow;
            this.boardPanel.Visible = isBoardShow;

            _Page = 0;
            
            //初始化预定义事件的颜色选项与名称选项
            PreDefineEvent.InitPreDefineEventNameWithArray(nfi.EventCount, nfi.PreDefineEventNameArray, nfi.PreDefineEventColorArray);

            myBandFilterForm = new BandFilterForm(this);
            
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="control"></param>
        public void ReigisterVeegControl(VeegControl control)
        {
            this.controller = control;
            myLeadConfigForm.ReigisterVeegControl(control);
            
        }

        #region 各种初始化
        /// <summary>
        /// 初始化PlaybackForm中各参数
        /// --by zt
        /// </summary>
        public void InitPlaybackFormParas() 
        {
            //诺诚文件
            InitNationFileParas();

            //配置文件
            InitFromConfig();

            //根据配置导联里的通道个数通道显示选项数组
            SetSignalNumArray(leadConfigArrayList.Count);

            //初始化一些画图必要的变量
            InitChartParas();

            //初始化时间基准、灵敏度与通道显示数目的选项,（注意，要放在InitFromConfig()后面）
            InitMenuItems();


            
            //解析事件，并将事件添加进事件列表中
            ParseEvent();

            //解析完事件后在右边列表中显示出事件
            UpdateLVPreDefineEvents();
            UpdateLVCustomEvents();
        }

        /// <summary>
        /// 从诺诚文件中获得信息
        /// --by zt
        /// </summary>
        private void InitNationFileParas() 
        {
            //采样率 --by zt
            sampleRate = this.nfi.NatInfo.Freq;

            //总点数  --by zt
            numberOfSamples = this.nfi.NumberOfSamples;

            //开始时间  --by zt
            DT = this.nfi.StartDateTime;
            //GetHsprogressMax();                   不知道谁写的什么鬼，没发现用处，作用仅仅对hsProgress.maximum赋值（似乎像是以前按页来算的赋值方式），然而下方又赋值了一次（下方赋值为正确），故注释 -- by lxl

            //修改 --by zt
            totalSeconds = (int)this.nfi.Duration.TotalSeconds;
            hsProgress.Maximum = totalSeconds;         //不一定是整数秒 故maximum不需要-1

            //硬件配置名称
            this.InitHardwareConfigParameters(nfi.NatInfo.ByteConfigType);

            this.byteOfPerData = nfi.NatInfo.RowsOfData;
            
        }

        private void InitChartParas() 
        {
            //根据有多少条信道设置数值滚动条的最大值
            this.vScroll.Maximum = this.leadConfigArrayList.Count;

            //画图相关变量的初始化
            isChangingBoardShow = true;
            currentTopSignal = 0;
            isAddingEvent = false;

            if (leadConfigArrayList.Count >= 20)
                SetSignalNum(20);
            else
                SetSignalNum(leadConfigArrayList.Count);
        }

        /// <summary>
        /// 从配置文件中读取变量并赋值,待定  --by zt
        /// </summary>
        private void InitFromConfig()
        {
            //得到公共数据池
            //this.commonDataPool = controller.GetCommonData();
            this.commonDataPool = controller.CommonDataPool;
            #region 画图参数

            mmPerYGrid = commonDataPool.MMPerYGrid;
            pixelPerMM = commonDataPool.PixelPerMM;
            setTimeStandard(commonDataPool.TimeStandard);
            setSensitivity(commonDataPool.Sensitivity);

            #endregion

            #region 导联参数
            InitLeadParameters();
            #endregion
        }

        #region 初始化格式内各个选项 -- by lxl
        /// <summary>
        /// 初始化时间基准与灵敏度的选项
        /// -- by lxl
        /// </summary>
        private void InitMenuItems()
        {
            //初始化时间基准选项
            InitTimeStandardMenuItems();

            //初始化灵敏度选项
            InitSensitivityMenuItems();

            //初始化通道数量选项
            InitSignalNumMenuItems();
        }

        /// <summary>
        /// 初始化时间基准选项
        /// -- by lxl
        /// </summary>
        private void InitTimeStandardMenuItems()
        {
            System.Windows.Forms.ToolStripMenuItem item;

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
        }

        /// <summary>
        /// 初始化灵敏度选项
        /// -- by lxl
        /// </summary>
        private void InitSensitivityMenuItems()
        {
            System.Windows.Forms.ToolStripMenuItem item;

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
        }

        /// <summary>
        /// 初始化通道数量选项
        /// -- by lxl
        /// </summary>
        private void InitSignalNumMenuItems()
        {
            System.Windows.Forms.ToolStripMenuItem item;

            this.signalToolStripMenuItem.DropDownItems.Clear();

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
        /// 根据所设置的最大通道值设置通道显示选项数组
        /// -- by lxl
        /// </summary>
        /// <param name="signalNum">通道的最大数目</param>
        private void SetSignalNumArray(int signalNum)
        {
            //屏幕最多显示20路通道，故>=20显示的数目都为20
            if (signalNum >= 20)
            {
                signalNumArray = new int[] { 1, 2, 4, 8, 16, 20 };
            }
            else
            {
                //获得通道数目所在的区间（即大于2^i && 小于2^i+1）
                int i;
                for (i = 0; i < signalNum; i++)
                {
                    if (signalNum < Math.Pow(2, i))
                        break;
                }

                //分配通道显示数据数组大小
                if (Math.Pow(2, i - 1) == signalNum)
                    signalNumArray = new int[i];
                else
                    signalNumArray = new int[i + 1];

                //给数组内容赋值。（注意，数组内最后一个值无论math.pow(2,i)是否等于count都为count的值，故将其单独提出来）
                for (int j = 0; j < signalNumArray.Count() - 1; j++)
                {
                    signalNumArray[j] = (int)Math.Pow(2, j);
                }
                signalNumArray[signalNumArray.Length - 1] = signalNum;
            }
        }

        #endregion

        /// <summary>
        /// 更新导联参数  --by zt
        /// </summary>
        public void InitLeadParameters() 
        {
            Hashtable defaultLeadSource = (Hashtable)this.controller.CommonDataPool.GetLeadSource(this.hardwareConfigName)[0];
            Hashtable myLeadSource = (Hashtable)this.controller.CommonDataPool.GetLeadSource(this.hardwareConfigName)[1];

            this.leadSource = myLeadSource.Count != 0 ? myLeadSource : defaultLeadSource;
            this.leadConfigList = (Hashtable)this.controller.CommonDataPool.GetLeadList(this.hardwareConfigName);

            InitLeadItems();
        }

        /// <summary>
        /// 根据不同的设备类型，定义硬件配置名称，每个数据块占用的字节数、脑电数据占用的字节数、脑电数据开始位置  --by zt
        /// </summary>
        /// <param name="configName">硬件配置</param>
        private void InitHardwareConfigParameters(byte config) 
        {
            switch (config)
            {
                //8导脑电
                case 0x00:
                    this.hardwareConfigName = "8导脑电";
                    //byteOfPerData = 26;
                    numberOfPerData = 8;
                    indexOfData = 6;
                    break;

                //8导脑电+多参数
                case 0x40:
                    this.hardwareConfigName = "8导脑电+多参数";
                    //byteOfPerData = 48;
                    numberOfPerData = 8;
                    indexOfData = 28;
                    break;

                //16导脑电
                case 0x04:
                    this.hardwareConfigName = "16导脑电";
                    //byteOfPerData = 46;
                    numberOfPerData = 16;
                    indexOfData = 6;
                    break;

                //16导脑电+多参数
                case 0x44:
                    this.hardwareConfigName = "16导脑电+多参数";
                    //byteOfPerData = 68;
                    numberOfPerData = 16;
                    indexOfData = 28;
                    break;

                //24导脑电
                case 0x02:
                    this.hardwareConfigName = "24导脑电";
                    //byteOfPerData = 86;
                    numberOfPerData = 19;
                    indexOfData = 6;
                    break;

                //32导脑电
                case 0x08:
                    this.hardwareConfigName = "32导脑电";
                    //byteOfPerData = 86;
                    numberOfPerData = 19;
                    indexOfData = 6;
                    break;

                //32导脑电+多参数
                case 0x48:
                    this.hardwareConfigName = "32导脑电+多参数";
                    //byteOfPerData = 108;
                    numberOfPerData = 19;
                    indexOfData = 28;
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 从文件中加载数据
        /// </summary>
        /// <param name="Offset"></param>
        public void  LoadData(int Offset)
        {
            if (nfi == null)
            {
                return;
            }
            int loadRec = WINDOW_SECONDS * sampleRate;
            DateTime begin = DateTime.Now;
            #region 加载数据
            ParseData(byteOfPerData, numberOfPerData, indexOfData, Offset, loadRec);
            #endregion

            DateTime end = DateTime.Now;
            Debug.WriteLine(string.Format("Read a window of data {0} records in {1} seconds", packets.Count, (end - begin).TotalSeconds));
        }

        /// <summary>
        /// 解析事件
        /// -- by lxl
        /// </summary>
        private void ParseEvent()
        {
            preDefineEventsList = nfi.ParsePreDefineEvent(nfi.NedFileName.Split('.')[0] + ".NAT",nfi.NatInfo.CfgOff);
            customEventList = nfi.ParseCustomEvent(nfi.NedFileName.Split('.')[0] + ".ent", nfi.NatInfo.CfgOff);
            eventProperty = nfi.ParseEventProperty(nfi.NedFileName.Split('.')[0] + ".NAT", nfi.NatInfo.CfgOff);
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
            FileStream fs = new FileStream(nfi.NedFileName, FileMode.Open, FileAccess.Read);

            //数据从512开始  --by zt
            fs.Seek(0x200, SeekOrigin.Begin);

            //将文件流定位在偏移值为OFFSET秒的位置
            fs.Seek(byteOfPerData * Offset * sampleRate, SeekOrigin.Current);//  --by lxl
            byte[] buf = new byte[byteOfPerData];
            packets.Clear();
            testList.Clear();//测试  --by zt

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
                //double ekg = Util.RawToSignal((short)(buf[6] | (buf[7] << 8)));//心电数据   --by zt
                double ekg = 0;//不知道心电数据，暂时为0
                List<double> eeg = new List<double>();

                //加载脑电数据
                foreach (int off in Enumerable.Range(0, numberOfPerData))
                {
                    eeg.Add(Util.RawToSignal((short)(buf[indexOfData + 2 * off] | (buf[indexOfData + 1 + 2 * off] << 8))));
                    //测试代码 --by zt
                    if (off == 0)
                    {
                        testList.Add(eeg[off]);
                        //Console.WriteLine(eeg[off]);
                    }
                }

                //其余的用0补全
                for (int i = numberOfPerData; i < leadSource.Count; i++)
                {
                    eeg.Add(0);
                }
                EegPacket pkt = new EegPacket(ekg, eeg.ToArray());
                packets.Add(pkt);
            }

            //Console.WriteLine("最大值 {0}，最小值 {1}", testList.Max(), testList.Min());  //测试用到  --by zt
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        public void ShowData()
        {
            DateTime begin = DateTime.Now;
            SeriesCollection col = chartWave.Series;
            double interval = 2000D / signalNum;
            chartWave.SuspendLayout();
            foreach (int sIdx in Enumerable.Range(0, chartWave.Series.Count))
            {
                int a = col[sIdx].Points.Count;
                col[sIdx].Points.Clear();
            }
            #region 滤波处理
            //每个通道的数据     
            //对每个通道的数据进行滤波
                //for (int i = 0; i < _packets.Count; i++)
                //{
                //    foreach (int sIdx in Enumerable.Range(0, signalNum - 1))
                //    {
                //        double[] NewData = freFilter.BandpassFilter(sIdx, ReturnSignalData(sIdx), is50HzFilter, isBandFilter, 10, 100, sampleRate);
                //        _packets[i].EEG[sIdx] = NewData[i];
                //    }
                //}
                foreach (int sIdx in Enumerable.Range(0, signalNum - 1))
                {
                    double[] NewData = freFilter.BandpassFilter(sIdx, ReturnSignalData(sIdx), is50HzFilter, isBandFilter, 10, 100, sampleRate);
                    for (int i = 0; i < packets.Count; i++)
                    {
                    packets[i].EEG[sIdx] = NewData[i];
                    }
                }
            //if (isBandFilter == true)
            //{
            //    foreach (int j in Enumerable.Range(0, _packets.Count))
            //    {
            //        double[] NewData = freFilter.BandpassFilter(j, ReturnSignalDataZhong(j), false, true, myBandFilterForm.low, myBandFilterForm.high, sampleRate);
            //        for (int i = 0; i < signalNum - 1; i++)
            //        {
            //            _packets[j].EEG[i] = NewData[i];
            //        }
            //    }

            //}
            //if (is50HzFilter == true)
            //{
            //    foreach (int j in Enumerable.Range(0, _packets.Count))
            //    {                 
            //            double[] NewData = freFilter.BandpassFilter(j, ReturnSignalDataZhong(j), true, false, 10, 100, sampleRate);
            //            for (int i = 0; i < signalNum - 1; i++)
            //            {         
            //            _packets[j].EEG[i] = NewData[i];
            //        }
            //    }
            //}
            //freFilter.BandpassFilter()
            #endregion   
            try 
            {
                if (leadConfigArrayList == null || leadConfigArrayList.Count == 0)
                    return;

                //做差后的值
                double[] sampleDifferValue = new double[leadConfigArrayList.Count];
                foreach (int tIdx in Enumerable.Range(0, packets.Count))
                {
                    foreach (int sIdx in Enumerable.Range(currentTopSignal, signalNum))
                    {
                        //offset = 2400 - i * 200;   注释掉，offset不能这么写   -by lxl
                        //offset = (int)chart_EEG.ChartAreas[0].AxisY.Maximum - i * 2 * vertical_axis_Scale - vertical_axis_Scale;  // -- by lxl

                        //第20路为心电数据，放在最后一路显示
                        //if (sIdx == 19)
                        //{
                        //    col[19].Points.AddXY(tIdx / 128.0, packets[tIdx].EKG * interval * 10 / sensitivity / mmPerYGrid + (2000D - interval * (19 - currentTopSignal) - interval / 2));
                        //    continue;
                        //}

                        //获取做差的两个电极名称
                        string[] FPi_FPj = leadConfigArrayList[sIdx].ToString().Split(new char[] { '-' });

                        //查找多差电极对应的通道号
                        int channelNum_Positive = 1;
                        int channelNum_Negative = 1;

                        //由通道号对应通道名称的哈希表中读取需要显示的通道号
                        foreach (DictionaryEntry item in leadSource)
                        {
                            if (item.Value.ToString() == FPi_FPj[0])
                                channelNum_Positive = Convert.ToInt32(item.Key);
                            if (item.Value.ToString() == FPi_FPj[1])
                                channelNum_Negative = Convert.ToInt32(item.Key);
                        }

                        //求两个电极电位差
                        double sampleValue_Positive = 0;
                        double sampleValue_Negative = 0;
                        if (FPi_FPj[0] != "REF")
                            sampleValue_Positive = packets[tIdx].EEG[channelNum_Positive - 1]; //data[channelNum_Positive - 1][k];
                        if (FPi_FPj[1] != "REF")
                            sampleValue_Negative = packets[tIdx].EEG[channelNum_Positive - 1];
                        sampleDifferValue[sIdx] = sampleValue_Positive - sampleValue_Negative;
                        sampleDifferValue[sIdx] = sampleDifferValue[sIdx] * 1000 / sensitivity / mmPerYGrid;              //根据所校准的单位与灵敏度调整Y轴值-- by lxl
                        sampleDifferValue[sIdx] += (2000D - interval * (sIdx - currentTopSignal) - interval / 2);
                        col[sIdx].Points.AddXY(tIdx / (double)this.sampleRate, sampleDifferValue[sIdx]);
                    }
                }

                //foreach (int tIdx in Enumerable.Range(0, _packets.Count))
                //{
                //    foreach (int sIdx in Enumerable.Range(currentTopSignal, signalNum))
                //    {
                //        if (sIdx == 19)
                //        {
                //            col[19].Points.AddXY(tIdx / 128.0, _packets[tIdx].EKG * interval * 10 / sensitivity / mmPerYGrid + (2000D - interval * (19 - currentTopSignal) - interval / 2));
                //            continue;
                //        }
                //        double val = _packets[tIdx].EEG[sIdx];
                //        //val /= 20;   //修改，注释掉  --by zt
                //        val = val * interval * 10 / sensitivity / mmPerYGrid;              //根据所校准的单位与灵敏度调整Y轴值-- by lxl
                //        //val += (2000 - 100 * sIdx - 50);
                //        val += (2000D - interval * (sIdx - currentTopSignal) - interval / 2);

                //        col[sIdx].Points.AddXY(tIdx / 128.0, val);
                //    }
                //}
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message + ":chart");
            }
            
            chartWave.ResumeLayout();
            DateTime end = DateTime.Now;
            Debug.WriteLine(string.Format("Show a window of data in {0} seconds", (end - begin).TotalSeconds));
        }

        private void PlaybackForm_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btn_accelerate, "加速");
            toolTip1.SetToolTip(btn_decelerate, "减速");
            toolTip1.SetToolTip(btn_hide, "隐藏");
            if (nfi.Duration.TotalSeconds <= pageWidthInMM / timeStandard)
                btnNext.Enabled = false;
            if (nfi == null)
            {
                Close();
                return;
            }
            #region 绝对时间和总时间初始化模块--by wsp
            //初始化绝对时间和总时间，并显示
            DT_RelativeTime=DateTime.Parse("2016-05-23  00:00:00");
            DT_TotalTime = DateTime.Parse("2016-05-23 00:00:00");
            DT = nfi.StartDateTime;
            displayStartTime.Text = DT.ToLongTimeString().ToString();
            if ((int)nfi.Duration.TotalSeconds < nfi.Duration.TotalSeconds)
            DT_TotalTime = DT_TotalTime.AddSeconds(nfi.Duration.TotalSeconds+1);
            else
            DT_TotalTime = DT_TotalTime.AddSeconds(nfi.Duration.TotalSeconds);
            displayRecordingTime.Text = DT_RelativeTime.ToLongTimeString().ToString();
            displayTotalTime.Text = DT_TotalTime.ToLongTimeString().ToString();
            #endregion
            chartWave.Series.Clear();
            foreach (int idx in Enumerable.Range(0, leadConfigArrayList.Count))
            {
                Series ser = new Series();
                ser.ChartType = SeriesChartType.FastLine;
                ser.BorderDashStyle = ChartDashStyle.Solid;
                chartWave.Series.Add(ser);
            }
            if (nfi.HasVideo)
            {
                IMediaPlayerFactory factory = new MediaPlayerFactory();

                //创建播放器
                Player = factory.CreatePlayer<IVideoPlayer>();

                //将panelvideo句柄赋给播放器窗口句柄，在panelvideo上面播放视频
                Player.WindowHandle = panelVideo.Handle;

                //宽高比例
                Player.AspectRatio = AspectRatioMode.Mode2;
                if (nfi.HasVideo)
                {
                    //获得需要播放的视频的数据文件
                    media = factory.CreateMedia<IMediaFromFile>(nfi.VideoFullName);
                    Player.Open(media);
                    Debug.WriteLine(Player.IsSeekable);
                    Debug.WriteLine(Player.Length);

                    //初始阶段，不播放视频 
                    Player.Play();
                    Player.Time = (long)nfi.VideoOffset * 1000;
                }
                //方便该Form与视频弹出Form进行数据交换
                video = new VideoForm(this);
                Thread.Sleep(100);
                Player.Pause();              
            }
            else
            {
               // interfaceToolStripMenuItem.Enabled = false;
                this.panelVideo.Enabled = false;
                this.btn_hide.Enabled = false;
                this.vScroll.Location = new Point(this.chartWave.Location.X + this.chartWave.Width - this.vScroll.Width, this.vScroll.Location.Y);
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private void PagePrev()
        {
            //图表开头的秒数左移一页 -- by lxl
            CurrentSeconds -= (int)(Math.Floor(xMaximum));

            //图表开头的秒数不为负 -- by lxl
            if (CurrentSeconds <= 0)
                CurrentSeconds = 0;

            //重新加载与显示数据
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
            CurrentSeconds -= (int)(Math.Floor(xMaximum));
            if (CurrentSeconds <= 0)
                CurrentSeconds = 0;
            CurrentOffset = CurrentSeconds;
            LoadData(CurrentSeconds);
            ShowData();
            set_hsScrollBarValue_Next();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        private void PageNext()
        {
            //图表开头的秒数右移一页 -- by lxl
           CurrentSeconds += (int)(Math.Floor(xMaximum));

            //图表开头秒数不能大于总秒数减去水平滚动条的largechange+1，（即保证留且仅留两秒的空白提醒用户后面没有数据了） --by lxl
            if (CurrentSeconds > totalSeconds - hsProgress.LargeChange + 1)
                CurrentSeconds = totalSeconds - hsProgress.LargeChange + 1;

            //重新加载显示数据
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
            CurrentSeconds += (int)(Math.Floor(xMaximum));
            if (CurrentSeconds > totalSeconds - hsProgress.LargeChange + 1)
                CurrentSeconds = totalSeconds - hsProgress.LargeChange + 1;
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
            Player.Time = CurrentSeconds * 1000 + (long)nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;

            if(isPop==1)
            //videoForm视频同步变化
            video.PlayerVideo.Time = CurrentSeconds * 1000 + (long)nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
        }

        /// <summary>
        /// "上一页"按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            Pause();
            if (nfi.HasVideo)
            {
                if(isPop==1)
                video.PauseVideo();
            }
            PagePrev2();

            //点击了上一页，进度条，时间要变化
            Changed();

            ////视频同步变化
            //Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
          
            ////videoForm视频同步变化
            //video.Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            if (nfi.HasVideo)
                TimeChange();
            UpdateBtnEnable();
        }

        /// <summary>
        /// "下一页"按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            //暂停播放
            Pause();
            if (nfi.HasVideo)
            {
                if (isPop == 1)
                    video.PauseVideo();
            }
            PageNext2();

            //点击了下一页，进度条，时间要变化
            Changed();

            ////视频同步变化
            //Player.Time = CurrentSeconds * 1000+(long)_nfi.VideoOffset*1000+(long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset*1000;
            
            ////videoForm视频同步变化
            //video.Player.Time = CurrentSeconds * 1000 + (long)_nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
            if (nfi.HasVideo)
                TimeChange();
            
            //翻页后，更新按钮状态是否可用
            UpdateBtnEnable();
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
            if(CurrentOffset>=nfi.Duration.TotalSeconds)
            {
                Pause();
                _LastTime = null;            
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                if (isPop == 1)
                {
                    if (nfi.HasVideo)
                    {
                        video.PlayerVideo.Pause();
                        video.btn_play.Enabled = true;
                        video.btn_pause.Enabled = false;
                    }
                }
            }
            else
             {

                //播放本页结束，下一页处理
                if (chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= xMaximum)
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

                    //保证脑电采集时信号中断情况--by wsp
                    //测试使用
                  //  eventProperty.BeginningTime = new List<DateTime> { DateTime.Parse("2016-05-24  16:10:32"),DateTime.Parse("2016-05-24  16:25:40"), DateTime.Parse("2016-05-24  16:56:40") };
                  // eventProperty.RecordTime = new List<TimeSpan> { TimeSpan.Parse("00:00:00"),TimeSpan.Parse("00:00:10"), TimeSpan.Parse("00:00:20") };
                    GetAllDvalue();
                    for (int j = 0; j < timeSignal.GetLength(0); j++)
                    {
                        if ((int)(CurrentSeconds + this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset) == timeSignal[j])
                        {
                                displayStartTime.Text = eventProperty.BeginningTime[j].ToLongTimeString().ToString();
                                //TimeSpan dValue = eventProperty.BeginningTime[j] - nfi.StartDateTime;
                                // getDvalue = dValue.TotalSeconds - timeSignal[j];
                                getDvalue = eventProperty.BeginningTime[j].Hour * 3600 + eventProperty.BeginningTime[j].Minute * 60 + eventProperty.BeginningTime[j].Second - (nfi.StartDateTime.Hour * 3600 + nfi.StartDateTime.Minute * 60 + nfi.StartDateTime.Second) - timeSignal[j];
                                if (nfi.HasVideo)
                                {
                                    Player.Time = (long)(nfi.VideoOffset * 1000 + CurrentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000 + getDvalue * 1000);
                                   if(isPop==1)
                                    video.PlayerVideo.Time = Player.Time;
                                }
                        }
                        else
                        {
                            if (GetStarTotalSecond() + CurrentSeconds + getDvalue + this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= 86400)
                            {
                                getDvalue = -(GetStarTotalSecond() + CurrentSeconds + this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                            }
                            AllShowTime(GetStarTotalSecond(), CurrentSeconds + getDvalue, this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                        }
                    }           
                    ShowTime(CurrentSeconds, this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                }
            }
            //为了保证竖线画在正确的位置，所以相对时间和竖线最后需要特殊处理一下--by wsp
            if (CurrentOffset >= nfi.Duration.TotalSeconds)
             {
                 chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = nfi.Duration.TotalSeconds - CurrentSeconds;
                 DateTime dt_TotalTime1;
                 DateTime dt_Begin1;
                 dt_Begin1 = nfi.StartDateTime;
                 dt_TotalTime1 = DateTime.Parse("2016-05-23 00:00:00");

                //由于脑电数据总时间可能为小数，但是时间显示上作为整数显示，因此要特殊处理（不满一秒的也按照一秒来计算）
                 if ((int)nfi.Duration.TotalSeconds < nfi.Duration.TotalSeconds)
                 {
                     dt_TotalTime1 = dt_TotalTime1.AddSeconds(nfi.Duration.TotalSeconds + 1);
                     //dt_Begin1 = dt_Begin1.AddSeconds(nfi.Duration.TotalSeconds + 1);
                     AllShowTime(GetStarTotalSecond(), CurrentSeconds + getDvalue+1, this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                 }
                 else
                 {
                     dt_TotalTime1 = dt_TotalTime1.AddSeconds(nfi.Duration.TotalSeconds);
                     //dt_Begin1 = dt_Begin1.AddSeconds(nfi.Duration.TotalSeconds);
                     AllShowTime(GetStarTotalSecond(), CurrentSeconds + getDvalue, this.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
                 }
                 //displayStartTime.Text = dt_Begin1.ToLongTimeString().ToString();
                 displayRecordingTime.Text = dt_TotalTime1.ToLongTimeString().ToString();
             }
            UpdateBtnEnable();
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
            if (nfi.HasVideo)
            {

                //如果播放完毕后再次点击播放按钮，则重头开始播放
                if (CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= nfi.Duration.TotalSeconds)
                {
                    Clear();
                } 
                if (!Player.IsPlaying)
                    Player.Play();         
                    Player.Time = (long)(nfi.VideoOffset * 1000 + CurrentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000+getDvalue*1000);
            }
            else
            {
                timer.Enabled = true;
                if (CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= nfi.Duration.TotalSeconds)
                {
                    Clear();
                } 
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
            if (nfi.HasVideo)
            {
                if (Player.IsPlaying)
                {
                    Player.Pause();
                }
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
            if (isPop == 1)
            {
                if (nfi.HasVideo)
                {
                    if (CurrentSeconds == 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                        video.PlayerVideo.Time = (long)nfi.VideoOffset * 1000;
                    if (CurrentSeconds != 0 && chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset == 0)
                        video.PlayerVideo.Time = (long)(nfi.VideoOffset * 1000 + CurrentSeconds * 1000);
                    video.PlayVideo();
                    video.btn_pause.Enabled = true;
                    video.btn_play.Enabled = false;
                }
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
            if (isPop == 1)
            {
                if (nfi.HasVideo)
                {
                    video.PauseVideo();
                    video.btn_play.Enabled = true;
                    video.btn_pause.Enabled = false;
                }
            }
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
        //private void hsProgress_MouseCaptureChanged(object sender, EventArgs e)
        //    hsProgress.Value = currentSeconds;
        //}

        /// <summary>
        /// 水平滚动条鼠标状态改变事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSProgress_MouseCaptureChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Format("Scroll mouse cap changed {0}", e));
            //暂停播放
            Pause();
            if (isPop == 1)
            {
                if (nfi.HasVideo)
                    video.PauseVideo();
            }
            if (CurrentSeconds != hsProgress.Value)
            {
                CurrentSeconds = hsProgress.Value;
                Changed();
                //为保证拖动进度条之后，竖线位置保持不变--by wsp
                if (CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset < nfi.Duration.TotalSeconds)
                    CurrentOffset = CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset;
                else
                {
                    CurrentOffset = nfi.Duration.TotalSeconds;
                    chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = nfi.Duration.TotalSeconds - CurrentSeconds;
                }
                LoadData(CurrentSeconds);
                ShowData();
                //如果有视频，视频也要同步跟随--by wsp
                if (nfi.HasVideo)
                {
                    Player.Time = CurrentSeconds * 1000 + (long)nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
                    if(isPop==1)
                    video.PlayerVideo.Time = CurrentSeconds * 1000 + (long)nfi.VideoOffset * 1000 + (long)chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
                }
             }
            UpdateBtnEnable();
        }

        /// <summary>
        /// 更新翻页按钮是否可用
        /// -- by lxl
        /// </summary>
        private void UpdateBtnEnable()
        {
            if (CurrentSeconds <= 0)
            {
                btnPrev.Enabled = false;
                if (nfi.Duration.TotalSeconds <= pageWidthInMM / timeStandard)
                    btnNext.Enabled = false;
                else
                btnNext.Enabled = true;
                return;
            }
            else if (CurrentSeconds > totalSeconds - hsProgress.LargeChange)
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
            Set_PationInfoPanel(nfi.PatInfo);   //  --by zt

            this.PationInfoPanel.Visible = true;

            //实现病人属性菜单项勾选功能，当显示面板时显示勾选，当去除勾选时隐藏面板
            this.pationInfoToolStripMenuItem.Checked = !this.pationInfoToolStripMenuItem.Checked;
            if(this.pationInfoToolStripMenuItem.Checked == false)
            {
                this.PationInfoPanel.Hide();
            }

            //病人属性面板和检查属性面板需要同时存在
            //this.DetectionInfoPanel.Visible = false;
        }

        private void detectionInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set_DetectionInfoPanel(nfi.PatInfo);  //  --by zt

            //病人属性面板和检查属性面板需要同时存在
            //this.PationInfoPanel.Visible = false;

            this.DetectionInfoPanel.Visible = true;

            //实现检查属性菜单项勾选功能，当显示面板时显示勾选，当去除勾选时隐藏面板
            this.detectionInfoToolStripMenuItem.Checked = !this.detectionInfoToolStripMenuItem.Checked;
            if(this.detectionInfoToolStripMenuItem.Checked ==false)
            {
                this.DetectionInfoPanel.Hide();
            }
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
            if (nfi.HasVideo)
            {
                Player.Time = CurrentSeconds * 1000;
                if (isPop == 1)
                {
                    video.PlayerVideo.Time = CurrentSeconds * 1000;
                    video.btn_play.Enabled = true;
                }
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

        #region 灵敏度 -- by lxl
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

            setSensitivity(int.Parse(num));
            //根据修改的值再重新显示图表
            ShowData();
        }

        private void setSensitivity(int value)
        {
            sensitivity = value;
            this.toolStripStatusLabel_sensitivity.Text = value + "μv/cm";
        }
        #endregion

        #region 时间基准 -- by lxl
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

            //设置新的时间灵敏度
            setTimeStandard(int.Parse(num));

            //修改X轴的最大值
            SetAxisXMaximum(pageWidthInMM / timeStandard);

            //让画图函数重新计算一下当前的_xMaximum是多少，（因为若面板是显示的状态则只能再根据坐标与像素点来计算X轴显示的最大值）
            isChangingBoardShow = true; 
            
            //重新加载数据与显示数据
            if (chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset > pageWidthInMM / timeStandard)
            {
                CurrentSeconds += (int)(chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset) - 1;
                chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset + 1 - (int)(chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            }
                LoadData(CurrentSeconds);
                hsProgress.Value = CurrentSeconds;
                ShowData();
                UpdateBtnEnable();
        }

        private void setTimeStandard(int value)
        {
            timeStandard = value;
            this.toolStripStatusLabel_timeStandard.Text = value + "mm/sec";
        }
        #endregion

        #region 设置图表范围 -- by lxl

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

        #endregion

        #region 图表校准 -- by lxl

        /// <summary>
        /// Y轴校准点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibrateYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calibForm == null || calibForm.IsDisposed)
                calibForm = new CalibrateYForm(this, pixelPerMM * 10);
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
            if (height == 0)
                return;
            //一毫米多少像素点
            pixelPerMM = height / 10D;

            //表格高度有多少毫米
            pageHeightInMM = pageHeightInPixel / pixelPerMM;

            //一格应该有多少毫米
            mmPerYGrid = pageHeightInMM / 20;
            commonDataPool.MMPerYGrid = mmPerYGrid;

            ShowData();
        }
        /// <summary>
        /// X轴校准点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibrateXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calibXForm == null || calibXForm.IsDisposed)
                calibXForm = new CalibrateXForm(this, pixelPerMM * 10);
            calibXForm.Show();
            calibXForm.BringToFront();
        }
        /// <summary>
        /// X轴校准函数
        /// </summary>
        /// <param name="width">一厘米多少个像素点</param>
        public void CalibrateX(double width)
        {
            if (width == 0)
                return;
            //一毫米多少像素点
            pixelPerMM = width / 10D;
            commonDataPool.PixelPerMM = pixelPerMM;

            //更新表格的宽度
            UpdatePageWidthInMM();

            //由于修改了X轴最大值，故重新加载、重新显示数据
            LoadData(CurrentSeconds);
            ShowData();
        }

        /// <summary>
        /// 更新表格的宽度
        /// -- by lxl
        /// </summary>
        private void UpdatePageWidthInMM()
        {
            //表格高度有多少毫米
            pageWidthInMM = pageWidthInPixel / pixelPerMM;

            //根据表格宽度设置X轴的最大值
            SetAxisXMaximum(pageWidthInMM / timeStandard);
        }

        #endregion

        /// <summary>
        /// --by wsp
        /// 进度条，PagePrev,PageNext变化时，对应的时间也要发生变化；
        /// </summary>
        private void Changed()
        {
        //    eventProperty.BeginningTime = new List<DateTime> { DateTime.Parse("2016-05-24  16:10:32"), DateTime.Parse("2016-05-24  16:56:40"), DateTime.Parse("2016-05-24  23:59:40") };
       //     eventProperty.RecordTime = new List<TimeSpan> { TimeSpan.Parse("00:00:00"), TimeSpan.Parse("00:00:10"), TimeSpan.Parse("00:00:20") };
            GetAllDvalue();
            for (int i = timeSignal.GetLength(0) - 1; i >0 ; i--)
            {
                if (CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= timeSignal[i])
                {
                    getDvalue = GettimeSignalNumber(i) - GetStarTotalSecond() - timeSignal[i];
                    if (nfi.HasVideo)
                    {
                        Player.Play();
                        Player.Time = (long)(nfi.VideoOffset * 1000 + CurrentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000 + getDvalue * 1000);
                        Thread.Sleep(100);
                        Player.Pause();
                        if (isPop == 1)
                        {
                            video.PlayerVideo.Play();
                            video.PlayerVideo.Time = (long)(nfi.VideoOffset * 1000 + CurrentSeconds * 1000 + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000 + getDvalue * 1000);
                            Thread.Sleep(100);
                            video.PlayerVideo.Pause();
                        }
                    }
                    break;
                }
                else if (CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= timeSignal[i - 1])
                {
                    getDvalue = GettimeSignalNumber(i - 1) - GetStarTotalSecond() - timeSignal[i-1];
                    break;
                }
            }
            DT = nfi.StartDateTime;
            DT_RelativeTime = DateTime.Parse("2016-05-23  00:00:00");
            DT = DT.AddSeconds(CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
            if (GetStarTotalSecond() + CurrentSeconds + getDvalue + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset >= 86400)
            {
                getDvalue -=86400;
            }
            AllShowTime(GetStarTotalSecond(), CurrentSeconds+getDvalue, chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
      //      displayStartTime.Text = DT.ToLongTimeString().ToString();
            DT_RelativeTime = DT_RelativeTime.AddSeconds(CurrentSeconds + chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
      //      displayRecordingTime.Text = DT_RelativeTime.ToLongTimeString().ToString();
            ShowTime(CurrentSeconds, chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset);
        }

        /// <summary>
        /// 初始化导联选择选项  --by zt
        /// </summary>
        private void InitLeadItems() 
        {
            //清除
            this.leadChooseToolStripMenuItem.DropDownItems.Clear();
            System.Windows.Forms.ToolStripMenuItem item;//= new ToolStripMenuItem();
            currentLeadConfigName = "默认导联配置";
            //初始化导联选择选项
            foreach (string name in leadConfigList.Keys) 
            {
                item = new ToolStripMenuItem();
                item.Name = "leadNameMenuItem_" + name;
                item.Size = new Size(140, 22);
                item.Text = name;
                item.Click += new EventHandler(this.LeadNameMenuItem_Click);
                if (name == currentLeadConfigName)
                    item.Checked = true;
                this.leadChooseToolStripMenuItem.DropDownItems.Add(item);
            }

            //当前导联配置
            leadConfigArrayList = (ArrayList)leadConfigList[currentLeadConfigName];

            //
            signalNum = (leadConfigArrayList.Count <= signalNum) ? leadConfigArrayList.Count : signalNum;
            
            


        }

        /// <summary>
        /// 导联菜单选项点击事件
        /// -- by zt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeadNameMenuItem_Click(object sender, EventArgs e)
        {
            //string num = sender.ToString().Substring(0, sender.ToString().Length - 6);
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            //将位选中的checked改为false，把选中的item的checked改为true
            foreach (ToolStripMenuItem i in this.leadChooseToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;

            //当前导联名称即选择导联名称
            currentLeadConfigName = item.Text;

            //选择导联配置
            leadConfigArrayList = (ArrayList)leadConfigList[currentLeadConfigName];

            //label改变
            this.labelPanel.Invalidate();

            //通道数目改变
            SetSignalNum((leadConfigArrayList.Count <= signalNum) ? leadConfigArrayList.Count : signalNum);
            SetSignalNumArray(signalNum);
            InitSignalNumMenuItems();

            //重新显示数据
            ShowData();

            //timeStandard = int.Parse(num);

            ////修改X轴的最大值
            //SetAxisXMaximum(pageWidthInMM / timeStandard);

            ////让画图函数重新计算一下当前的_xMaximum是多少，（因为若面板是显示的状态则只能再根据坐标与像素点来计算X轴显示的最大值）
            //isChangingBoardShow = true;

            ////重新加载数据与显示数据
            //LoadData(CurrentSeconds);
            //ShowData();
        }


       /// <summary>
       /// 视频加速
       /// --by wsp
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btn_accelerate_Click(object sender, EventArgs e)
        {
            if (nfi.HasVideo)
            {
                //倍速过快时，会使得视频花屏，因此对倍速做了限制
                if (Speed <= 4)
                {
                    btn_accelerate.Enabled = true;
                    btn_decelerate.Enabled = true;
                    Player.PlaybackRate = Player.PlaybackRate * 2;

                    //设置Speed是为了使得脑电数据也要加倍
                    Speed = Player.PlaybackRate;

                    if (isPop == 1)
                    {
                        //videoForm的倍速要与该Form倍速保持一致
                        video.PlayerVideo.PlaybackRate = Player.PlaybackRate;
                        video.btn_accelerate.Enabled = true;
                    }
                }
                else
                {
                    //加速到最大后，加速按钮不可用，减速按钮可用
                    btn_accelerate.Enabled = false;
                    btn_decelerate.Enabled = true;
                    if (isPop == 1)
                    {
                        video.btn_accelerate.Enabled = false;
                        video.btn_decelerate.Enabled = true;
                    }
                }
            }
            else
            {
                if (Speed <= 4)
                {
                    btn_accelerate.Enabled = true;
                    btn_decelerate.Enabled = true;
                    Speed *= 2;
                }
                else
                {
                    btn_accelerate.Enabled = false;
                    btn_decelerate.Enabled = true;
                }
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
            if (nfi.HasVideo)
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
                    if (isPop == 1)
                    {
                        video.PlayerVideo.PlaybackRate = Player.PlaybackRate;
                        video.btn_decelerate.Enabled = true;
                        video.btn_accelerate.Enabled = true;
                    }
                }
                else
                {
                    //加速到最大后，加速按钮不可用，减速按钮可用
                    btn_decelerate.Enabled = false;
                    btn_accelerate.Enabled = true;
                    if (isPop == 1)
                    {
                        video.btn_decelerate.Enabled = false;
                        video.btn_accelerate.Enabled = true;
                    }
                }
            }
            else
            {
                if (Speed >= 0.5)
                {
                    //倍速过慢时，视频也会花屏，因此对最低倍速也做了限制
                    btn_accelerate.Enabled = true;
                    btn_decelerate.Enabled = true;
                    Speed /= 2;
                }
                else
                {
                    //加速到最大后，加速按钮不可用，减速按钮可用
                    btn_decelerate.Enabled = false;
                    btn_accelerate.Enabled = true;
                }

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
                toolTip1.SetToolTip(btn_hide, "显示");
                panelVideo.Visible = false;
                if (isIdent == 0)
                {
                    video.Show();
                    Thread.Sleep(100);
                }
                else
                    video.Show();
                if (Player.IsPlaying == true)
                    video.PlayVideo();
                else
                {
                    video.PauseVideo();
                }
                isIdent++;
                if (isIdent == 1)
                isPop = 1;
            }
            else
            {
                toolTip1.SetToolTip(btn_hide, "隐藏");
                video.Hide();
                panelVideo.Visible = true;
            }
        }      

        private void GetHsprogressMax()
        {
            SecPerPage();
            hsProgress.Maximum = maxPage;
        }

        /// <summary>
        /// 数据有多少页
        /// </summary>
        private void SecPerPage()
        {
            maxPage = (numberOfSamples + ((int)(10 * 30D / Convert.ToDouble(timeStandard)) * sampleRate) - 1) / ((int)(10 * 30D / Convert.ToDouble(timeStandard)) * sampleRate);  // 修改 --by zt
        }

        #region 重绘及画图相关 -- by lxl
        /// <summary>
        /// chart的重绘函数
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartPaint(object sender, PaintEventArgs e)
        {
            //计算图表的宽与高,因为PixelPositionToValue与ValueToPixelPosition只能在重绘函数中调用          -- by lxl
            if (pageWidthInPixel == 0)                            
            {
                pageWidthInPixel = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisX.Maximum) - this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(0);
                pageWidthInMM = pageWidthInPixel / pixelPerMM;
                SetAxisXMaximum(pageWidthInMM / timeStandard);
                LoadData(CurrentSeconds);
                ShowData();
                pageHeightInPixel = Math.Abs(this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(this.chartWave.ChartAreas[0].AxisY.Maximum) - this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0));
                pageHeightInMM = pageHeightInPixel / pixelPerMM;
            }

            //画病人事件-- by lxl
            DrawEvents(e.Graphics);

            //计算面板显示与否的宽度,因为PixelPositionToValue与ValueToPixelPosition只能在重绘函数中调用    -- by lxl 
            if (isChangingBoardShow)                       
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

            //若正在添加事件，则画一条和所选事件颜色相同的线跟着鼠标走  --by lxl
            if (isAddingEvent)
            {
                if (isAddingPreDefineEvent)
                    e.Graphics.DrawLine(new Pen(PreDefineEvent.PreDefineEventColorArray[preDefineEventIndex]), new Point(Control.MousePosition.X - this.chartWave.Location.X, 0), new Point(Control.MousePosition.X - this.chartWave.Location.X, this.chartWave.Height));
                else
                    e.Graphics.DrawLine(new Pen(CustomEvent.CustomEventColor[addingEventColorIndex]), new Point(Control.MousePosition.X - this.chartWave.Location.X, 0), new Point(Control.MousePosition.X - this.chartWave.Location.X, this.chartWave.Height));
                
                //防止鼠标位置滑倒图表外面去
                if (Control.MousePosition.X > this.chartWave.Location.X && Control.MousePosition.X < this.boardPanel.Location.X)
                    mouseValueNow = (this.chartWave.ChartAreas[0].AxisX.PixelPositionToValue(Control.MousePosition.X - this.chartWave.Location.X) + currentSeconds) * sampleRate;
            }

            //画图表上的秒数,time1Pos为当前图表中前二分之一秒的位置
            double time1Pos = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition((int)Math.Floor(xMaximum) / 2);
            DrawSeconds(time1Pos,e.Graphics);
        }

        /// <summary>
        /// 画图表上的秒数
        /// -- by lxl
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="g"></param>
        private void DrawSeconds(double pos,Graphics g)
        {
            //若多余两秒则只每隔一半画一下    -- by lxl
            if (xMaximum >= 2)
            {
                g.DrawString((CurrentSeconds + (int)Math.Floor(xMaximum) / 2).ToString(), new System.Drawing.Font("黑体", 10, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF((int)pos, 25, 80, 15));
                g.DrawString((CurrentSeconds + (int)Math.Floor(xMaximum) / 2 * 2).ToString(), new System.Drawing.Font("黑体", 10, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF((int)pos * 2, 25, 80, 15));
            }
            else //若小于两秒则图表中只画一秒 -- by lxl
            {
                g.DrawString((CurrentSeconds + 1).ToString(), new System.Drawing.Font("黑体", 10, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF((int)(this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(1)), 25, 80, 15));
            }
        }

        #region 画事件 -- by lxl
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
            SolidBrush strBrush = new SolidBrush(Color.Black);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Bold);

            //画自定义和预定义事件
            DrawPreDefineEvents(g, dotPen, strBrush, strFont);
            DrawCustomEvents(g, dotPen, strBrush, strFont);
        }

        /// <summary>
        /// 画预定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="pen">画笔</param>
        /// <param name="strBrush">字体的画刷</param>
        /// <param name="strFont">字体</param>
        private void DrawPreDefineEvents(Graphics g,Pen pen,SolidBrush strBrush, Font strFont)
        {
            if(preDefineEventsList == null)
            {
                return;
            }
            //事件该画的位置
            double drawPosition;

            List<PreDefineEvent> drawList = new List<PreDefineEvent>(GetPreDefineEventList());
            drawList.AddRange(preDefineEventsListToBeAdd);

            //画预定义事件                   
            foreach (PreDefineEvent p in drawList)
            {
                //只画当前页面能显示的事件
                if (p.EventPosition / sampleRate < currentSeconds)
                    continue;
                if (p.EventPosition / sampleRate > CurrentSeconds + xMaximum)
                    break;
                pen.Color = p.EventColor; 
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(Convert.ToDouble(p.EventPosition) / sampleRate - currentSeconds);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, p.EventColor)), new Rectangle((int)drawPosition - 40, 5, 80, 15));
                g.DrawString(p.EventName, strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15));
                g.DrawLine(pen, new Point((int)drawPosition, 5), new Point((int)drawPosition, (int)this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0)));
            }
        }

        /// <summary>
        /// 画自定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="pen">画笔</param>
        /// <param name="strBrush">字体的画刷</param>
        /// <param name="strFont">字体</param>
        private void DrawCustomEvents(Graphics g,Pen pen,SolidBrush strBrush, Font strFont)
        {
            if (customEventList == null)
            {
                return;
            }

            //事件该画的位置
            double drawPosition;

            //画自定义事件                  
            foreach (CustomEvent p in customEventList)
            {
                //只画当前页面能显示的事件
                if (p.EventPosition / sampleRate < CurrentSeconds)
                    continue;
                if (p.EventPosition / sampleRate > currentSeconds + xMaximum)
                    continue;
                pen.Color = CustomEvent.CustomEventColor[p.EventColorIndex];
                drawPosition = this.chartWave.ChartAreas[0].AxisX.ValueToPixelPosition(Convert.ToDouble(p.EventPosition) / sampleRate - currentSeconds);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, CustomEvent.CustomEventColor[p.EventColorIndex])), new Rectangle((int)drawPosition - 40, 5, 80, 15));
                g.DrawString(p.EventName, strFont, strBrush, new RectangleF((int)drawPosition - 30, 5, 60, 15));
                g.DrawLine(pen, new Point((int)drawPosition, 5), new Point((int)drawPosition, (int)this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(0)));
            }
        }

        #endregion

        /// <summary>
        /// labelPanel的重绘函数,若要更新label则需调用labelPanel.Invalidate()函数
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawLabelPanel(object sender, PaintEventArgs e)
        {
            //画图所需变量的定义
            SolidBrush strBrush = new SolidBrush(Color.Black);
            Font strFont = new System.Drawing.Font("黑体", 10, FontStyle.Regular);
            double topSigPos = this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(2000D - 2000D / signalNum / 2);
            double intervalPos = this.chartWave.ChartAreas[0].AxisY.ValueToPixelPosition(2000D - 2000D / signalNum / 2 * 3) - topSigPos;

            //根据信道所在的位置分别画25通道的label
            foreach (int i in Enumerable.Range(currentTopSignal, signalNum))
            {
                e.Graphics.DrawString(leadConfigArrayList[i].ToString(), strFont, strBrush, new RectangleF(0, (int)(topSigPos + (i - currentTopSignal) * intervalPos), this.labelPanel.Width, 15));
            }
        }
        #endregion
       
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
            if (nfi.HasVideo)
            Player.Time =(long)nfi.VideoOffset*1000;
            DT = nfi.StartDateTime;
            hsProgress.Value = 0;
            LoadData(CurrentSeconds);
            ShowData();
        }

        #region 修改通道数目部分 -- by lxl
        /// <summary>
        /// 通道数目显示点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            //先将所有的item的checked设置为false,再将选中的设置为true
            foreach (ToolStripMenuItem i in this.signalToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }
            item.Checked = true;

            //活得所点击的所显示的显示信道数量，并修改(获得到的为text)
            SetSignalNum(int.Parse(sender.ToString()));

            //重新显示数据
            ShowData();
        }

        /// <summary>
        /// 设置当前显示的通道数目是多少，并进行相对应的修改
        /// PS:由于会修改到图表和label，切记一定要在调用完InitializeComponent（）后调用这个函数！ 不然会报错！
        /// PPS:调用该函数后需要调用ShowData()才能更新显示的数据
        /// -- by lxl
        /// </summary>
        /// <param name="num"></param>
        private void SetSignalNum(int num)
        {
            signalNum = num;

            //修改chart的样式
            this.chartWave.ChartAreas[0].AxisY.MajorGrid.Interval = 2000D / signalNum;
            this.chartWave.ChartAreas[0].AxisY.MajorTickMark.Interval = 2000D / signalNum;

            //保证最上方的currentTopSignal不大于应该有的20
            if (currentTopSignal + signalNum > 20)
                currentTopSignal = 20 - signalNum;

            UpdateVScrollVisibleStatus();

            //设置数值滚动条的可变范围
            this.vScroll.LargeChange = signalNum + 1;

            //重画左边的labelPanel
            this.labelPanel.Invalidate();

        }

        /// <summary>
        /// 更新当前右边垂直滚动条是否可用
        /// -- by lxl
        /// </summary>
        private void UpdateVScrollVisibleStatus()
        {
            //根据设置的显示数量设置右边数值滚动条是否可见
            if (signalNum < leadConfigArrayList.Count)
                SetVScrollVisible(true);
            else
                SetVScrollVisible(false);
        }

        /// <summary>
        /// 设置竖直滚动条是否可用
        /// -- by lxl
        /// </summary>
        /// <param name="flag"></param>
        private void SetVScrollVisible(bool flag)
        {
            this.vScroll.Visible = flag;
            if (flag)
            {
                UpdateVScrollLocation();
            }
        }

        /// <summary>
        /// 更新竖直滚动条的位置
        /// -- by lxl
        /// </summary>
        private void UpdateVScrollLocation()
        {

            //修改竖直滚动条的位置
            if (isBoardShow)
                this.vScroll.Location = new Point(this.boardPanel.Location.X - this.vScroll.Width, this.vScroll.Location.Y);
            else
                this.vScroll.Location = new Point(this.chartWave.Location.X + this.chartWave.Width - this.vScroll.Width, this.vScroll.Location.Y);
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

        #endregion

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
                if (myPreDefineEventFormEventForm == null || myPreDefineEventFormEventForm.IsDisposed)
                    myPreDefineEventFormEventForm = new PredefineEventsForm(this);
                myPreDefineEventFormEventForm.Show();
                myPreDefineEventFormEventForm.BringToFront();
                myPreDefineEventFormEventForm.InitList();
            }
            else//若名字以pr开头则是custom（自定义）事件
            {
                if (myCustomEventForm == null || myCustomEventForm.IsDisposed)
                    myCustomEventForm = new CustomEventForm(this);
                myCustomEventForm.Show();
                myCustomEventForm.BringToFront();
                myCustomEventForm.InitList();
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
            //取反isBoardShow，更改当前面板显示状态
            isBoardShow = !isBoardShow;

            this.boardPanel.Visible = isBoardShow;
            this.boardToolStripMenuItem.Checked = isBoardShow;

            //重新计算X轴显示的最大值
            isChangingBoardShow = true;

            //修改竖直滚动条的位置
            UpdateVScrollLocation();

            //this.chartWave.Invalidate();
        }

        /// <summary>
        /// 根据事件点的位置得到事件发生的时间
        /// -- by lxl
        /// </summary>
        /// <param name="pointPos">点的位置</param>
        /// <returns></returns>
        public DateTime GetEventTime(int pointPos)
        {
            int i;
            for (i = 0; i < eventProperty.RecordQuantity.Count(); i++)
            {
                if (pointPos <= eventProperty.RecordQuantity[i])
                    break;
            }
            return eventProperty.BeginningTime[i - 1].AddSeconds((pointPos - eventProperty.RecordQuantity[i - 1]) / sampleRate);
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
                        //将事件添加进列表并排序
                        FileStream fs = new FileStream(this.nfi.NedFileName.Split('.')[0] + ".NAT", FileMode.Open, FileAccess.Read); 
                        AddPreDefineEvents(preDefineEventIndex, Convert.ToUInt16(mouseValueNow), (int)fs.Length);

                        //关闭文件，释放文件流
                        fs.Close();
                        fs.Dispose();

                        //事件添加后更新对应的列表
                        UpdateEventsListView(true);
                    }
                    else
                    {
                        //将事件添加进列表并排序
                        AddCustomEvents(addedEventName, Convert.ToUInt16(mouseValueNow), addingEventColorIndex);

                        //事件添加后更新对应的列表
                        UpdateEventsListView(false);
                    }
                }

                //添加完数据后将isAddingEvent变成false
                isAddingEvent = !isAddingEvent;
                this.chartWave.Invalidate();
            }
        }

        #region 事件处理区 -- by lxl
        /// <summary>
        /// 删除指定索引的事件，根据索引值flag判定是删除预定义事件还是自定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="flag">是否是预定义事件</param>
        /// <param name="index">索引</param>
        /// <param name="id">预定义事件的ID,当FLAG为true时需要</param>
        public void RemoveEvent(bool flag, int index,int id=0)
        {
            if (flag)
            {
                //判断ID必须要赋值
                if (id == 0)
                    throw new Exception("删除预定义事件ID不能为0，请设置要删除的预定义事件的ID");

                //判定预定义事件列表最少有一个事件可供删除
                if (preDefineEventsList.Count() <= 0 && preDefineEventsListToBeAdd.Count <= 0)
                    return;

                //positionInFileList.Add(preDefineEventsList[index].PosInFile);
                //preDefineEventsList.RemoveAt(index);
                foreach (PreDefineEvent p in preDefineEventsList)
                {
                    if (p.EventID == id)
                    {
                        positionInFileOfDeletedFileList.Add(p.PosInFile);
                        preDefineEventsList.Remove(p);

                        //事件删除后更新列表
                        UpdateEventsListView(flag);

                        this.chartWave.Invalidate();
                        return;
                    }
                }

                foreach (PreDefineEvent p in preDefineEventsListToBeAdd)
                {
                    if (p.EventID == id)
                    {
                        preDefineEventsListToBeAdd.Remove(p);

                        //事件删除后更新列表
                        UpdateEventsListView(flag);

                        this.chartWave.Invalidate();
                        return;
                    }
                }
            }
            else
            {
                //判定自定义事件列表最少有一个事件可供删除
                if (customEventList.Count() <= 0)
                    return;

                customEventList.RemoveAt(index);

                //事件删除后更新列表
                UpdateEventsListView(flag);
            }
            this.chartWave.Invalidate();
        }

        /// <summary>
        /// 更新操作过后的事件里的列表
        /// </summary>
        /// <param name="flag">操作的是否预定义事件</param>
        private void UpdateEventsListView(bool flag)
        {
            if (flag)
            {
                //更新事件列表
                if (myPreDefineEventFormEventForm != null && !myPreDefineEventFormEventForm.IsDisposed)
                    myPreDefineEventFormEventForm.InitList();
                UpdateLVPreDefineEvents();
            }
            else
            {
                //事件删除后更新添加事件的form里的列表
                if (myCustomEventForm != null && !myCustomEventForm.IsDisposed)
                    myCustomEventForm.InitList();
                UpdateLVCustomEvents();
            }
        }

        #region 事件list操作 -- by lxl
        /// <summary>
        /// 往列表中添加预定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="index">预定义事件的索引</param>
        /// <param name="pointPos">预定义事件点的位置</param>
        /// <param name="posInFile">预定义事件在文件中的位置</param>
        private void AddPreDefineEvents(int index,ushort pointPos,int posInFile)
        {
            //将事件添加进列表
            //preDefineEventsList.Add(new PreDefineEvent(index, pointPos, posInFile,preDefineEventsList.Count + 1));
            preDefineEventsListToBeAdd.Add(new PreDefineEvent(index, pointPos, 0, preDefineEventsList.Count + preDefineEventsListToBeAdd.Count + 1));
        }

        /// <summary>
        /// 往列表中添加自定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="name">自定义事件的名字</param>
        /// <param name="pointPos">自定义事件点的位置</param>
        /// <param name="colorIndex">自定义事件颜色索引</param>
        private void AddCustomEvents(string name,ushort pointPos,int colorIndex)
        {
            //将事件添加进列表并排序
            customEventList.Add(new CustomEvent(name, pointPos, colorIndex));
            customEventList.Sort(new CustomEventComparer());
        }

        /// <summary>
        /// 获得排序后的预定义事件列表,返回的列表是经过排序过的
        /// -- by lxl
        /// </summary>
        /// <returns>预定义事件列表</returns>
        public List<PreDefineEvent> GetSortedPreDefineEventList()
        {
            List<PreDefineEvent> returnList = new List<PreDefineEvent>(preDefineEventsList);
            returnList.AddRange(preDefineEventsListToBeAdd);
            returnList.Sort(new PreDefineEventComparer());
            return returnList;
        }

        /// <summary>
        /// 获得未排序的预定义事件列表
        /// -- by lxl
        /// </summary>
        /// <returns></returns>
        public List<PreDefineEvent> GetPreDefineEventList()
        {
            return preDefineEventsList;
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
        #endregion


        /// <summary>
        /// 开始添加预定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="index">预定义事件的事件编号</param>
        public void StartAddEvents(int index)
        {

            //保存添加的预定义事件的编号
            this.preDefineEventIndex = index;

            //设置当前为正在添加预定义事件
            isAddingPreDefineEvent = true;

            //设置当前开始添加事件
            isAddingEvent = true;
        }
        /// <summary>
        /// 开始添加自定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="colorIndex">事件颜色索引</param>
        /// <param name="name">事件名称</param>
        public void StartAddEvents(int colorIndex, string name)
        {
            //设置添加事件过程中所要画的直线的颜色
            addingEventColorIndex = colorIndex;

            //设置现在是添加自定义事件
            isAddingPreDefineEvent = false;

            //设置当前开始添加事件
            isAddingEvent = true;

            //根据现在是在添加什么事件来确定事件名称
            addedEventName = name;
        }

        #region 事件listView -- by lxl

        /// <summary>
        /// 更新预定义事件列表
        /// -- by lxl
        /// </summary>
        private void UpdateLVPreDefineEvents()
        {
            //事件显示编号
            int index = 1;

            //开始更新列表
            lvPreDefineEvents.BeginUpdate();

            //更新列表前先清空列表内容
            lvPreDefineEvents.Items.Clear();

            if (preDefineEventsList != null)
            {
                //根将从Playbackform中读取的内容插入到列表中
                foreach (PreDefineEvent p in GetSortedPreDefineEventList())
                {
                    ListViewItem li = new ListViewItem(index.ToString());


                    //允许更改item的颜色
                    li.UseItemStyleForSubItems = false;

                    //初始化listview的内容项
                    li.Name = p.EventID.ToString();
                    li.SubItems.Add(p.EventName);
                    li.SubItems.Add(GetEventTime(p.EventPosition).ToLongTimeString());
                    li.SubItems.Add("");

                    //序号递增
                    index++;
                    lvPreDefineEvents.Items.Add(li);

                    lvPreDefineEvents.Items[index - 2].SubItems[3].BackColor = p.EventColor;
                }
            }

            lvPreDefineEvents.EndUpdate();
        }

        /// <summary>
        /// 更新自定义事件
        /// -- by lxl
        /// </summary>
        private void UpdateLVCustomEvents()
        {
            //事件显示的编号
            int index = 1;

            //开始更新列表
            lvCustomEvents.BeginUpdate();

            //先把列表清空
            lvCustomEvents.Items.Clear();

            if (customEventList != null)
            {
                //从playbackform中读取事件列表，然后将事件添加到列表中
                foreach (CustomEvent p in customEventList)
                {
                    ListViewItem li = new ListViewItem(index.ToString());

                    //允许更改item的颜色
                    li.UseItemStyleForSubItems = false;

                    li.SubItems.Add(p.EventName);
                    li.SubItems.Add(GetEventTime(p.EventPosition).ToLongTimeString());
                    li.SubItems.Add("");
                    index++;
                    lvCustomEvents.Items.Add(li);
                    this.lvCustomEvents.Items[index - 2].SubItems[3].BackColor = CustomEvent.CustomEventColor[p.EventColorIndex];
                    this.lvCustomEvents.Items[index - 2].SubItems[3].Name = p.EventColorIndex.ToString();
                }
            }

            //结束更新列表
            lvCustomEvents.EndUpdate();
        }

        #endregion

        /// <summary>
        /// 编辑事件指定位置的事件
        /// -- by lxl
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="name">名称</param>
        /// <param name="colorIndex">颜色索引号</param>
        public void EditCustomEvent(int index, string name, int colorIndex)
        {
            //将新的编辑后的事件保存
            this.customEventList[index].EventName = name;
            this.customEventList[index].EventColorIndex = colorIndex;

            UpdateEventsListView(false);

            //重画图表
            this.chartWave.Invalidate();
        }

        #endregion

        /// <summary>
        /// 当回放Form关闭时，弹出视频Form也要关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaybackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(nfi.HasVideo)
                video.Close();
            this.SaveDataToCommonDataPool();
            controller.PlaybackQuit();

            //将事件写入文件中，先写在此处测试，没问题后移到controller中
            SavePreDefineEventsToFile(nfi.NedFileName.Split('.')[0]+".NAT");
            SaveCustomeEventsToFile(nfi.NedFileName.Split('.')[0] + ".ent");
        }

        /// <summary>
        /// 把数据保存在commondatapool中
        /// -- by lxl
        /// </summary>
        private void SaveDataToCommonDataPool()
        {
            commonDataPool.Sensitivity = sensitivity;
            commonDataPool.TimeStandard = timeStandard;
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
            return nfi.StartDateTime.Hour*3600+nfi.StartDateTime.Minute*60+nfi.StartDateTime.Second;
        }
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

        #region 事件存储 -- by lxl
        /// <summary>
        /// 将预定义事件列表存到文件中
        /// -- by lxl
        /// </summary>
        /// <param name="filename">文件名路径</param>
        private int SavePreDefineEventsToFile(string filename)
        {
            try
            {
                //打开文件流
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);

                ////读取每个事件，并把每个事件写入其在文件中的位置
                foreach (PreDefineEvent p in preDefineEventsList)
                {
                    //建立一个字节BUFFER，将要数据按格式转换成BYTE放入BUFFER中
                    byte[] byteBuf = PreDefineEventsToHex(p);
                    if (byteBuf == null)
                    {
                        MessageBox.Show("解析失败:数据转换成BYTE出错");
                        return 0;
                    }
                    bw.Seek(p.PosInFile, SeekOrigin.Begin);
                    bw.Write(byteBuf);
                }

                //读取每个添加的事件，并把事件写入文件末尾
                foreach (PreDefineEvent p in preDefineEventsListToBeAdd)
                {
                    byte[] byteBuf = PreDefineEventsToHex(p);
                    bw.Seek(0, SeekOrigin.End);
                    bw.Write(byteBuf);
                }

                byte[] allBytes = new byte[fs.Length - nfi.NatInfo.CfgOff + 1];
                fs.Read(allBytes, 0, (int)(fs.Length - nfi.NatInfo.CfgOff + 1));


                //positionInFileOfDeletedFileList.Sort();
                //for (int i = 0; i < positionInFileOfDeletedFileList.Count; i++)
                //{
                //    bw.Seek(positionInFileOfDeletedFileList[i] - nfi.NatInfo.CfgOff, SeekOrigin.Begin);
                //    byte[] writeByte=allBytes.Skip(positionInFileOfDeletedFileList[i]-1).Take(5).ToArray();
                //    bw.Write(allBytes)
                //}

                //读取每个删除的事件，并把其位置置为0
                foreach (int p in positionInFileOfDeletedFileList)
                {
                    bw.Seek(p, SeekOrigin.Begin);
                    bw.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
                }
                bw.Close();
                fs.Close();

                //返回1表示成功
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("预定义事件写入文件错误" + e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 将预定义事件转换成BYTE数组
        /// -- by lxl
        /// </summary>
        /// <param name="pdEvent"></param>
        /// <returns></returns>
        private byte[] PreDefineEventsToHex(PreDefineEvent pdEvent)
        {
            //建立一个BYTE数组用于存储事件信息
            byte[] returnBytes;
            returnBytes = new byte[8];

            //按照格式构造BYTE数组
                returnBytes[0] = 0x45;
                returnBytes[1] = Convert.ToByte(pdEvent.EventNameIndex);
                returnBytes[2] = returnBytes[3] = 0x00;
                returnBytes[4] = BitConverter.GetBytes(pdEvent.EventPosition)[0];
                returnBytes[5] = BitConverter.GetBytes(pdEvent.EventPosition)[1];
                returnBytes[6] = returnBytes[7] = 0x00;

            return returnBytes;
        }

        /// <summary>
        /// 将自定义事件列表保存到文件中
        /// -- by lxl
        /// </summary>
        /// <param name="filename"></param>
        private int SaveCustomeEventsToFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                //list没有内容则只删除文件
                if (customEventList.Count <= 0)
                    return 0;

                //打开文件流
                FileStream fs = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);

                //建立一个字节BUFFER，将要数据按格式转换成BYTE放入BUFFER中
                byte[] eventByteBuf = CustomEventsToHex(customEventList);

                //建立一个文件头的BYTEBUF，将自定义事件文件头写进去
                byte[] headByteBuf = generateCustomEventHeadByte();
                if (eventByteBuf == null)
                {
                    MessageBox.Show("解析失败:数据转换成BYTE出错");
                    return 0;
                }
                bw.Seek(0, SeekOrigin.Begin);
                bw.Write(headByteBuf);
                bw.Write(eventByteBuf);
                bw.Close();
                fs.Close();

                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("自定义事件写入文件错误" + e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 生成自定义事件.ent文件的头部二进制
        /// -- by lxl
        /// </summary>
        /// <returns></returns>
        private byte[] generateCustomEventHeadByte()
        {
            byte[] returnBytes = new byte[9];

            //将ENTCM按照ASCII转码存入前5个BYTE
            Encoding.ASCII.GetBytes("ENTCM", 0, "ENTCM".Count(), returnBytes, 0);

            //将事件个数转码存入第六个BYTE
            returnBytes[5] = BitConverter.GetBytes(customEventList.Count())[0];

            return returnBytes;
        }

        /// <summary>
        /// 将自定义事件按格式转成BYTE数组
        /// -- by lxl
        /// </summary>
        /// <param name="cEvent"></param>
        /// <returns></returns>
        private byte[] CustomEventsToHex(List<CustomEvent> cEvent)
        {
            //建立一个BYTE数组用于存储事件信息
            byte[] returnBytes;
            returnBytes = new byte[cEvent.Count() * 128];

            for (int i = 0; i < cEvent.Count(); i++)
            {
                Encoding.GetEncoding(936).GetBytes(cEvent[i].EventName, 0, cEvent[i].EventName.Count(), returnBytes, i * 128);
                returnBytes[i * 128 + 104] = BitConverter.GetBytes(cEvent[i].EventPosition)[0];
                returnBytes[i * 128 + 105] = BitConverter.GetBytes(cEvent[i].EventPosition)[1];
                returnBytes[i * 128 + 108] = BitConverter.GetBytes(cEvent[i].EventColorIndex)[0];           //int32转换成BYTE数组为4个BYTE，由于只有第一个故取数组下标[0]

                //将时间写入事件文件中
                DateTime time = nfi.StartDateTime.AddSeconds(cEvent[i].EventPosition/sampleRate);
                returnBytes[i * 128 + 120] = BitConverter.GetBytes((UInt16)time.Hour)[0];
                returnBytes[i * 128 + 121] = BitConverter.GetBytes((UInt16)time.Hour)[1];
                returnBytes[i * 128 + 122] = BitConverter.GetBytes((UInt16)time.Minute)[0];
                returnBytes[i * 128 + 123] = BitConverter.GetBytes((UInt16)time.Minute)[1];
                returnBytes[i * 128 + 124] = BitConverter.GetBytes((UInt16)time.Second)[0];
                returnBytes[i * 128 + 125] = BitConverter.GetBytes((UInt16)time.Second)[1];
            }

            return returnBytes;
        }

        #endregion

        /// <summary>
        /// 鼠标放上去显示点的值，测试代码，正式版删掉。  --by zt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartWave_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            switch (e.HitTestResult.ChartElementType)
            {
                //鼠标所在位置为数据点
                case ChartElementType.DataPoint:

                    //获取点的横坐标，第几个点
                    int i = e.HitTestResult.PointIndex;

                    //点所在的通道
                    int j = Convert.ToInt32(e.HitTestResult.Series.Name.Split('s')[1])-1;

                    //坐标点
                    DataPoint dp = e.HitTestResult.Series.Points[i - 1];

                    //还原数值
                    double interval = 2000D / signalNum;
                    double YValue = (dp.YValues[0] - (2000D - interval * (j - currentTopSignal) - interval / 2));
                    YValue = YValue * mmPerYGrid * sensitivity / 10 / interval;

                    //显示数值
                    e.Text = YValue.ToString();
                    break;
            }
        }

        private void 导联设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myLeadConfigForm.InitLeadConfig(this.hardwareConfigName);
            //myLeadConfigForm.Show();
            myLeadConfigForm.ShowDialog();
        }

		private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gra = e.Graphics;
            Pen myPen = Pens.Black;
            gra.DrawRectangle(myPen, new Rectangle(5, 5, 190, 190));
            //Graphics gra2 = e.Graphics;
            //Pen myPen2 = Pens.Black;
            //gra2.DrawRectangle(myPen2, new Rectangle(5,190,190,730));
        }

        /// <summary>
        /// 设置带通滤波是否选中,及状态栏中带通滤波
        /// </summary>
        public void SetBandFilterChecked()
        {
            this.BandFilterToolStripMenuItem.Checked = isBandFilter;
            if (this.BandFilterToolStripMenuItem.Checked == false) 
            {
                this.toolStripStatusLabel_bandFilter.Text = "None";
            }
            else 
            {
                this.toolStripStatusLabel_bandFilter.Text = lowFrequency + "Hz,  " + this.highFrequency + "Hz";
            }
        }

        private void Filter50HzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Filter50HzToolStripMenuItem.Checked == true)
            {
                //不选50Hz滤波
                Filter50HzToolStripMenuItem.Checked = false;
                is50HzFilter = false;
                this.toolStripStatusLabel_trap.Text = "None";
                LoadData(CurrentSeconds);
                ShowData();
            }
            else
            {
                //选择50Hz滤波
                Filter50HzToolStripMenuItem.Checked = true;
                is50HzFilter = true;
                this.toolStripStatusLabel_trap.Text = "50Hz";
                LoadData(CurrentSeconds);
                ShowData();
            }
        }

        private void BandFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myBandFilterForm.InitFormFilter();
            myBandFilterForm.Show();
        }

        /// <summary>
        /// 返回一个通道所有的数据--by wsp
        /// </summary>
        /// <param name="NumSigal"></param>
        /// <returns></returns>
        private double[] ReturnSignalData(int NumSigal)
        {
            double[] singalData = new double[packets.Count];
            int i = 0;
            foreach (int sIdx in Enumerable.Range(0, packets.Count))
            {
                    singalData[i] = packets[sIdx].EEG[NumSigal];
                    i++;
            }
            return singalData;
        }

        private double[] ReturnSignalDataZhong(int NumSigal)
        {
            double[] singalData = new double[signalNum-1];
            int i = 0;
            foreach (int sIdx in Enumerable.Range(0, signalNum-1))
            {
                singalData[i] = packets[NumSigal].EEG[sIdx];
                i++;
            }
            return singalData;
        }

        #region 面板listview控制按钮点击事件  -- by lxl
        /// <summary>
        /// 删除按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletePredefineEvents_Click(object sender, EventArgs e)
        {
            //判定是否有选择一个事件
            if (this.lvPreDefineEvents.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行删除");
                return;
            }

            //删除事件
            RemoveEvent(true, lvPreDefineEvents.SelectedIndices[0],int.Parse(lvPreDefineEvents.SelectedItems[0].Name));

            //更新相关的事件列表
            UpdateEventsListView(true);
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditPreDefineEvents_Click(object sender, EventArgs e)
        {
            //弹出事件编辑框
            if (myPreDefineEventFormEventForm == null || myPreDefineEventFormEventForm.IsDisposed)
                myPreDefineEventFormEventForm = new PredefineEventsForm(this);
            myPreDefineEventFormEventForm.Show();
            myPreDefineEventFormEventForm.BringToFront();
            myPreDefineEventFormEventForm.InitList();
        }

        /// <summary>
        /// 自定义事件列表增加按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCustomEvents_Click(object sender, EventArgs e)
        {
            addCustomEventForm myAddCustomEventForm = new addCustomEventForm(this);
            
            //置状态为编辑事件
            myAddCustomEventForm.IsAddEvent();

            //添加自定义事件的form弹出，并且为关闭前不允许操作该form
            myAddCustomEventForm.ShowDialog();
        }

        /// <summary>
        /// 自定义事件列表删除按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCustomEvents_Click(object sender, EventArgs e)
        {
            //判定是否有选择一个事件
            if (this.lvCustomEvents.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行删除");
                return;
            }

            //移除掉事件
            RemoveEvent(false, lvCustomEvents.SelectedIndices[0]);

            //更新相关的时间列表
            UpdateEventsListView(false);
        }

        /// <summary>
        /// 自定义时间列表编辑按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCustomEvents_Click(object sender, EventArgs e)
        {
            //判断是否有选择一个事件
            if (lvCustomEvents.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行编辑");
                return;
            }

            addCustomEventForm myAddCustomEventForm = new addCustomEventForm(this, lvCustomEvents.SelectedIndices[0]);

            //置状态为编辑事件
            myAddCustomEventForm.IsEditEvent(int.Parse(this.lvCustomEvents.SelectedItems[0].SubItems[3].Name), this.lvCustomEvents.SelectedItems[0].SubItems[0].Text);

            //添加自定义事件的form弹出，并且为关闭前不允许操作该form
            myAddCustomEventForm.ShowDialog();
        }

        #endregion

        /// 获取执行中断过程中所有时间差
        /// --by wsp
        /// </summary>
        /// <returns></returns>
        private double[] GetAllDvalue()
        {
            timeSignal = new double[eventProperty.RecordTime.Count];
            for (int i = 0; i < eventProperty.RecordTime.Count; i++)
            {
                timeSignal[i] = eventProperty.RecordTime[i].TotalSeconds;
            }
            return timeSignal;
        }

        /// <summary>
        /// 获取前一个中断位置的开始总时长
        /// </summary>
        /// <returns></returns>
        private int GettimeSignalNumber(int i)
        {
            return eventProperty.BeginningTime[i].Hour * 3600 + eventProperty.BeginningTime[i].Minute * 60 + eventProperty.BeginningTime[i].Second;
        }

        /// <summary>
        /// 病人属性隐藏按钮事件
        /// </summary>
        /// --by wsp
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DetectionInfoPanel.Hide(); 
   
            //去除检查属性菜单前的勾  by-xcg
            this.detectionInfoToolStripMenuItem.Checked = false;        }

        /// <summary>
        /// 检查属性隐藏按钮事件
        /// --by wsp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.PationInfoPanel.Hide();

            //去除病人属性菜单前的勾  by-xcg
            this.pationInfoToolStripMenuItem.Checked = false;    
			    }
    }
}