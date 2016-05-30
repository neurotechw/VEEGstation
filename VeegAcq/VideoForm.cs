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
    public partial class VideoForm : Form
    {
        public IVideoPlayer player;
        public IMedia _media;
        PlaybackForm playback;
  //      public NationFileInfo _nfi = null;
        public VideoForm(PlaybackForm playbackform)
        {
            InitializeComponent();
     //       _nfi = EegFile;
            playback = playbackform;
            this.ControlBox = false;
        }
        private void VideoForm_Load_1(object sender, EventArgs e)
        {
            if (playback._nfi == null)
            {
                Close();
                return;
            }
            IMediaPlayerFactory factory = new MediaPlayerFactory();
            player = factory.CreatePlayer<IVideoPlayer>();
        //    player.WindowHandle = panel_Mvideo.Handle;
            player.WindowHandle = pictureBox_Video.Handle;
            player.AspectRatio = AspectRatioMode.Mode2; // fill?
            if (playback._nfi.HasVideo)
            {
                _media = factory.CreateMedia<IMediaFromFile>(playback._nfi.VideoFullName);
                player.Open(_media);
                player.Time = (long)playback._nfi.VideoOffset * 1000;
                player.Pause();
            }
        }
        public void Play()
        {
            player.Play();
            //player.CropGeometry.CropArea.Location = new Point(100,500);
            player.Delay=1000;
        }
        public void Pause()
        {
            player.Pause();
        }
        private void btn_play_Click(object sender, EventArgs e)
        {
            if (!playback._player.IsPlaying)
            {
               Play();
               playback.Play();
            }
            btn_play.Enabled = false;
            btn_pause.Enabled = true;
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            Pause();
            playback.Pause();
            btn_pause.Enabled = false;
            btn_play.Enabled = true;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
            playback.timer.Enabled = false;
            playback.Pause();
        }

        private void btn_accelerate_Click(object sender, EventArgs e)
        {
            player.PlaybackRate = player.PlaybackRate * 2;
            playback._player.PlaybackRate = player.PlaybackRate;
            playback.speed = player.PlaybackRate;
        }

        private void btn_decelerate_Click(object sender, EventArgs e)
        {
            player.PlaybackRate = player.PlaybackRate / 2;
            playback._player.PlaybackRate = player.PlaybackRate;
            playback.speed = player.PlaybackRate;
           // player.Position = 100;
        }
        Point pt;
        private void panel_Mvideo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                int px = System.Windows.Forms.Control.MousePosition.X - pt.X;
                int py = System.Windows.Forms.Control.MousePosition.Y - pt.Y;
                pictureBox_Video.Location = new Point(pictureBox_Video.Location.X + px, pictureBox_Video.Location.Y + py);
                pt = System.Windows.Forms.Control.MousePosition;
            }
        }
        private void panel_Mvideo_MouseDown(object sender, MouseEventArgs e)
        {
           pt = System.Windows.Forms.Control.MousePosition;
        }

        private void panel_Mvideo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (player.VideoScale < 1.5f)
                    player.VideoScale += 0.5f;
                else
                    MessageBox.Show("放大到最大化","提示");
            }
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                if (player.VideoScale > 0.0f)
                    player.VideoScale -= 0.5f;
                else
                    MessageBox.Show("缩小到最小化", "提示");
            }
        }
    }
}
