using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeegStation
{
    
    //标志的结构
    public class EEGFmt
    {
        public string Name;//标志
        public int Off;
    };

    //事件颜色
    public class EventColor
    {
        public EventColor(byte red, byte green, byte blue)
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
    public class EventHead
    {
        public string Sign;
        public int Number;                  //事件数目
    };

    //结构EVENT
    public class Event
    {
        public Event(string name, EventColor color)
        {
            Name = name;
            Color = color;
        }
        public string Name;
        public EventColor Color;
    } ;
}
