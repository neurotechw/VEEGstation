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
    public partial class addCustomEventForm : Form
    {
        customEventForm customEventForm;
        public addCustomEventForm(customEventForm form)
        {
            InitializeComponent();
            customEventForm = form;
            btn_color.BackColor = colorDialog.Color = Color.Blue;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("事件描述不能为空");
                return;
            }
            customEventForm.startAddEvent(colorDialog.Color, nameTextBox.Text);
            this.Hide();
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_color.BackColor = colorDialog.Color;
            }
        }
    }
}
