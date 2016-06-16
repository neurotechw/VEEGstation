namespace VeegStation
{
    partial class CustomEventForm
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
            this.eventList = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_quit = new System.Windows.Forms.Button();
            this.color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // eventList
            // 
            this.eventList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.time,
            this.num,
            this.color});
            this.eventList.FullRowSelect = true;
            this.eventList.GridLines = true;
            this.eventList.Location = new System.Drawing.Point(12, 12);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(252, 249);
            this.eventList.TabIndex = 0;
            this.eventList.UseCompatibleStateImageBehavior = false;
            this.eventList.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "事件名称";
            // 
            // time
            // 
            this.time.Text = "事件时间";
            // 
            // num
            // 
            this.num.Text = "编号";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(272, 12);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "增加事件";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(272, 82);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_edit.TabIndex = 2;
            this.btn_edit.Text = "编辑事件";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(272, 152);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 3;
            this.btn_delete.Text = "删除事件";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btn_quit
            // 
            this.btn_quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_quit.Location = new System.Drawing.Point(272, 222);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(75, 23);
            this.btn_quit.TabIndex = 4;
            this.btn_quit.Text = "退出";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.Btn_quit_Click);
            // 
            // color
            // 
            this.color.Text = "颜色";
            // 
            // CustomEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_quit;
            this.ClientSize = new System.Drawing.Size(359, 273);
            this.ControlBox = false;
            this.Controls.Add(this.btn_quit);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.eventList);
            this.Name = "CustomEventForm";
            this.Text = "自定义事件";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView eventList;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader num;
        private System.Windows.Forms.ColumnHeader color;
    }
}