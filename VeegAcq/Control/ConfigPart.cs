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
            #region 导联配置参数  --by zt
            commonDataPool.LeadSources = xmlConfig.LeadSources;
            commonDataPool.LeadConfigLists = xmlConfig.LeadConfigLists;
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

            if (configManage.SaveToFile("config", xmlConfig) == false)
            {
                 System.Windows.Forms.MessageBox.Show("写XML配置文件失败！");
            }
        }


    }
}
