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
    public partial class BandFilterForm : Form
    {
        private PlaybackForm myPlaybackForm;
        private bool isFirst = true;
        /// <summary>
        /// 带通滤波form  --by zt
        /// </summary>
        public BandFilterForm(PlaybackForm parentForm)
        {
            InitializeComponent();
            this.myPlaybackForm = parentForm;
            textBox_lowFrequency.MaxLength = 3;
            textBox_highFrequency.MaxLength = 3;
            isFirst = false;
        }

        /// <summary>
        /// 记录现在的滤波参数设置
        /// </summary>
        public void InitFormFilter()
        {
            if (!isFirst)
            {
                checkBox_bandFilter.Checked = myPlaybackForm.IsBandFilter; 
                groupBox_bandFilter.Enabled = myPlaybackForm.IsBandFilter;
                textBox_lowFrequency.Text = myPlaybackForm.LowFrequency.ToString();
                textBox_highFrequency.Text = myPlaybackForm.HighFrequency.ToString();
            }

        }

        private void textBox_lowFrequency_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }

        private void textBox_highFrequency_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void checkBox_bandFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_bandFilter.Checked == true)
            {
                groupBox_bandFilter.Enabled = true;
                
            }
            else
            {
                if (checkBox_bandFilter.Checked == false)
                {
                    groupBox_bandFilter.Enabled = false;
                }
            }
        }

        private void FormBandFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.myPlaybackForm.SetBandFilterChecked();
            this.Hide();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (checkBox_bandFilter.Checked == true)
            {
                if (string.IsNullOrEmpty(textBox_lowFrequency.Text) || string.IsNullOrEmpty(textBox_highFrequency.Text))
                {
                    MessageBox.Show("请输入滤波器的高低频率！", "提示", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    int low = Convert.ToInt32(textBox_lowFrequency.Text);
                    int high = Convert.ToInt32(textBox_highFrequency.Text);
                    if (low > high)
                    {
                        MessageBox.Show("低通频率大于高通频率，请重新选择。", "提示", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        this.myPlaybackForm.IsBandFilter = true;
                        this.myPlaybackForm.LowFrequency = low;
                        this.myPlaybackForm.HighFrequency = high;
                    }
                }
            }
            else
            {
                this.myPlaybackForm.IsBandFilter = false;
            }
            this.myPlaybackForm.SetBandFilterChecked();
            this.myPlaybackForm.ShowData();
            this.Hide();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            textBox_lowFrequency.Text = this.myPlaybackForm.LowFrequency.ToString();
            textBox_highFrequency.Text = this.myPlaybackForm.HighFrequency.ToString();
            this.myPlaybackForm.SetBandFilterChecked();
            this.Hide();
        }
    }
}
