using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// CFG块  --by zt
    /// </summary>
    public class CFGInfo
    {
        #region 变量
        /// <summary>
        /// 标志位,[CFG]
        /// </summary>
        private string sign;

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
        private TimeSpan[] beginningTime;
        
        /// <summary>
        /// 本次记录之前采集数据的总秒数，格式
        /// </summary>
        private TimeSpan[] recordTime;
        #endregion

        #region 访问器
        public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }

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

        public TimeSpan[] BeginningTime
        {
            get { return beginningTime; }
            set { beginningTime = value; }
        }

        public TimeSpan[] RecordTime
        {
            get { return recordTime; }
            set { recordTime = value; }
        }
        #endregion

        public CFGInfo(FileStream fs) 
        {
            ParseCFGInfo(fs);
        }

        private void ParseCFGInfo(FileStream fs) 
        {
            //解析CFG区信息
        }
    }
}
