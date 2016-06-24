namespace VeegStation
{
    partial class BandFilterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_lowFrequency = new System.Windows.Forms.TextBox();
            this.textBox_highFrequency = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_bandFilter = new System.Windows.Forms.CheckBox();
            this.groupBox_bandFilter = new System.Windows.Forms.GroupBox();
            this.groupBox_bandFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "低频参数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "高频参数：";
            // 
            // textBox_lowFrequency
            // 
            this.textBox_lowFrequency.Location = new System.Drawing.Point(66, 26);
            this.textBox_lowFrequency.Name = "textBox_lowFrequency";
            this.textBox_lowFrequency.Size = new System.Drawing.Size(71, 21);
            this.textBox_lowFrequency.TabIndex = 2;
            this.textBox_lowFrequency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_lowFrequency_KeyPress);
            // 
            // textBox_highFrequency
            // 
            this.textBox_highFrequency.Location = new System.Drawing.Point(66, 63);
            this.textBox_highFrequency.Name = "textBox_highFrequency";
            this.textBox_highFrequency.Size = new System.Drawing.Size(71, 21);
            this.textBox_highFrequency.TabIndex = 3;
            this.textBox_highFrequency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_highFrequency_KeyPress);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(37, 158);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(61, 23);
            this.button_OK.TabIndex = 4;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(130, 158);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(61, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hz";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Hz";
            // 
            // checkBox_bandFilter
            // 
            this.checkBox_bandFilter.AutoSize = true;
            this.checkBox_bandFilter.Location = new System.Drawing.Point(39, 26);
            this.checkBox_bandFilter.Name = "checkBox_bandFilter";
            this.checkBox_bandFilter.Size = new System.Drawing.Size(72, 16);
            this.checkBox_bandFilter.TabIndex = 8;
            this.checkBox_bandFilter.Text = "带通滤波";
            this.checkBox_bandFilter.UseVisualStyleBackColor = true;
            this.checkBox_bandFilter.CheckedChanged += new System.EventHandler(this.checkBox_bandFilter_CheckedChanged);
            // 
            // groupBox_bandFilter
            // 
            this.groupBox_bandFilter.Controls.Add(this.label4);
            this.groupBox_bandFilter.Controls.Add(this.label3);
            this.groupBox_bandFilter.Controls.Add(this.textBox_highFrequency);
            this.groupBox_bandFilter.Controls.Add(this.textBox_lowFrequency);
            this.groupBox_bandFilter.Controls.Add(this.label2);
            this.groupBox_bandFilter.Controls.Add(this.label1);
            this.groupBox_bandFilter.Location = new System.Drawing.Point(31, 53);
            this.groupBox_bandFilter.Name = "groupBox_bandFilter";
            this.groupBox_bandFilter.Size = new System.Drawing.Size(180, 99);
            this.groupBox_bandFilter.TabIndex = 9;
            this.groupBox_bandFilter.TabStop = false;
            this.groupBox_bandFilter.Text = "滤波参数";
            // 
            // FormBandFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 207);
            this.Controls.Add(this.groupBox_bandFilter);
            this.Controls.Add(this.checkBox_bandFilter);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Name = "FormBandFilter";
            this.Text = "带通滤波";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBandFilter_FormClosing);
            this.groupBox_bandFilter.ResumeLayout(false);
            this.groupBox_bandFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_lowFrequency;
        private System.Windows.Forms.TextBox textBox_highFrequency;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_bandFilter;
        private System.Windows.Forms.GroupBox groupBox_bandFilter;

    }
}