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
    public partial class customEventForm : Form
    {
        PlaybackForm pbForm;
        addCustomEventForm addEventForm;
        public customEventForm(PlaybackForm form)
        {
            InitializeComponent();
            pbForm = form;
        }

        public void initList()
        {
            int index = 1;
            eventList.BeginUpdate();
            eventList.Items.Clear();
            foreach (customEvent p in pbForm.getCustomEventList())
            {
                ListViewItem li = new ListViewItem(p.Event);
                li.SubItems.Add(pbForm.getStartTime().AddSeconds((int)(p.PointPosition / pbForm.getSampleRate())).ToLongTimeString());
                li.SubItems.Add(index.ToString());
                index++;
                eventList.Items.Add(li);
            }
            eventList.EndUpdate();
        }

        /// <summary>
        /// 退出按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 添加事件点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (addEventForm == null)
                addEventForm = new addCustomEventForm(this);
            addEventForm.ShowDialog();
        }

        public void startAddEvent(Color clr, string name)
        {
            pbForm.startAddEvents(false, clr, name);
        }
        /// <summary>
        /// 更新listView内容
        /// -- by lxl
        /// </summary>
        /// <param name="isAdded">是添加事件还是删除事件</param>
        public void updateListView(bool isAdded)
        {
            if (isAdded)
            {
                ListViewItem li = new ListViewItem(pbForm.getCustomEventList()[pbForm.getCustomEventList().Count - 1].Event);
                li.SubItems.Add(pbForm.getStartTime().AddSeconds((int)(pbForm.getCustomEventList()[pbForm.getCustomEventList().Count - 1].PointPosition / pbForm.getSampleRate())).ToLongTimeString());
                li.SubItems.Add(pbForm.getCustomEventList().Count.ToString());
                eventList.Items.Add(li);
            }
            else
            {
                for (int i = eventList.SelectedIndices[0]; i <= pbForm.getCustomEventList().Count; i++)
                {
                    eventList.Items[i].SubItems[2].Text = (int.Parse(eventList.Items[i].SubItems[2].Text) - 1).ToString();
                }
                eventList.Items.RemoveAt(eventList.SelectedIndices[0]);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            pbForm.removeEvent(false, eventList.SelectedIndices[0]);
        }
    }
}
