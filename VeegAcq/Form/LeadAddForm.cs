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

        /// <summary>
        /// Table
        /// </summary>
        private DataTable dt = new DataTable();
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

            this.dataGridViewTest.AllowUserToAddRows = false;
            this.dataGridViewTest.DataSource = dt;
            this.dataGridViewTest.ReadOnly = true;

            //List初始化
            //InitList();
            InitDataGridView();
            //当前导联配置
            myLeadList = controller.CommonDataPool.GetLeadList(config);
        }

        /// <summary>
        /// 加载当前导联配置
        /// </summary>
        public void InitDataGridView()
        {
            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            InitDT(dt);

            dt.Columns.Add("编号", typeof(string));
            dt.Columns.Add("First", typeof(string));
            dt.Columns.Add("Second", typeof(string));

            for (int i = 0; i < rowsCount; i++)   //添加30行数据  
            {

                //dt.Rows[i][0] = i + 1;
                dt.Rows[i][0] = i+1;
                dt.Rows[i][1] = "";
                dt.Rows[i][2] = "";
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
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFpz_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFp1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFp2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnSp1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnSp2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnAF3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnAF4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnF7_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnF3_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnFz_Click(object sender, EventArgs e)
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

        private void btnCz_Click(object sender, EventArgs e)
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

        private void btnCp4_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnCp8_Click(object sender, EventArgs e)
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

        private void btnPo4_Click(object sender, EventArgs e)
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

        private void btnA1_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnA2_Click(object sender, EventArgs e)
        {
            SetDataGridViewText(((Button)sender).Text);
            //SetListViewText(((Button)sender).Text);
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            //if (this.lvAddLeadList.SelectedItems.Count != 0) 
            //{
            //    if (columnIndex == 1) 
            //    {
            //        this.lvAddLeadList.SelectedItems[0].SubItems[columnIndex].Text = "C";
            //        this.lvAddLeadList.SelectedItems[0].SubItems[columnIndex+1].Text = "";
            //    }
            //}
            if (this.dataGridViewTest.CurrentCell.ColumnIndex == 1)
            {
                this.dataGridViewTest.CurrentCell.Value = "C";
                dt.Rows[dataGridViewTest.CurrentCell.RowIndex][2] = "";
                dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex + 1].Cells[1].Selected = true;
                this.dataGridViewTest.CurrentCell = dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex + 1].Cells[1];
            }
            //SetListViewText(((Button)sender).Text);
        }

        private void btnClearOne_Click(object sender, EventArgs e)
        {
            SetDataGridViewText("");
            //if (this.lvAddLeadList.SelectedItems.Count != 0)
            //{
            //    if (columnIndex != 0)
            //    {
                   
            //       this.lvAddLeadList.SelectedItems[0].SubItems[columnIndex].Text = "";
                    
            //    }
            //}
            ////清除选项，主要目的是点清除一项时不清除上次点选的数据
            //this.lvAddLeadList.SelectedItems.Clear();
            //SetListViewText("");
        }
        #endregion

        

        private void SetDataGridViewText(string text)
        {
            columnIndex = this.dataGridViewTest.CurrentCell.ColumnIndex;
            int rowIndex = this.dataGridViewTest.CurrentCell.RowIndex;
            if (columnIndex != 0)
            {
                //如果第一个是C，鼠标又在第二项，则不能改。反之能改
                if (!(columnIndex == 2 && dt.Rows[rowIndex][1].Equals("C")))
                {
                    this.dataGridViewTest.CurrentCell.Value = text;
                    //this.lvAddLeadList.SelectedItems[0].SubItems[columnIndex].Text = text;

                    MoveCurrentCellToNext();
                }
            }

        }

        /// <summary>
        /// 将当前选中的cell移动到下一个
        /// -- by lxl
        /// </summary>
        private void MoveCurrentCellToNext()
        {
            //所选中的cell后移一格 -- by lxl
            this.dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex].Cells[columnIndex].Selected = false;
            if (columnIndex == 1)
            {
                dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex].Cells[columnIndex + 1].Selected = true;
                this.dataGridViewTest.CurrentCell = dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex].Cells[columnIndex + 1];
            }
            else
            {
                if (this.dataGridViewTest.CurrentCell.RowIndex < rowsCount) 
                {
                    dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex + 1].Cells[1].Selected = true;
                    this.dataGridViewTest.CurrentCell = dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex + 1].Cells[1];
                }
                //dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex + 1].Cells[1].Selected = true;
                //this.dataGridViewTest.CurrentCell = dataGridViewTest.Rows[this.dataGridViewTest.CurrentCell.RowIndex + 1].Cells[1];
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            

            #region dataView
            for (int i = 0; i < rowsCount; i++)   //添加30行数据  
            {

                //dt.Rows[i][0] = i + 1;
                dt.Rows[i][1] = "";
                dt.Rows[i][2] = "";
            }
            #endregion
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (this.txtLeadName.Text.ToString() != "")
            {
                #region 保存到配置文件

                #region 不能出现空的导联配置
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    if (!(dt.Rows[i][1].Equals("C") || dt.Rows[i][2].Equals("C")))
                        {
                            if ((!dt.Rows[i][1].Equals("") && dt.Rows[i][2].Equals("")) || (dt.Rows[i][1].Equals("") && !dt.Rows[i][2].Equals("")))
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
                for (int i = 0; i < dt.Rows.Count; i++) 
                {

                    if (!dt.Rows[i][1].Equals("") || !dt.Rows[i][2].Equals(""))
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!dt.Rows[i][1].Equals("") && !dt.Rows[i][2].Equals(""))
                    {
                        if (dt.Rows[i][1] == dt.Rows[i][2])
                        {
                            MessageBox.Show("同一导联中电极不能重复，请重新选择");
                            return;
                        }
                    }
                }
                #endregion

                #region 导联不能重复
                ArrayList newLead = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!(dt.Rows[i][1].Equals("C") || dt.Rows[i][2].Equals("C"))) 
                    {
                        if (!dt.Rows[i][1].Equals("") && !dt.Rows[i][2].Equals(""))
                        {
                            newLead.Add(dt.Rows[i][1] + "-" + dt.Rows[i][2]);
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
                //myLeadConfigForm.InitList(name);
                myLeadConfigForm.InitDataView(name);
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
           // myLeadConfigForm.InitList();
            myLeadConfigForm.InitDataView();
        }

        private void LeadAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            //myLeadConfigForm.InitList();
            myLeadConfigForm.InitDataView();
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

        /// <summary>
        /// datagridview的选择CELL更改事件，确保第一列的编号不能选择
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewTest_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridViewTest.CurrentCell != null)
            {
                if (this.dataGridViewTest.CurrentCell.ColumnIndex == 0)
                {
                    this.dataGridViewTest.ClearSelection();
                }
            }
        }

        
    }
}
