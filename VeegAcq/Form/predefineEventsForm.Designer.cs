namespace VeegStation
 {
     partial class PredefineEventsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PredefineEventsForm));
            this.eventList = new System.Windows.Forms.ListView();
            this.num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addEvent = new System.Windows.Forms.Button();
            this.btn_Quit = new System.Windows.Forms.Button();
            this.nameGroup = new System.Windows.Forms.GroupBox();
            this.deleteEvent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventList
            // 
            this.eventList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.num,
            this.name,
            this.time});
            this.eventList.FullRowSelect = true;
            this.eventList.GridLines = true;
            this.eventList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.eventList.HideSelection = false;
            this.eventList.Location = new System.Drawing.Point(12, 13);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(191, 299);
            this.eventList.TabIndex = 0;
            this.eventList.UseCompatibleStateImageBehavior = false;
            this.eventList.View = System.Windows.Forms.View.Details;
            // 
            // num
            // 
            this.num.Text = "编号";
            // 
            // name
            // 
            this.name.Text = "事件名称";
            // 
            // time
            // 
            this.time.Text = "发生时间";
            // 
            // addEvent
            // 
            this.addEvent.Location = new System.Drawing.Point(239, 13);
            this.addEvent.Name = "addEvent";
            this.addEvent.Size = new System.Drawing.Size(75, 23);
            this.addEvent.TabIndex = 1;
            this.addEvent.Text = "添加事件";
            this.addEvent.UseVisualStyleBackColor = true;
            this.addEvent.Click += new System.EventHandler(this.addEvent_Click);
            // 
            // btn_Quit
            // 
            this.btn_Quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Quit.Location = new System.Drawing.Point(239, 93);
            this.btn_Quit.Name = "btn_Quit";
            this.btn_Quit.Size = new System.Drawing.Size(75, 23);
            this.btn_Quit.TabIndex = 2;
            this.btn_Quit.Text = "退出";
            this.btn_Quit.UseVisualStyleBackColor = true;
            this.btn_Quit.Click += new System.EventHandler(this.btn_Quit_Click);
            // 
            // nameGroup
            // 
            this.nameGroup.Location = new System.Drawing.Point(320, 13);
            this.nameGroup.Name = "nameGroup";
            this.nameGroup.Size = new System.Drawing.Size(84, 299);
            this.nameGroup.TabIndex = 3;
            this.nameGroup.TabStop = false;
            this.nameGroup.Text = "事件名称";
            // 
            // deleteEvent
            // 
            this.deleteEvent.Location = new System.Drawing.Point(239, 53);
            this.deleteEvent.Name = "deleteEvent";
            this.deleteEvent.Size = new System.Drawing.Size(75, 23);
            this.deleteEvent.TabIndex = 4;
            this.deleteEvent.Text = "删除事件";
            this.deleteEvent.UseVisualStyleBackColor = true;
            this.deleteEvent.Click += new System.EventHandler(this.deleteEvent_Click);
            // 
            // PredefineEventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Quit;
            this.ClientSize = new System.Drawing.Size(419, 316);
            this.ControlBox = false;
            this.Controls.Add(this.deleteEvent);
            this.Controls.Add(this.nameGroup);
            this.Controls.Add(this.btn_Quit);
            this.Controls.Add(this.addEvent);
            this.Controls.Add(this.eventList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PredefineEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "预定义事件";
            this.TopMost = true;
            this.ResumeLayout(false);

         }

         #endregion

         private System.Windows.Forms.ListView eventList;
         private System.Windows.Forms.Button addEvent;
         private System.Windows.Forms.Button btn_Quit;
         private System.Windows.Forms.ColumnHeader name;
         private System.Windows.Forms.ColumnHeader time;
         private System.Windows.Forms.ColumnHeader num;
         private System.Windows.Forms.GroupBox nameGroup;
         private System.Windows.Forms.Button deleteEvent;
     }
 }