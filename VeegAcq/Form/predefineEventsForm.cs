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
    public partial class PredefineEventsForm : Form
    {
        PlaybackForm myPlaybackForm;

        //PreDefineEvent.PreDefineEventsName eventName;

        /// <summary>
        /// 所添加的预定义事件的编号
        /// </summary>
        private int eventIndex;

        public PredefineEventsForm(PlaybackForm form)
        {
            InitializeComponent();
            myPlaybackForm = form;
            eventIndex = -1;

            //根据预定义事件列表初始化可选择的事件名称的radiobutton
            InitRadioButton();
        }

        /// <summary>
        /// 根据预定义事件列表初始化可选择的事件名称的radiobutton
        /// </summary>
        private void InitRadioButton()
        {
            RadioButton rbName;
            for (int i = 0; i < PreDefineEvent.PreDefineEventNameArray.Count(); i++)
            {
                rbName = new RadioButton();
                rbName.AutoSize = true;
                rbName.Size = new Size(47, 16);
                rbName.Text = PreDefineEvent.PreDefineEventNameArray[i];
                rbName.Location = new Point(3, 17 + 22 * i);
                rbName.CheckedChanged += new EventHandler(this.radioButton_CheckChanged);
                rbName.Name = i.ToString();
                this.nameGroup.Controls.Add(rbName);
            }
        }

        /// <summary>
        /// 初始化事件列表
        /// </summary>
        public void InitList()
        {
            //事件显示编号
            int index = 1;

            //开始更新列表
            eventList.BeginUpdate();

            //更新列表前先清空列表内容
            eventList.Items.Clear();

            //根将从Playbackform中读取的内容插入到列表中
            foreach (PreDefineEvent p in myPlaybackForm.GetSortedPreEventList())
            {
                //初始化listview的内容项
                ListViewItem li = new ListViewItem(p.EventName);
                li.Name = p.EventID.ToString();
                li.SubItems.Add(myPlaybackForm.GetEventTime(p.EventPosition).ToLongTimeString());
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
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 添加事件按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEvent_Click(object sender, EventArgs e)
        {
            if (eventIndex < 0)
            {
                MessageBox.Show("请先选择事件的名称");
                return;
            }
            myPlaybackForm.StartAddEvents(eventIndex);
        }

        /// <summary>
        /// 更新删除后listView内容
        /// -- by lxl
        /// </summary>
        public void updateListView()
        {
            //把事件删除掉，并将所删除事件后的事件序号各减一
            for (int i = eventList.SelectedIndices[0]; i <= myPlaybackForm.GetSortedPreEventList().Count; i++)
            {
                eventList.Items[i].SubItems[2].Text = (int.Parse(eventList.Items[i].SubItems[2].Text) - 1).ToString();
            }
            eventList.Items.RemoveAt(eventList.SelectedIndices[0]);
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
            eventIndex = int.Parse(rb.Name);
        }

        /// <summary>
        /// 删除事件按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteEvent_Click(object sender, EventArgs e)
        {

            //判定是否有选择一个事件
            if (this.eventList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行删除");
                return;
            }

            //在事件列表中把事件删除
            myPlaybackForm.RemoveEvent(true, eventList.SelectedIndices[0], int.Parse(eventList.SelectedItems[0].Name));
        }
    }
}