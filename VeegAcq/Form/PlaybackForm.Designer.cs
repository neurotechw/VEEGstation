namespace VeegStation
{
    partial class PlaybackForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaybackForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine2 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.InformationTSSSBt = new System.Windows.Forms.ToolStripDropDownButton();
            this.pationInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectionInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStriplabel_abtime = new System.Windows.Forms.ToolStripStatusLabel();
            this.displayStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStriplabel_retime = new System.Windows.Forms.ToolStripStatusLabel();
            this.displayRecordingTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStriplabel_totaltime = new System.Windows.Forms.ToolStripStatusLabel();
            this.displayTotalTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_timeStandardLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_timeStandard = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_sensitivityLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_sensitivity = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_lbTrap = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_trap = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_lbBandFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_bandFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.chartWave = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.hsProgress = new System.Windows.Forms.HScrollBar();
            this.PationInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.DetectionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.Hiding = new System.Windows.Forms.ToolStripMenuItem();
            this.PationInfoPanel = new System.Windows.Forms.Panel();
            this.btnHide = new System.Windows.Forms.Button();
            this.PatAgeL = new System.Windows.Forms.Label();
            this.PatAgeTextBt = new System.Windows.Forms.TextBox();
            this.SingleHandAdvanCB = new System.Windows.Forms.ComboBox();
            this.SingleHandAdvanL = new System.Windows.Forms.Label();
            this.PatGenderCB = new System.Windows.Forms.ComboBox();
            this.PatGenderL = new System.Windows.Forms.Label();
            this.PatNameL = new System.Windows.Forms.Label();
            this.PatNameTextBt = new System.Windows.Forms.TextBox();
            this.PatIDL = new System.Windows.Forms.Label();
            this.PatIDTextBt = new System.Windows.Forms.TextBox();
            this.DetectionInfoPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.FilePathL = new System.Windows.Forms.Label();
            this.FilePathTextBt = new System.Windows.Forms.TextBox();
            this.DetectionRemarksL = new System.Windows.Forms.Label();
            this.DetectionRemarksTextBt = new System.Windows.Forms.TextBox();
            this.PharmacyL = new System.Windows.Forms.Label();
            this.PharmacyTextBt = new System.Windows.Forms.TextBox();
            this.PationStateL = new System.Windows.Forms.Label();
            this.PationStateTextBt = new System.Windows.Forms.TextBox();
            this.PhysicianL = new System.Windows.Forms.Label();
            this.PhysicianTextBT = new System.Windows.Forms.TextBox();
            this.TechnicianL = new System.Windows.Forms.Label();
            this.TechnicianTextBt = new System.Windows.Forms.TextBox();
            this.RequesterL = new System.Windows.Forms.Label();
            this.RequesterTextBt = new System.Windows.Forms.TextBox();
            this.DetectinonL = new System.Windows.Forms.Label();
            this.DetectionTextBt = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensitivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeStandartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leadChooseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.滤波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Filter50HzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BandFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.predefineEventstoolstripmenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customeEventstoolstripmenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导联设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_accelerate = new System.Windows.Forms.Button();
            this.btn_decelerate = new System.Windows.Forms.Button();
            this.btn_hide = new System.Windows.Forms.Button();
            this.boardPanel = new System.Windows.Forms.Panel();
            this.tyPanelEventListView = new System.Windows.Forms.TableLayoutPanel();
            this.lvCustomEvents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPreDefineEvents = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddCustomEvents = new System.Windows.Forms.Button();
            this.btnDeleteCustomEvents = new System.Windows.Forms.Button();
            this.btnEditPreDefineEvents = new System.Windows.Forms.Button();
            this.btnDeletePredefineEvents = new System.Windows.Forms.Button();
            this.btnEditCustomEvents = new System.Windows.Forms.Button();
            this.lblCustomEvent = new System.Windows.Forms.Label();
            this.lblPreDefineEvent = new System.Windows.Forms.Label();
            this.vScroll = new System.Windows.Forms.VScrollBar();
            this.labelPanel = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWave)).BeginInit();
            this.PationInfoPanel.SuspendLayout();
            this.DetectionInfoPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.boardPanel.SuspendLayout();
            this.tyPanelEventListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlay,
            this.btnPause,
            this.toolStripSeparator1,
            this.btnPrev,
            this.btnNext,
            this.InformationTSSSBt});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(942, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(51, 22);
            this.btnPlay.Text = "播放";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(51, 22);
            this.btnPause.Text = "暂停";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(43, 22);
            this.btnPrev.Text = "<<";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(43, 22);
            this.btnNext.Text = ">>";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // InformationTSSSBt
            // 
            this.InformationTSSSBt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pationInfoToolStripMenuItem,
            this.detectionInfoToolStripMenuItem,
            this.hideingToolStripMenuItem});
            this.InformationTSSSBt.Image = ((System.Drawing.Image)(resources.GetObject("InformationTSSSBt.Image")));
            this.InformationTSSSBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InformationTSSSBt.Name = "InformationTSSSBt";
            this.InformationTSSSBt.Size = new System.Drawing.Size(60, 22);
            this.InformationTSSSBt.Text = "信息";
            // 
            // pationInfoToolStripMenuItem
            // 
            this.pationInfoToolStripMenuItem.Name = "pationInfoToolStripMenuItem";
            this.pationInfoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pationInfoToolStripMenuItem.Text = "病人属性";
            this.pationInfoToolStripMenuItem.Click += new System.EventHandler(this.pationInfoToolStripMenuItem_Click);
            // 
            // detectionInfoToolStripMenuItem
            // 
            this.detectionInfoToolStripMenuItem.Name = "detectionInfoToolStripMenuItem";
            this.detectionInfoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.detectionInfoToolStripMenuItem.Text = "检查属性";
            this.detectionInfoToolStripMenuItem.Click += new System.EventHandler(this.detectionInfoToolStripMenuItem_Click);
            // 
            // hideingToolStripMenuItem
            // 
            this.hideingToolStripMenuItem.Name = "hideingToolStripMenuItem";
            this.hideingToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.hideingToolStripMenuItem.Text = "隐藏";
            this.hideingToolStripMenuItem.Click += new System.EventHandler(this.hideingToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStriplabel_abtime,
            this.displayStartTime,
            this.toolStriplabel_retime,
            this.displayRecordingTime,
            this.toolStriplabel_totaltime,
            this.displayTotalTime,
            this.toolStripStatusLabel_timeStandardLabel,
            this.toolStripStatusLabel_timeStandard,
            this.toolStripStatusLabel_sensitivityLabel,
            this.toolStripStatusLabel_sensitivity,
            this.toolStripStatusLabel_lbTrap,
            this.toolStripStatusLabel_trap,
            this.toolStripStatusLabel_lbBandFilter,
            this.toolStripStatusLabel_bandFilter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 556);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(942, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStriplabel_abtime
            // 
            this.toolStriplabel_abtime.Name = "toolStriplabel_abtime";
            this.toolStriplabel_abtime.Size = new System.Drawing.Size(56, 21);
            this.toolStriplabel_abtime.Text = "绝对时间";
            // 
            // displayStartTime
            // 
            this.displayStartTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.displayStartTime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.displayStartTime.Name = "displayStartTime";
            this.displayStartTime.Size = new System.Drawing.Size(84, 21);
            this.displayStartTime.Text = "##：##：##";
            // 
            // toolStriplabel_retime
            // 
            this.toolStriplabel_retime.Name = "toolStriplabel_retime";
            this.toolStriplabel_retime.Size = new System.Drawing.Size(56, 21);
            this.toolStriplabel_retime.Text = "相对时间";
            // 
            // displayRecordingTime
            // 
            this.displayRecordingTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.displayRecordingTime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.displayRecordingTime.Name = "displayRecordingTime";
            this.displayRecordingTime.Size = new System.Drawing.Size(84, 21);
            this.displayRecordingTime.Text = "##：##：##";
            // 
            // toolStriplabel_totaltime
            // 
            this.toolStriplabel_totaltime.Name = "toolStriplabel_totaltime";
            this.toolStriplabel_totaltime.Size = new System.Drawing.Size(44, 21);
            this.toolStriplabel_totaltime.Text = "总时间";
            // 
            // displayTotalTime
            // 
            this.displayTotalTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.displayTotalTime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.displayTotalTime.Name = "displayTotalTime";
            this.displayTotalTime.Size = new System.Drawing.Size(84, 21);
            this.displayTotalTime.Text = "##：##：##";
            // 
            // toolStripStatusLabel_timeStandardLabel
            // 
            this.toolStripStatusLabel_timeStandardLabel.Name = "toolStripStatusLabel_timeStandardLabel";
            this.toolStripStatusLabel_timeStandardLabel.Size = new System.Drawing.Size(56, 21);
            this.toolStripStatusLabel_timeStandardLabel.Text = "时间基准";
            // 
            // toolStripStatusLabel_timeStandard
            // 
            this.toolStripStatusLabel_timeStandard.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_timeStandard.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel_timeStandard.Name = "toolStripStatusLabel_timeStandard";
            this.toolStripStatusLabel_timeStandard.Size = new System.Drawing.Size(49, 21);
            this.toolStripStatusLabel_timeStandard.Text = "##/##";
            // 
            // toolStripStatusLabel_sensitivityLabel
            // 
            this.toolStripStatusLabel_sensitivityLabel.Name = "toolStripStatusLabel_sensitivityLabel";
            this.toolStripStatusLabel_sensitivityLabel.Size = new System.Drawing.Size(44, 21);
            this.toolStripStatusLabel_sensitivityLabel.Text = "灵敏度";
            // 
            // toolStripStatusLabel_sensitivity
            // 
            this.toolStripStatusLabel_sensitivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_sensitivity.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel_sensitivity.Name = "toolStripStatusLabel_sensitivity";
            this.toolStripStatusLabel_sensitivity.Size = new System.Drawing.Size(49, 21);
            this.toolStripStatusLabel_sensitivity.Text = "##/##";
            // 
            // toolStripStatusLabel_lbTrap
            // 
            this.toolStripStatusLabel_lbTrap.Name = "toolStripStatusLabel_lbTrap";
            this.toolStripStatusLabel_lbTrap.Size = new System.Drawing.Size(32, 21);
            this.toolStripStatusLabel_lbTrap.Text = "陷波";
            // 
            // toolStripStatusLabel_trap
            // 
            this.toolStripStatusLabel_trap.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_trap.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel_trap.Name = "toolStripStatusLabel_trap";
            this.toolStripStatusLabel_trap.Size = new System.Drawing.Size(44, 21);
            this.toolStripStatusLabel_trap.Text = "None";
            // 
            // toolStripStatusLabel_lbBandFilter
            // 
            this.toolStripStatusLabel_lbBandFilter.Name = "toolStripStatusLabel_lbBandFilter";
            this.toolStripStatusLabel_lbBandFilter.Size = new System.Drawing.Size(56, 21);
            this.toolStripStatusLabel_lbBandFilter.Text = "带通滤波";
            // 
            // toolStripStatusLabel_bandFilter
            // 
            this.toolStripStatusLabel_bandFilter.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_bandFilter.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel_bandFilter.Name = "toolStripStatusLabel_bandFilter";
            this.toolStripStatusLabel_bandFilter.Size = new System.Drawing.Size(44, 21);
            this.toolStripStatusLabel_bandFilter.Text = "None";
            // 
            // panelVideo
            // 
            this.panelVideo.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.panelVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVideo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelVideo.Location = new System.Drawing.Point(20, 10);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(160, 120);
            this.panelVideo.TabIndex = 2;
            this.panelVideo.Click += new System.EventHandler(this.panelVideo_Click);
            // 
            // chartWave
            // 
            this.chartWave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LabelStyle.Interval = 1D;
            chartArea2.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea2.AxisX.MajorGrid.Interval = 1D;
            chartArea2.AxisX.MajorGrid.IntervalOffset = 1D;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightSalmon;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX.MajorTickMark.Interval = 1D;
            chartArea2.AxisX.MajorTickMark.IntervalOffset = 0D;
            chartArea2.AxisX.Minimum = 0D;
            stripLine2.BorderColor = System.Drawing.Color.Black;
            stripLine2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            stripLine2.StripWidth = 0.0001D;
            chartArea2.AxisX.StripLines.Add(stripLine2);
            chartArea2.AxisY.InterlacedColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.IsInterlaced = true;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.MajorGrid.Interval = 100D;
            chartArea2.AxisY.MajorGrid.IntervalOffset = 0D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.MajorTickMark.Interval = 100D;
            chartArea2.AxisY.MajorTickMark.IntervalOffset = 0D;
            chartArea2.AxisY.Maximum = 2000D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "mainArea";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 94F;
            chartArea2.Position.Width = 100F;
            chartArea2.Position.Y = 3F;
            this.chartWave.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            legend2.Position.Auto = false;
            legend2.Position.Height = 100F;
            legend2.Position.Width = 100F;
            this.chartWave.Legends.Add(legend2);
            this.chartWave.Location = new System.Drawing.Point(62, 63);
            this.chartWave.Name = "chartWave";
            series2.ChartArea = "mainArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartWave.Series.Add(series2);
            this.chartWave.Size = new System.Drawing.Size(880, 494);
            this.chartWave.TabIndex = 3;
            this.chartWave.Text = "chart1";
            this.chartWave.ClientSizeChanged += new System.EventHandler(this.ChartSizeChanged);
            this.chartWave.Click += new System.EventHandler(this.Chartwave_Click);
            this.chartWave.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartPaint);
            this.chartWave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveOnChart);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // hsProgress
            // 
            this.hsProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hsProgress.LargeChange = 2;
            this.hsProgress.Location = new System.Drawing.Point(9, 48);
            this.hsProgress.Name = "hsProgress";
            this.hsProgress.Size = new System.Drawing.Size(933, 17);
            this.hsProgress.TabIndex = 4;
            this.hsProgress.MouseCaptureChanged += new System.EventHandler(this.HSProgress_MouseCaptureChanged);
            // 
            // PationInfo
            // 
            this.PationInfo.Name = "PationInfo";
            this.PationInfo.Size = new System.Drawing.Size(158, 22);
            this.PationInfo.Text = "Pation Info";
            // 
            // DetectionInfo
            // 
            this.DetectionInfo.Name = "DetectionInfo";
            this.DetectionInfo.Size = new System.Drawing.Size(158, 22);
            this.DetectionInfo.Text = "Detection Info";
            // 
            // Hiding
            // 
            this.Hiding.Name = "Hiding";
            this.Hiding.Size = new System.Drawing.Size(158, 22);
            this.Hiding.Text = "Hiding";
            // 
            // PationInfoPanel
            // 
            this.PationInfoPanel.AllowDrop = true;
            this.PationInfoPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PationInfoPanel.Controls.Add(this.btnHide);
            this.PationInfoPanel.Controls.Add(this.PatAgeL);
            this.PationInfoPanel.Controls.Add(this.PatAgeTextBt);
            this.PationInfoPanel.Controls.Add(this.SingleHandAdvanCB);
            this.PationInfoPanel.Controls.Add(this.SingleHandAdvanL);
            this.PationInfoPanel.Controls.Add(this.PatGenderCB);
            this.PationInfoPanel.Controls.Add(this.PatGenderL);
            this.PationInfoPanel.Controls.Add(this.PatNameL);
            this.PationInfoPanel.Controls.Add(this.PatNameTextBt);
            this.PationInfoPanel.Controls.Add(this.PatIDL);
            this.PationInfoPanel.Controls.Add(this.PatIDTextBt);
            this.PationInfoPanel.Location = new System.Drawing.Point(86, 65);
            this.PationInfoPanel.Name = "PationInfoPanel";
            this.PationInfoPanel.Size = new System.Drawing.Size(281, 168);
            this.PationInfoPanel.TabIndex = 1;
            this.PationInfoPanel.Visible = false;
            this.PationInfoPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PationInfoPanel_MouseDown);
            this.PationInfoPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PationInfoPanel_MouseMove);
            // 
            // btnHide
            // 
            this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
            this.btnHide.Location = new System.Drawing.Point(256, 5);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(22, 23);
            this.btnHide.TabIndex = 22;
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // PatAgeL
            // 
            this.PatAgeL.AllowDrop = true;
            this.PatAgeL.AutoSize = true;
            this.PatAgeL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PatAgeL.Location = new System.Drawing.Point(12, 62);
            this.PatAgeL.Name = "PatAgeL";
            this.PatAgeL.Size = new System.Drawing.Size(29, 12);
            this.PatAgeL.TabIndex = 21;
            this.PatAgeL.Text = "年龄";
            this.PatAgeL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatAgeTextBt
            // 
            this.PatAgeTextBt.Enabled = false;
            this.PatAgeTextBt.Location = new System.Drawing.Point(59, 57);
            this.PatAgeTextBt.Multiline = true;
            this.PatAgeTextBt.Name = "PatAgeTextBt";
            this.PatAgeTextBt.Size = new System.Drawing.Size(190, 20);
            this.PatAgeTextBt.TabIndex = 20;
            // 
            // SingleHandAdvanCB
            // 
            this.SingleHandAdvanCB.AllowDrop = true;
            this.SingleHandAdvanCB.Enabled = false;
            this.SingleHandAdvanCB.FormattingEnabled = true;
            this.SingleHandAdvanCB.Location = new System.Drawing.Point(59, 115);
            this.SingleHandAdvanCB.MaxLength = 32767;
            this.SingleHandAdvanCB.Name = "SingleHandAdvanCB";
            this.SingleHandAdvanCB.Size = new System.Drawing.Size(190, 20);
            this.SingleHandAdvanCB.TabIndex = 19;
            // 
            // SingleHandAdvanL
            // 
            this.SingleHandAdvanL.Location = new System.Drawing.Point(12, 118);
            this.SingleHandAdvanL.Name = "SingleHandAdvanL";
            this.SingleHandAdvanL.Size = new System.Drawing.Size(41, 14);
            this.SingleHandAdvanL.TabIndex = 18;
            this.SingleHandAdvanL.Text = "左右利";
            // 
            // PatGenderCB
            // 
            this.PatGenderCB.AllowDrop = true;
            this.PatGenderCB.Enabled = false;
            this.PatGenderCB.FormattingEnabled = true;
            this.PatGenderCB.Location = new System.Drawing.Point(59, 85);
            this.PatGenderCB.MaxLength = 32767;
            this.PatGenderCB.Name = "PatGenderCB";
            this.PatGenderCB.Size = new System.Drawing.Size(190, 20);
            this.PatGenderCB.TabIndex = 17;
            // 
            // PatGenderL
            // 
            this.PatGenderL.AutoSize = true;
            this.PatGenderL.Location = new System.Drawing.Point(12, 88);
            this.PatGenderL.Name = "PatGenderL";
            this.PatGenderL.Size = new System.Drawing.Size(29, 12);
            this.PatGenderL.TabIndex = 16;
            this.PatGenderL.Text = "性别";
            // 
            // PatNameL
            // 
            this.PatNameL.AllowDrop = true;
            this.PatNameL.AutoSize = true;
            this.PatNameL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PatNameL.Location = new System.Drawing.Point(12, 36);
            this.PatNameL.Name = "PatNameL";
            this.PatNameL.Size = new System.Drawing.Size(29, 12);
            this.PatNameL.TabIndex = 3;
            this.PatNameL.Text = "姓名";
            this.PatNameL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatNameTextBt
            // 
            this.PatNameTextBt.Enabled = false;
            this.PatNameTextBt.Location = new System.Drawing.Point(59, 31);
            this.PatNameTextBt.Multiline = true;
            this.PatNameTextBt.Name = "PatNameTextBt";
            this.PatNameTextBt.Size = new System.Drawing.Size(190, 20);
            this.PatNameTextBt.TabIndex = 2;
            // 
            // PatIDL
            // 
            this.PatIDL.AutoSize = true;
            this.PatIDL.Location = new System.Drawing.Point(12, 8);
            this.PatIDL.Name = "PatIDL";
            this.PatIDL.Size = new System.Drawing.Size(41, 12);
            this.PatIDL.TabIndex = 1;
            this.PatIDL.Text = "病人ID";
            // 
            // PatIDTextBt
            // 
            this.PatIDTextBt.Enabled = false;
            this.PatIDTextBt.Location = new System.Drawing.Point(59, 5);
            this.PatIDTextBt.Multiline = true;
            this.PatIDTextBt.Name = "PatIDTextBt";
            this.PatIDTextBt.Size = new System.Drawing.Size(190, 20);
            this.PatIDTextBt.TabIndex = 0;
            // 
            // DetectionInfoPanel
            // 
            this.DetectionInfoPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DetectionInfoPanel.Controls.Add(this.btnClose);
            this.DetectionInfoPanel.Controls.Add(this.FilePathL);
            this.DetectionInfoPanel.Controls.Add(this.FilePathTextBt);
            this.DetectionInfoPanel.Controls.Add(this.DetectionRemarksL);
            this.DetectionInfoPanel.Controls.Add(this.DetectionRemarksTextBt);
            this.DetectionInfoPanel.Controls.Add(this.PharmacyL);
            this.DetectionInfoPanel.Controls.Add(this.PharmacyTextBt);
            this.DetectionInfoPanel.Controls.Add(this.PationStateL);
            this.DetectionInfoPanel.Controls.Add(this.PationStateTextBt);
            this.DetectionInfoPanel.Controls.Add(this.PhysicianL);
            this.DetectionInfoPanel.Controls.Add(this.PhysicianTextBT);
            this.DetectionInfoPanel.Controls.Add(this.TechnicianL);
            this.DetectionInfoPanel.Controls.Add(this.TechnicianTextBt);
            this.DetectionInfoPanel.Controls.Add(this.RequesterL);
            this.DetectionInfoPanel.Controls.Add(this.RequesterTextBt);
            this.DetectionInfoPanel.Controls.Add(this.DetectinonL);
            this.DetectionInfoPanel.Controls.Add(this.DetectionTextBt);
            this.DetectionInfoPanel.Location = new System.Drawing.Point(373, 68);
            this.DetectionInfoPanel.Name = "DetectionInfoPanel";
            this.DetectionInfoPanel.Size = new System.Drawing.Size(295, 297);
            this.DetectionInfoPanel.TabIndex = 2;
            this.DetectionInfoPanel.Visible = false;
            this.DetectionInfoPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetectionInfoPanel_MouseDown);
            this.DetectionInfoPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetectionInfoPanel_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(269, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.TabIndex = 44;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FilePathL
            // 
            this.FilePathL.AutoSize = true;
            this.FilePathL.Location = new System.Drawing.Point(13, 245);
            this.FilePathL.Name = "FilePathL";
            this.FilePathL.Size = new System.Drawing.Size(53, 12);
            this.FilePathL.TabIndex = 43;
            this.FilePathL.Text = "文件路径";
            // 
            // FilePathTextBt
            // 
            this.FilePathTextBt.Enabled = false;
            this.FilePathTextBt.Location = new System.Drawing.Point(72, 223);
            this.FilePathTextBt.Multiline = true;
            this.FilePathTextBt.Name = "FilePathTextBt";
            this.FilePathTextBt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FilePathTextBt.Size = new System.Drawing.Size(190, 56);
            this.FilePathTextBt.TabIndex = 42;
            // 
            // DetectionRemarksL
            // 
            this.DetectionRemarksL.AutoSize = true;
            this.DetectionRemarksL.Location = new System.Drawing.Point(13, 182);
            this.DetectionRemarksL.Name = "DetectionRemarksL";
            this.DetectionRemarksL.Size = new System.Drawing.Size(53, 12);
            this.DetectionRemarksL.TabIndex = 39;
            this.DetectionRemarksL.Text = "检验备注";
            // 
            // DetectionRemarksTextBt
            // 
            this.DetectionRemarksTextBt.Enabled = false;
            this.DetectionRemarksTextBt.Location = new System.Drawing.Point(72, 159);
            this.DetectionRemarksTextBt.Multiline = true;
            this.DetectionRemarksTextBt.Name = "DetectionRemarksTextBt";
            this.DetectionRemarksTextBt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DetectionRemarksTextBt.Size = new System.Drawing.Size(190, 58);
            this.DetectionRemarksTextBt.TabIndex = 38;
            // 
            // PharmacyL
            // 
            this.PharmacyL.AutoSize = true;
            this.PharmacyL.Location = new System.Drawing.Point(13, 137);
            this.PharmacyL.Name = "PharmacyL";
            this.PharmacyL.Size = new System.Drawing.Size(29, 12);
            this.PharmacyL.TabIndex = 37;
            this.PharmacyL.Text = "用药";
            // 
            // PharmacyTextBt
            // 
            this.PharmacyTextBt.Enabled = false;
            this.PharmacyTextBt.Location = new System.Drawing.Point(72, 133);
            this.PharmacyTextBt.Multiline = true;
            this.PharmacyTextBt.Name = "PharmacyTextBt";
            this.PharmacyTextBt.Size = new System.Drawing.Size(190, 20);
            this.PharmacyTextBt.TabIndex = 36;
            // 
            // PationStateL
            // 
            this.PationStateL.AutoSize = true;
            this.PationStateL.Location = new System.Drawing.Point(13, 111);
            this.PationStateL.Name = "PationStateL";
            this.PationStateL.Size = new System.Drawing.Size(53, 12);
            this.PationStateL.TabIndex = 35;
            this.PationStateL.Text = "病人状态";
            // 
            // PationStateTextBt
            // 
            this.PationStateTextBt.Enabled = false;
            this.PationStateTextBt.Location = new System.Drawing.Point(72, 107);
            this.PationStateTextBt.Multiline = true;
            this.PationStateTextBt.Name = "PationStateTextBt";
            this.PationStateTextBt.Size = new System.Drawing.Size(190, 20);
            this.PationStateTextBt.TabIndex = 34;
            // 
            // PhysicianL
            // 
            this.PhysicianL.AutoSize = true;
            this.PhysicianL.Location = new System.Drawing.Point(13, 85);
            this.PhysicianL.Name = "PhysicianL";
            this.PhysicianL.Size = new System.Drawing.Size(29, 12);
            this.PhysicianL.TabIndex = 29;
            this.PhysicianL.Text = "医师";
            // 
            // PhysicianTextBT
            // 
            this.PhysicianTextBT.Enabled = false;
            this.PhysicianTextBT.Location = new System.Drawing.Point(72, 81);
            this.PhysicianTextBT.Multiline = true;
            this.PhysicianTextBT.Name = "PhysicianTextBT";
            this.PhysicianTextBT.Size = new System.Drawing.Size(190, 20);
            this.PhysicianTextBT.TabIndex = 28;
            // 
            // TechnicianL
            // 
            this.TechnicianL.AutoSize = true;
            this.TechnicianL.Location = new System.Drawing.Point(13, 60);
            this.TechnicianL.Name = "TechnicianL";
            this.TechnicianL.Size = new System.Drawing.Size(29, 12);
            this.TechnicianL.TabIndex = 27;
            this.TechnicianL.Text = "技师";
            // 
            // TechnicianTextBt
            // 
            this.TechnicianTextBt.Enabled = false;
            this.TechnicianTextBt.Location = new System.Drawing.Point(72, 56);
            this.TechnicianTextBt.Multiline = true;
            this.TechnicianTextBt.Name = "TechnicianTextBt";
            this.TechnicianTextBt.Size = new System.Drawing.Size(190, 20);
            this.TechnicianTextBt.TabIndex = 26;
            // 
            // RequesterL
            // 
            this.RequesterL.AutoSize = true;
            this.RequesterL.Location = new System.Drawing.Point(13, 34);
            this.RequesterL.Name = "RequesterL";
            this.RequesterL.Size = new System.Drawing.Size(41, 12);
            this.RequesterL.TabIndex = 25;
            this.RequesterL.Text = "申请者";
            // 
            // RequesterTextBt
            // 
            this.RequesterTextBt.Enabled = false;
            this.RequesterTextBt.Location = new System.Drawing.Point(72, 30);
            this.RequesterTextBt.Multiline = true;
            this.RequesterTextBt.Name = "RequesterTextBt";
            this.RequesterTextBt.Size = new System.Drawing.Size(190, 20);
            this.RequesterTextBt.TabIndex = 24;
            // 
            // DetectinonL
            // 
            this.DetectinonL.AutoSize = true;
            this.DetectinonL.Location = new System.Drawing.Point(13, 8);
            this.DetectinonL.Name = "DetectinonL";
            this.DetectinonL.Size = new System.Drawing.Size(41, 12);
            this.DetectinonL.TabIndex = 21;
            this.DetectinonL.Text = "检查号";
            // 
            // DetectionTextBt
            // 
            this.DetectionTextBt.Enabled = false;
            this.DetectionTextBt.Location = new System.Drawing.Point(72, 4);
            this.DetectionTextBt.Multiline = true;
            this.DetectionTextBt.Name = "DetectionTextBt";
            this.DetectionTextBt.Size = new System.Drawing.Size(190, 20);
            this.DetectionTextBt.TabIndex = 20;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.格式ToolStripMenuItem,
            this.calibrateToolStripMenuItem,
            this.interfaceToolStripMenuItem,
            this.eventToolStripMenuItem,
            this.导联设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(942, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 格式ToolStripMenuItem
            // 
            this.格式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sensitivityToolStripMenuItem,
            this.timeStandartToolStripMenuItem,
            this.signalToolStripMenuItem,
            this.leadChooseToolStripMenuItem,
            this.滤波ToolStripMenuItem});
            this.格式ToolStripMenuItem.Name = "格式ToolStripMenuItem";
            this.格式ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.格式ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.格式ToolStripMenuItem.Text = "格式(R)";
            // 
            // sensitivityToolStripMenuItem
            // 
            this.sensitivityToolStripMenuItem.Name = "sensitivityToolStripMenuItem";
            this.sensitivityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sensitivityToolStripMenuItem.Text = "灵敏度";
            // 
            // timeStandartToolStripMenuItem
            // 
            this.timeStandartToolStripMenuItem.Name = "timeStandartToolStripMenuItem";
            this.timeStandartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.timeStandartToolStripMenuItem.Text = "时间基准";
            // 
            // signalToolStripMenuItem
            // 
            this.signalToolStripMenuItem.Name = "signalToolStripMenuItem";
            this.signalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.signalToolStripMenuItem.Text = "显示通道";
            // 
            // leadChooseToolStripMenuItem
            // 
            this.leadChooseToolStripMenuItem.Name = "leadChooseToolStripMenuItem";
            this.leadChooseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.leadChooseToolStripMenuItem.Text = "导联选择";
            // 
            // 滤波ToolStripMenuItem
            // 
            this.滤波ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Filter50HzToolStripMenuItem,
            this.BandFilterToolStripMenuItem});
            this.滤波ToolStripMenuItem.Name = "滤波ToolStripMenuItem";
            this.滤波ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.滤波ToolStripMenuItem.Text = "滤波";
            // 
            // Filter50HzToolStripMenuItem
            // 
            this.Filter50HzToolStripMenuItem.Name = "Filter50HzToolStripMenuItem";
            this.Filter50HzToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.Filter50HzToolStripMenuItem.Text = "50Hz陷波";
            this.Filter50HzToolStripMenuItem.Click += new System.EventHandler(this.Filter50HzToolStripMenuItem_Click);
            // 
            // BandFilterToolStripMenuItem
            // 
            this.BandFilterToolStripMenuItem.Name = "BandFilterToolStripMenuItem";
            this.BandFilterToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.BandFilterToolStripMenuItem.Text = "带通滤波";
            this.BandFilterToolStripMenuItem.Click += new System.EventHandler(this.BandFilterToolStripMenuItem_Click);
            // 
            // calibrateToolStripMenuItem
            // 
            this.calibrateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrateYToolStripMenuItem,
            this.calibrateXToolStripMenuItem});
            this.calibrateToolStripMenuItem.Name = "calibrateToolStripMenuItem";
            this.calibrateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.calibrateToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.calibrateToolStripMenuItem.Text = "校准(C)";
            // 
            // calibrateYToolStripMenuItem
            // 
            this.calibrateYToolStripMenuItem.Name = "calibrateYToolStripMenuItem";
            this.calibrateYToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.calibrateYToolStripMenuItem.Text = "Y轴校准";
            this.calibrateYToolStripMenuItem.Click += new System.EventHandler(this.CalibrateYToolStripMenuItem_Click);
            // 
            // calibrateXToolStripMenuItem
            // 
            this.calibrateXToolStripMenuItem.Name = "calibrateXToolStripMenuItem";
            this.calibrateXToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.calibrateXToolStripMenuItem.Text = "X轴校准";
            this.calibrateXToolStripMenuItem.Click += new System.EventHandler(this.CalibrateXToolStripMenuItem_Click);
            // 
            // interfaceToolStripMenuItem
            // 
            this.interfaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boardToolStripMenuItem});
            this.interfaceToolStripMenuItem.Name = "interfaceToolStripMenuItem";
            this.interfaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.interfaceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.interfaceToolStripMenuItem.Text = "界面(I)";
            // 
            // boardToolStripMenuItem
            // 
            this.boardToolStripMenuItem.Name = "boardToolStripMenuItem";
            this.boardToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.boardToolStripMenuItem.Text = "面板";
            this.boardToolStripMenuItem.Click += new System.EventHandler(this.BoardToolStripMenuItem_Click);
            // 
            // eventToolStripMenuItem
            // 
            this.eventToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.predefineEventstoolstripmenuItem,
            this.customeEventstoolstripmenuItem});
            this.eventToolStripMenuItem.Name = "eventToolStripMenuItem";
            this.eventToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.eventToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.eventToolStripMenuItem.Text = "事件(E)";
            // 
            // predefineEventstoolstripmenuItem
            // 
            this.predefineEventstoolstripmenuItem.Name = "predefineEventstoolstripmenuItem";
            this.predefineEventstoolstripmenuItem.Size = new System.Drawing.Size(134, 22);
            this.predefineEventstoolstripmenuItem.Text = "预定义事件";
            this.predefineEventstoolstripmenuItem.Click += new System.EventHandler(this.Events_Click);
            // 
            // customeEventstoolstripmenuItem
            // 
            this.customeEventstoolstripmenuItem.Name = "customeEventstoolstripmenuItem";
            this.customeEventstoolstripmenuItem.Size = new System.Drawing.Size(134, 22);
            this.customeEventstoolstripmenuItem.Text = "自定义事件";
            this.customeEventstoolstripmenuItem.Click += new System.EventHandler(this.Events_Click);
            // 
            // 导联设置ToolStripMenuItem
            // 
            this.导联设置ToolStripMenuItem.Name = "导联设置ToolStripMenuItem";
            this.导联设置ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.导联设置ToolStripMenuItem.Text = "导联编制";
            this.导联设置ToolStripMenuItem.Click += new System.EventHandler(this.导联设置ToolStripMenuItem_Click);
            // 
            // btn_accelerate
            // 
            this.btn_accelerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_accelerate.Image = ((System.Drawing.Image)(resources.GetObject("btn_accelerate.Image")));
            this.btn_accelerate.Location = new System.Drawing.Point(20, 150);
            this.btn_accelerate.Name = "btn_accelerate";
            this.btn_accelerate.Size = new System.Drawing.Size(45, 32);
            this.btn_accelerate.TabIndex = 10;
            this.btn_accelerate.UseVisualStyleBackColor = true;
            this.btn_accelerate.Click += new System.EventHandler(this.btn_accelerate_Click);
            // 
            // btn_decelerate
            // 
            this.btn_decelerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_decelerate.Image = ((System.Drawing.Image)(resources.GetObject("btn_decelerate.Image")));
            this.btn_decelerate.Location = new System.Drawing.Point(71, 150);
            this.btn_decelerate.Name = "btn_decelerate";
            this.btn_decelerate.Size = new System.Drawing.Size(49, 32);
            this.btn_decelerate.TabIndex = 11;
            this.btn_decelerate.UseVisualStyleBackColor = true;
            this.btn_decelerate.Click += new System.EventHandler(this.btn_decelerate_Click);
            // 
            // btn_hide
            // 
            this.btn_hide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_hide.Image = ((System.Drawing.Image)(resources.GetObject("btn_hide.Image")));
            this.btn_hide.Location = new System.Drawing.Point(126, 150);
            this.btn_hide.Name = "btn_hide";
            this.btn_hide.Size = new System.Drawing.Size(54, 32);
            this.btn_hide.TabIndex = 12;
            this.btn_hide.UseVisualStyleBackColor = true;
            this.btn_hide.Click += new System.EventHandler(this.btn_hide_Click);
            // 
            // boardPanel
            // 
            this.boardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boardPanel.BackColor = System.Drawing.Color.White;
            this.boardPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.boardPanel.Controls.Add(this.tyPanelEventListView);
            this.boardPanel.Controls.Add(this.btn_hide);
            this.boardPanel.Controls.Add(this.btn_decelerate);
            this.boardPanel.Controls.Add(this.btn_accelerate);
            this.boardPanel.Controls.Add(this.panelVideo);
            this.boardPanel.Location = new System.Drawing.Point(742, 60);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(200, 499);
            this.boardPanel.TabIndex = 13;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            // 
            // tyPanelEventListView
            // 
            this.tyPanelEventListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tyPanelEventListView.ColumnCount = 7;
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tyPanelEventListView.Controls.Add(this.lvCustomEvents, 0, 1);
            this.tyPanelEventListView.Controls.Add(this.lvPreDefineEvents, 0, 3);
            this.tyPanelEventListView.Controls.Add(this.btnAddCustomEvents, 1, 0);
            this.tyPanelEventListView.Controls.Add(this.btnDeleteCustomEvents, 2, 0);
            this.tyPanelEventListView.Controls.Add(this.btnEditPreDefineEvents, 1, 2);
            this.tyPanelEventListView.Controls.Add(this.btnDeletePredefineEvents, 2, 2);
            this.tyPanelEventListView.Controls.Add(this.btnEditCustomEvents, 5, 0);
            this.tyPanelEventListView.Controls.Add(this.lblCustomEvent, 0, 0);
            this.tyPanelEventListView.Controls.Add(this.lblPreDefineEvent, 0, 2);
            this.tyPanelEventListView.Location = new System.Drawing.Point(11, 199);
            this.tyPanelEventListView.Name = "tyPanelEventListView";
            this.tyPanelEventListView.RowCount = 4;
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tyPanelEventListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tyPanelEventListView.Size = new System.Drawing.Size(186, 297);
            this.tyPanelEventListView.TabIndex = 15;
            // 
            // lvCustomEvents
            // 
            this.lvCustomEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCustomEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.tyPanelEventListView.SetColumnSpan(this.lvCustomEvents, 7);
            this.lvCustomEvents.FullRowSelect = true;
            this.lvCustomEvents.GridLines = true;
            this.lvCustomEvents.Location = new System.Drawing.Point(3, 14);
            this.lvCustomEvents.Name = "lvCustomEvents";
            this.lvCustomEvents.Size = new System.Drawing.Size(180, 130);
            this.lvCustomEvents.TabIndex = 14;
            this.lvCustomEvents.UseCompatibleStateImageBehavior = false;
            this.lvCustomEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 36;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "发生时间";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "编号";
            this.columnHeader3.Width = 36;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "颜色";
            this.columnHeader4.Width = 36;
            // 
            // lvPreDefineEvents
            // 
            this.lvPreDefineEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPreDefineEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.time,
            this.number,
            this.color});
            this.tyPanelEventListView.SetColumnSpan(this.lvPreDefineEvents, 7);
            this.lvPreDefineEvents.FullRowSelect = true;
            this.lvPreDefineEvents.GridLines = true;
            this.lvPreDefineEvents.Location = new System.Drawing.Point(3, 161);
            this.lvPreDefineEvents.Name = "lvPreDefineEvents";
            this.lvPreDefineEvents.Size = new System.Drawing.Size(180, 133);
            this.lvPreDefineEvents.TabIndex = 13;
            this.lvPreDefineEvents.UseCompatibleStateImageBehavior = false;
            this.lvPreDefineEvents.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "名称";
            this.name.Width = 36;
            // 
            // time
            // 
            this.time.Text = "发生时间";
            // 
            // number
            // 
            this.number.Text = "编号";
            this.number.Width = 36;
            // 
            // color
            // 
            this.color.Text = "颜色";
            this.color.Width = 36;
            // 
            // btnAddCustomEvents
            // 
            this.btnAddCustomEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tyPanelEventListView.SetColumnSpan(this.btnAddCustomEvents, 2);
            this.btnAddCustomEvents.Location = new System.Drawing.Point(29, 3);
            this.btnAddCustomEvents.Name = "btnAddCustomEvents";
            this.btnAddCustomEvents.Size = new System.Drawing.Size(46, 5);
            this.btnAddCustomEvents.TabIndex = 17;
            this.btnAddCustomEvents.Text = "增加";
            this.btnAddCustomEvents.UseVisualStyleBackColor = true;
            this.btnAddCustomEvents.Click += new System.EventHandler(this.btnAddCustomEvents_Click);
            // 
            // btnDeleteCustomEvents
            // 
            this.btnDeleteCustomEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tyPanelEventListView.SetColumnSpan(this.btnDeleteCustomEvents, 2);
            this.btnDeleteCustomEvents.Location = new System.Drawing.Point(81, 3);
            this.btnDeleteCustomEvents.Name = "btnDeleteCustomEvents";
            this.btnDeleteCustomEvents.Size = new System.Drawing.Size(46, 5);
            this.btnDeleteCustomEvents.TabIndex = 18;
            this.btnDeleteCustomEvents.Text = "删除";
            this.btnDeleteCustomEvents.UseVisualStyleBackColor = true;
            this.btnDeleteCustomEvents.Click += new System.EventHandler(this.btnDeleteCustomEvents_Click);
            // 
            // btnEditPreDefineEvents
            // 
            this.btnEditPreDefineEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tyPanelEventListView.SetColumnSpan(this.btnEditPreDefineEvents, 3);
            this.btnEditPreDefineEvents.Location = new System.Drawing.Point(29, 150);
            this.btnEditPreDefineEvents.Name = "btnEditPreDefineEvents";
            this.btnEditPreDefineEvents.Size = new System.Drawing.Size(72, 5);
            this.btnEditPreDefineEvents.TabIndex = 19;
            this.btnEditPreDefineEvents.Text = "添加";
            this.btnEditPreDefineEvents.UseVisualStyleBackColor = true;
            this.btnEditPreDefineEvents.Click += new System.EventHandler(this.btnEditPreDefineEvents_Click);
            // 
            // btnDeletePredefineEvents
            // 
            this.btnDeletePredefineEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tyPanelEventListView.SetColumnSpan(this.btnDeletePredefineEvents, 3);
            this.btnDeletePredefineEvents.Location = new System.Drawing.Point(107, 150);
            this.btnDeletePredefineEvents.Name = "btnDeletePredefineEvents";
            this.btnDeletePredefineEvents.Size = new System.Drawing.Size(76, 5);
            this.btnDeletePredefineEvents.TabIndex = 20;
            this.btnDeletePredefineEvents.Text = "删除";
            this.btnDeletePredefineEvents.UseVisualStyleBackColor = true;
            this.btnDeletePredefineEvents.Click += new System.EventHandler(this.btnDeletePredefineEvents_Click);
            // 
            // btnEditCustomEvents
            // 
            this.btnEditCustomEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tyPanelEventListView.SetColumnSpan(this.btnEditCustomEvents, 2);
            this.btnEditCustomEvents.Location = new System.Drawing.Point(133, 3);
            this.btnEditCustomEvents.Name = "btnEditCustomEvents";
            this.btnEditCustomEvents.Size = new System.Drawing.Size(50, 5);
            this.btnEditCustomEvents.TabIndex = 21;
            this.btnEditCustomEvents.Text = "修改";
            this.btnEditCustomEvents.UseVisualStyleBackColor = true;
            this.btnEditCustomEvents.Click += new System.EventHandler(this.btnEditCustomEvents_Click);
            // 
            // lblCustomEvent
            // 
            this.lblCustomEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustomEvent.AutoSize = true;
            this.lblCustomEvent.Location = new System.Drawing.Point(3, 0);
            this.lblCustomEvent.Name = "lblCustomEvent";
            this.lblCustomEvent.Size = new System.Drawing.Size(20, 11);
            this.lblCustomEvent.TabIndex = 16;
            this.lblCustomEvent.Text = "自";
            // 
            // lblPreDefineEvent
            // 
            this.lblPreDefineEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreDefineEvent.AutoSize = true;
            this.lblPreDefineEvent.Location = new System.Drawing.Point(3, 147);
            this.lblPreDefineEvent.Name = "lblPreDefineEvent";
            this.lblPreDefineEvent.Size = new System.Drawing.Size(20, 11);
            this.lblPreDefineEvent.TabIndex = 15;
            this.lblPreDefineEvent.Text = "预";
            // 
            // vScroll
            // 
            this.vScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScroll.Location = new System.Drawing.Point(725, 65);
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(17, 492);
            this.vScroll.TabIndex = 14;
            this.vScroll.MouseCaptureChanged += new System.EventHandler(this.VScrollBar_mouseCapture);
            // 
            // labelPanel
            // 
            this.labelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPanel.Location = new System.Drawing.Point(0, 63);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(62, 494);
            this.labelPanel.TabIndex = 15;
            this.labelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawLabelPanel);
            // 
            // PlaybackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 582);
            this.Controls.Add(this.vScroll);
            this.Controls.Add(this.hsProgress);
            this.Controls.Add(this.boardPanel);
            this.Controls.Add(this.PationInfoPanel);
            this.Controls.Add(this.DetectionInfoPanel);
            this.Controls.Add(this.chartWave);
            this.Controls.Add(this.labelPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PlaybackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "脑电视频回放";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlaybackForm_FormClosed);
            this.Load += new System.EventHandler(this.PlaybackForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPaint);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWave)).EndInit();
            this.PationInfoPanel.ResumeLayout(false);
            this.PationInfoPanel.PerformLayout();
            this.DetectionInfoPanel.ResumeLayout(false);
            this.DetectionInfoPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.boardPanel.ResumeLayout(false);
            this.tyPanelEventListView.ResumeLayout(false);
            this.tyPanelEventListView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelVideo;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartWave;
        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton btnPause;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.HScrollBar hsProgress;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        private System.Windows.Forms.ToolStripMenuItem PationInfo;
        private System.Windows.Forms.ToolStripMenuItem DetectionInfo;
        private System.Windows.Forms.ToolStripMenuItem Hiding;
        private System.Windows.Forms.Panel PationInfoPanel;
        private System.Windows.Forms.Label PatIDL;
        private System.Windows.Forms.TextBox PatIDTextBt;
        private System.Windows.Forms.Label PatNameL;
        private System.Windows.Forms.TextBox PatNameTextBt;
        private System.Windows.Forms.Label PatGenderL;
        private System.Windows.Forms.ComboBox SingleHandAdvanCB;
        private System.Windows.Forms.Label SingleHandAdvanL;
        private System.Windows.Forms.Panel DetectionInfoPanel;
        private System.Windows.Forms.Label DetectinonL;
        private System.Windows.Forms.TextBox DetectionTextBt;
        private System.Windows.Forms.Label DetectionRemarksL;
        private System.Windows.Forms.TextBox DetectionRemarksTextBt;
        private System.Windows.Forms.Label PharmacyL;
        private System.Windows.Forms.TextBox PharmacyTextBt;
        private System.Windows.Forms.Label PationStateL;
        private System.Windows.Forms.TextBox PationStateTextBt;
        private System.Windows.Forms.Label PhysicianL;
        private System.Windows.Forms.TextBox PhysicianTextBT;
        private System.Windows.Forms.Label TechnicianL;
        private System.Windows.Forms.TextBox TechnicianTextBt;
        private System.Windows.Forms.Label RequesterL;
        private System.Windows.Forms.TextBox RequesterTextBt;
        private System.Windows.Forms.ComboBox PatGenderCB;
        private System.Windows.Forms.Label PatAgeL;
        private System.Windows.Forms.TextBox PatAgeTextBt;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripDropDownButton InformationTSSSBt;
        private System.Windows.Forms.ToolStripMenuItem pationInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detectionInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideingToolStripMenuItem;
        private System.Windows.Forms.Label FilePathL;
        private System.Windows.Forms.TextBox FilePathTextBt;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensitivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeStandartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predefineEventstoolstripmenuItem;
        private System.Windows.Forms.ToolStripMenuItem customeEventstoolstripmenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStriplabel_abtime;
        private System.Windows.Forms.ToolStripStatusLabel displayStartTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStriplabel_retime;
        private System.Windows.Forms.ToolStripStatusLabel displayRecordingTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStriplabel_totaltime;
        private System.Windows.Forms.ToolStripStatusLabel displayTotalTime;
        public System.Windows.Forms.Button btn_accelerate;
        public System.Windows.Forms.Button btn_decelerate;
        private System.Windows.Forms.Button btn_hide;
        private System.Windows.Forms.ToolStripMenuItem calibrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrateYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrateXToolStripMenuItem;
        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.ToolStripMenuItem interfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boardToolStripMenuItem;
        private System.Windows.Forms.VScrollBar vScroll;
        private System.Windows.Forms.Panel labelPanel;
        private System.Windows.Forms.ToolStripMenuItem leadChooseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导联设置ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem 滤波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Filter50HzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BandFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_timeStandardLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_timeStandard;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_sensitivityLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_sensitivity;
        private System.Windows.Forms.ListView lvPreDefineEvents;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader number;
        private System.Windows.Forms.ColumnHeader color;
        private System.Windows.Forms.ListView lvCustomEvents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TableLayoutPanel tyPanelEventListView;
        private System.Windows.Forms.Label lblPreDefineEvent;
        private System.Windows.Forms.Label lblCustomEvent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_lbTrap;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_trap;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_lbBandFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_bandFilter;
        private System.Windows.Forms.Button btnAddCustomEvents;
        private System.Windows.Forms.Button btnDeleteCustomEvents;
        private System.Windows.Forms.Button btnEditPreDefineEvents;
        private System.Windows.Forms.Button btnDeletePredefineEvents;
        private System.Windows.Forms.Button btnEditCustomEvents;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnClose;
    }
}
