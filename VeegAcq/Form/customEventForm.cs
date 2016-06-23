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
                ListViewItem li = new ListViewItem(index.ToString());

                //允许更改item的颜色
                li.UseItemStyleForSubItems = false;

                li.SubItems.Add(p.EventName);
                li.SubItems.Add(myPlaybackForm.GetEventTime(p.EventPosition).ToLongTimeString());
                li.SubItems.Add("");
                index++;
                eventList.Items.Add(li);
                this.eventList.Items[index-2].SubItems[3].BackColor = CustomEvent.CustomEventColor[p.EventColorIndex];
                this.eventList.Items[index - 2].SubItems[3].Name = p.EventColorIndex.ToString();
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
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 添加事件点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (myAddCustomEventForm == null || myAddCustomEventForm.IsDisposed)
                myAddCustomEventForm = new addCustomEventForm(this.myPlaybackForm);

            //置状态为添加事件
            myAddCustomEventForm.IsAddEvent();

            //添加自定义事件的form弹出，并且为关闭前不允许操作该form
            myAddCustomEventForm.ShowDialog();
        }

        /// <summary>
        /// 开始添加事件
        /// </summary>
        /// <param name="colorIndex"></param>
        /// <param name="name"></param>
        public void StartAddEvents(int colorIndex, string name)
        {
            myPlaybackForm.StartAddEvents(colorIndex, name);
        }
        /// <summary>
        /// 更新listView内容
        /// -- by lxl
        /// </summary>
        /// <param name="isAdded">是添加事件还是删除事件</param>
        public void UpdateListView()
        {
            //直接把事件删除掉，并将所删除事件后的事件序号各减一
            for (int i = eventList.SelectedIndices[0]; i <= myPlaybackForm.GetCustomEventList().Count; i++)
            {
                eventList.Items[i].SubItems[2].Text = (int.Parse(eventList.Items[i].SubItems[2].Text) - 1).ToString();
            }
            eventList.Items.RemoveAt(eventList.SelectedIndices[0]);
        }

        /// <summary>
        /// 删除事件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //判定是否有选择一个事件
            if (this.eventList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行删除");
                return;
            }

            //移除掉事件(该移除事件属性里有返回来操作eventlist的代码)
            myPlaybackForm.RemoveEvent(false, eventList.SelectedIndices[0]);
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //判定是否有选择一个事件
            if (this.eventList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个事件进行编辑");
                return;
            }

            if (myAddCustomEventForm == null)
                myAddCustomEventForm = new addCustomEventForm(this.myPlaybackForm,eventList.SelectedIndices[0]);
            
            //置状态为编辑事件
            myAddCustomEventForm.IsEditEvent(int.Parse(this.eventList.SelectedItems[0].SubItems[3].Name), this.eventList.SelectedItems[0].SubItems[0].Text);

            //添加自定义事件的form弹出，并且为关闭前不允许操作该form
            myAddCustomEventForm.ShowDialog();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colorIndex"></param>
        public void EditEvent(string text, int colorIndex)
        {
            this.eventList.SelectedItems[0].SubItems[0].Text = text;
            this.eventList.SelectedItems[0].SubItems[3].BackColor = CustomEvent.CustomEventColor[colorIndex];
            this.eventList.SelectedItems[0].SubItems[3].Name = colorIndex.ToString();

            //将编辑后的事件保存在事件列表中
            myPlaybackForm.EditCustomEvent(this.eventList.SelectedIndices[0], text,colorIndex);
        }
    }
}