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
        public void InitLeadConfig()
        {
            //硬件配置名称
            cbConfigList.Text = cbConfigList.Items[0].ToString();

            //初始化表格
            InitList();
        }

        /// <summary>
        /// 初始化导联列表
        /// </summary>
        /// <param name="p">硬件配置名称</param>
        public void InitList()
        {
            txtName.Text = "默认导联配置";
            myLeadLists = controller.CommonDataPool.LeadConfigLists;
            myLeadList = (Hashtable)myLeadLists[cbConfigList.Text];

            DataTable dt = new DataTable();
            InitDT(dt);
            ArrayList keys = new ArrayList(myLeadList.Keys);
            keys.Sort();
            for (int i = 0; i < keys.Count; i++)
            {
                dt.Columns.Add((string)keys[i], typeof(string));
                ArrayList value = (ArrayList)myLeadList[keys[i]];
                for (int j = 0; j < value.Count; j++)
                {
                    dt.Rows[j][i] = value[j];
                }
            }

            #region 数据绑定到listview
            //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            this.lvLeadList.BeginUpdate();  
 
            //清空listview
            this.lvLeadList.Items.Clear();  
            this.lvLeadList.Columns.Clear();

            //列
            this.lvLeadList.Columns.Add("编号", 100, HorizontalAlignment.Left);
            for (int j = 0; j < keys.Count; j++)
            {
                this.lvLeadList.Columns.Add(keys[j].ToString(), 120, HorizontalAlignment.Left);
            }
            
            //数据绑定
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                ListViewItem lvi = new ListViewItem();//新增一个ListViewItem
                lvi.Text = (i + 1).ToString();
               
                for (int j = 0; j < keys.Count; j++) 
                {
                    lvi.SubItems.Add(dt.Rows[i][keys[j].ToString()].ToString());
                }
                this.lvLeadList.Items.Add(lvi);//将新增列加入ListView
            }
            this.lvLeadList.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            #endregion
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
            if (selectedColumn == 0)
            {
                MessageBox.Show("您还没有选中导联编制。");
                return;
            }
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
            if (selectedColumn == 0)
            {
                MessageBox.Show("您还没有选中导联编制。");
                return;
            }
            if (nameOfColumn.Equals("默认导联配置"))
            {
                MessageBox.Show("这是系统默认的导联方式，您不能删除。");
                return;
            }
            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                myLeadList.Remove(nameOfColumn);
            }
            InitList();
        }

        private void lvLeadList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            selectedColumn = e.Column;
            nameOfColumn = this.lvLeadList.Columns[e.Column].Text;
            txtName.Text = nameOfColumn;
        }

        private void cbConfigList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitList();
        }
    }
}
