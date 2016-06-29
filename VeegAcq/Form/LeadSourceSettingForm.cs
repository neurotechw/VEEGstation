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
    /// 导联源配置  --by zt
    /// </summary>
    public partial class LeadSourceSettingForm : Form
    {
        /// <summary>
        /// controller
        /// </summary>
        private VeegControl controller;

        /// <summary>
        /// ListView的行数
        /// </summary>
        private int rowsCount;

        /// <summary>
        /// 当前导联源
        /// </summary>
        private Hashtable myLeadSource;

        /// <summary>
        /// 默认导联源
        /// </summary>
        private Hashtable defaultLeadSource;

        /// <summary>
        /// 导联源的size
        /// </summary>
        private int sizeOfLeadSource;

        public LeadSourceSettingForm()
        {
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

        #region 根据硬件配置显示按钮
        /// <summary>
        /// 显示所有按钮，即32导配置
        /// </summary>
        private void DisplayAllButtons() 
        {
            //未完
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
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="text">硬件配置</param>
        public void InitLeadSetting(string text)
        {
            lbConfig.Text = text;
            DisplayAllButtons();
            switch (text) 
            {
                case "8导脑电":
                               Hide16Buttons();
                               rowsCount = 10;
                               break;
                case "8导脑电+多参数":
                               Hide16Buttons();
                               rowsCount = 10;
                               break;
                case "16导脑电":
                               Hide24Buttons();
                               rowsCount = 20;
                               break;
                case "16导脑电+多参数":
                               Hide24Buttons();
                               rowsCount = 20;
                               break;
                case "24导脑电":
                               Hide32Buttons();
                               rowsCount = 30;
                               break;
                case "32导脑电":
                               rowsCount = 40;
                               break;
                case "32导脑电+多参数":
                               rowsCount = 40;
                               break;
                default:
                               break;
            }
            
            //导联源
            defaultLeadSource = (Hashtable)controller.CommonDataPool.GetLeadSource(text)[0];
            myLeadSource = (Hashtable) controller.CommonDataPool.GetLeadSource(text)[1];
            sizeOfLeadSource = defaultLeadSource.Count;
          
            DrawListView();
        }

        /// <summary>
        /// 加载当前导联源 
        /// </summary>
        public void DrawListView()
        {
            //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            this.lvSourceList.BeginUpdate();

            //清空listview
            this.lvSourceList.Items.Clear();

            //清空列
            this.lvSourceList.Columns.Clear();

            //添加列名
            this.lvSourceList.Columns.Add("数据源", 50, HorizontalAlignment.Left);
            this.lvSourceList.Columns.Add("电极名称", 100, HorizontalAlignment.Left);

            #region 加载LeadSource中的数据

            //如果自定义导联源有数据，则加载自定义导联源中数据，若自定义导联源无数据，则加载默认导联源中数据。
            if (myLeadSource.Count == 0)
            {
                //加载defaultLeadSource的数据
                ArrayList dKeys = new ArrayList(defaultLeadSource.Keys);

                //排序key
                dKeys.Sort();
                foreach (int key in dKeys)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = key.ToString();
                    lvi.SubItems.Add(defaultLeadSource[key].ToString());
                    this.lvSourceList.Items.Add(lvi);
                }
            }
            else
            {
                //加载自定义导联源中数据
                ArrayList lKeys = new ArrayList(myLeadSource.Keys);

                //排序key
                lKeys.Sort();
                foreach (int key in lKeys)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = key.ToString();
                    //lvi.SubItems.Add(key);
                    lvi.SubItems.Add(myLeadSource[key].ToString());
                    this.lvSourceList.Items.Add(lvi);
                }
            }
            #endregion

            //结束数据处理，UI界面一次性绘制。
            this.lvSourceList.EndUpdate();
        }


        /// <summary>
        /// 设置ListView中每项的值即鼠标点击按钮的文本
        /// </summary>
        /// <param name="text"></param>
        private void SetListViewText(string text)
        {
            if (this.lvSourceList.SelectedItems.Count != 0)
            {
                this.lvSourceList.SelectedItems[0].SubItems[1].Text = text;
            }
            //清除选项，主要目的是点清除一项时不清除上次点选的数据
            this.lvSourceList.SelectedItems.Clear();
        }

        #region 按钮点击
        private void btnFp1_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFpz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFp2_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnSp2_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnSp1_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF7_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnAF3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF3_Click(object sender, EventArgs e)
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

        private void btnA1_Click(object sender, EventArgs e)
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

        private void btnCp7_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCp3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnFz_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCz_Click(object sender, EventArgs e)
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

        private void btnPo3_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnPoz_Click(object sender, EventArgs e)
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

        private void btnPo4_Click(object sender, EventArgs e)
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

        private void btnCp8_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnCp4_Click(object sender, EventArgs e)
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

        private void btnA2_Click(object sender, EventArgs e)
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

        private void btnF4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnF8_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnAF4_Click(object sender, EventArgs e)
        {
            SetListViewText(((Button)sender).Text);
        }

        private void btnClearOne_Click(object sender, EventArgs e)
        {
            SetListViewText("");
        }
        #endregion

        private void btnDefault_Click(object sender, EventArgs e)
        {
            //导联源清除
            myLeadSource.Clear();

            //添加默认配置
            DrawListView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lvSourceList.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            this.lvSourceList.Items.Clear();  //清空listview

            for (int i = 0; i < rowsCount; i++)   //添加30行数据  
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add("");
                this.lvSourceList.Items.Add(lvi);
            }

            this.lvSourceList.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //导联源个数不能为0
            int leadCount = 0;
            foreach (ListViewItem item in this.lvSourceList.Items)
            {
                if (!item.SubItems[1].Text.Equals(""))
                {
                    leadCount++;
                }
            }
            if (leadCount == 0)
            {
                MessageBox.Show("请至少配置一条导联源");
                return;
            }
            //判断是否有重复
            bool isRepeat = false;
            ArrayList sourceArray = new ArrayList();

            foreach (ListViewItem item in this.lvSourceList.Items)
            {
                if (!item.SubItems[1].Text.Equals(""))
                {
                    sourceArray.Add(item.SubItems[1].Text);
                }

            }
            isRepeat = IsSourceRepeat(sourceArray);
            if (isRepeat)
            {
                MessageBox.Show("不能重复选择导联源，请重新选择导联源");
                return;
            }

            //判断当前导联源的size是否为初始size
            int count = 0;
            foreach (ListViewItem item in this.lvSourceList.Items)
            {
                if (!item.SubItems[1].Text.Equals(""))
                {
                    count++;
                }
            }
            if (count != sizeOfLeadSource) 
            {
                MessageBox.Show("导联源数目不能改变");
                return;
            }

            //保存自定义的导联源
            myLeadSource.Clear();
            foreach (ListViewItem item in this.lvSourceList.Items)
            {
                if (!item.SubItems[1].Text.Equals(""))
                {
                    myLeadSource.Add(Convert.ToInt32(item.SubItems[0].Text), item.SubItems[1].Text);
                }

            }
            this.Hide();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 判断是否有重复项
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public bool IsSourceRepeat(ArrayList stringArray)
        {
            stringArray.Sort();

            int MaxLine = stringArray.Count;

            for (int i = 1; i < MaxLine; i++)
            {
                if (stringArray[i].Equals(stringArray[i - 1]))
                {
                    return true;
                }
            }
            return false;
        }

        private void LeadSourceSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
