using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// Controller类  --by zt
    /// </summary>
    public partial class VeegControl
    {
        /// <summary>
        /// 回放Form
        /// </summary>
        public PlaybackForm myPlaybackForm;

        /// <summary>
        /// 初始化Controller
        /// </summary>
        /// <param name="playbackForm">回放Form</param>
        public VeegControl(PlaybackForm playbackForm) 
        {
            this.myPlaybackForm = playbackForm;
            ModuleInitial(); 
        }

        /// <summary>
        /// 初始化各自模块
        /// </summary>
        private void ModuleInitial()
        {
            //公共数据初始化
            CommonDataInit();

            //配置模块初始化
            ConfigInit();
        }

        /// <summary>
        /// 退出时，保存配置文件
        /// </summary>
        public void PlaybackQuit() 
        {
            SaveXmlConfig();
        }
    }
}
