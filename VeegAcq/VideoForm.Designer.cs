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
            this.panel_Mvideo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.kryptonbtn_close = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonbtnpause = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonbtnplay = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pictureBox_Video = new System.Windows.Forms.PictureBox();
            this.panel_Mvideo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Mvideo
            // 
            this.panel_Mvideo.Controls.Add(this.pictureBox_Video);
            this.panel_Mvideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Mvideo.Location = new System.Drawing.Point(0, 0);
            this.panel_Mvideo.Name = "panel_Mvideo";
            this.panel_Mvideo.Size = new System.Drawing.Size(547, 540);
            this.panel_Mvideo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.kryptonbtn_close);
            this.panel1.Controls.Add(this.kryptonbtnpause);
            this.panel1.Controls.Add(this.kryptonbtnplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 540);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 35);
            this.panel1.TabIndex = 1;
            // 
            // kryptonbtn_close
            // 
            this.kryptonbtn_close.Location = new System.Drawing.Point(196, 7);
            this.kryptonbtn_close.Name = "kryptonbtn_close";
            this.kryptonbtn_close.Size = new System.Drawing.Size(90, 25);
            this.kryptonbtn_close.TabIndex = 2;
            this.kryptonbtn_close.Values.Text = "关闭";
            this.kryptonbtn_close.Click += new System.EventHandler(this.kryptonbtn_close_Click);
            // 
            // kryptonbtnpause
            // 
            this.kryptonbtnpause.Location = new System.Drawing.Point(100, 6);
            this.kryptonbtnpause.Name = "kryptonbtnpause";
            this.kryptonbtnpause.Size = new System.Drawing.Size(90, 25);
            this.kryptonbtnpause.TabIndex = 1;
            this.kryptonbtnpause.Values.Text = "暂停";
            this.kryptonbtnpause.Click += new System.EventHandler(this.kryptonbtnpause_Click);
            // 
            // kryptonbtnplay
            // 
            this.kryptonbtnplay.Location = new System.Drawing.Point(3, 7);
            this.kryptonbtnplay.Name = "kryptonbtnplay";
            this.kryptonbtnplay.Size = new System.Drawing.Size(90, 25);
            this.kryptonbtnplay.TabIndex = 0;
            this.kryptonbtnplay.Values.Text = "播放";
            this.kryptonbtnplay.Click += new System.EventHandler(this.kryptonbtnplay_Click);
            // 
            // pictureBox_Video
            // 
            this.pictureBox_Video.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Video.Name = "pictureBox_Video";
            this.pictureBox_Video.Size = new System.Drawing.Size(547, 541);
            this.pictureBox_Video.TabIndex = 0;
            this.pictureBox_Video.TabStop = false;
            this.pictureBox_Video.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Video_Paint);
            this.pictureBox_Video.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Video_MouseDown);
            this.pictureBox_Video.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Video_MouseMove);
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 575);
            this.Controls.Add(this.panel_Mvideo);
            this.Controls.Add(this.panel1);
            this.Name = "VideoForm";
            this.Text = "VideoForm";
            this.Load += new System.EventHandler(this.VideoForm_Load_1);
            this.panel_Mvideo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Mvideo;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonbtnplay;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonbtnpause;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonbtn_close;
        private System.Windows.Forms.PictureBox pictureBox_Video;
    }
}