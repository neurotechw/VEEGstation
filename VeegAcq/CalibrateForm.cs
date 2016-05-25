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

        public CalibrateForm(PlaybackForm form)
        {
            InitializeComponent();
            this.pbform = form;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            pbform.calibrateY(int.Parse(this.textBox1.Text));
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
