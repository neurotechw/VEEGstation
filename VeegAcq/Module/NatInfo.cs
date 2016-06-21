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
        private string sign;    
        
        /// <summary>
        /// 版本号	
        /// </summary>
        private string version;		    	
        
        /// <summary>
        /// 操作时间,精确到日
        /// </summary>
        private DateTime startDate;			

        /// <summary>
        /// 每秒采集点数
        /// </summary>
        private int freq;			

        /// <summary>
        /// 脑电导数	诺诚文档是byte类型
        /// </summary>
        private int brainNumber;		
        
        /// <summary>
        /// 心电导数	诺诚文档是byte类型
        /// </summary>
        private int heartNumber;		
        
        /// <summary>
        /// eeg类型,未知
        /// </summary>
        private string eegType;
        
        /// <summary>
        /// 病人信息区位置
        /// </summary>
        private int patOff;		
        
        /// <summary>
        /// 事件区位置
        /// </summary>
        private int entOff;		
        
        /// <summary>
        /// 导联设置区位置
        /// </summary>
        private int monOff;		
        
        /// <summary>
        /// 状态区位置
        /// </summary>
        private int cfgOff;			
        
        /// <summary>
        /// 数据区位置
        /// </summary>
        private int datOff;		
        
        /// <summary>
        /// 呼吸选择
        /// </summary>
        private byte respNumber;		
        
        /// <summary>
        /// 闪光选择
        /// </summary>
        private byte flashNumber;		
        
        /// <summary>
        /// 
        /// </summary>
        private byte operate;
        
        /// <summary>
        /// 标志,未知
        /// </summary>
        private string flag;  
        
        /// <summary>
        /// 是否有视频
        /// </summary>
        private bool isHaveVideo;           
        
        /// <summary>
        /// 一帧数据的大小
        /// </summary>
        private int rowsOfData;            
        
        /// <summary>
        /// 配置信息：b0 b1 b2 b3 b4 b5 b6 b7
        /// 字节顺序b7 b6 b5 b4 b3 b2 b1 b0，详细解释：	
        /// b7 b6 : 00   无睡眠
        /// b7 b6 : 01   有睡眠	
        /// b5~b2: 0000 脑电10导
        /// b5~b2: 0001 脑电20导	
        /// b5~b2: 0010 脑电40导	
        /// b5~b2: 0011 脑电64导
        /// b1 b0 : 00   保留	
        /// 例如：                        2进制       16进制        10进制    一帧大小     16进制
        /// P10配置（8导脑电）:         00000000       0x00            0  	    26	       0x1A
        /// P11配置（8导脑电+多参数）:  01000000        0x40           64      48           0x30
        /// P20配置（16导脑电）:        00000100        0x04           4       46           0x2E
        /// P21配置（16导脑电+多参数）: 01000100        0x44 	          68      68          0x44
        /// P30配置（24导脑电）：       00000010        0x02            2       86         0x56
        /// P40配置（32导脑电）:        00001000       0x08             8       86         0x56
        /// P41配置（32导脑电+多参数）: 01001000        0x48            72      108        0x6C
        /// P80配置 :                  00001100        0x0C            12      134        0x86
        /// </summary>
        private byte byteConfigType;          
        
        private byte[] remain = new byte[88];		//保留字节

        #endregion

        #region 访问器
        public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public int Freq
        {
            get { return freq; }
            set { freq = value; }
        }

        public int BrainNumber
        {
            get { return brainNumber; }
            set { brainNumber = value; }
        }

        public int HeartNumber
        {
            get { return heartNumber; }
            set { heartNumber = value; }
        }

        public string EegType
        {
            get { return EegType; }
            set { EegType = value; }
        }

        public int PatOff
        {
            get { return patOff; }
            set { patOff = value; }
        }

        public int EntOff
        {
            get { return entOff; }
            set { entOff = value; }
        }

        public int MonOff
        {
            get { return monOff; }
            set { monOff = value; }
        }

        public int CfgOff
        {
            get { return cfgOff; }
            set { cfgOff = value; }
        }

        public int DatOff
        {
            get { return datOff; }
            set { datOff = value; }
        }

        public byte RespNumber
        {
            get { return respNumber; }
            set { respNumber = value; }
        }

        public byte FlashNumber
        {
            get { return flashNumber; }
            set { flashNumber = value; }
        }

        public byte Operate
        {
            get { return operate; }
            set { operate = value; }
        }

        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public bool IsHaveVideo
        {
            get { return isHaveVideo; }
            set { isHaveVideo = value; }
        }

        public int RowsOfData
        {
            get { return rowsOfData; }
            set { rowsOfData = value; }
        }

        public byte ByteConfigType
        {
            get { return byteConfigType; }
            set { byteConfigType = value; }
        }
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
        public static int FixedLength_ByteConfigType = 1;
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
            this.sign = new string(Encoding.ASCII.GetChars(sign));
            fileIndex += FixedLength_Sign;

            //版本号
            byte[] version = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Version);
            this.version = ((version[1] << 8) | version[0]).ToString();
            fileIndex += FixedLength_Version;

            //检查时间 年月日
            byte[] startDate = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StartDate);
            string date = ((startDate[3] << 8 | startDate[2]) + "-" + startDate[1] + "-" + startDate[0]).ToString();
            this.startDate = DateTime.Parse(date);
            fileIndex += FixedLength_StartDate;

            //每秒采集点数
            byte[] freq = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Freq);
            this.freq = (freq[1] << 8 | freq[0]);
            fileIndex += FixedLength_Freq;

            //脑电导数
            byte[] brainNumber = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_BrainNumber);
            this.brainNumber = brainNumber[0];
            fileIndex += FixedLength_BrainNumber;

            //心电导数
            byte[] heartNumber = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_HeartNumber);
            this.heartNumber = heartNumber[0];
            fileIndex += FixedLength_HeartNumber;

            //不知含义
            byte[] eegType = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_EegType);
            this.eegType =  eegType[0].ToString();
            fileIndex += FixedLength_EegType;

            //3个不知含义的字节
            fileIndex += 3;

            //获得nat文件中存储病人信息的位置  
            byte[] patOff = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this.patOff = (patOff[3] << 24 | patOff[2] << 16 | patOff[1] << 8 | patOff[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得nat文件中存储事件的配置信息的位置    --by zt
            byte[] entOff = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this.entOff = (entOff[3] << 24 | entOff[2] << 16 | entOff[1] << 8 | entOff[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得nat文件中导联配置中每个通道的导名    --by zt
            byte[] monoffbuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this.monOff = (monoffbuf[3] << 24 | monoffbuf[2] << 16 | monoffbuf[1] << 8 | monoffbuf[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得nat文件中采集时的时间信息的位置  
            byte[] cfgoffbuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this.cfgOff = (cfgoffbuf[3] << 24 | cfgoffbuf[2] << 16 | cfgoffbuf[1] << 8 | cfgoffbuf[0]);
            fileIndex += FixedLength_StoreOffSet;

            //获得数据区的位置，数据区包括 采集时间（年月日，时分秒，总点数）
            byte[] dataoffbuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_StoreOffSet);
            this.datOff = (dataoffbuf[3] << 24 | dataoffbuf[2] << 16 | dataoffbuf[1] << 8 | dataoffbuf[0]);
            fileIndex += FixedLength_StoreOffSet;

            //呼吸选择??不知道含义
            this.respNumber = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);
            fileIndex += 1;

            //闪光选择??不知道含义
            this.flashNumber = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);
            fileIndex += 1;

            //??不知道含义
            this.operate = Convert.ToByte(Util.getFixedLengthByteArray(nationInfo, fileIndex, 1)[0].ToString(), 16);
            fileIndex += 1;
            fileIndex += 1;

            //??不知道含义
            byte[] flag = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_Flag);
            string flgdata = (flag[3] << 24 | flag[2] << 16 | flag[1] << 8 | flag[0]).ToString();
            this.flag = flgdata;
            fileIndex += FixedLength_Flag;

            //是否有视频的标志，但不知道0表示有视频还是没视频 
            byte[] isHaveVideo = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_IsHaveVideo);
            int flg = (isHaveVideo[3] << 24 | isHaveVideo[2] << 16 | isHaveVideo[1] << 8 | isHaveVideo[0]);
            if (flg == 0)
                this.isHaveVideo = false;
            else
                this.isHaveVideo = true;
            fileIndex += FixedLength_IsHaveVideo;

            //一帧的数据大小 
            byte[] rowsOfData = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_RowsOfData);
            this.rowsOfData = (rowsOfData[3] << 24 | rowsOfData[2] << 16 | rowsOfData[1] << 8 | rowsOfData[0]);
            fileIndex += FixedLength_RowsOfData;

            // 配置信息
            byte[] byteConfigTypeBuf = Util.getFixedLengthByteArray(nationInfo, fileIndex, FixedLength_ByteConfigType);
            //this.byteConfigType = Convert.ToByte(byteConfigTypeBuf[0].ToString(), 16);
            this.byteConfigType = byteConfigTypeBuf[0];

            Debug.WriteLine(string.Format("NatInfo {0},byteConfigType {1}", this.StartDate,this.byteConfigType));

        }
    }
}
