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
        private static Color[] preDefineEventColorArray;

        public static Color[] PreDefineEventColorArray
        {
            get { return PreDefineEvent.preDefineEventColorArray; }
        }

        private static string[] preDefineEventNameArray;

        public static string[] PreDefineEventNameArray
        {
            get { return PreDefineEvent.preDefineEventNameArray; }
        }

        public PreDefineEvent(int index, double pos)
        {
            if (preDefineEventColorArray == null || preDefineEventNameArray == null)
            {
                System.Windows.Forms.MessageBox.Show("请先初始化预定义事件名称数组和颜色数组");
                return;
            }
            eventName = preDefineEventNameArray[index];
            eventColor = preDefineEventColorArray[index];
            eventPosition = pos;
        }

        private Color eventColor;
        private double eventPosition;
        //private PreDefineEventsName eventName;
        private string eventName;
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
        public double EventPosition
        {
            get { return eventPosition; }
            set { eventPosition = value; }
        }
        /// <summary>
        /// 预定义事件的描述，值为pdEvents类型
        /// -- by lxl
        /// </summary>
        //public PreDefineEventsName EventName
        //{
        //    get { return eventName; }
        //    set { eventName = value; }
        //}

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
            preDefineEventNameArray = new string[length];
            preDefineEventColorArray = new Color[length];
            for (int i = 0; i < length; i++)
            {
                preDefineEventNameArray[i] = name[i];
                preDefineEventColorArray[i] = clr[i];
            }
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
        public CustomEvent(string name,double pos,int i)
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
        private double eventPosition;
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
        public double EventPosition
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
}