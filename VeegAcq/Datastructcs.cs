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

    //导联配置结构
    public class Montage
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

        public Montage(int num)
        {
            FirstName = new string[num];
            SecondName = new string[num];
            FirstID = new string[num];
            SecondID = new string[num];
        }
    }

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
