using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// NAT文件中基本配置信息  --by zt
    /// </summary>
    public class NatInfo
    {
        #region 声明域和变量
        /// <summary>
        /// "VE"
        /// </summary>
        private string _Sign;    
        public string Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        /// <summary>
        /// 版本号	
        /// </summary>
        private string _Version;		    	
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        private DateTime _StartDate;			//操作时间,精确到日

        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private int _Freq;			//每秒采集点数

        public int Freq
        {
            get { return _Freq; }
            set { _Freq = value; }
        }

        private int _BrainNumber;		//脑电导数	诺诚文档是byte类型
        public int BrainNumber
        {
            get { return _BrainNumber; }
            set { _BrainNumber = value; }
        }

        private int _HeartNumber;		//心电导数	诺诚文档是byte类型
        public int HeartNumber
        {
            get { return _HeartNumber; }
            set { _HeartNumber = value; }
        }

        private string _EegType;
        public string EegType
        {
            get { return EegType; }
            set { EegType = value; }
        }

        private int _PatOff;		//病人信息区位置
        public int PatOff
        {
            get { return _PatOff; }
            set { _PatOff = value; }
        }

        private int _EntOff;		//事件区位置
        public int EntOff
        {
            get { return _EntOff; }
            set { _EntOff = value; }
        }

        private int _MonOff;		//导联设置区位置
        public int MonOff
        {
            get { return _MonOff; }
            set { _MonOff = value; }
        }

        private int _CfgOff;		//状态区位置	
        public int CfgOff
        {
            get { return _CfgOff; }
            set { _CfgOff = value; }
        }

        private int _DatOff;		//数据区位置
        public int DatOff
        {
            get { return _DatOff; }
            set { _DatOff = value; }
        }

        private byte _RespNumber;		//呼吸选择
        public byte RespNumber
        {
            get { return _RespNumber; }
            set { _RespNumber = value; }
        }

        private byte _FlashNumber;		//闪光选择
        public byte FlashNumber
        {
            get { return _FlashNumber; }
            set { _FlashNumber = value; }
        }

        private byte _Operate;
        public byte Operate
        {
            get { return _Operate; }
            set { _Operate = value; }
        }

        private string _Flag;  //未知
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        private bool _IsHaveVideo;           //是否有视频
        public bool IsHaveVideo
        {
            get { return _IsHaveVideo; }
            set { _IsHaveVideo = value; }
        }

        private int _RowsOfData;            // 一帧数据的大小
        public int RowsOfData
        {
            get { return _RowsOfData; }
            set { _RowsOfData = value; }
        }

        private byte _ByteConfigType;         //  配置信息：b0 b1 b2 b3 b4 b5 b6 b7
        public byte ByteConfigType
        {
            get { return _ByteConfigType; }
            set { _ByteConfigType = value; }
        }
        private byte[] _Remain = new byte[88];		//保留字节

        #endregion

        #region 定义每个内容占的字节
        public static int FixedLength_Sign = 2;
        public static int FixedLength_Version = 2;
        public static int FixedLength_StartDate = 4;
        public static int FixedLength_Freq = 2;
        public static int FixedLength_BrainNumber = 1;
        public static int FixedLength_HeartNumber = 1;
        public static int FixedLength_EegType = 1;
        public static int FixedLength_StoreOffSet = 4;
        public static int FixedLength_Flag = 4;
        public static int FixedLength_IsHaveVideo = 4;
        public static int FixedLength_RowsOfData = 4;
        #endregion

        /// <summary>
        /// 空的构造函数
        /// </summary>
        public NatInfo()
        {
            InitializeNatInfo();
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        private void InitializeNatInfo()
        {
            //初始化各变量
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nationInfo"></param>
        public NatInfo(byte[] nationInfo)
        {
            //byte[] nationInfoByte = HexToByte(new string(nationInfo));
            ParseNatInfo(nationInfo);
        }

        /// <summary>
        /// 解析Nat数据
        /// </summary>
        private void ParseNatInfo(byte[] nationInfo)
        {
            int fileIndex = 0;

            //ve
            byte[] sign = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Sign);
            this._Sign = new string(Encoding.ASCII.GetChars(sign));
            fileIndex += FixedLength_Sign;

            //版本号
            byte[] version = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Version);
            this._Version = ((version[1] << 8) | version[0]).ToString();
            fileIndex += FixedLength_Version;

            //检查时间 年月日
            byte[] startDate = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StartDate);
            string date = ((startDate[3] << 8 | startDate[2]) + "-" + startDate[1] + "-" + startDate[0]).ToString();
            this._StartDate = DateTime.Parse(date);
            fileIndex += FixedLength_StartDate;

            //每秒采集点数
            byte[] freq = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Freq);
            this._Freq = (freq[1] << 8 | freq[0]);
            fileIndex += FixedLength_Freq;

            //脑电导数
            byte[] brainNumber = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_BrainNumber);
            this._BrainNumber = brainNumber[0];
            fileIndex += FixedLength_BrainNumber;

            //心电导数
            byte[] heartNumber = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_HeartNumber);
            this._HeartNumber = heartNumber[0];
            fileIndex += FixedLength_HeartNumber;

            //不知含义
            byte[] eegType = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_EegType);
            this._EegType =  eegType[0].ToString();
            fileIndex += FixedLength_EegType;

            //3个不知含义的字节
            fileIndex += 3;

            //获得nat文件中存储病人信息的位置  
            byte[] patOff = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this._PatOff = (patOff[3] << 24 | patOff[2] << 16 | patOff[1] << 8 | patOff[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得nat文件中存储事件的配置信息的位置    --by zt
            byte[] entOff = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this._EntOff = (entOff[3] << 24 | entOff[2] << 16 | entOff[1] << 8 | entOff[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得nat文件中导联配置中每个通道的导名    --by zt
            byte[] monoffbuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this._MonOff = (monoffbuf[3] << 24 | monoffbuf[2] << 16 | monoffbuf[1] << 8 | monoffbuf[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得nat文件中采集时的时间信息的位置  
            byte[] cfgoffbuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this._CfgOff = (cfgoffbuf[3] << 24 | cfgoffbuf[2] << 16 | cfgoffbuf[1] << 8 | cfgoffbuf[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得数据区的位置，数据区包括 采集时间（年月日，时分秒，总点数）
            byte[] dataoffbuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this._DatOff = (dataoffbuf[3] << 24 | dataoffbuf[2] << 16 | dataoffbuf[1] << 8 | dataoffbuf[0]);
            fileIndex += FixedLength_StoreOffSet;

            //呼吸选择??不知道含义
            this._RespNumber = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);
            fileIndex += 1;

            //闪光选择??不知道含义
            this._FlashNumber = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);
            fileIndex += 1;

            //??不知道含义
            this._Operate = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);
            fileIndex += 1;
            fileIndex += 1;

            //??不知道含义
            byte[] flag = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Flag);
            string flgdata = (flag[3] << 24 | flag[2] << 16 | flag[1] << 8 | flag[0]).ToString();
            this._Flag = flgdata;
            fileIndex += FixedLength_Flag;

            //是否有视频的标志，但不知道0表示有视频还是没视频 
            byte[] isHaveVideo = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_IsHaveVideo);
            int flg = (isHaveVideo[3] << 24 | isHaveVideo[2] << 16 | isHaveVideo[1] << 8 | isHaveVideo[0]);
            if (flg == 0)
                this._IsHaveVideo = false;
            else
                this._IsHaveVideo = true;
            fileIndex += FixedLength_IsHaveVideo;

            //一帧的数据大小,不知道帧的含义 
            byte[] rowsOfData = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_RowsOfData);
            this._RowsOfData = (rowsOfData[3] << 24 | rowsOfData[2] << 16 | rowsOfData[1] << 8 | rowsOfData[0]);
            fileIndex += FixedLength_RowsOfData;

            // 配置信息??不知道含义
            this._ByteConfigType = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);

            Debug.WriteLine(string.Format("NatInfo {0}", this.StartDate));

        }

        

        

    }
}
