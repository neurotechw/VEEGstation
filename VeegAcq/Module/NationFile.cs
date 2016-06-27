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

            //解析事件代码    -- by lxl
            ParseEventInfo(eventInfo);        
            //由于病人事件列表可能很长，防止一开始加载文件列表时便解析事件，故将解析具体事件列表放在playbackform中，解析所选择的文件的事件 -- by lxl


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
        /// 解析预定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="filename">文件绝对路径</param>
        /// <param name="cfgoff">Cfg字段所处的位置</param>
        /// <returns></returns>
        public List<PreDefineEvent> ParsePreDefineEvent(string filename,int cfgoff)
        {
            List<PreDefineEvent> pdList = new List<PreDefineEvent>();
            
            //预定义事件在文件中存储的位置
            int posInFile;

            //解析预定义事件
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            //定位到CFG的位置
            fs.Seek(cfgoff + 68, SeekOrigin.Begin);
            posInFile = cfgoff + 68;

            //一直读，独到值为0x45的BYTE即为读出事件
            int mark;
            int id = 1;
            byte[] myByte = new byte[7];
            do
            {
                mark = fs.ReadByte();
                posInFile++;
                if (mark != 69)
                {
                    fs.Seek(7, SeekOrigin.Current);
                    posInFile += 7;
                    continue;
                }

                //将标志位后的7个BYTE全部读出来
                fs.Read(myByte, 0, 7);

                //BYTE第一位为标志预定义事件编号位，第四第五位为存储点位置位（低位存储）
                pdList.Add(new PreDefineEvent(myByte[0], Convert.ToInt32(myByte[3] | myByte[4] << 8 | myByte[5] << 16 | myByte[6] << 32), posInFile - 1, id));   //减1把readbyte那的+1减掉
                posInFile += 7;
                id++;
            }
            while (mark >= 0);

            //关闭文件流
            fs.Close();
            fs.Dispose();

            return pdList;
        }

        /// <summary>
        /// 解析自定义事件
        /// -- by lxl
        /// </summary>
        /// <param name="filename">文件绝对路径</param>
        /// <param name="cfgoff">Cfg字段所处的位置</param>
        /// <returns></returns>
        public List<CustomEvent> ParseCustomEvent(string filename, int cfgoff)
        {
            List<CustomEvent> cList = new List<CustomEvent>();  

            //解析自定义事件
            string name;
            byte[] nameInByte = new byte[104];
            if (File.Exists(filename))
            {
                //打开.ent文件流并将其中数据读出来
                FileStream entFS = new FileStream(filename, FileMode.Open, FileAccess.Read);
                byte[] entByte = new byte[entFS.Length];

                if (entFS.Length <= 0)
                {
                    return cList;
                }
                entFS.Read(entByte, 0, (int)entFS.Length);

                //解析其中的数据
                for (int i = 0; i < entByte[5]; i++)
                {
                    Array.Copy(entByte, i * 128 + 9, nameInByte, 0, 104);
                    name = Encoding.GetEncoding(936).GetString(nameInByte).Trim('\0');
                    cList.Add(new CustomEvent(name, Convert.ToInt32(entByte[i * 128 + 9 + 104] | entByte[i * 128 + 9 + 105] << 8 | entByte[i * 128 + 9 + 106] << 16 | entByte[i * 128 + 9 + 107] << 32), entByte[i * 128 + 9 + 108]));
                }

                //将时间列表排序
                cList.Sort(new CustomEventComparer());
            }
            return cList;
        }

        /// <summary>
        /// 解析CFG字段后除开具体事件信息外的信息
        /// -- by xcg
        /// </summary>
        /// <param name="filename">文件绝对路径</param>
        /// <param name="cfgoff">Cfg字段所处的位置</param>
        /// <returns></returns>
        public EventProperty ParseEventProperty(string filename, int cfgoff)
        {
            EventProperty eventProperty = new EventProperty();
            eventProperty.BeginningTime = new List<DateTime>();
            eventProperty.RecordQuantity = new List<int>();
            eventProperty.RecordTime = new List<TimeSpan>();

            //打开文件流，并定位到CFG后的第12位
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            fs.Seek(cfgoff + 12, SeekOrigin.Begin);

            byte[] myByte = new byte[8];

            //开始时间，年月日
            fs.Read(myByte, 0, 8);
            string StartDay = ((myByte[7] << 8 | myByte[6]) + "-" + myByte[5] + "-" + myByte[4]).ToString();
            this.startDay = DateTime.Parse(StartDay);

            // 添加病例时点击“脑电图采集”按钮的时间，时分秒
            fs.Read(myByte, 0, 8);
            eventProperty.ColletTime = new TimeSpan(myByte[7] << 8 | myByte[6], myByte[5], myByte[4]);

            //采集时点击“退出”按钮的时间，时分秒
            fs.Read(myByte, 0, 8);
            eventProperty.ExitTime = new TimeSpan(myByte[7] << 8 | myByte[6], myByte[5], myByte[4]);

            //采集的总点数
            fs.Read(myByte, 0, 8);
            eventProperty.NumberOfSamples = myByte[7] << 24 | myByte[6] << 16 | myByte[5] << 8 | myByte[4];

            #region 解析NAT文件中的时间信息
            int mark;
            int mRTArrayIndex = -1;
            do
            {
                mark = fs.ReadByte();
                if (mark != 84)
                {
                    fs.Seek(7, SeekOrigin.Current);
                    continue;
                }

                //将标志位后的7个BYTE全部读出来
                fs.Read(myByte, 0, 7);
                if (myByte[0] == 0X03)
                {
                    eventProperty.BeginningTime.Add(new DateTime(eventProperty.StartDay.Year,eventProperty.StartDay.Month,eventProperty.StartDay.Day,myByte[6] << 8 | myByte[5], myByte[4], myByte[3]));
                }

                if (myByte[0] == 0X04)
                {
                    mRTArrayIndex++;
                    eventProperty.RecordQuantity.Add(myByte[6] << 24 | myByte[5] << 16 | myByte[4] << 8 | myByte[3]);
                    eventProperty.RecordTime.Add(new TimeSpan(0, 0, eventProperty.RecordQuantity[mRTArrayIndex] /natInfo.Freq));
                }
            } while (mark >= 0);
            #endregion

            fs.Close();
            fs.Dispose();

            return eventProperty;
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

        /// <summary>
        /// Video
        /// </summary>
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