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
    /// 导联编辑Form  --by zt
    /// </summary>
    public partial class LeadEditForm : Form
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
        /// 当前导联
        /// </summary>
        private ArrayList currentLead;

        /// <summary>
        /// 列索引位置，从0开始
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// 行数
        /// </summary>
        private int rowsCount;
        #endregion

        public LeadEditForm(LeadConfigForm leadConfigForm)
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
        /// 初始化Form
        /// </summary>
        /// <param name="config"></param>
        /// <param name="nameOfLead"></param>
        public void InitEditLead(string config, string nameOfLead) 
        {
            lbConfig.Text = config;
            lbLeadConfigName.Text = nameOfLead;

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
            //初始化当前导联
            myLeadList = controller.CommonDataPool.GetLeadList(config);

            //初始化当前导联
            myLeadList = controller.CommonDataPool.GetLeadList(config);
            //初始化ListView
            InitList(nameOfLead);
        }

        /// <summary>
        /// 初始化List
        /// </summary>
        private void InitList(string nameOfLead)
        {
            currentLead = (ArrayList)myLeadList[nameOfLead];

            //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            this.lvEditLeadList.BeginUpdate();    
            this.lvEditLeadList.Items.Clear();

            //列表头信息
            this.lvEditLeadList.Columns.Clear();
            this.lvEditLeadList.Columns.Add("编号", 50, HorizontalAlignment.Left);
            this.lvEditLeadList.Columns.Add("First", 50, HorizontalAlignment.Left);
            this.lvEditLeadList.Columns.Add("Second", 50, HorizontalAlignment.Left);
            
            //添加导联配置中数据到列表
            for (int i = 0; i < currentLead.Count; i++)
            {
                string oneData = (string)currentLead[i];
                //如果不是C
                if (!oneData.Equals("C")) 
                {
                    string[] firstAndSecond = oneData.Split(new char[] { '-' }); ;
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (i + 1).ToString();
                    for (int j = 0; j < firstAndSecond.Length; j++)
                    {
                        lvi.SubItems.Add(firstAndSecond[j]);
                    }
                    this.lvEditLeadList.Items.Add(lvi);
                }
                //如果是C
                else 
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (i + 1).ToString();
                    lvi.SubItems.Add("C");
                    lvi.SubItems.Add("");
                    this.lvEditLeadList.Items.Add(lvi);
                }
                

            }

            //添加其余行数据 
            for (int i = currentLead.Count; i < rowsCount; i++)    
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");

                this.lvEditLeadList.Items.Add(lvi);
            }

            //结束数据处理，UI界面一次性绘制。
            this.lvEditLeadList.EndUpdate();    
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
            if (this.lvEditLeadList.SelectedItems.Count != 0)
            {
                if (columnIndex == 1)
                {
                    this.lvEditLeadList.SelectedItems[0].SubItems[columnIndex].Text = "C";
                    this.lvEditLeadList.SelectedItems[0].SubItems[columnIndex + 1].Text = "";
                }
            }
            //SetListViewText(((Button)sender).Text);
        }

        private void btnClearOne_Click(object sender, EventArgs e)
        {
            if (this.lvEditLeadList.SelectedItems.Count != 0)
            {
                if (columnIndex != 0)
                {

                    this.lvEditLeadList.SelectedItems[0].SubItems[columnIndex].Text = "";

                }
            }
            //清除选项，主要目的是点清除一项时不清除上次点选的数据
            this.lvEditLeadList.SelectedItems.Clear();
            //SetListViewText("");
        }
        #endregion

        /// <summary>
        /// 设置ListView中每项的值
        /// </summary>
        /// <param name="text"></param>
        private void SetListViewText(string text)
        {
            if (this.lvEditLeadList.SelectedItems.Count != 0)
            {
                if (columnIndex != 0)
                {
                    if (!(columnIndex == 2 && this.lvEditLeadList.SelectedItems[0].SubItems[1].Text.Equals("C")))
                    {
                        this.lvEditLeadList.SelectedItems[0].SubItems[columnIndex].Text = text;
                    }
                }
            }
            //清除选项，主要目的是点清除一项时不清除上次点选的数据
            this.lvEditLeadList.SelectedItems.Clear();
        }

        private void lvEditLeadList_MouseDown(object sender, MouseEventArgs e)
        {
            int StartX = 0;

            //文本框的索引
            columnIndex = 0;

            //获取列的索引
            foreach (ColumnHeader Column in this.lvEditLeadList.Columns)
            {
                if (e.X >= StartX + Column.Width)
                {
                    StartX += Column.Width;
                    columnIndex += 1;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvEditLeadList.Items)
            {

                for (int i = 1; i < item.SubItems.Count; i++)
                {
                    item.SubItems[i].Text = "";
                }
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //正确的导联配置
            foreach (ListViewItem item in this.lvEditLeadList.Items)
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
            //导联数据不能为空
            int leadCount = 0;
            foreach (ListViewItem item in this.lvEditLeadList.Items)
            {
                if (!item.SubItems[1].Text.Equals("") || !item.SubItems[2].Text.Equals(""))
                {
                    leadCount++;
                }
            }
            if (leadCount == 0)
            {
                MessageBox.Show("请输入至少一条导联数据，请重新选择");
                return;
            }
            //判断导联电极是否有重复
            foreach (ListViewItem item in this.lvEditLeadList.Items)
            {
                if (!item.SubItems[1].Text.Equals("") && !item.SubItems[2].Text.Equals(""))
                {
                    if (item.SubItems[1].Text == item.SubItems[2].Text)
                    {
                        MessageBox.Show("同一导联中电极不能重复,请重新选择");
                        return;
                    }
                }
            }
            //保存导联配置
            //判断导联是否有重复
            ArrayList tempLead = new ArrayList();
            foreach (ListViewItem item in this.lvEditLeadList.Items)
            {
                if (!(item.SubItems[1].Text.Equals("C") || item.SubItems[2].Text.Equals("C")))
                {
                    if (!item.SubItems[1].Text.Equals("") && !item.SubItems[2].Text.Equals(""))
                    {
                        tempLead.Add(item.SubItems[1].Text + "-" + item.SubItems[2].Text);
                    }
                }
                else
                {
                    tempLead.Add("C");
                }
            }
            bool isRepeat;
            ArrayList sortLead = new ArrayList();

            for (int i = 0; i < tempLead.Count; i++)
            {
                sortLead.Add(tempLead[i]);
            }
            isRepeat = IsRepeat(sortLead);
            if (isRepeat)
            {
                MessageBox.Show("不能出现重复导联，请重新选择导联");
                return;
            }
            currentLead.Clear();
            for (int i = 0; i < tempLead.Count; i++)
            {
                currentLead.Add(tempLead[i]);
            }
            //保存完直接退出
            this.Hide();
            myLeadConfigForm.InitList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            myLeadConfigForm.InitList();
        }

        private void LeadEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            myLeadConfigForm.InitList();
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

        
    }
}
