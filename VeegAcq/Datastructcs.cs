using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeegStation
{
    public class NatInfo
    {
        public  string          Sign;     // "VE"		
        public  string          Version;		    //版本号		
        public  DateTime    Date;			//操作时间
        public  int                Freq;			//每秒采集点数
        public  byte             BrainNumber;		//脑电导数	
        public  byte             HeartNumber;		//心电导数	
        public  string             EegType;

        public  int                PatOff;		//病人信息区位置
        public  int                EntOff;		//事件区位置
        public  int                MonOff;		//导练设置区位置
        public  int                CfgOff;		//状态区位置	
        public  int                DatOff;		//数据区位置

        public  byte             RespNumber;		//呼吸选择
        public  byte             FlashNumber;		//闪光选择
        public  byte            Operate;
        public  string          Flag;
        public  bool            bIsHaveVideo;           //是否有视频
        public  int               nRowsOfData;            // 一帧数据的大小
        public  byte             byteConfigType;         //  配置信息：b0 b1 b2 b3 b4 b5 b6 b7
        public  byte[]          Remain = new byte[88];		//保留字节
    }

    public class PatInfo                       
    {
	   public   string    Name; // 姓名
       public   string    Gender;	// 性别
       public   string    ID;		// 检查号
	   public   string    Age;	        // 病人年龄
       public   string    Handedness;	// 左右利
       public   string    State;	// 状态
       public   string    ResidentDoctor;	// 申请医师
	   public   string    Type;	// 检查类型
	   public   string    MengZhen ;	// 门诊号
	   public   string    ZhuYuan;	// 住院号
       public   string    OperateDoctor; //操作医生
	   public   string    Diagnosis;// 诊断
	   public   string    History;	// 既往病史
	   public   string    Medicine;	// 用药
	   public   string    Archives;       // 归档
	   public   string    Note;      // 备注
	   public   string    BingQu ;   
	   public   string    ChuangHao;
       public   string     KeShi;
       public   string     FilePath;
    }

    //标志的结构
    public class EEGFMT
    {
        public string Name;//标志
        public int Off;
    };

    //导联配置结构
    public class MONTAGE
    {
        public byte MontageMode;                                         //Montage mode
        public byte MontageRoutes;                                       //Electrorode number
        public string[] FirstName;                                               //相对导名
        public string[] SecondName;                                         //基导名
        public string[] FirstID;                                                    //相对导ID
        public string[] SecondID;                                               //基导名ID
        public string Name;
        public short Flag;
        public string Note;
        public string szSetting;                                //脑电盒支持的最高配置如P41
        public string szNote;                                   //最高配置的含义

        public MONTAGE(int num)
        {
            FirstName = new string[num];
            SecondName = new string[num];
            FirstID = new string[num];
            SecondID = new string[num];
        }
    }
    public class COLORREF
    {
        public COLORREF(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
        }
        public byte R;
        public byte G;
        public byte B;
    }

    //短事件
    public class EVENTHEAD
    {
        public string Sign;
        public int Number;                  //事件数目
    };

    //结构EVENT
    public class EVENT
    {
        public EVENT(string name, COLORREF color)
        {
            Name = name;
            Color = color;
        }
        public string Name;
        public COLORREF Color;
    } ;
}
