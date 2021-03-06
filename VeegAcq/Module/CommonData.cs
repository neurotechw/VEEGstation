﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// 公共数据池  --by zt
    /// </summary>
    public partial class CommonData
    {
        #region 定义配置文件中保存的变量

        #region 导联配置
        /// <summary>
        /// 导联源
        /// </summary>
        public Hashtable LeadSources;

        /// <summary>
        /// 导联配置
        /// </summary>
        public Hashtable LeadConfigLists;

        #endregion

        #region 画图配置

        /// <summary>
        /// Y轴每格多少毫米
        /// -- by lxl
        /// </summary>
        private double mmPerYGrid;

        /// <summary>
        /// 表格每页多少毫米
        /// -- by lxl
        /// </summary>
        private double pixelPerMM;

        /// <summary>
        /// 时间基准
        /// -- by lxl
        /// </summary>
        private int timeStandard;

        /// <summary>
        /// 灵敏度
        /// -- by lxl
        /// </summary>
        private int sensitivity;

        /// <summary>
        /// EEG文件所在文件夹路径
        /// -- by lxl
        /// </summary>
        private string eegDataPath;

        /// <summary>
        /// 视频文件所在文件夹基础路径
        /// -- by lxl
        /// </summary>
        private string videoPath;

        public string VideoPath
        {
            get { return videoPath; }
            set { videoPath = value; }
        }

        public string EegDataPath
        {
            get { return eegDataPath; }
            set { eegDataPath = value; }
        }

        public int Sensitivity
        {
            get { return sensitivity; }
            set { sensitivity = value; }
        }

        public int TimeStandard
        {
            get { return timeStandard; }
            set { timeStandard = value; }
        }

        public double PixelPerMM
        {
            get { return pixelPerMM; }
            set { pixelPerMM = value; }
        }

        public double MMPerYGrid
        {
            get { return mmPerYGrid; }
            set { mmPerYGrid = value; }
        }
        #endregion

        #endregion

        /// <summary>
        /// 获得当前硬件配置的导联源
        /// 数组[0]为默认导联源，数组[1]为自定义导联源
        /// </summary>
        /// <param name="config">硬件配置名称</param>
        /// <returns></returns>
        public ArrayList GetLeadSource(string config) 
        {
            return (ArrayList)LeadSources[config];
        }

        /// <summary>
        /// 获得当前硬件配置的导联配置
        /// </summary>
        /// <param name="config">硬件配置名称</param>
        /// <returns></returns>
        public Hashtable GetLeadList(string config) 
        {
            return (Hashtable)LeadConfigLists[config];
        }
    }
}
