using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// 公共数据池模块  --by zt
    /// </summary>
    public partial class VeegControl
    {
        /// <summary>
        /// 公共数据池
        /// </summary>
        private CommonData commonDataPool;

        

        /// <summary>
        /// 初始化公共数据池
        /// </summary>
        private void CommonDataInit() 
        {
            commonDataPool = new CommonData();
        }

        #region  获取公共数据池中的数据
        public CommonData CommonDataPool
        {
            get { return commonDataPool; }
            set { commonDataPool = value; }
        }

        //public CommonData GetCommonData()
        //{
        //    return this.commonDataPool;
        //}
        #endregion
    }
}
