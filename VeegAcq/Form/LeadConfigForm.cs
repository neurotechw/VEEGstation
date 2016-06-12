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

        /// <summary>
        /// 添加导联Form
        /// </summary>
        private LeadAddForm myLeadAddForm;

        #endregion

        public LeadConfigForm()
        {
            InitializeComponent();
            this.txtName.Enabled = false;
            //this.rtxtNote.Enabled = false;
            myLeadSourceSettingForm = new LeadSourceSettingForm();
            myLeadAddForm = new LeadAddForm(this);
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="control"></param>
        public void ReigisterVeegControl(VeegControl control)
        {
            this.controller = control;
            myLeadSourceSettingForm.ReigisterVeegControl(control);
            myLeadAddForm.ReigisterVeegControl(control);
        }

        /// <summary>
        /// 初始化LeadConfig
        /// </summary>
        public void InitLeadConfig()
        {
            //硬件配置名称
            cbConfigList.Text = cbConfigList.Items[0].ToString();
            //初始化表格
            InitList(cbConfigList.Text);
        }

        /// <summary>
        /// 初始化导联列表
        /// </summary>
        /// <param name="p">硬件配置名称</param>
        private void InitList(string config)
        {
            
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            myLeadSourceSettingForm.InitLeadSetting(cbConfigList.Text);
            myLeadSourceSettingForm.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            myLeadAddForm.InitLeadAdd(cbConfigList.Text);
            myLeadAddForm.ShowDialog();
        }
    }
}
