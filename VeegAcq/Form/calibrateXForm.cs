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
    /// 校准X轴的FORM
    /// -- by lxl
    /// </summary>
    public partial class CalibrateXForm : Form
    {
        /// <summary>
        /// 量取的宽度
        /// </summary>
        private int width;
        private PlaybackForm myPlayBackform;

        public CalibrateXForm(PlaybackForm form,double wid)
        {
            InitializeComponent();

            //初始化一个默认的VALUE,(以后需要改成从playbackform中读取）
            width = (int)(wid * 5);
            this.valueBox.Value = width;
            this.myPlayBackform = form;
        }

        /// <summary>
        /// 线段的重绘函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(Pens.Black, new Point(0, 10), new Point(width, 10));
            g.DrawLine(Pens.Black, new Point(0, 10), new Point(0, 6));
            g.DrawLine(Pens.Black, new Point(1 * width / 5, 10), new Point(1 * width / 5, 6));
            g.DrawLine(Pens.Black, new Point(2 * width / 5, 10), new Point(2 * width / 5, 6));
            g.DrawLine(Pens.Black, new Point(3 * width / 5, 10), new Point(3 * width / 5, 6));
            g.DrawLine(Pens.Black, new Point(4 * width / 5, 10), new Point(4 * width / 5, 6));
            g.DrawLine(Pens.Black, new Point(5 * width / 5, 10), new Point(5 * width / 5, 6));
        }

        /// <summary>
        /// valueBox值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valueChanged(object sender, EventArgs e)
        {
            width = int.Parse(this.valueBox.Value.ToString());

            //重绘线段，更改线段长度
            this.linePanel.Invalidate();
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            //通过量好的长度得出每厘米的像素点，然后配置图表大小，height / 5 = 每一厘米多少像素点
            myPlayBackform.CalibrateX(width / 5D);

            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
