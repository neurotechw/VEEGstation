namespace VeegStation
{
    partial class VideoForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_decelerate = new System.Windows.Forms.Button();
            this.btn_accelerate = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.pictureBox_Video = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_decelerate);
            this.panel1.Controls.Add(this.btn_accelerate);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_pause);
            this.panel1.Controls.Add(this.btn_play);
            this.panel1.Location = new System.Drawing.Point(0, 554);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 28);
            this.panel1.TabIndex = 0;
            // 
            // btn_decelerate
            // 
            this.btn_decelerate.Location = new System.Drawing.Point(410, 0);
            this.btn_decelerate.Name = "btn_decelerate";
            this.btn_decelerate.Size = new System.Drawing.Size(75, 23);
            this.btn_decelerate.TabIndex = 4;
            this.btn_decelerate.Text = "减速";
            this.btn_decelerate.UseVisualStyleBackColor = true;
            this.btn_decelerate.Click += new System.EventHandler(this.btn_decelerate_Click);
            // 
            // btn_accelerate
            // 
            this.btn_accelerate.Location = new System.Drawing.Point(315, 0);
            this.btn_accelerate.Name = "btn_accelerate";
            this.btn_accelerate.Size = new System.Drawing.Size(75, 23);
            this.btn_accelerate.TabIndex = 3;
            this.btn_accelerate.Text = "加速";
            this.btn_accelerate.UseVisualStyleBackColor = true;
            this.btn_accelerate.Click += new System.EventHandler(this.btn_accelerate_Click_1);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(216, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click_1);
            // 
            // btn_pause
            // 
            this.btn_pause.Location = new System.Drawing.Point(120, 0);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(75, 23);
            this.btn_pause.TabIndex = 1;
            this.btn_pause.Text = "暂停";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click_1);
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(26, 0);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(75, 23);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "播放";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click_1);
            // 
            // pictureBox_Video
            // 
            this.pictureBox_Video.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Video.Name = "pictureBox_Video";
            this.pictureBox_Video.Size = new System.Drawing.Size(606, 549);
            this.pictureBox_Video.TabIndex = 1;
            this.pictureBox_Video.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Parent = this.pictureBox_Video;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 549);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 582);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox_Video);
            this.Name = "VideoForm";
            this.Text = "VideoForm";
            this.Load += new System.EventHandler(this.VideoForm_Load_1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btn_decelerate;
        public System.Windows.Forms.Button btn_accelerate;
        private System.Windows.Forms.Button btn_close;
        public System.Windows.Forms.Button btn_pause;
        public System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.PictureBox pictureBox_Video;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}