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
    /// 自定义事件FORM
    /// -- by lxl
    /// </summary>
    public partial class CustomEventForm : Form
    {
        PlaybackForm myPlaybackForm;
        addCustomEventForm myAddCustomEventForm;

        public CustomEventForm(PlaybackForm form)
        {
            InitializeComponent();
            myPlaybackForm = form;
        }

        /// <summary>
        /// 初始化列表内显示的内容
        /// </summary>
        public void InitList()
        {
            //事件显示的编号
            int index = 1;

            //开始更新列表
            eventList.BeginUpdate();

            //先把列表清空
            eventList.Items.Clear();

            //从playbackform中读取事件列表，然后将事件添加到列表中
            foreach (CustomEvent p in myPlaybackForm.GetCustomEventList())
            {
                ListViewItem li = new ListViewItem(p.EventName);

                //允许更改item的颜色
                li.UseItemStyleForSubItems = false;

                li.SubItems.Add(myPlaybackForm.GetStartTime().AddSeconds((int)(p.EventPosition / myPlaybackForm.GetSampleRate())).ToLongTimeString());
                li.SubItems.Add(index.ToString());
                li.SubItems.Add("");
                index++;
                eventList.Items.Add(li);
                this.eventList.Items[index-2].SubItems[3].BackColor = p.EventColor;
            }

            //结束更新列表
            eventList.EndUpdate();
        }

        /// <summary>
        /// 退出按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_quit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 添加事件点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (myAddCustomEventForm == null)
                myAddCustomEventForm = new addCustomEventForm(this);

            //置状态为添加事件
            myAddCustomEventForm.IsAddEvent();

            //添加自定义事件的form弹出，并且为关闭前不允许操作该form
            myAddCustomEventForm.ShowDialog();
        }

        /// <summary>
        /// 开始添加事件
        /// </summary>
        /// <param name="clr"></param>
        /// <param name="name"></param>
        public void StartAddEvent(Color clr, string name)
        {
            myPlaybackForm.StartAddEvents(false, clr, name);
        }
        /// <summary>
        /// 更新listView内容
        /// -- by lxl
        /// </summary>
        /// <param name="isAdded">是添加事件还是删除事件</param>
        public void UpdateListView(bool isAdded)
        {
            //若是添加事件，则直接将事件添加到后方（日后还需要对事件进行排序后再添加）
            if (isAdded)
            {
                ListViewItem li = new ListViewItem(myPlaybackForm.GetCustomEventList()[myPlaybackForm.GetCustomEventList().Count - 1].EventName);
                li.SubItems.Add(myPlaybackForm.GetStartTime().AddSeconds((int)(myPlaybackForm.GetCustomEventList()[myPlaybackForm.GetCustomEventList().Count - 1].EventPosition / myPlaybackForm.GetSampleRate())).ToLongTimeString());
                li.SubItems.Add(myPlaybackForm.GetCustomEventList().Count.ToString());
                eventList.Items.Add(li);
            }
            else //若是删除事件则直接把事件删除掉，并将所删除事件后的事件序号各加一
            {
                for (int i = eventList.SelectedIndices[0]; i <= myPlaybackForm.GetCustomEventList().Count; i++)
                {
                    eventList.Items[i].SubItems[2].Text = (int.Parse(eventList.Items[i].SubItems[2].Text) - 1).ToString();
                }
                eventList.Items.RemoveAt(eventList.SelectedIndices[0]);
            }
        }

        /// <summary>
        /// 删除事件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            myPlaybackForm.RemoveEvent(false, eventList.SelectedIndices[0]);
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (myAddCustomEventForm == null)
                myAddCustomEventForm = new addCustomEventForm(this);

            if (this.eventList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行编辑");
                return;
            }
            //置状态为编辑事件
            myAddCustomEventForm.IsEditEvent(this.eventList.SelectedItems[0].SubItems[3].BackColor, this.eventList.SelectedItems[0].SubItems[0].Text);

            //添加自定义事件的form弹出，并且为关闭前不允许操作该form
            myAddCustomEventForm.ShowDialog();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="clr"></param>
        public void EditEvent(string text, Color clr)
        {
            this.eventList.SelectedItems[0].SubItems[0].Text = text;
            this.eventList.SelectedItems[0].SubItems[3].BackColor = clr;

            //将编辑后的事件保存在时间列表中
            myPlaybackForm.editCustomEvent(this.eventList.SelectedIndices[0], text, clr);
        }
    }
}
