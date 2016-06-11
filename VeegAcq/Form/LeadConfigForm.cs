using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VeegStation
{
    /// <summary>
    /// 导联配置Form  --by zt
    /// </summary>
    public partial class LeadConfigForm : Form
    {
        #region 变量
        /// <summary>
        /// Controller  
        /// </summary>
        private VeegControl controller;
        
        /// <summary>
        /// 导联配置Form
        /// </summary>
        private LeadSourceSettingForm myLeadSourceSettingForm;


        #endregion

        public LeadConfigForm()
        {
            InitializeComponent();
            this.txtName.Enabled = false;
            this.rtxtNote.Enabled = false;
            myLeadSourceSettingForm = new LeadSourceSettingForm();
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="control"></param>
        public void ReigisterVeegControl(VeegControl control)
        {
            this.controller = control;
            myLeadSourceSettingForm.ReigisterVeegControl(control);
        }

        /// <summary>
        /// 初始化LeadConfig
        /// </summary>
        public void InitLeadConfig()
        {
            //待验证
            cbConfigList.Text = cbConfigList.Items[0].ToString();
            //初始化
            
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            myLeadSourceSettingForm.InitLeadSetting(cbConfigList.Text);
            myLeadSourceSettingForm.ShowDialog();
        }
    }
}
