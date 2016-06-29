using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// Config接口  --by zt
    /// </summary>
    interface IVeegConfig 
    {
        void SetDefaultConfig();
    }


    /// <summary>
    /// Config类   --by zt
    /// </summary>
    [Serializable]
    class VeegConfig : IVeegConfig
    {
        #region 定义配置文件中保存的变量

        #region 导联参数
        /// <summary>
        /// 导联源
        /// </summary>
        public Hashtable LeadSources;

        /// <summary>
        /// 导联配置
        /// </summary>
        public Hashtable LeadConfigLists;
        #endregion

        #region 画图参数
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

        public void SetDefaultConfig()
        {
            #region 导联参数
            string[] leadSourceNames = new string[7] { "8导脑电", "8导脑电+多参数", "16导脑电", "16导脑电+多参数", "24导脑电", "32导脑电", "32导脑电+多参数" };
            #region 导联源
            LeadSources = new Hashtable();
            #region 8导脑电导联源
            ArrayList leadSourceList8 = new ArrayList();

            //8导默认导联源
            Hashtable defaultLeadSource8 = new Hashtable();
            string[] lead8Names = new string[10] { "Fp1", "Fp2", "T3", "T4", "C3", "C4", "O1", "O2", "A1", "A2" };
            for (int i = 0; i < lead8Names.Length; i++) 
            {
                defaultLeadSource8.Add(i + 1, lead8Names[i]);
            }

            //8导自定义导联源
            Hashtable customLeadSource8 = new Hashtable();
            leadSourceList8.Add(defaultLeadSource8);
            leadSourceList8.Add(customLeadSource8);
            LeadSources.Add(leadSourceNames[0], leadSourceList8);
            #endregion

            #region 8导脑电+多参数导联源
            ArrayList leadSourceList8P = new ArrayList();

            //8导+多参数默认导联源
            Hashtable defaultLeadSource8P = new Hashtable();
            //string[] lead8Names = new string[10] { "Fp1", "Fp2", "T3", "T4", "C3", "C4", "O1", "O2", "A1", "A2" };
            for (int i = 0; i < lead8Names.Length; i++)
            {
                defaultLeadSource8P.Add(i + 1, lead8Names[i]);
            }

            //8导+多参数自定义导联源
            Hashtable customLeadSource8P = new Hashtable();
            leadSourceList8P.Add(defaultLeadSource8P);
            leadSourceList8P.Add(customLeadSource8P);
            LeadSources.Add(leadSourceNames[1], leadSourceList8P);
            #endregion

            #region 16导脑电导联源
            ArrayList leadSourceList16 = new ArrayList();

            //16导默认导联源
            Hashtable defaultLeadSource16 = new Hashtable();
            string[] lead16Names = new string[20] { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "Sp1", "Sp2", "A1", "A2" };
            for (int i = 0; i < lead16Names.Length; i++)
            {
                defaultLeadSource16.Add(i + 1, lead16Names[i]);
            }

            //16导自定义导联源
            Hashtable customLeadSource16 = new Hashtable();

            leadSourceList16.Add(defaultLeadSource16);
            leadSourceList16.Add(customLeadSource16);

            LeadSources.Add(leadSourceNames[2], leadSourceList16);
            #endregion

            #region 16导脑电+多参数导联源
            ArrayList leadSourceList16P = new ArrayList();

            //16导+多参数默认导联源
            Hashtable defaultLeadSource16P = new Hashtable();
            //string[] lead16Names = new string[20] { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "Sp1", "Sp2", "A1", "A2" };
            for (int i = 0; i < lead16Names.Length; i++)
            {
                defaultLeadSource16P.Add(i + 1, lead16Names[i]);
            }

            //16导+多参数自定义导联源
            Hashtable customLeadSource16P = new Hashtable();

            leadSourceList16P.Add(defaultLeadSource16P);
            leadSourceList16P.Add(customLeadSource16P);

            LeadSources.Add(leadSourceNames[3], leadSourceList16P);
            #endregion

            #region 24导脑电导联源
            ArrayList leadSourceList24 = new ArrayList();

            //24导默认导联源
            Hashtable defaultLeadSource24 = new Hashtable();
            string[] lead24Names = new string[26] { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "Fz", "Cz", "Pz", "Oz", 
                                                            "Cp3", "Cp4", "Po3", "Po4", "A1", "A2" };
            for (int i = 0; i < lead24Names.Length; i++)
            {
                defaultLeadSource24.Add(i + 1, lead24Names[i]);
            }

            //24导自定义导联源
            Hashtable customLeadSource24 = new Hashtable();

            leadSourceList24.Add(defaultLeadSource24);
            leadSourceList24.Add(customLeadSource24);

            LeadSources.Add(leadSourceNames[4], leadSourceList24);
            #endregion

            #region 32导脑电导联源
            ArrayList leadSourceList32 = new ArrayList();

            //32导默认导联源
            Hashtable defaultLeadSource32 = new Hashtable();
            string[] lead32Names = new string[36] { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6","Fz", "Cz", "Pz",
                "AF3", "AF4", "Fc3","Fc4", "Cp3", "Cp4", "Po3", "Po4", "FT7", "FT8", "Cp7", "Cp8",  "Oz", "Sp1", "Sp2", "A1", "A2" };
            for (int i = 0; i < lead32Names.Length; i++)
            {
                defaultLeadSource32.Add(i + 1, lead32Names[i]);
            }

            //32导自定义导联源
            Hashtable customLeadSource32 = new Hashtable();

            leadSourceList32.Add(defaultLeadSource32);
            leadSourceList32.Add(customLeadSource32);

            LeadSources.Add(leadSourceNames[5], leadSourceList32);
            #endregion

            #region 32导脑电+多参数导联源
            ArrayList leadSourceList32P = new ArrayList();

            //32导+多参数默认导联源
            Hashtable defaultLeadSource32P = new Hashtable();
            //string[] lead32Names = new string[36] { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "AF3", "AF4", "Fc3",
            //                                                "Fc4", "Cp3", "Cp4", "Po3", "Po4", "FT7", "FT8", "Cp7", "Cp8", "Fz", "Cz", "Pz", "Oz", "Sp1", "Sp2", "A1", "A2" };
            for (int i = 0; i < lead32Names.Length; i++)
            {
                defaultLeadSource32P.Add(i + 1, lead32Names[i]);
            }

            //32导+多参数自定义导联源
            Hashtable customLeadSource32P = new Hashtable();

            leadSourceList32P.Add(defaultLeadSource32P);
            leadSourceList32P.Add(customLeadSource32P);

            LeadSources.Add(leadSourceNames[6], leadSourceList32P);
            #endregion
            #endregion

            #region 导联配置
            LeadConfigLists = new Hashtable();

            //默认导联配置名称
            string currentLeadConfigName = "默认导联配置";
            #region 8导脑电导联配置
            Hashtable leadConfigList8 = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig8 = new ArrayList();
            for (int i = 0; i < lead8Names.Length; i++) 
            {
                if (lead8Names[i] != "") 
                {
                    defaultLeadConfig8.Add(lead8Names[i] + "-Ref");
                }
            }

            leadConfigList8.Add(currentLeadConfigName, defaultLeadConfig8);

            LeadConfigLists.Add(leadSourceNames[0], leadConfigList8);
            #endregion

            #region 8导脑电+多参数导联配置
            Hashtable leadConfigList8P = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig8P = new ArrayList();
            for (int i = 0; i < lead8Names.Length; i++)
            {
                if (lead8Names[i] != "")
                {
                    defaultLeadConfig8P.Add(lead8Names[i] + "-Ref");
                }
            }

            leadConfigList8P.Add(currentLeadConfigName, defaultLeadConfig8P);

            LeadConfigLists.Add(leadSourceNames[1], leadConfigList8P);
            #endregion

            #region 16导脑电导联配置
            Hashtable leadConfigList16 = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig16 = new ArrayList();
            for (int i = 0; i < lead16Names.Length; i++)
            {
                if (lead16Names[i] != "")
                {
                    defaultLeadConfig16.Add(lead16Names[i] + "-Ref");
                }
            }

            leadConfigList16.Add(currentLeadConfigName, defaultLeadConfig16);

            LeadConfigLists.Add(leadSourceNames[2], leadConfigList16);
            #endregion

            #region 16导脑电+多参数导联配置
            Hashtable leadConfigList16P = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig16P = new ArrayList();
            for (int i = 0; i < lead16Names.Length; i++)
            {
                if (lead16Names[i] != "")
                {
                    defaultLeadConfig16P.Add(lead16Names[i] + "-Ref");
                }
            }

            leadConfigList16P.Add(currentLeadConfigName, defaultLeadConfig16P);

            LeadConfigLists.Add(leadSourceNames[3], leadConfigList16P);
            #endregion

            #region 24导脑电导联配置
            Hashtable leadConfigList24 = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig24 = new ArrayList();
            for (int i = 0; i < lead24Names.Length; i++)
            {
                if (lead24Names[i] != "")
                {
                    defaultLeadConfig24.Add(lead24Names[i] + "-Ref");
                }
            }

            leadConfigList24.Add(currentLeadConfigName, defaultLeadConfig24);

            LeadConfigLists.Add(leadSourceNames[4], leadConfigList24);
            #endregion

            #region 32导脑电导联配置
            Hashtable leadConfigList32 = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig32 = new ArrayList();
            for (int i = 0; i < lead32Names.Length; i++)
            {
                if (lead32Names[i] != "")
                {
                    defaultLeadConfig32.Add(lead32Names[i] + "-Ref");
                }
            }

            leadConfigList32.Add(currentLeadConfigName, defaultLeadConfig32);

            LeadConfigLists.Add(leadSourceNames[5], leadConfigList32);
            #endregion

            #region 32导脑电+多参数导联配置
            Hashtable leadConfigList32P = new Hashtable();

            //默认导联配置
            ArrayList defaultLeadConfig32P = new ArrayList();
            for (int i = 0; i < lead32Names.Length; i++)
            {
                if (lead32Names[i] != "")
                {
                    defaultLeadConfig32P.Add(lead32Names[i] + "-Ref");
                }
            }

            leadConfigList32P.Add(currentLeadConfigName, defaultLeadConfig32P);

            LeadConfigLists.Add(leadSourceNames[6], leadConfigList32P);
            #endregion
            #endregion

            #region 画图参数

            mmPerYGrid = 11.5;
            pixelPerMM = 3.8;
            timeStandard = 30;
            sensitivity = 100;

            #endregion
            #endregion
        }

    }
}
