namespace VeegStation
{
    partial class LeadConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeadConfigForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.cbConfigList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.lvLeadList = new System.Windows.Forms.ListView();
            this.dataGridViewTest = new System.Windows.Forms.DataGridView();
            this.gbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTest)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前导联名称：";
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.cbConfigList);
            this.gbInfo.Controls.Add(this.label3);
            this.gbInfo.Controls.Add(this.txtName);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Location = new System.Drawing.Point(12, 12);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(302, 93);
            this.gbInfo.TabIndex = 1;
            this.gbInfo.TabStop = false;
            // 
            // cbConfigList
            // 
            this.cbConfigList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConfigList.FormattingEnabled = true;
            this.cbConfigList.Items.AddRange(new object[] {
            "8导脑电",
            "8导脑电+多参数",
            "16导脑电",
            "16导脑电+多参数",
            "24导脑电",
            "32导脑电",
            "32导脑电+多参数"});
            this.cbConfigList.Location = new System.Drawing.Point(95, 58);
            this.cbConfigList.Name = "cbConfigList";
            this.cbConfigList.Size = new System.Drawing.Size(187, 20);
            this.cbConfigList.TabIndex = 5;
            this.cbConfigList.SelectedIndexChanged += new System.EventHandler(this.cbConfigList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "硬件配置：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(115, 21);
            this.txtName.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(331, 26);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(331, 62);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(412, 26);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(412, 62);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 5;
            this.btnSetting.Text = "导联源设置";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // lvLeadList
            // 
            this.lvLeadList.GridLines = true;
            this.lvLeadList.Location = new System.Drawing.Point(12, 111);
            this.lvLeadList.Name = "lvLeadList";
            this.lvLeadList.Size = new System.Drawing.Size(738, 456);
            this.lvLeadList.TabIndex = 6;
            this.lvLeadList.UseCompatibleStateImageBehavior = false;
            this.lvLeadList.View = System.Windows.Forms.View.Details;
            this.lvLeadList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvLeadList_ColumnClick);
            // 
            // dataGridViewTest
            // 
            this.dataGridViewTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTest.Location = new System.Drawing.Point(817, 111);
            this.dataGridViewTest.Name = "dataGridViewTest";
            this.dataGridViewTest.RowHeadersVisible = false;
            this.dataGridViewTest.RowTemplate.Height = 23;
            this.dataGridViewTest.Size = new System.Drawing.Size(540, 456);
            this.dataGridViewTest.TabIndex = 7;
            this.dataGridViewTest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTest_CellClick);
            // 
            // LeadConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 579);
            this.Controls.Add(this.dataGridViewTest);
            this.Controls.Add(this.lvLeadList);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LeadConfigForm";
            this.Text = "导联配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LeadConfigForm_FormClosing);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.ComboBox cbConfigList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.ListView lvLeadList;
        private System.Windows.Forms.DataGridView dataGridViewTest;
    }
}