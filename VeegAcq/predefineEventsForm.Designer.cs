namespace VeegStation
 {
     partial class predefineEventsForm
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
             this.addEvent = new System.Windows.Forms.Button();
             this.btn_Quit = new System.Windows.Forms.Button();
             this.nameGroup = new System.Windows.Forms.GroupBox();
             this.calibrate = new System.Windows.Forms.RadioButton();
             this.deepBreath = new System.Windows.Forms.RadioButton();
             this.eyesClose = new System.Windows.Forms.RadioButton();
             this.eyesOpen = new System.Windows.Forms.RadioButton();
             this.deleteEvent = new System.Windows.Forms.Button();
             this.nameGroup.SuspendLayout();
             this.SuspendLayout();
             // 
             // eventList
             // 
             this.eventList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.time,
            this.num});
             this.eventList.FullRowSelect = true;
             this.eventList.GridLines = true;
             this.eventList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
             this.eventList.HideSelection = false;
             this.eventList.Location = new System.Drawing.Point(12, 43);
             this.eventList.Name = "eventList";
             this.eventList.Size = new System.Drawing.Size(191, 259);
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
             this.time.Text = "发生时间";
             // 
             // num
             // 
             this.num.Text = "编号";
             // 
             // addEvent
             // 
             this.addEvent.Location = new System.Drawing.Point(239, 43);
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
             this.btn_Quit.Location = new System.Drawing.Point(239, 123);
             this.btn_Quit.Name = "btn_Quit";
             this.btn_Quit.Size = new System.Drawing.Size(75, 23);
             this.btn_Quit.TabIndex = 2;
             this.btn_Quit.Text = "退出";
             this.btn_Quit.UseVisualStyleBackColor = true;
             this.btn_Quit.Click += new System.EventHandler(this.btn_Quit_Click);
             // 
             // nameGroup
             // 
             this.nameGroup.Controls.Add(this.calibrate);
             this.nameGroup.Controls.Add(this.deepBreath);
             this.nameGroup.Controls.Add(this.eyesClose);
             this.nameGroup.Controls.Add(this.eyesOpen);
             this.nameGroup.Location = new System.Drawing.Point(320, 43);
             this.nameGroup.Name = "nameGroup";
             this.nameGroup.Size = new System.Drawing.Size(84, 193);
             this.nameGroup.TabIndex = 3;
             this.nameGroup.TabStop = false;
             this.nameGroup.Text = "事件名称";
             // 
             // calibrate
             // 
             this.calibrate.AutoSize = true;
             this.calibrate.Location = new System.Drawing.Point(3, 83);
             this.calibrate.Name = "calibrate";
             this.calibrate.Size = new System.Drawing.Size(47, 16);
             this.calibrate.TabIndex = 3;
             this.calibrate.TabStop = true;
             this.calibrate.Text = "定标";
             this.calibrate.UseVisualStyleBackColor = true;
             this.calibrate.CheckedChanged += new System.EventHandler(this.radioButton_CheckChanged);
             // 
             // deepBreath
             // 
             this.deepBreath.AutoSize = true;
             this.deepBreath.Location = new System.Drawing.Point(3, 61);
             this.deepBreath.Name = "deepBreath";
             this.deepBreath.Size = new System.Drawing.Size(59, 16);
             this.deepBreath.TabIndex = 2;
             this.deepBreath.TabStop = true;
             this.deepBreath.Text = "深呼吸";
             this.deepBreath.UseVisualStyleBackColor = true;
             this.deepBreath.CheckedChanged += new System.EventHandler(this.radioButton_CheckChanged);
             // 
             // eyesClose
             // 
             this.eyesClose.AutoSize = true;
             this.eyesClose.Location = new System.Drawing.Point(3, 39);
             this.eyesClose.Name = "eyesClose";
             this.eyesClose.Size = new System.Drawing.Size(47, 16);
             this.eyesClose.TabIndex = 1;
             this.eyesClose.TabStop = true;
             this.eyesClose.Text = "闭眼";
             this.eyesClose.UseVisualStyleBackColor = true;
             this.eyesClose.CheckedChanged += new System.EventHandler(this.radioButton_CheckChanged);
             // 
             // eyesOpen
             // 
             this.eyesOpen.AutoSize = true;
             this.eyesOpen.Location = new System.Drawing.Point(3, 17);
             this.eyesOpen.Name = "eyesOpen";
             this.eyesOpen.Size = new System.Drawing.Size(47, 16);
             this.eyesOpen.TabIndex = 0;
             this.eyesOpen.TabStop = true;
             this.eyesOpen.Text = "睁眼";
             this.eyesOpen.UseVisualStyleBackColor = true;
             this.eyesOpen.CheckedChanged += new System.EventHandler(this.radioButton_CheckChanged);
             // 
             // deleteEvent
             // 
             this.deleteEvent.Location = new System.Drawing.Point(239, 83);
             this.deleteEvent.Name = "deleteEvent";
             this.deleteEvent.Size = new System.Drawing.Size(75, 23);
             this.deleteEvent.TabIndex = 4;
             this.deleteEvent.Text = "删除事件";
             this.deleteEvent.UseVisualStyleBackColor = true;
             this.deleteEvent.Click += new System.EventHandler(this.deleteEvent_Click);
             // 
             // predefineEventsForm
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.CancelButton = this.btn_Quit;
             this.ClientSize = new System.Drawing.Size(436, 405);
             this.ControlBox = false;
             this.Controls.Add(this.deleteEvent);
             this.Controls.Add(this.nameGroup);
             this.Controls.Add(this.btn_Quit);
             this.Controls.Add(this.addEvent);
             this.Controls.Add(this.eventList);
             this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
             this.Name = "predefineEventsForm";
             this.Text = "预定义事件";
             this.TopMost = true;
             this.nameGroup.ResumeLayout(false);
             this.nameGroup.PerformLayout();
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
         private System.Windows.Forms.RadioButton calibrate;
         private System.Windows.Forms.RadioButton deepBreath;
         private System.Windows.Forms.RadioButton eyesClose;
         private System.Windows.Forms.RadioButton eyesOpen;
         private System.Windows.Forms.Button deleteEvent;
     }
 }