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
    public partial class CalibrateForm : Form
    {
        private PlaybackForm pbform;
        private int height;

        public CalibrateForm(PlaybackForm form)
        {
            InitializeComponent();
            height = 20;
            this.valueBox.Value = height;
            this.pbform = form;
        }

        private void draw(object sender, PaintEventArgs e)
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

        private void valueChanged(object sender, EventArgs e)
        {
            height = int.Parse(this.valueBox.Value.ToString());
            this.linePanel.Invalidate();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            pbform.calibrateY(height / 5D);          //height / 5 每一厘米多少像素点
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
