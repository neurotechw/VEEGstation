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
    public partial class calibrateXForm : Form
    {
        private int width;
        private PlaybackForm pbform;
        public calibrateXForm(PlaybackForm form)
        {
            InitializeComponent();
            width = 20;
            this.valueBox.Value = width;
            this.pbform = form;
        }

        private void draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(Pens.Black, new Point(0, 10), new Point(width, 10));
            g.DrawLine(Pens.Black, new Point(0, 10), new Point(0, 6));
            g.DrawLine(Pens.Black, new Point(width, 10), new Point(width, 6));
        }

        private void valueChanged(object sender, EventArgs e)
        {
            width = int.Parse(this.valueBox.Value.ToString());
            this.linePanel.Invalidate();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            pbform.calibrateX(width);
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
