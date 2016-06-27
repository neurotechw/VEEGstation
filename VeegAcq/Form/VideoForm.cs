using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Declarations;
using Declarations.Players;
using Declarations.Media;
using Implementation;
namespace VeegStation
{
    /// <summary>
    /// 弹出视频Form
    /// --by wsp
    /// </summary>
    public partial class VideoForm : Form
    {
        /// <summary>
        /// 定义播放器
        /// </summary>
        public IVideoPlayer PlayerVideo;

        /// <summary>
        /// 定义视频media
        /// </summary>
        public IMedia Media;

        /// <summary>
        /// 定义回放Form
        /// </summary>
        PlaybackForm playBack;

        /// <summary>
        /// 定义视频点击放大缩小比例
        /// </summary>
        double scale = 0.5;

        /// <summary>
        /// 定义pictureBox的X，Y位置
        /// </summary>
        int x;
        int y;

        /// <summary>
        /// 定义pictureBox的宽度，高度
        /// </summary>
        int width;
        int height;

        public VideoForm(PlaybackForm playbackform)
        {
            InitializeComponent();
            playBack = playbackform;
            this.ControlBox = false;
        }

        Point m_ptCanvas;
        private void VideoForm_Load_1(object sender, EventArgs e)
        {
            //文字浮动
            toolTip1.SetToolTip(btn_play, "播放");
            toolTip1.SetToolTip(btn_pause, "暂停");
            toolTip1.SetToolTip(btn_accelerate, "加速");
            toolTip1.SetToolTip(btn_decelerate, "减速");
            toolTip1.SetToolTip(btn_close, "关闭");
            m_ptCanvas = this.pictureBox_Video.Location;
            if (playBack.nfi == null)
            {
                Close();
                return;
            }

            IMediaPlayerFactory factory = new MediaPlayerFactory();

            //创建播放器
            PlayerVideo = factory.CreatePlayer<IVideoPlayer>();

            //窗口句柄（在picture_Box上播放视频）
            PlayerVideo.WindowHandle = pictureBox_Video.Handle;

            //宽高比（4：3，16：9等等）
            PlayerVideo.AspectRatio = AspectRatioMode.Mode2;
            if (playBack.nfi.HasVideo)
            {
                //获得media视频文件
                Media = factory.CreateMedia<IMediaFromFile>(playBack.nfi.VideoFullName);

                //打开该文件
                PlayerVideo.Open(Media); 
                PlayerVideo.Play(); 
                PlayerVideo.Time = (long)(playBack.nfi.VideoOffset * 1000 + playBack.CurrentSeconds * 1000 + playBack.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000);
            }
            //获得picturebox的X,Y值，宽，高
            x = this.pictureBox_Video.Location.X;
            y = this.pictureBox_Video.Location.Y; 
            width = this.pictureBox_Video.Width;
            height = this.pictureBox_Video.Height;
            if (playBack.Player.IsPlaying)
            {
                btn_pause.Enabled = true;
                btn_play.Enabled = false;
            }
            else
            {
                btn_pause.Enabled = false;
                btn_play.Enabled = true;
            }
        }

        /// <summary>
        /// 播放函数
        /// </summary>
        public void PlayVideo()
        {
            PlayerVideo.Play();
            PlayerVideo.Time = (long)(playBack.nfi.VideoOffset * 1000 + playBack.CurrentSeconds * 1000 + playBack.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000);
        }

        //暂停函数
        public void PauseVideo()
        {
            if (PlayerVideo.IsPlaying)
                PlayerVideo.Pause();
        }

        /// <summary>
        /// 播放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_play_Click(object sender, EventArgs e)
        {
            if (playBack.CurrentOffset >= playBack.nfi.Duration.TotalSeconds)
            {
                playBack.Clear();
            }
            if (!playBack.Player.IsPlaying)
            {
                PlayVideo();
                playBack.Play();
            }
            btn_play.Enabled = false;
            btn_pause.Enabled = true;
        }

        /// <summary>
        /// 暂停事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_pause_Click(object sender, EventArgs e)
        {
            PauseVideo();
            playBack.Pause();
            btn_pause.Enabled = false;
            btn_play.Enabled = true;
        }

        /// <summary>
        /// 关闭窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, EventArgs e)
        {
            //这里并不是销毁Form,只是隐藏，否则视频将不与回放Form同步
            this.Hide();
            playBack.panelVideo.Visible = true;
        }

        /// <summary>
        /// 加速事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_accelerate_Click(object sender, EventArgs e)
        {
            //倍速过大，视频花屏，需要设置阈值
            if (playBack.Speed <= 4)
            {
                btn_decelerate.Enabled = true;
                btn_accelerate.Enabled = true;
                PlayerVideo.PlaybackRate = PlayerVideo.PlaybackRate * 2;
                playBack.Player.PlaybackRate = PlayerVideo.PlaybackRate;
                playBack.Speed = PlayerVideo.PlaybackRate;
                playBack.btn_decelerate.Enabled = true;
                playBack.btn_accelerate.Enabled = true;
            }
            else
            {
                btn_decelerate.Enabled = true;
                btn_accelerate.Enabled = false;
                playBack.btn_decelerate.Enabled = true;
                playBack.btn_accelerate.Enabled = false;
            }
        }

        /// <summary>
        /// 鼠标单击事件（鼠标中心点进行放大）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {

            //左键点击是进行放大的
            if (e.Button == MouseButtons.Left)
            {
                //不能无限制放大，否则视频画面变得非常模糊
                if (pictureBox_Video.Height < height * 2 && pictureBox_Video.Width < width * 2)
                {
                    //相似三角形推导出的数学公式
                    Point pt = new Point((int)(scale * (this.pictureBox_Video.Location.X - e.Location.X)), (int)(scale * (this.pictureBox_Video.Location.Y - e.Location.Y)));
                    pictureBox_Video.Location = pt;
                    pictureBox_Video.Height = (int)(pictureBox_Video.Height * 1.5);
                    pictureBox_Video.Width = (int)(pictureBox_Video.Width * 1.5);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                //鼠标右键点击是回到初始状态，之所以不返回上一个状态是因为Location发生了变化，不能保证回到上一个状态不出现大的误差
                Point pt = new Point(x, y);
                pictureBox_Video.Location = pt;
                pictureBox_Video.Height = height;
                pictureBox_Video.Width = width;
            }
        }

        /// <summary>
        /// 减速事件
        ///--by wsp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_decelerate_Click_1(object sender, EventArgs e)
        {
            if (playBack.Speed >= 0.5)
            {
                btn_accelerate.Enabled = true;
                btn_decelerate.Enabled = true;
                PlayerVideo.PlaybackRate = PlayerVideo.PlaybackRate / 2;
                playBack.Player.PlaybackRate = PlayerVideo.PlaybackRate;
                playBack.Speed = PlayerVideo.PlaybackRate;
                playBack.btn_accelerate.Enabled = true;
                playBack.btn_decelerate.Enabled = true;
            }
            else
            {
                btn_accelerate.Enabled = true;
                btn_decelerate.Enabled = false;
                playBack.btn_accelerate.Enabled = true;
                playBack.btn_decelerate.Enabled = false;
            }
        }

        #region 拖动查看不同视频的部分
        /// <summary>
        /// 鼠标拖动事件
        /// </summary>
        Point pt;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //拖动鼠标左键，计算鼠标的位置和pictureBox_Video位置进行计算
                int px = System.Windows.Forms.Control.MousePosition.X - pt.X;
                int py = System.Windows.Forms.Control.MousePosition.Y - pt.Y;
                pictureBox_Video.Location = new Point(pictureBox_Video.Location.X + px, pictureBox_Video.Location.Y + py);
                pt = System.Windows.Forms.Control.MousePosition;
            }
        }

        /// <summary>
        /// 鼠标释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pt = System.Windows.Forms.Control.MousePosition;
        }
        #endregion

    }
}
