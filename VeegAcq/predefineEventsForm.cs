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
    public partial class predefineEventsForm : Form
    {
        PlaybackForm pbForm;
        public predefineEventsForm(PlaybackForm form)
        {
            InitializeComponent();
            pbForm = form;
        }

        public void initList()
        {
            string name;
            int index = 1;
            eventList.BeginUpdate();
            eventList.Items.Clear();
            foreach (preDefineEvent p in pbForm.getEventList())
            {
                switch (p.Event)
                {
                    case preDefineEvent.pdEvents.eyesOpen: name = "睁眼"; break;
                    case preDefineEvent.pdEvents.eyesClose: name = "闭眼"; break;
                    case preDefineEvent.pdEvents.deepBreath: name = "深呼吸"; break;
                    case preDefineEvent.pdEvents.calibrate: name = "定标"; break;
                    default: name = ""; break;
                }
                ListViewItem li = new ListViewItem(name);
                li.SubItems.Add(pbForm.getStartTime().AddSeconds((int)(p.PointPosition / pbForm.getSampleRate())).ToLongTimeString());
                li.SubItems.Add(index.ToString());
                index++;
                eventList.Items.Add(li);
            }
            eventList.EndUpdate();
        }

        /// <summary>
        /// 退出点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Quit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 添加事件点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEvent_Click(object sender, EventArgs e)
        {
            pbForm.startAddEvents(true, Color.Yellow, preDefineEvent.pdEvents.eyesOpen);
        }

        public void updateListView()
        {
            string name;
            switch (pbForm.getEventList()[pbForm.getEventList().Count-1].Event)
            {
                case preDefineEvent.pdEvents.eyesOpen: name = "睁眼"; break;
                case preDefineEvent.pdEvents.eyesClose: name = "闭眼"; break;
                case preDefineEvent.pdEvents.deepBreath: name = "深呼吸"; break;
                case preDefineEvent.pdEvents.calibrate: name = "定标"; break;
                default: name = ""; break;
            }
            ListViewItem li = new ListViewItem(name);
            li.SubItems.Add(pbForm.getStartTime().AddSeconds((int)(pbForm.getEventList()[pbForm.getEventList().Count - 1].PointPosition / pbForm.getSampleRate())).ToLongTimeString());
            li.SubItems.Add(pbForm.getEventList().Count.ToString());
            eventList.Items.Add(li);
        }
    }
}
