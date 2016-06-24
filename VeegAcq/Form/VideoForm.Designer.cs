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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoForm));
            this.btn_accelerate = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Video = new System.Windows.Forms.PictureBox();
            this.btn_decelerate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_accelerate
            // 
            this.btn_accelerate.Image = ((System.Drawing.Image)(resources.GetObject("btn_accelerate.Image")));
            this.btn_accelerate.Location = new System.Drawing.Point(306, 14);
            this.btn_accelerate.Name = "btn_accelerate";
            this.btn_accelerate.Size = new System.Drawing.Size(75, 23);
            this.btn_accelerate.TabIndex = 6;
            this.btn_accelerate.UseVisualStyleBackColor = true;
            this.btn_accelerate.Click += new System.EventHandler(this.btn_accelerate_Click);
            // 
            // btn_close
            // 
            this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
            this.btn_close.Location = new System.Drawing.Point(202, 14);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 5;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.Image = ((System.Drawing.Image)(resources.GetObject("btn_pause.Image")));
            this.btn_pause.Location = new System.Drawing.Point(107, 14);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(75, 23);
            this.btn_pause.TabIndex = 4;
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // btn_play
            // 
            this.btn_play.Image = ((System.Drawing.Image)(resources.GetObject("btn_play.Image")));
            this.btn_play.Location = new System.Drawing.Point(12, 14);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(75, 23);
            this.btn_play.TabIndex = 3;
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox_Video);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_decelerate);
            this.splitContainer1.Panel2.Controls.Add(this.btn_play);
            this.splitContainer1.Panel2.Controls.Add(this.btn_accelerate);
            this.splitContainer1.Panel2.Controls.Add(this.btn_pause);
            this.splitContainer1.Panel2.Controls.Add(this.btn_close);
            this.splitContainer1.Size = new System.Drawing.Size(606, 582);
            this.splitContainer1.SplitterDistance = 538;
            this.splitContainer1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Parent = this.pictureBox_Video;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 538);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick_1);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // pictureBox_Video
            // 
            this.pictureBox_Video.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Video.Name = "pictureBox_Video";
            this.pictureBox_Video.Size = new System.Drawing.Size(606, 538);
            this.pictureBox_Video.TabIndex = 0;
            this.pictureBox_Video.TabStop = false;
            // 
            // btn_decelerate
            // 
            this.btn_decelerate.Image = ((System.Drawing.Image)(resources.GetObject("btn_decelerate.Image")));
            this.btn_decelerate.Location = new System.Drawing.Point(420, 13);
            this.btn_decelerate.Name = "btn_decelerate";
            this.btn_decelerate.Size = new System.Drawing.Size(75, 23);
            this.btn_decelerate.TabIndex = 7;
            this.btn_decelerate.UseVisualStyleBackColor = true;
            this.btn_decelerate.Click += new System.EventHandler(this.btn_decelerate_Click_1);
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 582);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "VideoForm";
            this.Text = "VideoForm";
            this.Load += new System.EventHandler(this.VideoForm_Load_1);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Button btn_close;
        public System.Windows.Forms.Button btn_pause;
        public System.Windows.Forms.Button btn_accelerate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox_Video;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button btn_decelerate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

