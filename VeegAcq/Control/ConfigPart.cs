using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// 配置文件模块  --by zt
    /// </summary>
    public partial class VeegControl
    {
        #region 变量
        /// <summary>
        /// 配置文件实例
        /// </summary>
        private VeegConfig xmlConfig;

        /// <summary>
        /// 用于序列化和反序列化配置
        /// </summary>
        private IVeegFileSave<VeegConfig> configManage;
        #endregion

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private void ConfigInit() 
        {
            configManage = new VeegFileSave<VeegConfig>();
            try 
            {
                xmlConfig = configManage.GetFromFile("config");
                ImportXmlData();
            }
            catch (Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show("读配置文件信息出错！");
                if (xmlConfig == null) 
                {
                    xmlConfig = new VeegConfig();
                    xmlConfig.SetDefaultConfig();
                    configManage.SaveToFile("config", xmlConfig);
                    ImportXmlData();
                }
            }
        }

        /// <summary>
        /// 导入配置文件初始化公共数据池
        /// </summary>
        private void ImportXmlData()
        {
            //获取文件夹路径 -- by lxl
            commonDataPool.EegDataPath = xmlConfig.EegDataPath;
            commonDataPool.VideoPath = xmlConfig.VideoBasePath;

            #region 导联配置参数  --by zt
            commonDataPool.LeadSources = xmlConfig.LeadSources;
            commonDataPool.LeadConfigLists = xmlConfig.LeadConfigLists;
            #endregion

            #region 画图参数

            commonDataPool.MMPerYGrid = xmlConfig.MMPerYGrid;
            commonDataPool.PixelPerMM = xmlConfig.PixelPerMM;
            commonDataPool.TimeStandard = xmlConfig.TimeStandard;
            commonDataPool.Sensitivity = xmlConfig.Sensitivity;

            #endregion
        }

        /// <summary>
        /// 公共数据池数据导入配置文件
        /// </summary>
        public void SaveXmlConfig() 
        {
            if (xmlConfig == null)
                return;
            #region 导联配置参数  --by zt
            xmlConfig.LeadSources = commonDataPool.LeadSources;
            xmlConfig.LeadConfigLists = commonDataPool.LeadConfigLists;
            //xmlConfig.leadList = commonDataPool.leadList;
            //xmlConfig.leadSource = commonDataPool.leadSource;
            //xmlConfig.defaultLeadSource = commonDataPool.defaultLeadSource;
            //xmlConfig.currentLeadConfig = commonDataPool.currentLeadConfig;
            //xmlConfig.demarcateCV = commonDataPool.demarcateCV;
            #endregion  

            //保存文件夹路径
            xmlConfig.EegDataPath = commonDataPool.EegDataPath;
            xmlConfig.VideoBasePath = commonDataPool.VideoPath;

            #region 画图参数

            xmlConfig.MMPerYGrid = commonDataPool.MMPerYGrid;
            xmlConfig.PixelPerMM = commonDataPool.PixelPerMM;
            xmlConfig.Sensitivity = commonDataPool.Sensitivity;
            xmlConfig.TimeStandard = commonDataPool.TimeStandard;

            #endregion
            if (configManage.SaveToFile("config", xmlConfig) == false)
            {
                 System.Windows.Forms.MessageBox.Show("写XML配置文件失败！");
            }
        }


    }
}
