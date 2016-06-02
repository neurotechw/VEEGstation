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
        
        #endregion

        public NationFile()
        {
            _NatInfo = new NatInfo();
            _PatInfo = new PatInfo();
            _EEGFmt = new EEGFmt();
            _Montage = new Montage(64);
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
            byte[] nationInfo = new byte[0x90];
            file.Read(nationInfo, 0, 0x90);
            this.NatInfo = new NatInfo(nationInfo);

            //解析病人信息
            int sizeOfPatInfo =  this.NatInfo.EntOff - this.NatInfo.PatOff;
            byte[] patientInfo = new byte[sizeOfPatInfo];
            file.Read(patientInfo, 0, sizeOfPatInfo);
            //sr.Read(patientInfo, this.NatInfo.PatOff, sizeOfPatInfo);//要改
            this.PatInfo = new PatInfo(patientInfo);

            //解析导联区信息

            //解析数据区信息
            ParseDateInfo(file);

        }

        /// <summary>
        /// 解析数据区信息
        /// </summary>
        /// <param name="sr"></param>
        public void ParseDateInfo(FileStream file) 
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
