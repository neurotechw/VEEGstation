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
        Point[] ArrPoint = new Point[4];
        double scale=0.5;
        int x, y;
        int width, height;
  //      public NationFileInfo _nfi = null;
        public VideoForm(PlaybackForm playbackform)
        {
            InitializeComponent();
     //       _nfi = EegFile;
            playback = playbackform;
            this.ControlBox = false;
        }
         Point m_ptCanvas;
        private void VideoForm_Load_1(object sender, EventArgs e)
        {
            m_ptCanvas = this.pictureBox_Video.Location;
            if (playback._nfi == null)
            {
                Close();
                return;
            }
            IMediaPlayerFactory factory = new MediaPlayerFactory();
            player = factory.CreatePlayer<IVideoPlayer>();
            player.WindowHandle = pictureBox_Video.Handle;
            player.AspectRatio = AspectRatioMode.Mode2; // fill?
            if (playback._nfi.HasVideo)
            {
                _media = factory.CreateMedia<IMediaFromFile>(playback._nfi.VideoFullName);
                player.Open(_media);
                player.Pause();       
             }
             x = this.pictureBox_Video.Location.X;
             y = this.pictureBox_Video.Location.Y;
             width = this.pictureBox_Video.Width;
             height = this.pictureBox_Video.Height;
             //btn_accelerate.Enabled = playback.btn_accelerate.Enabled;
             //btn_decelerate.Enabled = playback.btn_decelerate.Enabled;
        }
        public void Play()
        {
            player.Play();
            player.Time = (long)playback._nfi.VideoOffset * 1000 + playback._currentSeconds * 1000 + (long)playback.chartWave.ChartAreas[0].AxisX.StripLines[0].IntervalOffset * 1000;
        }
        public void Pause()
        {
            player.Pause();
        }
        private void btn_play_Click(object sender, EventArgs e)
        {
            if (playback._CurrentOffset >= playback._nfi.Duration.TotalSeconds)
            {
                playback.clear();
            }
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
            if (playback.speed <= 4)
            {
                btn_decelerate.Enabled = true;
                btn_accelerate.Enabled = true;
                player.PlaybackRate = player.PlaybackRate * 2;
                playback._player.PlaybackRate = player.PlaybackRate;
                playback.speed = player.PlaybackRate;
                playback.btn_decelerate.Enabled = true;
                playback.btn_accelerate.Enabled = true;
            }
            else
            {
                btn_accelerate.Enabled = false;
                playback.btn_accelerate.Enabled = false;
            }
        }
        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pt = new Point((int)(scale * (this.pictureBox_Video.Location.X - e.Location.X)), (int)(scale * (this.pictureBox_Video.Location.Y - e.Location.Y)));
                pictureBox_Video.Location = pt;
                pictureBox_Video.Height = (int)(pictureBox_Video.Height * 1.4);
                pictureBox_Video.Width = (int)(pictureBox_Video.Width * 1.4);
            }
            if (e.Button == MouseButtons.Right)
            {            
                    Point pt = new Point(x, y);
                    //     pictureBox_Video.Width += (this.pictureBox_Video.Location.X + Height - e.Location.X) * scale +(this.pictureBox_Video.Location.X) * scale);
                    pictureBox_Video.Location = pt;
                    pictureBox_Video.Height = height;
                    pictureBox_Video.Width = width;
            }
        }

        private void btn_decelerate_Click_1(object sender, EventArgs e)
        {
            if (playback.speed >= 0.5)
            {
                btn_accelerate.Enabled = true;
                btn_decelerate.Enabled = true;
                player.PlaybackRate = player.PlaybackRate / 2;
                playback._player.PlaybackRate = player.PlaybackRate;
                playback.speed = player.PlaybackRate;
                playback.btn_accelerate.Enabled = true;
                playback.btn_decelerate.Enabled = true;
            }
            else
            {
                btn_decelerate.Enabled = false;
                playback.btn_decelerate.Enabled = false;
            }
        }
        Point pt;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                int px = System.Windows.Forms.Control.MousePosition.X - pt.X;
                int py = System.Windows.Forms.Control.MousePosition.Y - pt.Y;
                pictureBox_Video.Location = new Point(pictureBox_Video.Location.X + px, pictureBox_Video.Location.Y + py);
                pt = System.Windows.Forms.Control.MousePosition;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pt = System.Windows.Forms.Control.MousePosition;
        }
    }
}
