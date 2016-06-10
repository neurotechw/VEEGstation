using System;
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
        #endregion

        #endregion

        public void SetDefaultConfig()
        {
            #region 导联参数
            #endregion
        }

    }
}
