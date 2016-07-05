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

        /// <summary>
        /// Table
        /// </summary>
        private DataTable dt = new DataTable();

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
                               rowsCount =  26;// 30;
                               break;
                case "32导脑电":
                               rowsCount = 36;// 40;
                               break;
                case "32导脑电+多参数":
                               rowsCount = 36;// 40;
                               break;
                default:
                               break;
            }
            
            //导联源
            defaultLeadSource = (Hashtable)controller.CommonDataPool.GetLeadSource(text)[0];
            myLeadSource = (Hashtable) controller.CommonDataPool.GetLeadSource(text)[1];
            sizeOfLeadSource = defaultLeadSource.Count;

            this.dataGridViewTest.AllowUserToAddRows = false;
            this.dataGridViewTest.DataSource = dt;
            this.dataGridViewTest.ReadOnly = true;
            //DrawListView();
            DrawDataGridView();
        }

        /// <summary>
        /// 加载当前导联源
        /// </summary>
        public void DrawDataGridView()
        {
            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            InitDT(dt);

            dt.Columns.Add("数据源", typeof(string));
            dt.Columns.Add("电极名称", typeof(string));

            //如果自定义导联源有数据，则加载自定义导联源中数据，若自定义导联源无数据，则加载默认导联源中数据。
            if (myLeadSource.Count == 0)
            {
                //加载defaultLeadSource的数据
                ArrayList dKeys = new ArrayList(defaultLeadSource.Keys);

                //排序key
                dKeys.Sort();

                //j行i列
                for (int i = 0; i < dKeys.Count; i++) 
                {
                    dt.Rows[i][0] = dKeys[i].ToString();
                    dt.Rows[i][1] = defaultLeadSource[dKeys[i]].ToString();
                }
                
            }
            else
            {
                //加载自定义导联源中数据
                ArrayList lKeys = new ArrayList(myLeadSource.Keys);

                //排序key
                lKeys.Sort();

                //j行i列
                for (int i = 0; i < lKeys.Count; i++)
                {
                    dt.Rows[i][0] = lKeys[i].ToString();
                    dt.Rows[i][1] = myLeadSource[lKeys[i]].ToString();
                }
            }

            //取消点击列排序功能
            for (int i = 0; i < dataGridViewTest.Columns.Count; i++)
            {
                dataGridViewTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// 加载默认导联源
        /// </summary>
        public void DrawDefaultDataGridView()
        {
            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            InitDT(dt);

            dt.Columns.Add("数据源", typeof(string));
            dt.Columns.Add("电极名称", typeof(string));

            
                //加载defaultLeadSource的数据
                ArrayList dKeys = new ArrayList(defaultLeadSource.Keys);

                //排序key
                dKeys.Sort();

                //j行i列
                for (int i = 0; i < dKeys.Count; i++)
                {
                    dt.Rows[i][0] = dKeys[i].ToString();
                    dt.Rows[i][1] = defaultLeadSource[dKeys[i]].ToString();
                }

                //取消点击列排序功能
                for (int i = 0; i < dataGridViewTest.Columns.Count; i++)
                {
                    dataGridViewTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
        }

        /// <summary>
        /// 初始化DataTable
        /// </summary>
        /// <param name="dt"></param>
        private void InitDT(DataTable dt)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                DataRow dr1 = dt.NewRow();
                dt.Rows.Add(dr1);
            }
        }

        /// <summary>
        /// 设置datagridview的文本
        /// </summary>
        /// <param name="text"></param>
        private void SetDataGridViewText(string text)
        {
            if (this.dataGridViewTest.CurrentCell.ColumnIndex != 0) 
            {
                this.dataGridViewTest.CurrentCell.Value = text;
            }
            
            //if (this.lvSourceList.SelectedItems.Count != 0)
            //{
            //    this.lvSourceList.SelectedItems[0].SubItems[1].Text = text;
            //}
            ////清除选项，主要目的是点清除一项时不清除上次点选的数据
            //this.lvSourceList.SelectedItems.Clear();
        }

        #region 按钮点击
        private void btnFp1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFpz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFp2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnSp2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnSp1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnF7_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnAF3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnF3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFT7_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFc3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnA1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnT3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnC5_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnC3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnCp7_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnCp3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnCz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnT5_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnP3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnPz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnPo3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnPoz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnO1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnOz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnO2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnPo4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnP4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnT6_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnCp8_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnCp4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnC4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnC6_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnT4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnA2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFc4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFT8_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnF4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnF8_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnAF4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnClearOne_Click(object sender, EventArgs e)
        {
            SetDataGridViewText("");
            //SetListViewText("");
            
        }
        #endregion

        private void btnDefault_Click(object sender, EventArgs e)
        {
            //导联源清除
            //myLeadSource.Clear();

            //加载默认配置
            //DrawDefaultListView();
            DrawDefaultDataGridView();
        }

        
        private void btnClear_Click(object sender, EventArgs e)
        {
            
            #region dataView
            //dt.Clear();
            //dt.Columns.Clear();
            //InitDT(dt);

            //dt.Columns.Add("数据源", typeof(string));
            //dt.Columns.Add("电极名称", typeof(string));

            for (int i = 0; i < rowsCount; i++)   //添加30行数据  
            {

                //dt.Rows[i][0] = i + 1;
                dt.Rows[i][1] = "";
            }
            #endregion
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            #region dataView

            bool isRepeat = false;
            ArrayList sourceArray = new ArrayList();
            //导联源个数不能为0,也不能改变
            int leadCount = 0;

            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                if (!dt.Rows[i][1].ToString().Equals("")) 
                {
                    leadCount++;
                    sourceArray.Add(dt.Rows[i][1]);
                }
            }
            
            if (leadCount == 0)
            {
                MessageBox.Show("请至少配置一条导联源");
                return;
            }

            if (leadCount != sizeOfLeadSource)
            {
                MessageBox.Show("导联源数目不能改变");
                return;
            }
            
            //不能重复选择导联源
            isRepeat = IsSourceRepeat(sourceArray);
            if (isRepeat)
            {
                MessageBox.Show("不能重复选择导联源，请重新选择导联源");
                return;
            }

            //保存自定义的导联源
            myLeadSource.Clear();
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!dt.Rows[i][1].ToString().Equals(""))
                {
                    myLeadSource.Add(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1]);
                }
            }
            this.Hide();
            #endregion


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
