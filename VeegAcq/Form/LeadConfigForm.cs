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
    /// 导联配置Form  --by zt
    /// </summary>
    public partial class LeadConfigForm : Form
    {
        #region 变量
        /// <summary>
        /// Controller  
        /// </summary>
        private VeegControl controller;
        
        /// <summary>
        /// 导联配置Form
        /// </summary>
        private LeadSourceSettingForm myLeadSourceSettingForm;

        /// <summary>
        /// 添加导联Form
        /// </summary>
        private LeadAddForm myLeadAddForm;

        /// <summary>
        /// 编辑导联Form
        /// </summary>
        private LeadEditForm myLeadEditForm;

        /// <summary>
        /// 导联配置集合
        /// </summary>
        private Hashtable myLeadLists;

        /// <summary>
        /// 当前导联配置
        /// </summary>
        private Hashtable myLeadList;

        /// <summary>
        /// 选择的列号
        /// </summary>
        private int selectedColumn;

        /// <summary>
        /// 选择的列名
        /// </summary>
        private string nameOfColumn;

        /// <summary>
        /// 行数
        /// </summary>
        private int rowsCount = 40;

        /// <summary>
        /// Table
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// 编号所在列的宽度
        /// -- by lxl
        /// </summary>
        private const int NUM_COLUMN_WIDTH = 40;

        #endregion

        public LeadConfigForm()
        {
            InitializeComponent();
            this.txtName.Enabled = false;
            //this.rtxtNote.Enabled = false;
            myLeadSourceSettingForm = new LeadSourceSettingForm();
            myLeadAddForm = new LeadAddForm(this);
            myLeadEditForm = new LeadEditForm(this);
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="control"></param>
        public void ReigisterVeegControl(VeegControl control)
        {
            this.controller = control;
            myLeadSourceSettingForm.ReigisterVeegControl(control);
            myLeadAddForm.ReigisterVeegControl(control);
            myLeadEditForm.ReigisterVeegControl(control);
        }

        /// <summary>
        /// 初始化LeadConfig
        /// </summary>
        public void InitLeadConfig(string config)
        {
            this.dataGridViewTest.AllowUserToAddRows = false;
            this.dataGridViewTest.ReadOnly = true;
            this.dataGridViewTest.DataSource = dt;

            //硬件配置名称
            //cbConfigList.Text = cbConfigList.Items[0].ToString();
            cbConfigList.Text = config;
            txtName.Text = "默认导联配置";

            //初始化表格
            //InitList();
            InitDataView();

            //设置全列选择 -- by lxl
            SetFullColumnSelected();
        }

        /// <summary>
        /// 设置datagridview为全列选择
        /// -- by lxl
        /// </summary>
        private void SetFullColumnSelected()
        {
            //清楚所有默认选择
            this.dataGridViewTest.ClearSelection();
            //取消点击列排序功能
            for (int i = 0; i < dataGridViewTest.Columns.Count; i++)
            {
                dataGridViewTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //设置为全列选择
            this.dataGridViewTest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect;
        }

        public void InitDataView() 
        {
            myLeadLists = controller.CommonDataPool.LeadConfigLists;
            myLeadList = (Hashtable)myLeadLists[cbConfigList.Text];
            //myLeadList = controller.CommonDataPool.GetLeadList(cbConfigList.Text);

            //因为需要重新添加DT，故意先将其改为RowHeaderSelect，再改回fullColumnSelect
            this.dataGridViewTest.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect; //-- by lxl

            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            InitDT(dt);

            ArrayList keys = new ArrayList(myLeadList.Keys);
            keys.Sort();

            ////列
            dt.Columns.Add("编号", typeof(string));
            for (int k = 0; k < rowsCount; k++)
            {
                dt.Rows[k][0] = k+1;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                dt.Columns.Add((string)keys[i], typeof(string));
                ArrayList value = (ArrayList)myLeadList[keys[i]];
                for (int j = 0; j < value.Count; j++)
                {
                    dt.Rows[j][i+1] = value[j];
                }
            }

            //取消点击列排序功能
            for (int i = 0; i < dataGridViewTest.Columns.Count; i++)
            {
                dataGridViewTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //DT加载完后，改回fullColumnSelect
            this.dataGridViewTest.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;//-- by lxl

            //设置编号所在列的宽度（将其设置得窄一点）  -- by lxl
            this.dataGridViewTest.Columns[0].Width = NUM_COLUMN_WIDTH;
            
        }


        public void InitDataView(string name)
        {
            txtName.Text = name;
            myLeadLists = controller.CommonDataPool.LeadConfigLists;
            myLeadList = (Hashtable)myLeadLists[cbConfigList.Text];

            //因为需要重新添加DT，故意先将其改为RowHeaderSelect，再改回fullColumnSelect
            this.dataGridViewTest.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect; //-- by lxl

            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            
            InitDT(dt);

            ArrayList keys = new ArrayList(myLeadList.Keys);
            keys.Sort();

            //第一列
            dt.Columns.Add("编号", typeof(string));
            for (int k = 0; k < rowsCount; k++)
            {
                dt.Rows[k][0] = k + 1;
            }

            //数据列
            for (int i = 0; i < keys.Count; i++)
            {
                dt.Columns.Add((string)keys[i], typeof(string));
                ArrayList value = (ArrayList)myLeadList[keys[i]];
                for (int j = 0; j < value.Count; j++)
                {
                    dt.Rows[j][i + 1] = value[j];
                }
            }


            //取消点击列排序功能
            for (int i = 0; i < dataGridViewTest.Columns.Count; i++)
            {
                dataGridViewTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //DT加载完后，改回fullColumnSelect
            this.dataGridViewTest.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;//-- by lxl

            //设置编号所在列的宽度（将其设置得窄一点）  -- by lxl
            this.dataGridViewTest.Columns[0].Width = NUM_COLUMN_WIDTH;
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

        private void btnSetting_Click(object sender, EventArgs e)
        {
            myLeadSourceSettingForm.InitLeadSetting(cbConfigList.Text);
            myLeadSourceSettingForm.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            myLeadAddForm.InitLeadAdd(cbConfigList.Text);
            myLeadAddForm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (myLeadList.Count == 0)
            {
                MessageBox.Show("当前导联编制列表为空，请添加导联编制。");
                return;
            }
            
            nameOfColumn = txtName.Text;
            if (nameOfColumn.Equals("默认导联配置"))
            {
                MessageBox.Show("这是系统默认的导联方式，您不能编辑。");
                return;
            }

            //编辑导联界面
            myLeadEditForm.InitEditLead(cbConfigList.Text,nameOfColumn);
            myLeadEditForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (myLeadList.Count == 0)
            {
                MessageBox.Show("当前导联编制列表为空，您不能删除。");
                return;
            }
            
            nameOfColumn = txtName.Text;
            if (nameOfColumn.Equals("默认导联配置"))
            {
                MessageBox.Show("这是系统默认的导联方式，您不能删除。");
                return;
            }
            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                myLeadList.Remove(nameOfColumn);
                selectedColumn = 0;
                //InitList("默认导联配置");
                InitDataView("默认导联配置");
            }
            
            
        }

       
        private void cbConfigList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitList();
            InitDataView();
            this.txtName.Text = "默认导联配置";
        }

        private void LeadConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.controller.myPlaybackForm.InitLeadParameters();
            this.controller.myPlaybackForm.ShowData();
            this.Hide();

        }

        private void dataGridViewTest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedColumn = e.ColumnIndex;

            if (selectedColumn != 0)
            {
                nameOfColumn = this.dataGridViewTest.Columns[selectedColumn].HeaderCell.Value.ToString();
                txtName.Text = nameOfColumn;
            }
        }

        /// <summary>
        /// dataGridViewTest所选项改变事件，为了确保无法选择第一列“编号”
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
                else
                {
                    selectedColumn = this.dataGridViewTest.CurrentCell.ColumnIndex;
                    nameOfColumn = this.dataGridViewTest.Columns[selectedColumn].HeaderCell.Value.ToString();
                    txtName.Text = nameOfColumn;
                }
            }
        }
    }
}
