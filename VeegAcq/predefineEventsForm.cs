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
        preDefineEvent.pdEvents _name;
        Color _color;
        public predefineEventsForm(PlaybackForm form)
        {
            InitializeComponent();
            pbForm = form;
            eyesOpen.Checked = true;
        }

        public void initList()
        {
            string name;
            int index = 1;
            eventList.BeginUpdate();
            eventList.Items.Clear();
            foreach (preDefineEvent p in pbForm.getPreEventList())
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
            pbForm.startAddEvents(true, _color, _name);
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
                string name;
                switch (pbForm.getPreEventList()[pbForm.getPreEventList().Count - 1].Event)
                {
                    case preDefineEvent.pdEvents.eyesOpen: name = "睁眼"; break;
                    case preDefineEvent.pdEvents.eyesClose: name = "闭眼"; break;
                    case preDefineEvent.pdEvents.deepBreath: name = "深呼吸"; break;
                    case preDefineEvent.pdEvents.calibrate: name = "定标"; break;
                    default: name = ""; break;
                }
                ListViewItem li = new ListViewItem(name);
                li.SubItems.Add(pbForm.getStartTime().AddSeconds((int)(pbForm.getPreEventList()[pbForm.getPreEventList().Count - 1].PointPosition / pbForm.getSampleRate())).ToLongTimeString());
                li.SubItems.Add(pbForm.getPreEventList().Count.ToString());
                eventList.Items.Add(li);
            }
            else
            {
                for (int i = eventList.SelectedIndices[0]; i <= pbForm.getPreEventList().Count; i++)
                {
                    eventList.Items[i].SubItems[2].Text = (int.Parse(eventList.Items[i].SubItems[2].Text) - 1).ToString();
                }
                eventList.Items.RemoveAt(eventList.SelectedIndices[0]);
            }
        }

        /// <summary>
        /// radioButton状态改变事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
                return;
            switch (rb.Name)
            {
                case "eyesOpen": _name = preDefineEvent.pdEvents.eyesOpen; _color = preDefineEvent.eyesOpenColor; break;
                case "eyesClose": _name = preDefineEvent.pdEvents.eyesClose; _color = preDefineEvent.eyesCloseColor; break;
                case "deepBreath": _name = preDefineEvent.pdEvents.deepBreath; _color = preDefineEvent.deepBreathColor; break;
                case "calibrate": _name = preDefineEvent.pdEvents.calibrate; _color = preDefineEvent.calibrateColor; break;
            }
        }

        /// <summary>
        /// 删除事件按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteEvent_Click(object sender, EventArgs e)
        {
            MessageBox.Show(eventList.SelectedIndices[0].ToString());
            //eventList.Items.RemoveAt()
            pbForm.removeEvent(true, eventList.SelectedIndices[0]);
        }
    }
}
