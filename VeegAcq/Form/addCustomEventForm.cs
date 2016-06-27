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
        PlaybackForm parentForm;

        /// <summary>
        /// 所选择的事件索引
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// 所选事件颜色的编号
        /// </summary>
        private int colorIndex;

        /// <summary>
        /// 表示现在是在编辑事件还是添加事件
        /// </summary>
        private bool addOrEdit;

        /// <summary>
        /// 个设置颜色的按钮组
        /// </summary>
        Button[] colorButton;

        public addCustomEventForm(PlaybackForm form,int index)
        {
            colorButton = new Button[CustomEvent.CustomEventColor.Count()];
            InitializeComponent();
            selectedIndex=index;
            parentForm = form;
            InitColorButton();
            colorIndex = -1;
        }

        public addCustomEventForm(PlaybackForm form)
        {
            colorButton = new Button[CustomEvent.CustomEventColor.Count()];
            InitializeComponent();
            parentForm = form;
            InitColorButton();
            colorIndex = -1;
        }

        /// <summary>
        /// 按照颜色列表初始化颜色选择按钮
        /// </summary>
        private void InitColorButton()
        {
            //根据20个颜色初始化颜色按钮
            for (int i = 0; i < CustomEvent.CustomEventColor.Count(); i++)
            {
                colorButton[i] = new Button();
                colorButton[i].BackColor = CustomEvent.CustomEventColor[i];
                colorButton[i].Location = new Point(0 + i % 10 * this.buttonPanel.Width / 10, 0 + i / 10 * this.buttonPanel.Height / 2);
                colorButton[i].Size = new Size(this.buttonPanel.Width / 10, this.buttonPanel.Height / 2);
                colorButton[i].Name = i.ToString();
                colorButton[i].Click += new EventHandler(this.btnColor_Click);
                this.buttonPanel.Controls.Add(colorButton[i]);
            }
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
            this.Dispose();
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //如果这个form为添加事件的状态
            if (addOrEdit)
            {
                //对所输入的事件描述进行判断
                if (nameTextBox.Text == "")
                {
                    MessageBox.Show("事件描述不能为空");
                    return;
                }

                //对所选择的颜色进行判断
                if (colorIndex == -1)
                {
                    MessageBox.Show("请选择一个颜色");
                    return;
                }

                //开始添加事件
                parentForm.StartAddEvents(colorIndex, nameTextBox.Text);

                //form隐藏
                this.Close();
                this.Dispose();
            }
            else 
            {
                //对所输入的事件描述进行判断
                if (nameTextBox.Text == "")
                {
                    MessageBox.Show("事件描述不能为空");
                    return;
                }

                if (colorIndex == -1)
                {
                    MessageBox.Show("请选择一个颜色");
                    return;
                }

                //将编辑后的事件保存
                parentForm.EditCustomEvent(selectedIndex, nameTextBox.Text, colorIndex);

                //form隐藏
                this.Close();
                this.Dispose();
            }
        }

        /// <summary>
        /// 状态置为添加事件
        /// </summary>
        public void IsAddEvent()
        {
            addOrEdit = true;
        }

        /// <summary>
        /// 状态为编辑事件
        /// </summary>
        /// <param name="colorIndex">选中事件的颜色</param>
        /// <param name="text">选中事件的名称</param>
        public void IsEditEvent(int clrIndex,string text)
        {
            addOrEdit = false;
            //this.btn_color.BackColor =  clr;
            //this.buttonPanel.Controls.Find(clrIndex.ToString(), false)[0].Enabled = false;
            colorButton[clrIndex].Enabled = false;
            this.colorIndex = clrIndex;
            this.nameTextBox.Text = text;
        }
        
        /// <summary>
        /// 颜色按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            //将当前选择的按钮enable设置为false，并把之前按钮的enable设置为true，（这样可以标识哪个按钮当前被选中）
            if (colorIndex != -1)
            {
                colorButton[colorIndex].Enabled = true;
            }
            btn.Enabled = false;

            //根据所选的按钮编号设置当前颜色编号
            this.colorIndex = int.Parse(btn.Name);

            //选择颜色之后让输入框变为选中状态
            this.nameTextBox.Select();
        }
    }
}