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
        private NatInfo natInfo; 

        /// <summary>
        /// 病人信息
        /// </summary>
        private PatInfo patInfo; 

        /// <summary>
        /// 导联标识名称
        /// </summary>
        private EEGFmt myEEGFmt; 

        /// <summary>
        /// 导联配置结构
        /// </summary>
        private Montage myMontage; 

        //private Event[] _Events; //事件

        /// <summary>
        /// CFG块信息，主要是时间及事件
        /// </summary>
        private CFGInfo myCFGInfo;

        /// <summary>
        /// 采样的总点数
        /// </summary>
        private int numberOfSamples;

        /// <summary>
        /// 采样时间，精确到日
        /// </summary>
        private DateTime startDay;

        /// <summary>
        /// 采样时间，精确到秒
        /// </summary>
        private TimeSpan startTime;

        /// <summary>
        /// 开始时间，年月日时分秒
        /// </summary>
        private DateTime startDateTime;

        /// <summary>
        /// 持续时间
        /// </summary>
        private TimeSpan duration;

        public TimeSpan Duration
        {
            get { return Util.DurationToTimeSpan(this.numberOfSamples * 1.0 / this.NatInfo.Freq); }
        }
        
        /// <summary>
        /// 文件是否关联视频
        /// </summary>
        private bool hasVideo;

        /// <summary>
        /// 关联的视频名称
        /// </summary>
        private string videoFullName;

        /// <summary>
        /// 关联的视频偏移量
        /// </summary>
        private double videoOffset;

        /// <summary>
        /// NED文件名称
        /// </summary>
        private string nedFileName;
        #endregion

        #region 访问器
        public NatInfo NatInfo
        {
            get { return natInfo; }
            set { natInfo = value; }
        }

        public PatInfo PatInfo
        {
            get { return patInfo; }
            set { patInfo = value; }
        }

        public EEGFmt EEGFmt
        {
            get { return myEEGFmt; }
            set { myEEGFmt = value; }
        }

        public Montage Montage
        {
            get { return myMontage; }
            set { myMontage = value; }
        }

        public CFGInfo CFGInfo
        {
            get { return myCFGInfo; }
            set { myCFGInfo = value; }
        }

        public int NumberOfSamples
        {
            get { return numberOfSamples; }
            set { numberOfSamples = value; }
        }

        public DateTime StartDay
        {
            get { return startDay; }
            set { startDay = value; }
        }

        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value; }
        }
        
        public bool HasVideo
        {
            get { return hasVideo; }
            set { hasVideo = value; }
        }

        public string VideoFullName
        {
            get { return videoFullName; }
            set { videoFullName = value; }
        }

        public double VideoOffset
        {
            get { return videoOffset; }
            set { videoOffset = value; }
        }

        public string NedFileName
        {
            get { return nedFileName; }
            set { nedFileName = value; }
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
            this.nedFileName = file_path.Substring(0, file_path.Length - fi.Extension.Length) + ".NED";

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
            this.natInfo = new NatInfo(nationInfo);

            //解析病人信息
            int sizeOfPatInfo =  this.NatInfo.EntOff - this.NatInfo.PatOff;
            byte[] patientInfo = new byte[sizeOfPatInfo];
            file.Read(patientInfo, 0, sizeOfPatInfo);
            //sr.Read(patientInfo, this.NatInfo.PatOff, sizeOfPatInfo);//要改
            this.patInfo = new PatInfo(patientInfo);

            //解析事件区信息
            int sizeOfEventInfo = this.NatInfo.MonOff -this.NatInfo.EntOff;
            byte[] eventInfo = new byte[sizeOfEventInfo];
            file.Read(eventInfo, 0, sizeOfEventInfo);
            //解析事件代码，暂时用不到，所以没写

            //解析导联区信息
            int sizeOfMonOff = this.NatInfo.CfgOff - this.NatInfo.MonOff;
            byte[] monInfo = new byte[sizeOfMonOff];
            file.Read(monInfo, 0, sizeOfMonOff);
            this.myMontage = new Montage(monInfo);  
            
            //解析数据区信息
            //this.CFGInfo = new CFGInfo(file);
            ParseDataInfo(file);

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
                this.startDay = DateTime.Parse(StartDay); //开始时间

                file.Seek(20, SeekOrigin.Current);
                //总的采样点数
                byte[] counts = new byte[4];
                file.Read(counts, 0, 4);
                this.numberOfSamples = counts[3] << 24 | counts[2] << 16 | counts[1] << 8 | counts[0];

                //开始采集时间 时分秒
                file.Seek(4, SeekOrigin.Current);
                byte[] startTime = new byte[4];
                file.Read(startTime, 0, 4);
                this.startTime = new TimeSpan(startTime[3] << 8 | startTime[2], startTime[1], startTime[0]);
                
                //开始采集时间 年月日时分秒
                this.startDateTime = this.StartDay.AddSeconds(this.startTime.TotalSeconds);

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
