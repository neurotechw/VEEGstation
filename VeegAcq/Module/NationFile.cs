using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// 诺诚文件类  --by zt
    /// </summary>
    public class NationFile
    {
        #region 声明域和属性
        /// <summary>
        /// 基本信息
        /// </summary>
        private NatInfo _NatInfo; 

        public NatInfo NatInfo
        {
            get { return _NatInfo; }
            set { _NatInfo = value; }
        }
        /// <summary>
        /// 病人信息
        /// </summary>
        private PatInfo _PatInfo; 

        public PatInfo PatInfo
        {
            get { return _PatInfo; }
            set { _PatInfo = value; }
        }
        /// <summary>
        /// 导联标识名称
        /// </summary>
        private EEGFmt _EEGFmt; 

        public EEGFmt EEGFmt
        {
            get { return _EEGFmt; }
            set { _EEGFmt = value; }
        }
        /// <summary>
        /// 导联配置结构
        /// </summary>
        private Montage _Montage; 

        public Montage Montage
        {
            get { return _Montage; }
            set { _Montage = value; }
        }
        //private Event[] _Events; //事件

        /// <summary>
        /// 采样的总点数
        /// </summary>
        private int _NumberOfSamples;

        public int NumberOfSamples
        {
            get { return _NumberOfSamples; }
            set { _NumberOfSamples = value; }
        }

        /// <summary>
        /// 采样时间，精确到日
        /// </summary>
        private DateTime _StartDay;

        public DateTime StartDay
        {
            get { return _StartDay; }
            set { _StartDay = value; }
        }

        /// <summary>
        /// 采样时间，精确到秒
        /// </summary>
        private TimeSpan _StartTime;

        public TimeSpan StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        /// <summary>
        /// 开始时间，年月日时分秒
        /// </summary>
        private DateTime _StartDateTime;

        public DateTime StartDateTime
        {
            get { return _StartDateTime; }
            set { _StartDateTime = value; }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        private TimeSpan _Duration;

        public TimeSpan Duration { get { return Util.DurationToTimeSpan( this._NumberOfSamples* 1.0 / this.NatInfo.Freq); } }

        /// <summary>
        /// 文件是否关联视频
        /// </summary>
        private bool _HasVideo;

        public bool HasVideo
        {
            get { return _HasVideo; }
            set { _HasVideo = value; }
        }

        /// <summary>
        /// 关联的视频名称
        /// </summary>
        private string _VideoFullName;

        public string VideoFullName
        {
            get { return _VideoFullName; }
            set { _VideoFullName = value; }
        }

        /// <summary>
        /// 关联的视频偏移量
        /// </summary>
        private double _VideoOffset;

        public double VideoOffset
        {
            get { return _VideoOffset; }
            set { _VideoOffset = value; }
        }

        /// <summary>
        /// NED文件名称
        /// </summary>
        private string _NedFileName;

        public string NedFileName
        {
            get { return _NedFileName; }
            set { _NedFileName = value; }
        }

        /// <summary>
        /// 预定义事件种类个数
        /// -- by lxl
        /// </summary>
        private int _eventCount;

        public int EventCount
        {
            get { return _eventCount; }
            set { _eventCount = value; }
        }

        /// <summary>
        /// 预定义事件的名称数组
        /// -- by lxl
        /// </summary>
        private string[] _preDefineEventNameArray;

        public string[] PreDefineEventNameArray
        {
            get { return _preDefineEventNameArray; }
        }

        /// <summary>
        /// 预定义事件的颜色数组
        /// -- by lxl
        /// </summary>
        private System.Drawing.Color[] _preDefineEventColorArray;

        public System.Drawing.Color[] PreDefineEventColorArray
        {
            get { return _preDefineEventColorArray; }
        }
        
        #endregion

        public NationFile()
        {
            //_NatInfo = new NatInfo();
            //_PatInfo = new PatInfo();
            //_EEGFmt = new EEGFmt();
            //_Montage = new Montage(64);
            //事件
        }
        
        /// <summary>
        /// 从NAT文件中读取文件流
        /// </summary>
        /// <param name="file_path"></param>
        public void ReadNationFile(string file_path) 
        {
            //open the file to read the header
            FileStream fileStream = new FileStream(file_path, FileMode.Open, FileAccess.Read);

            FileInfo fi = new FileInfo(file_path);
            this._NedFileName = file_path.Substring(0, file_path.Length - fi.Extension.Length) + ".NED";

            ReadStream(fileStream);
            fileStream.Close();
            fileStream.Dispose();
        }

        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <param name="sr"></param>
        public void ReadStream(FileStream file)
        {
            ParseNationFile(file);
        }

        /// <summary>
        /// 解析NAT文件
        /// </summary>
        /// <param name="sr"></param>
        public void ParseNationFile(FileStream file) 
        {
            //解析NAT文件中基本配置
            int sizeOfNatInfo =  0x90;
            byte[] nationInfo = new byte[sizeOfNatInfo];
            file.Read(nationInfo, 0, sizeOfNatInfo);
            this._NatInfo = new NatInfo(nationInfo);

            //解析病人信息
            int sizeOfPatInfo =  this.NatInfo.EntOff - this.NatInfo.PatOff;
            byte[] patientInfo = new byte[sizeOfPatInfo];
            file.Read(patientInfo, 0, sizeOfPatInfo);
            //sr.Read(patientInfo, this.NatInfo.PatOff, sizeOfPatInfo);//要改
            this._PatInfo = new PatInfo(patientInfo);

            //解析事件区信息
            int sizeOfEventInfo = this.NatInfo.MonOff -this.NatInfo.EntOff;
            byte[] eventInfo = new byte[sizeOfEventInfo];
            file.Read(eventInfo, 0, sizeOfEventInfo);

            //解析事件代码    -- by lxl
            ParseEventInfo(eventInfo);        
            //由于病人事件列表可能很长，防止一开始加载文件列表时便解析事件，故将解析具体事件列表放在playbackform中，解析所选择的文件的事件 -- by lxl

            //解析导联区信息
            int sizeOfMonOff = this.NatInfo.CfgOff - this.NatInfo.MonOff;
            byte[] monInfo = new byte[sizeOfMonOff];
            file.Read(monInfo, 0, sizeOfMonOff);
            this._Montage = new Montage(monInfo);  
            
            //解析数据区信息
            ParseDataInfo(file);

        }

        /// <summary>
        /// 解析事件区信息
        /// -- by lxl
        /// </summary>
        /// <param name="eventInfo"></param>
        private void ParseEventInfo(byte[] eventInfo)
        {
            //短事件个数
            int shortEventNum = eventInfo[10];
            //长事件个数
            int longEventNum = eventInfo[11];

            byte[] name = new byte[10];

            //计算有多少个事件,并相应的分配事件颜色和名称数组的长度
            //eventInfo[8]为事件区的长度，每个事件占16个字节，故除以16则得到有多少个预定义事件
            _eventCount = shortEventNum + longEventNum;
            this._preDefineEventColorArray = new System.Drawing.Color[_eventCount];
            this._preDefineEventNameArray = new string[_eventCount];

            //解析出数据，将数据存入名称与颜色数组中
            for (int i = 12; i < _eventCount * 16; i += 16)
            {
                Array.Copy(eventInfo, i, name, 0, 10);
                _preDefineEventNameArray[(i - 12) / 16] = Encoding.GetEncoding(936).GetString(name).Trim('\0');
                _preDefineEventColorArray[(i - 12) / 16] = System.Drawing.Color.FromArgb(eventInfo[i + 12], eventInfo[i + 13], eventInfo[i + 14]);
            }
        }

        /// <summary>
        /// 解析数据区信息
        /// </summary>
        /// <param name="sr"></param>
        public void ParseDataInfo(FileStream file) 
        {
            file.Seek(this.NatInfo.CfgOff, SeekOrigin.Begin);
            byte[] dataNameBuf = new byte[5];
            int ret = file.Read(dataNameBuf, 0, 5);
            if (ret < 5)
                return;
            string dataName = new string(Encoding.ASCII.GetChars(dataNameBuf));
            if (dataName == "[CFG]")
            {
                file.Seek(11, SeekOrigin.Current);
                //开始时间，年月日
                byte[] startDay = new byte[4];
                file.Read(startDay, 0, 4);
                string StartDay = ((startDay[3] << 8 | startDay[2]) + "-" + startDay[1] + "-" + startDay[0]).ToString();
                this._StartDay = DateTime.Parse(StartDay); //开始时间

                file.Seek(20, SeekOrigin.Current);
                //总的采样点数
                byte[] counts = new byte[4];
                file.Read(counts, 0, 4);
                this._NumberOfSamples = counts[3] << 24 | counts[2] << 16 | counts[1] << 8 | counts[0];

                //开始采集时间 时分秒
                file.Seek(4, SeekOrigin.Current);
                byte[] startTime = new byte[4];
                file.Read(startTime, 0, 4);
                this._StartTime = new TimeSpan(startTime[3] << 8 | startTime[2], startTime[1], startTime[0]);

                //开始采集时间 年月日时分秒
                this._StartDateTime = this.StartDay.AddSeconds(this._StartTime.TotalSeconds);

                //解析事件

            }
        }

        public void CheckHasVideo() 
        {
            DirectoryInfo diTop = new DirectoryInfo(DefaultConfig.AssociatedVideoPath);
            var files = diTop.EnumerateFiles("*.MP4");
            this.HasVideo = false;
            foreach (var file in files)
            {
                if (file.Name.StartsWith(this.PatInfo.ID + "_"))
                {
                    this.HasVideo = true;
                    this.VideoFullName = file.FullName;
                    string offsetStr = file.Name.Substring(this.PatInfo.ID.Length + 1);
                    offsetStr = offsetStr.Substring(0, offsetStr.LastIndexOf('.'));
                    this.VideoOffset = double.Parse(offsetStr) / 1000;
                    break;
                }
            }
        }

    }
}
