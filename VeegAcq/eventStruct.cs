using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VeegStation
{
    /// <summary>
    /// 预定义事件类，包含颜色，名称（枚举类，4选1），和点的位置（第几个点）
    /// -- by lxl
    /// </summary>
    public class PreDefineEvent
    {
        public static Color EyesOpenColor = Color.Pink;
        public static Color EyesCloseColor = Color.Yellow;
        public static Color DeepBreathColor = Color.Green;
        public static Color CalibrateColor = Color.Blue;

        public PreDefineEvent(PreDefineEventsName e,double pos)
        {
            eventName = e; eventPosition = pos;
            switch (e)
            {
                case PreDefineEventsName.eyesOpen: eventColor = EyesOpenColor; break;
                case PreDefineEventsName.eyesClose: eventColor = EyesCloseColor; break;
                case PreDefineEventsName.deepBreath: eventColor = DeepBreathColor; break;
                case PreDefineEventsName.calibrate: eventColor = CalibrateColor; break;
            }
        }

        public enum PreDefineEventsName
        {
            eyesOpen = 0x4500,
            eyesClose = 0x4501,
            deepBreath = 0x4502,
            calibrate = 0x4503
        };
        private Color eventColor;
        private double eventPosition;
        private PreDefineEventsName eventName;
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
        public PreDefineEventsName EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
    }

    /// <summary>
    /// 自定义事件类，包含颜色，名称（string类型字符串），和点的位置（第几个点）
    /// -- by lxl
    /// </summary>
    public class CustomEvent
    {
        public CustomEvent(string name,double pos,Color clr)
        {
            eventName = name; eventPosition = pos; eventColor = clr;
        }
        private Color eventColor;
        private double eventPosition;
        private string eventName;

        /// <summary>
        /// 颜色
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