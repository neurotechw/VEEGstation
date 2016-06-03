using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VeegStation
{
    public class preDefineEvent
    {
        public preDefineEvent(pdEvents e,double pos)
        {
            _event = e; _pos = pos;
            switch (e)
            {
                case pdEvents.eyesOpen: _color = Color.Pink; break;
                case pdEvents.eyesClose: _color = Color.Yellow; break;
                case pdEvents.deepBreath: _color = Color.Green; break;
                case pdEvents.calibrate: _color = Color.Blue; break;
            }
        }

        public enum pdEvents
        {
            eyesOpen = 0x4500,
            eyesClose = 0x4501,
            deepBreath = 0x4502,
            calibrate = 0x4503
        };
        private Color _color;
        private double _pos;
        private pdEvents _event;
        /// <summary>
        /// 事件的颜色
        /// -- by lxl
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        /// <summary>
        /// 事件所在的点的位置
        /// -- by lxl
        /// </summary>
        public double PointPosition
        {
            get { return _pos; }
            set { _pos = value; }
        }
        /// <summary>
        /// 预定义事件的描述，值为pdEvents类型
        /// -- by lxl
        /// </summary>
        public pdEvents Event
        {
            get { return _event; }
            set { _event = value; }
        }
    }

    public class customEvent
    {
        public customEvent(string name,double pos,Color clr)
        {
            _name = name; _pos = pos; _color = clr;
        }
        private Color _color;
        private double _pos;
        private string _name;
        /// <summary>
        /// 颜色
        /// -- by lxl
        /// </summary>
        public Color Color
        {
            get { return Color; }
            set { Color = value; }
        }
        /// <summary>
        /// 事件所在的点的位置
        /// -- by lxl
        /// </summary>
        public double PointPostion
        {
            get { return _pos; }
            set { _pos = value; }
        }
        /// <summary>
        /// 自定义事件的名字
        /// -- by lxl
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
