using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeegStation
{
    

    /// <summary>
    /// 标志类
    /// </summary>
    public class EEGFmt
    {
        //标志名
        public string Name;//标志

        //偏移量
        public int Off;
    };


    /// <summary>
    ///事件颜色
    /// </summary>
    public class EventColor
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="red">红色</param>
        /// <param name="green">绿色</param>
        /// <param name="blue">蓝色</param>
        public EventColor(byte red, byte green, byte blue)
        {
            //设置红色
            R = red;

            //设置绿色
            G = green;

            //设置蓝色
            B = blue;
        }

        //声明红色变量
        public byte R;

        //声明绿色变量
        public byte G;

        //声明蓝色变量
        public byte B;
    }

    /// <summary>
    /// 事件头类
    /// </summary>
    public class EventHead
    {
        //事件标识
        public string Sign;

        //事件数目
        public int Number;                 
    };

   /// <summary>
   /// 事件类
   /// </summary>
    public class Event
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public Event(string name, EventColor color)
        {
            //设置姓名
            Name = name;

            //设置颜色
            Color = color;
        }

        //声明存储事件名变量
        public string Name;

        //声明存储事件颜色变量
        public EventColor Color;
    } ;
}
