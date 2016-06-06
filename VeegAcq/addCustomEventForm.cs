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
    /// 添加自定义事件FORM
    /// -- by lxl
    /// </summary>
    public partial class addCustomEventForm : Form
    {
        CustomEventForm myCustomEventForm;

        public addCustomEventForm(CustomEventForm form)
        {
            InitializeComponent();
            myCustomEventForm = form;
            btn_color.BackColor = colorDialog.Color = Color.Blue;
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭form
            this.Close();
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //对所输入的事件描述进行判断
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("事件描述不能为空");
                return;
            }

            //开始添加事件
            myCustomEventForm.StartAddEvent(colorDialog.Color, nameTextBox.Text);

            //form隐藏
            this.Hide();
        }
        
        /// <summary>
        /// 颜色按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            //弹出颜色设置对话框，并选择颜色。选好后将按钮的颜色设置为选择好的颜色
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_color.BackColor = colorDialog.Color;
            }
        }
    }
}
