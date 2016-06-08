namespace VeegStation
{
    partial class CalibrateYForm
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.valueBox = new System.Windows.Forms.NumericUpDown();
            this.linePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.valueBox)).BeginInit();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(33, 197);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "确定";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(129, 197);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "调整线段长度，直到其长度正好为5CM";
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(51, 27);
            this.valueBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(120, 21);
            this.valueBox.TabIndex = 5;
            this.valueBox.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // linePanel
            // 
            this.linePanel.Location = new System.Drawing.Point(51, 117);
            this.linePanel.Name = "linePanel";
            this.linePanel.Size = new System.Drawing.Size(263, 40);
            this.linePanel.TabIndex = 6;
            this.linePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            // 
            // CalibrateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 232);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.linePanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Name = "CalibrateForm";
            this.Text = "Y轴校准";
            ((System.ComponentModel.ISupportInitialize)(this.valueBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown valueBox;
        private System.Windows.Forms.Panel linePanel;
    }
}