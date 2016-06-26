using System;
using System.Collections;
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
    /// 添加导联Form  --by zt
    /// </summary>
    public partial class LeadAddForm : Form
    {
        #region 变量
        /// <summary>
        /// Controller  
        /// </summary>
        private VeegControl controller;

        /// <summary>
        /// 导联配置界面
        /// </summary>
        private LeadConfigForm myLeadConfigForm;

        /// <summary>
        /// 当前导联配置
        /// </summary>
        private Hashtable myLeadList;

        /// <summary>
        /// 列索引位置，从0开始
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// 行数
        /// </summary>
        private int rowsCount;
        #endregion

        public LeadAddForm(LeadConfigForm leadConfigForm)
        {
            this.myLeadConfigForm = leadConfigForm;
            InitializeComponent();
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="control"></param>
        public void ReigisterVeegControl(VeegControl control)
        {
            this.controller = control;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void InitLeadAdd(string config)
        {
            lbConfig.Text = config;
            txtLeadName.Text = "";
            this.rowsCount = 40;

            //按钮初始化
            DisplayAllButtons();
            switch (config)
            {
                case "8导脑电":
                    Hide16Buttons();
                    HideMultiparameterButtons();
                    //rowsCount = 10;
                    break;
                case "8导脑电+多参数":
                    Hide16Buttons();
                    //rowsCount = 10;
                    break;
                case "16导脑电":
                    Hide24Buttons();
                    HideMultiparameterButtons();
                    //rowsCount = 20;
                    break;
                case "16导脑电+多参数":
                    Hide24Buttons();
                    //rowsCount = 20;
                    break;
                case "24导脑电":
                    Hide32Buttons();
                    HideMultiparameterButtons();
                    //rowsCount = 30;
                    break;
                case "32导脑电":
                    HideMultiparameterButtons();
                    //rowsCount = 40;
                    break;
                case "32导脑电+多参数":
                    //rowsCount = 40;
                    break;
                default:
                    break;
            }
            //List初始化
            InitList();

            //当前导联配置
            myLeadList = controller.CommonDataPool.GetLeadList(config);
        }

        /// <summary>
        /// 初始化List
        /// </summary>
        private void InitList()
        {
            //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            this.lvAddLeadList.BeginUpdate();

            //清空listview
            this.lvAddLeadList.Items.Clear();

            //清空列
            this.lvAddLeadList.Columns.Clear();

            //添加列名称
            this.lvAddLeadList.Columns.Add("编号", 50, HorizontalAlignment.Left);
            this.lvAddLeadList.Columns.Add("First", 50, HorizontalAlignment.Left);
            this.lvAddLeadList.Columns.Add("Second", 50, HorizontalAlignment.Left);

            //初始化ListView
            for (int i = 0; i < rowsCount; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                this.lvAddLeadList.Items.Add(lvi);
            }

            //结束数据处理，UI界面一次性绘制
            this.lvAddLeadList.EndUpdate();   
        }

        #region 根据硬件配置显示按钮
        /// <summary>
        /// 显示所有按钮，即32导配置
        /// </summary>
        private void DisplayAllButtons()
        {
            this.btnRef.Visible = true;
            this.btnFp1.Visible = true;
            this.btnFp2.Visible = true;
            this.btnT3.Visible = true;
            this.btnT4.Visible = true;
            this.btnC3.Visible = true;
            this.btnC4.Visible = true;
            this.btnO1.Visible = true;
            this.btnO2.Visible = true;
            this.btnA1.Visible = true;
            this.btnA2.Visible = true;
            this.btnF3.Visible = true;
            this.btnF4.Visible = true;
            this.btnP3.Visible = true;
            this.btnP4.Visible = true;
            this.btnF7.Visible = true;
            this.btnF8.Visible = true;
            this.btnT5.Visible = true;
            this.btnT6.Visible = true;
            this.btnSp1.Visible = true;
            this.btnSp2.Visible = true;
            this.btnFz.Visible = true;
            this.btnCz.Visible = true;
            this.btnPz.Visible = true;
            this.btnAF3.Visible = true;
            this.btnAF4.Visible = true;
            this.btnFc3.Visible = true;
            this.btnFc4.Visible = true;
            this.btnCp3.Visible = true;
            this.btnCp4.Visible = true;
            this.btnPo3.Visible = true;
            this.btnPo4.Visible = true;
            this.btnFT7.Visible = true;
            this.btnFT8.Visible = true;
            this.btnCp7.Visible = true;
            this.btnCp8.Visible = true;
            this.btnOz.Visible = true;
            this.btnC5.Visible = true;
            this.btnC6.Visible = true;
            this.btnFpz.Visible = true;
            this.btnPoz.Visible = true;

            this.btnC.Visible = true;
        }

        /// <summary>
        /// 隐藏16比8多的
        /// </summary>
        private void Hide16Buttons()
        {

            this.btnF3.Visible = false;
            this.btnF4.Visible = false;
            this.btnP3.Visible = false;
            this.btnP4.Visible = false;
            this.btnF7.Visible = false;
            this.btnF8.Visible = false;
            this.btnT5.Visible = false;
            this.btnT6.Visible = false;
            this.btnSp1.Visible = false;
            this.btnSp2.Visible = false;
            this.btnFz.Visible = false;
            this.btnCz.Visible = false;
            this.btnPz.Visible = false;

            Hide24Buttons();
        }

        /// <summary>
        /// 隐藏24比16多的
        /// </summary>
        private void Hide24Buttons()
        {
            this.btnAF3.Visible = false;
            this.btnAF4.Visible = false;
            this.btnFc3.Visible = false;
            this.btnFc4.Visible = false;
            this.btnCp3.Visible = false;
            this.btnCp4.Visible = false;
            this.btnPo3.Visible = false;
            this.btnPo4.Visible = false;
            this.btnFT7.Visible = false;
            this.btnFT8.Visible = false;
            this.btnCp7.Visible = false;
            this.btnCp8.Visible = false;
            this.btnOz.Visible = false;

            Hide32Buttons();
        }

        /// <summary>
        /// 隐藏32比24多的
        /// </summary>
        private void Hide32Buttons()
        {
            this.btnC5.Visible = false;
            this.btnC6.Visible = false;
            this.btnFpz.Visible = false;
            this.btnPoz.Visible = false;
        }

        /// <summary>
        /// 隐藏多参数按钮
        /// </summary>
        private void HideMultiparameterButtons()
        {
            this.btnC.Visible = false;
        }
        #endregion


        #region 按钮事件
        private void btnRef_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFpz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFp1_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFp2_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnSp1_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnSp2_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnAF3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnAF4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF7_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF8_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFT7_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFc3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFc4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFT8_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnT3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnC5_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnC3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnC4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnC6_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnT4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCp7_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCp3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCp4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCp8_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnT5_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnP3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnPz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnP4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnT6_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnPo3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnPoz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnPo4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnO1_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnOz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnO2_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnA1_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnA2_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnClearOne_Click(object sender, EventArgs e)
        {
            SetListViewText("");
        }
        #endregion

        /// <summary>
        /// 设置ListView中每项的值
        /// </summary>
        /// <param name="text"></param>
        private void SetListViewText(string text)
        {
            if (this.lvAddLeadList.SelectedItems.Count != 0)
            {
                if (columnIndex != 0)
                {
                    this.lvAddLeadList.SelectedItems[0].SubItems[columnIndex].Text = text;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvAddLeadList.Items)
            {

                for (int i = 1; i < item.SubItems.Count; i++)
                {
                    item.SubItems[i].Text = "";
                }
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtLeadName.Text.ToString() != "")
            {
                #region 保存到配置文件

                #region 不能出现空的导联配置
                foreach (ListViewItem item in this.lvAddLeadList.Items)
                {
                    if (!(item.SubItems[1].Text.Equals("C") || item.SubItems[2].Text.Equals("C"))) 
                    {
                        if ((!item.SubItems[1].Text.Equals("") && item.SubItems[2].Text.Equals("")) || (item.SubItems[1].Text.Equals("") && !item.SubItems[2].Text.Equals("")))
                        {
                            MessageBox.Show("请正确填写导联配置");
                            return;
                        }
                    }
                    
                }
                #endregion

                #region 填写的导联名称不能与已有的导联名称重复
                string name = this.txtLeadName.Text;
                //获取当前
                if (myLeadList.ContainsKey(name))
                {
                    MessageBox.Show("当前导联名称已存在，请重新填写导联名称");
                    return;
                }
                #endregion

                #region 导联不能为空
                int leadCount = 0;
                foreach (ListViewItem item in this.lvAddLeadList.Items)
                {
                    if (!item.SubItems[1].Text.Equals("") && !item.SubItems[2].Text.Equals(""))
                    {
                        leadCount++;
                    }
                }
                if (leadCount == 0)
                {
                    MessageBox.Show("请输入至少一条导联数据，请重新选择");
                    return;
                }
                #endregion

                #region 导联电极不能重复
                foreach (ListViewItem item in this.lvAddLeadList.Items)
                {
                    if (!item.SubItems[1].Text.Equals("") && !item.SubItems[2].Text.Equals(""))
                    {
                        if (item.SubItems[1].Text == item.SubItems[2].Text)
                        {
                            MessageBox.Show("同一导联中电极不能重复，请重新选择");
                            return;
                        }
                    }
                }
                #endregion

                #region 导联不能重复
                ArrayList newLead = new ArrayList();
                foreach (ListViewItem item in this.lvAddLeadList.Items)
                {
                    if (!(item.SubItems[1].Text.Equals("C") || item.SubItems[2].Text.Equals("C"))) 
                    {
                        if (!item.SubItems[1].Text.Equals("") && !item.SubItems[2].Text.Equals(""))
                        {
                            newLead.Add(item.SubItems[1].Text + "-" + item.SubItems[2].Text);
                        }
                    }
                    else 
                    {
                        newLead.Add("C");
                    }
                    
                }
                bool isRepeat;
                ArrayList sortLead = new ArrayList();

                for (int i = 0; i < newLead.Count; i++)
                {
                    sortLead.Add(newLead[i]);
                }
                isRepeat = IsRepeat(sortLead);
                if (isRepeat)
                {
                    MessageBox.Show("不能出现重复导联，请重新选择导联");
                    return;
                }
                #endregion

                //添加导联
                myLeadList.Add(name, newLead);

                //保存完直接退出
                this.Close();

                //跳转到导联配置主界面,待定
                myLeadConfigForm.InitList(name);
                #endregion
            }
            else
            {
                MessageBox.Show("请输入导联名");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            myLeadConfigForm.InitList();
        }

        private void LeadAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            myLeadConfigForm.InitList();
        }

        private void lvAddLeadList_MouseDown(object sender, MouseEventArgs e)
        {
            int StartX = 0;

            //文本框的索引
            columnIndex = 0;

            //获取列的索引
            foreach (ColumnHeader Column in this.lvAddLeadList.Columns)
            {
                if (e.X >= StartX + Column.Width)
                {
                    StartX += Column.Width;
                    columnIndex += 1;
                }
            }
        }

        /// <summary>
        /// 判断数组中是否含有重复项
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public bool IsRepeat(ArrayList stringArray)
        {
            //先将数组排序
            stringArray.Sort();

            //获取数组的长度
            int MaxLine = stringArray.Count;

            //每一项与前一项比较，相同则返回true
            for (int i = 1; i < MaxLine; i++)
            {
                if (stringArray[i].Equals(stringArray[i - 1]))
                {
                    return true;
                }
            }
            return false;
        }

        private void txtLeadName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是或字母数字,也不是回车键、Backspace键,则取消该输入
            if (!(Char.IsLetterOrDigit(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        
    }
}
