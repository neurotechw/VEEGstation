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
        public NationFileInfo _nfi = null;
        public VideoForm(NationFileInfo EegFile)
        {
            InitializeComponent();
            _nfi = EegFile;
            this.ControlBox = false;
            this.pictureBox_Video.MouseWheel += new MouseEventHandler(pictureBox_Video_MouseWheel);
            playback = new PlaybackForm(EegFile);
        }
        float m_nScale = 1.0F;      //缩放比例
        Point m_ptCanvas;           //画布原点在设备上的坐标
        Bitmap m_bmp;               //画布图像
        Point m_ptBmp;              //图像位于画布坐标系中的坐标
        Point m_ptCanvasBuf;        //重置画布坐标计算时用的临时变量
        string m_strMousePt;        //鼠标当前位置对应的坐标
        Point m_ptMouseDown;        //鼠标点下是在设备坐标上的坐标
        private void VideoForm_Load_1(object sender, EventArgs e)
        {
            m_bmp = GetScreen();
            //初始化 坐标
            m_ptCanvas = new Point(pictureBox_Video.Width / 2, pictureBox_Video.Height / 2);
            m_ptBmp = new Point(-(m_bmp.Width / 2), -(m_bmp.Height / 2));
            if (_nfi == null)
            {
                Close();
                return;
            }
            IMediaPlayerFactory factory = new MediaPlayerFactory();
            player = factory.CreatePlayer<IVideoPlayer>();
     //       player.WindowHandle = panel_Mvideo.Handle;
            player.WindowHandle = pictureBox_Video.Handle;
            player.AspectRatio = AspectRatioMode.Mode2; // fill?
            if (_nfi.HasVideo)
            {
                _media = factory.CreateMedia<IMediaFromFile>(_nfi.VideoFullName);
                player.Open(_media);
                player.Time = (long)_nfi.VideoOffset * 1000;
                player.Pause();
            }
//            playback = new PlaybackForm(_nfi);
        }
        public void Play()
        {
            if(!player.IsPlaying)
            player.Play();
        }
        public void Pause()
        {
            player.Pause();
        }

        private void kryptonbtnplay_Click(object sender, EventArgs e)
        {
            player.Play();
            playback.Play();
        }

        private void kryptonbtn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
            playback.timer.Enabled = false;
        }

        private void kryptonbtnpause_Click(object sender, EventArgs e)
        {
            player.Pause();
            playback.Pause();
        }
        private void pictureBox_Video_MouseWheel(object sender, MouseEventArgs e)
        {
            if (m_nScale <= 0.3 && e.Delta <= 0) return;        //缩小下线
            if (m_nScale >= 4.9 && e.Delta >= 0) return;        //放大上线
            //获取 当前点到画布坐标原点的距离
            SizeF szSub = (Size)m_ptCanvas - (Size)e.Location;
            //当前的距离差除以缩放比还原到未缩放长度
            float tempX = szSub.Width / m_nScale;           //这里
            float tempY = szSub.Height / m_nScale;          //将画布比例
            //还原上一次的偏移                               //按照当前缩放比还原到
            m_ptCanvas.X -= (int)(szSub.Width - tempX);     //没有缩放
            m_ptCanvas.Y -= (int)(szSub.Height - tempY);    //的状态
            //重置距离差为  未缩放状态                       
            szSub.Width = tempX;
            szSub.Height = tempY;
            m_nScale += e.Delta > 0 ? 0.2F : -0.2F;
            //重新计算 缩放并 重置画布原点坐标
            m_ptCanvas.X += (int)(szSub.Width * m_nScale - szSub.Width);
            m_ptCanvas.Y += (int)(szSub.Height * m_nScale - szSub.Height);
            pictureBox_Video.Invalidate();
        }

        private void pictureBox_Video_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(m_ptCanvas.X, m_ptCanvas.Y);       //设置坐标偏移
            g.ScaleTransform(m_nScale, m_nScale);                   //设置缩放比
            g.DrawImage(m_bmp, m_ptBmp);                            //绘制图像
            g.ResetTransform();                                     //重置坐标系
            Pen p = new Pen(Color.Cyan, 3);
            p.Dispose();        
            Size szTemp = pictureBox_Video.Size - (Size)m_ptCanvas;
            PointF ptCanvasOnShowRectLT = new PointF(
                -m_ptCanvas.X / m_nScale, -m_ptCanvas.Y / m_nScale);
            PointF ptCanvasOnShowRectRB = new PointF(
                szTemp.Width / m_nScale, szTemp.Height / m_nScale);
        }
        public Bitmap GetScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            }
            return bmp;
        }

        private void pictureBox_Video_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {      //如果中键点下    初始化计算要用的临时数据
                m_ptMouseDown = e.Location;
                m_ptCanvasBuf = m_ptCanvas;
            }
            pictureBox_Video.Focus();
        }

        private void pictureBox_Video_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {   
                m_ptCanvas = (Point)((Size)m_ptCanvasBuf + ((Size)e.Location - (Size)m_ptMouseDown));
                pictureBox_Video.Invalidate();
            }
            //计算 右上角显示的坐标信息
            SizeF szSub = (Size)e.Location - (Size)m_ptCanvas;  //计算鼠标当前点对应画布中的坐标
            szSub.Width /= m_nScale;
            szSub.Height /= m_nScale;
            Size sz = TextRenderer.MeasureText(m_strMousePt, this.Font);    //获取上一次的区域并重绘
            pictureBox_Video.Invalidate(new Rectangle(pictureBox_Video.Width - sz.Width, 0, sz.Width, sz.Height));
            m_strMousePt = e.Location.ToString() + "\n" + ((Point)(szSub.ToSize())).ToString();
            sz = TextRenderer.MeasureText(m_strMousePt, this.Font);         //绘制新的区域
            pictureBox_Video.Invalidate(new Rectangle(pictureBox_Video.Width - sz.Width, 0, sz.Width, sz.Height));
        }
    }
}
