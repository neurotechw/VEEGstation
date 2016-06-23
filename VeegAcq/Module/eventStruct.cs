using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VeegStation
{
    /// <summary>
    /// 预定义事件类，包含颜色，名称（字符串数组），和点的位置（第几个点）
    /// -- by lxl
    /// </summary>
    public class PreDefineEvent
    {
        /// <summary>
        /// 预定义事件的名称数组
        /// </summary>
        private static Color[] preDefineEventColorArray;

        public static Color[] PreDefineEventColorArray
        {
            get { return PreDefineEvent.preDefineEventColorArray; }
        }

        /// <summary>
        /// 预定义事件的颜色数组
        /// </summary>
        private static string[] preDefineEventNameArray;

        public static string[] PreDefineEventNameArray
        {
            get { return PreDefineEvent.preDefineEventNameArray; }
        }

        /// <summary>
        /// 根据名称的索引和点的位置构造预定义事件
        /// </summary>
        /// <param name="index">预定义事件的索引</param>
        /// <param name="pos">事件点所在数据点中的位置</param>
        /// <param name="posInF">事件位于文件中的位置，若是放入添加事件列表中则该值可为任意数</param>
        /// <param name="id">事件的标识ID</param>
        public PreDefineEvent(int index, UInt16 pos,int posInF,int id)
        {
            if (preDefineEventColorArray == null || preDefineEventNameArray == null)
            {
                throw new Exception("请先初始化预定义事件名称数组和颜色数组");
            }

            //若是读出的index为0x32，则为stop事件
            if (index == 0x32)
                index = preDefineEventNameArray.Length - 1;
            eventNameIndex = index;
            eventName = preDefineEventNameArray[index];
            eventColor = preDefineEventColorArray[index];
            eventPosition = pos;
            posInFile = posInF;
            eventID = id;
        }

        private Color eventColor;
        private UInt16 eventPosition;
        private string eventName;
        private int eventNameIndex;
        private int posInFile;
        private int eventID;

        /// <summary>
        /// 预定义事件ID，用于寻找事件用。值为int形，为预定义事件存储在.nat文件中的顺序，从1开始
        /// </summary>
        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        /// <summary>
        /// 预定义事件所在文件里的位置
        /// </summary>
        public int PosInFile
        {
            get { return posInFile; }
            set { posInFile = value; }
        }

        /// <summary>
        /// 预定义事件名称在名称数组里的索引
        /// </summary>
        public int EventNameIndex
        {
            get { return eventNameIndex; }
            set { eventNameIndex = value; }
        }
        /// <summary>
        /// 事件的颜色
        /// -- by lxl
        /// </summary>
        public Color EventColor
        {
            get { return eventColor; }
            set { eventColor = value; }
        }
        /// <summary>
        /// 事件所在的点的位置
        /// -- by lxl
        /// </summary>
        public UInt16 EventPosition
        {
            get { return eventPosition; }
            set { eventPosition = value; }
        }

        /// <summary>
        /// 预定义事件的名称
        /// </summary>
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

        /// <summary>
        /// 初始化预定义事件的名称项与颜色项
        /// </summary>
        /// <param name="length"></param>
        /// <param name="name"></param>
        public static void InitPreDefineEventNameWithArray(int length, string[] name, Color[] clr)
        {
            
            preDefineEventNameArray = new string[length + 1];
            preDefineEventColorArray = new Color[length + 1];
            for (int i = 0; i < length; i++)
            {
                preDefineEventNameArray[i] = name[i];
                preDefineEventColorArray[i] = clr[i];
            }

            //最后添加一个额外的stop事件
            preDefineEventNameArray[length] = "Stop";
            preDefineEventColorArray[length] = Color.Red;
        }
    }

    /// <summary>
    /// 自定义事件类，包含颜色，名称（string类型字符串），和点的位置（第几个点）
    /// -- by lxl
    /// </summary>
    public class CustomEvent
    {
        /// <summary>
        /// 构造一个自定义事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="pos">事件所在位置</param>
        /// <param name="i">事件的颜色索引</param>
        public CustomEvent(string name,UInt16 pos,int i)
        {
            eventName = name; eventPosition = pos; eventColorIndex = i;
        }

        /// <summary>
        /// 自定义事件可选颜色
        /// </summary>
        private static Color[] customEventColor = new Color[20] { Color.FromArgb(128, 0, 0), Color.FromArgb(128, 128, 0), Color.FromArgb(0, 128, 0), Color.FromArgb(0, 128, 128), Color.FromArgb(0, 0, 128),
        Color.FromArgb(128, 0, 128),Color.FromArgb(0,128,255),Color.FromArgb(64,64,255),Color.FromArgb(128,0,255),Color.FromArgb(128,64,0),Color.FromArgb(255,0,0),Color.FromArgb(255,255,0),Color.FromArgb(0,255,0),
        Color.FromArgb(0,255,255),Color.FromArgb(0,0,255),Color.FromArgb(255,0,255),Color.FromArgb(128,255,0),Color.FromArgb(0,64,255),Color.FromArgb(255,0,128),Color.FromArgb(255,128,0)};

        public static Color[] CustomEventColor
        {
            get { return CustomEvent.customEventColor; }
        }

        private int eventColorIndex;
        private UInt16 eventPosition;
        private string eventName;

        /// <summary>
        /// 颜色
        /// -- by lxl
        /// </summary>
        public int EventColorIndex
        {
            get { return eventColorIndex; }
            set { eventColorIndex = value; }
        }
        /// <summary>
        /// 事件所在的点的位置
        /// -- by lxl
        /// </summary>
        public UInt16 EventPosition
        {
            get { return eventPosition; }
            set { eventPosition = value; }
        }
        /// <summary>
        /// 自定义事件的名字
        /// -- by lxl
        /// </summary>
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

    }

    public struct EventProperty
    {
        /// <summary>
        /// 采集开始时间，精确到日
        /// 54 00 最后4个字节 
        /// </summary>
        private DateTime startDay;

        /// <summary>
        /// 添加病例时点击“脑电图采集”按钮的时间，格式：秒分时
        /// 第一个 54 01 最后4个字节
        /// </summary>
        private TimeSpan colletTime;

        /// <summary>
        /// 采集时点击“退出”按钮的时间，格式：秒分时
        /// 第二个 54 01 最后4个字节
        /// </summary>
        private TimeSpan exitTime;

        /// <summary>
        /// 采集的总点数
        /// 54 02 最后4个字节
        /// </summary>
        private int numberOfSamples;

        /// <summary>
        /// 采集时点击“记录”按钮的时间，格式：秒分时
        /// 54 03 最后4个字节
        /// </summary>
        private List<DateTime> beginningTime;

        /// <summary>
        /// 本次记录之前采集数据的总点数
        /// 54 04 最后四个字节
        /// </summary>
        private List<int> recordQuantity;

        /// <summary>
        /// 本次记录之前采集数据的总秒数
        /// t =M/N,M为本次记录之前采集数据的总点数,N为每秒采样的点数
        /// </summary>
        private List<TimeSpan> recordTime;

        #region 访问器

        public DateTime StartDay
        {
            get { return startDay; }
            set { startDay = value; }
        }

        public TimeSpan ColletTime
        {
            get { return colletTime; }
            set { colletTime = value; }
        }


        public TimeSpan ExitTime
        {
            get { return exitTime; }
            set { exitTime = value; }
        }

        public int NumberOfSamples
        {
            get { return numberOfSamples; }
            set { numberOfSamples = value; }
        }

        public List<DateTime> BeginningTime
        {
            get { return beginningTime; }
            set { beginningTime = value; }
        }

        public List<int> RecordQuantity
        {
            get { return recordQuantity; }
            set { recordQuantity = value; }
        }

        public List<TimeSpan> RecordTime
        {
            get { return recordTime; }
            set { recordTime = value; }
        }
        #endregion

    }
}