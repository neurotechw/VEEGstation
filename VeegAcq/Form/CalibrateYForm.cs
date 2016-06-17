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
    /// Y轴配置FORM
    /// -- by lxl
    /// </summary>
    public partial class CalibrateYForm : Form
    {
        private PlaybackForm myPlayBackform;
        private int height;

        public CalibrateYForm(PlaybackForm form,double hei)
        {
            InitializeComponent();

            ///初始化一个默认的VALUE,(以后需要改成从playbackform中读取）
            height = (int)(hei * 5);
            this.valueBox.Value = height;
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
            g.DrawLine(Pens.Black, new Point(0, 10), new Point(height, 10));
            g.DrawLine(Pens.Black, new Point(0, 10), new Point(0, 6));
            g.DrawLine(Pens.Black, new Point(1 * height / 5, 10), new Point(1 * height / 5, 6));
            g.DrawLine(Pens.Black, new Point(2 * height / 5, 10), new Point(2 * height / 5, 6));
            g.DrawLine(Pens.Black, new Point(3 * height / 5, 10), new Point(3 * height / 5, 6));
            g.DrawLine(Pens.Black, new Point(4 * height / 5, 10), new Point(4 * height / 5, 6));
            g.DrawLine(Pens.Black, new Point(5 * height / 5, 10), new Point(5 * height / 5, 6));
        }

        /// <summary>
        /// valueBox值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueChanged(object sender, EventArgs e)
        {
            height = int.Parse(this.valueBox.Value.ToString());
            this.linePanel.Invalidate();
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            //通过量好的长度得出每厘米的像素点，然后配置图标大小，height / 5 = 每一厘米多少像素点
            myPlayBackform.CalibrateY(height / 5D);

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
