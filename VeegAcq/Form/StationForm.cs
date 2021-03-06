﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Threading;

using Declarations;
using Declarations.Players;
using Declarations.Media;
using Implementation;

namespace VeegStation
{
    public partial class StationForm : Form
    {
        /// <summary>
        /// controller  --by zt
        /// </summary>
        private VeegControl myController;

        private List<VideoFileInfo> _videoFiles = new List<VideoFileInfo>();
        private List<NationFile> _eegFiles = new List<NationFile>();

        private IVideoPlayer _player;
        private IMedia _media;

        public StationForm()
        {
            InitializeComponent();
            LogHelper.WriteLog(typeof(StationForm), "测试日志是否添加成功");
            myController = new VeegControl();
            this.tbEEGPath.Text = myController.CommonDataPool.EegDataPath;
            this.tbVideoPath.Text = myController.CommonDataPool.VideoPath;
        }

        private void HelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("VEEG");
        }

        private void AcqForm_Load(object sender, EventArgs e)
        {
            RefreshFiles();

            chartWave.Series.Clear();
            foreach (int n in Enumerable.Range(0, 20))
            {
                Series s = new Series("Ch" + (n + 1).ToString());
                s.ChartType = SeriesChartType.FastLine;
                s.BorderDashStyle = ChartDashStyle.Solid;
                s.Color = Color.Black;
                s.BorderWidth = 1;
                foreach (int x in Enumerable.Range(0, 1024))
                {
                    s.Points.AddXY(x / 128.0, 4000 - 100 - 200 * n);
                }

                chartWave.Series.Add(s);
            }
            timerVideo.Enabled = true;
            UpdateAssoctionButton();

        }

        private void AcqForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // TODO: fixed media-related operations cause null reference
            try
            {
                _player.Stop();
                _player.Dispose();
            }
            catch { }
            try
            {
                _media.Dispose();
            }
            catch { }

            myController.SaveConfitWhileQuite();
        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {
            if (_player != null && _player.IsPlaying)
            {

            }
            else
            {
                picturePreview.Image = null;
            }
        }

        private void LoadEegDataList()
        {
            if (!Directory.Exists(myController.CommonDataPool.EegDataPath))
            {
                MessageBox.Show("文件夹:" + myController.CommonDataPool.EegDataPath + " 不存在，请重新选择保存脑电数据的病例库文件夹");
                return;
            }
            _eegFiles.Clear();
            DirectoryInfo diTop = new DirectoryInfo(myController.CommonDataPool.EegDataPath);

            lvFiles.BeginUpdate();
            lvFiles.Items.Clear();
            foreach (var sub in diTop.EnumerateDirectories())
            {
                //foreach (var file in sub.EnumerateFiles("*.NED"))
                //{
                //    try
                //    {
                //        NationFileInfo nfi = new NationFileInfo(file.FullName);
                //        _eegFiles.Add(nfi);
                //     }
                //    catch
                //    {
                //    }
                //}
                
                foreach (var file in sub.EnumerateFiles("*.NAT"))
                {
                    try
                    {
                        NationFile nationFile = new NationFile(this.myController);
                        nationFile.ReadNationFile(file.FullName);
                        nationFile.CheckHasVideo();
                        _eegFiles.Add(nationFile);
                    }
                    catch
                    {
                    }
                }
            }
            //按照开始时间比较
            _eegFiles.Sort(new NationFileComparer());
            //_eegFiles.Sort(new NationFileInfoComparer());
            //foreach (NationFile nfi in _eegFiles)
            //{
            //    string dt = nfi.StartTime.ToString("s").Replace('T', ' ');
            //    string marker = nfi.HasVideo ? "V" : "";
            //    ListViewItem lvi = new ListViewItem(marker);
            //    lvi.SubItems.Add(nfi.SerialNo);
            //    lvi.SubItems.Add(nfi.Patient);
            //    TimeSpan ts = nfi.Duration;
            //    string tsText = (ts.Days * 24 + ts.Hours).ToString() + ts.ToString(@"\:mm\:ss");
            //    lvi.SubItems.Add(tsText);
            //    lvi.SubItems.Add(dt);
            //    lvFiles.Items.Add(lvi);
            //}

            foreach (var file in diTop.EnumerateFiles("*.NAT"))
            {
                try
                {
                    NationFile nationFile = new NationFile(this.myController);
                    nationFile.ReadNationFile(file.FullName);
                    nationFile.CheckHasVideo();
                    _eegFiles.Add(nationFile);
                }
                catch
                {
                }
            }

            foreach (NationFile nfi in _eegFiles)
            {
                string dt = nfi.StartDateTime.ToString("s").Replace('T', ' ');
                string marker = nfi.HasVideo ? "V" : "";
                ListViewItem lvi = new ListViewItem(marker);
                lvi.SubItems.Add(nfi.PatInfo.ID);
                lvi.SubItems.Add(nfi.PatInfo.Name);
                TimeSpan ts = nfi.Duration;
                string tsText = (ts.Days * 24 + ts.Hours).ToString() + ts.ToString(@"\:mm\:ss");
                lvi.SubItems.Add(tsText);
                lvi.SubItems.Add(dt);
                lvFiles.Items.Add(lvi);
            }

            lvFiles.EndUpdate();

        }

        private void LoadVideoList()
        {
            if (!Directory.Exists(myController.CommonDataPool.VideoPath))
            {
                MessageBox.Show("文件夹:" + myController.CommonDataPool.VideoPath + " 不存在，请重新选择保存视频的文件夹");
                return;
            }
            _videoFiles.Clear();
            //DirectoryInfo diTop = new DirectoryInfo(DefaultConfig.VideoSegmentPath);
            DirectoryInfo diTop = new DirectoryInfo(myController.CommonDataPool.VideoPath);
            var files = diTop.EnumerateFiles("*.MP4");
            foreach (var file in files)
            {
                try
                {
                    VideoFileInfo vfi = new VideoFileInfo(file.FullName);
                    _videoFiles.Add(vfi);
                }
                catch
                {
                }
            }
            _videoFiles.Sort(new VideoFileInfoComparer());
            lvVideoFiles.BeginUpdate();
            lvVideoFiles.Items.Clear();
            foreach (var vfi in _videoFiles)
            {
                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add(vfi.StartTime == null ? "" : vfi.StartTime.ToString("s").Replace('T', ' '));
                TimeSpan ts = vfi.Duration;
                string tsText = (ts.Days * 24 + ts.Hours).ToString() + ts.ToString(@"\:mm\:ss");
                string duration = tsText;
                lvi.SubItems.Add(duration);
                lvVideoFiles.Items.Add(lvi);
            }
            lvVideoFiles.EndUpdate();
        }

        private void lvVideoFiles_Leave(object sender, EventArgs e)
        {

        }

        private void lvVideoFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var selItems = lvVideoFiles.SelectedIndices;
            if (selItems == null || selItems.Count == 0)
            {
                return;
            }
            int sel = selItems[0];
            VideoFileInfo vfi = _videoFiles[sel];
            PlayPreviewFile(vfi.FileFullName);
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            Debug.Write("MediaEnded");
        }

        private void OnPlayerStopped(object sender, EventArgs e)
        {
            Debug.Write("PlayerStopped");
        }

        void PlayPreviewFile(string FileName)
        {
            try
            {
                _player.Stop();
                _player.Dispose();
            }
            catch { }
            try
            {
                _media.Dispose();
            }
            catch { }

            IMediaPlayerFactory factory = new MediaPlayerFactory();
            _media = factory.CreateMedia<IMediaFromFile>(FileName);
            _player = factory.CreatePlayer<IVideoPlayer>();
            _player.WindowHandle = picturePreview.Handle;
            _player.AspectRatio = AspectRatioMode.Mode2; // fill?         
            _player.Open(_media);
            _player.Events.MediaEnded += new EventHandler(OnMediaEnded);
            _player.Events.PlayerStopped += new EventHandler(OnPlayerStopped);
            _player.Play();

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var selItems = lvVideoFiles.SelectedIndices;
            if (selItems == null)
            {
                return;
            }
            int sel = selItems[0];

            _player.Stop();
            _media.Dispose();

            VideoFileInfo vfi = _videoFiles[sel];
            IMediaPlayerFactory factory = new MediaPlayerFactory();
            _media = factory.CreateMedia<IMediaFromFile>(vfi.FileFullName);
            _player = factory.CreatePlayer<IVideoPlayer>();
            _player.WindowHandle = picturePreview.Handle;
            _player.AspectRatio = AspectRatioMode.Mode2;
            _player.Open(_media);
            _player.Events.MediaEnded += new EventHandler(OnMediaEnded);
            _player.Events.PlayerStopped += new EventHandler(OnPlayerStopped);
            _player.Play();
        }

        private void UpdateAssoctionButton()
        {
            //if (lvFiles.SelectedItems.Count == 1 && lvVideoFiles.SelectedItems.Count == 1 && !_eegFiles[lvFiles.SelectedIndices[0]].HasVideo)
            //{
            //    btnAssociate.Enabled = true;
            //}
            //else
            //{
            //    btnAssociate.Enabled = false;
            //}
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            RecordingForm rec = new RecordingForm(this.myController);
            rec.ShowDialog();
            rec.Dispose();
            RefreshFiles();
        }

        private void lvFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateAssoctionButton();
        }

        private void toolButtonAssociate_Click(object sender, EventArgs e)
        {
            #region 需要修改
            //_player.Stop();

            //if (!Directory.Exists(DefaultConfig.AssociatedVideoPath))
            //{
            //    Directory.CreateDirectory(DefaultConfig.AssociatedVideoPath);
            //}

            //int idxFile = lvFiles.SelectedIndices[0];
            //int idxVideo = lvVideoFiles.SelectedIndices[0];
            //NationFile nfi = _eegFiles[idxFile];
            //VideoFileInfo vfi = _videoFiles[idxVideo];
            ////xcg
            ////string vfidate = vfi.StartTime.ToString("yyyy:MM:dd");
            ////string nfidate = nfi.StartDateTime.ToString("yyyy:MM:dd");
            ////if (string.Equals(vfidate,nfidate))
            ////{
            //bool videoStartLate = vfi.StartTime > nfi.StartDateTime;
            //bool videoStopBefore = (vfi.StartTime + vfi.Duration) < (nfi.StartDateTime + nfi.Duration);
            //List<string> msg = new List<string>();
            //if (videoStartLate)
            //{
            //    msg.Add("视频开始时间晚于脑电开始时间");
            //}
            //if (videoStopBefore)
            //{
            //    msg.Add("视频结束时间早于脑电结束时间");
            //}
            //if (videoStartLate || videoStopBefore)
            //{
            //    MessageBox.Show(string.Join("\n", msg), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (MessageBox.Show(
            //        string.Format("要关联脑电[{0}:{1}]和视频[{2}]吗？", nfi.PatInfo.ID, nfi.PatInfo.Name, vfi.StartTime.ToString("s").Replace('T', ' ')),
            //        "关联视频", MessageBoxButtons.YesNo)
            //         == System.Windows.Forms.DialogResult.Yes)
            //{
            //    File.Move(vfi.FileFullName, DefaultConfig.AssociatedVideoPath + @"\" + nfi.PatInfo.ID + "_" + ((int)(nfi.StartDateTime - vfi.StartTime).TotalMilliseconds).ToString() + ".MP4");
            //    RefreshFiles();
            //    MessageBox.Show("视频文件已成功关联，并移动到：" + DefaultConfig.AssociatedVideoPath + " 中");
            //}
            //}
            //////xcg
            ////else
            ////{
            ////    MessageBox.Show("视频和脑电数据的记录日期不一样，无法进行关联！","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
            #endregion
            AutoAssociateNatAndVideo();
            //RefreshFiles();
        }

        private void RefreshFiles()
        {
            LoadEegDataList();
            LoadVideoList();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshFiles();
        }

        private void StationForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && !e.Control && !e.Alt && !e.Shift)
            {
                RefreshFiles();
            }
        }

        private void btnPlayBack_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count != 1)
            {
                return;
            }
            PlaybackForm play = new PlaybackForm(_eegFiles[lvFiles.SelectedIndices[0]]);
            //--by zt
            myController.RegisterPlayBackForm(play);
            play.ReigisterVeegControl(myController);
            play.InitPlaybackFormParas();
            play.ShowDialog();
            Thread.Sleep(500);
            play.Dispose();
        }

        /// <summary>
        /// 浏览文件夹路径按钮点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseEEGFile_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// 声明文件浏览对话框 
            /// </summary>
            FolderBrowserDialog openFolder = new FolderBrowserDialog();

            //设置对话框的名字 
            openFolder.Description = "请选择文件路径";

            //文件浏览对话框成功打开
            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                //保存所选择的路径
                myController.CommonDataPool.EegDataPath = openFolder.SelectedPath;

                //设置显示脑电数据文件夹的路径的TextBox的值
                this.tbEEGPath.Text = openFolder.SelectedPath;

                //重新加载数据
                LoadEegDataList();
            }
        }

        /// <summary>
        /// 浏览视频文件夹路径点击事件
        /// -- by lxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseVideoPath_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// 声明文件浏览对话框 
            /// </summary>
            FolderBrowserDialog openFolder = new FolderBrowserDialog();

            //设置对话框的名字 
            openFolder.Description = "请选择视频文件路径";

            if (openFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //保存所选择的路径
                myController.CommonDataPool.VideoPath = openFolder.SelectedPath;

                //设置显示脑电视频文件夹的路径的TextBox的值
                this.tbVideoPath.Text = openFolder.SelectedPath;

                //重新加载数据
                LoadVideoList();
            }
        }

        private void My_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            ListView listView = (ListView)sender;

            // Check if e.Item is selected and the ListView has a focus.
            if (!listView.Focused && e.Item.Selected)
            {
                Rectangle rowBounds = e.Bounds;
                int leftMargin = e.Item.GetBounds(ItemBoundsPortion.Label).Left;
                Rectangle bounds = new Rectangle(leftMargin, rowBounds.Top, rowBounds.Width - leftMargin, rowBounds.Height);
                e.Graphics.FillRectangle(SystemBrushes.ActiveCaption, bounds);
            }
            else
                e.DrawDefault = true;
        }

        private void My_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListView lv = (ListView)sender;
            if (!lv.Focused)
            {
                const int TEXT_OFFSET = 1;
                Rectangle rowBounds = e.SubItem.Bounds;
                Rectangle labelBounds = e.Item.GetBounds(ItemBoundsPortion.Label);
                int leftMargin = labelBounds.Left - TEXT_OFFSET;
                Rectangle bounds = new Rectangle(rowBounds.Left + leftMargin, rowBounds.Top, e.ColumnIndex == 0 ? labelBounds.Width : (rowBounds.Width - leftMargin - TEXT_OFFSET), rowBounds.Height);
                TextFormatFlags align;
                switch (lv.Columns[e.ColumnIndex].TextAlign)
                {
                    case HorizontalAlignment.Right:
                        align = TextFormatFlags.Right;
                        break;
                    case HorizontalAlignment.Center:
                        align = TextFormatFlags.HorizontalCenter;
                        break;
                    default:
                        align = TextFormatFlags.Left;
                        break;
                }
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, lv.Font, bounds, SystemColors.ActiveCaptionText,
                    align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void My_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        //点击视频关联按钮，视频和脑电数据自动关联  by--xcg
        /// </summary>
        private void AutoAssociateNatAndVideo()
        {
            int mNatAssociateVideoCount = 0;
            if (_eegFiles.Count == 0 || _videoFiles.Count == 0)
            {
                MessageBox.Show("没有脑电数据或者视频，不能进行关联操作！");
                return;
            }
            foreach (NationFile mNationFile in _eegFiles)
            {
                foreach (VideoFileInfo mVideoFileInfo in _videoFiles)
                {
                    bool videoStartBefore = mVideoFileInfo.StartTime <mNationFile.StartDateTime;
                    bool videoStopLate = (mVideoFileInfo.StartTime + mVideoFileInfo.Duration) > (mNationFile.StartDateTime + mNationFile.Duration);
                    if (videoStartBefore && videoStopLate)
                    {
                        string sourceFileName = mVideoFileInfo.FileFullName;
                        string destFileName = DefaultConfig.AssociatedVideoPath + @"\" + mNationFile.PatInfo.ID + "_" + ((int)(mNationFile.StartDateTime - mVideoFileInfo.StartTime).TotalMilliseconds).ToString() + ".MP4";
                        if (!File.Exists(destFileName))
                        {
                            mNatAssociateVideoCount++;
                            File.Move(sourceFileName, destFileName);
                        }
                    }
                }
            }

            if (mNatAssociateVideoCount == 0)
            {
                MessageBox.Show("视频与脑电数据没有可以关联的！");
            }
            else
            {
                RefreshFiles();
            }
        }
    }
}
