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
    /// 预定义事件FORM
    /// -- by lxl
    /// </summary>
    public partial class predefineEventsForm : Form
    {
        PlaybackForm myPlaybackForm;

        /// <summary>
        /// 事件名称
        /// </summary>
        PreDefineEvent.pdEvents eventName;

        /// <summary>
        /// 事件颜色
        /// </summary>
        Color eventColor;
        public predefineEventsForm(PlaybackForm form)
        {
            InitializeComponent();
            myPlaybackForm = form;

            //默认选择睁眼事件
            eyesOpen.Checked = true;
        }

        /// <summary>
        /// 初始化事件列表
        /// </summary>
        public void initList()
        {
            string name;

            //事件显示编号
            int index = 1;

            //开始更新列表
            eventList.BeginUpdate();

            //更新列表前先清空列表内容
            eventList.Items.Clear();

            //根将从Playbackform中读取的内容插入到列表中
            foreach (PreDefineEvent p in myPlaybackForm.GetPreEventList())
            {
                //由于预定义事件的存储格式为0x4500-ox4503，故不对应ASCII编码，故需要判断
                switch (p.Event)
                {
                    case PreDefineEvent.pdEvents.eyesOpen: name = "睁眼"; break;
                    case PreDefineEvent.pdEvents.eyesClose: name = "闭眼"; break;
                    case PreDefineEvent.pdEvents.deepBreath: name = "深呼吸"; break;
                    case PreDefineEvent.pdEvents.calibrate: name = "定标"; break;
                    default: name = ""; break;
                }
                ListViewItem li = new ListViewItem(name);
                li.SubItems.Add(myPlaybackForm.GetStartTime().AddSeconds((int)(p.PointPosition / myPlaybackForm.GetSampleRate())).ToLongTimeString());
                li.SubItems.Add(index.ToString());

                //序号递增
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
            myPlaybackForm.StartAddEvents(true, eventColor, eventName);
        }

        /// <summary>
        /// 更新listView内容
        /// -- by lxl
        /// </summary>
        /// <param name="isAdded">是添加事件还是删除事件</param>
        public void updateListView(bool isAdded)
        {
            //若是添加事件，则直接将事件添加到后方（日后还需要对事件进行排序后再添加）
            if (isAdded)
            {
                string name;

                //由于预定义事件的存储格式为0x4500-ox4503，故不对应ASCII编码，故需要判断
                switch (myPlaybackForm.GetPreEventList()[myPlaybackForm.GetPreEventList().Count - 1].Event)
                {
                    case PreDefineEvent.pdEvents.eyesOpen: name = "睁眼"; break;
                    case PreDefineEvent.pdEvents.eyesClose: name = "闭眼"; break;
                    case PreDefineEvent.pdEvents.deepBreath: name = "深呼吸"; break;
                    case PreDefineEvent.pdEvents.calibrate: name = "定标"; break;
                    default: name = ""; break;
                }
                ListViewItem li = new ListViewItem(name);
                li.SubItems.Add(myPlaybackForm.GetStartTime().AddSeconds((int)(myPlaybackForm.GetPreEventList()[myPlaybackForm.GetPreEventList().Count - 1].PointPosition / myPlaybackForm.GetSampleRate())).ToLongTimeString());
                li.SubItems.Add(myPlaybackForm.GetPreEventList().Count.ToString());
                eventList.Items.Add(li);
            }
            else //若是删除事件则直接把事件删除掉，并将所删除事件后的事件序号各加一
            {
                for (int i = eventList.SelectedIndices[0]; i <= myPlaybackForm.GetPreEventList().Count; i++)
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
            
            //若没选中则不处理
            if (!rb.Checked)
                return;

            //根据所选择的按钮名称来设置预定义事件名称
            switch (rb.Name)
            {
                case "eyesOpen": eventName = PreDefineEvent.pdEvents.eyesOpen; eventColor = PreDefineEvent.eyesOpenColor; break;
                case "eyesClose": eventName = PreDefineEvent.pdEvents.eyesClose; eventColor = PreDefineEvent.eyesCloseColor; break;
                case "deepBreath": eventName = PreDefineEvent.pdEvents.deepBreath; eventColor = PreDefineEvent.deepBreathColor; break;
                case "calibrate": eventName = PreDefineEvent.pdEvents.calibrate; eventColor = PreDefineEvent.calibrateColor; break;
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

            //在事件列表中把事件删除
            myPlaybackForm.RemoveEvent(true, eventList.SelectedIndices[0]);
        }
    }
}