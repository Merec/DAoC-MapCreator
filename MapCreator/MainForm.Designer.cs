namespace MapCreator
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.queueProcessedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.queueTotalLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createShapedNIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cachesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearfixturesPolygonCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearheightmapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dawnOfLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportABugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMapCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.widthTextBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.createBackgroundCheckBox = new System.Windows.Forms.CheckBox();
            this.generateLightmapCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.heightmapZVector3TextBox = new System.Windows.Forms.NumericUpDown();
            this.heightmapZVector2TextBox = new System.Windows.Forms.NumericUpDown();
            this.heightmapZVector1TextBox = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.heightmapZScaleTextBox = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.heightmapLightMaxTextBox = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.heightmapLightMinTextBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.selectMapsButton = new System.Windows.Forms.Button();
            this.selectedMapsListBox = new System.Windows.Forms.ListBox();
            this.selecetedMapsResetButton = new System.Windows.Forms.Button();
            this.selectedMapsCounterLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.excludeBoundsFromMapCheckbox = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mapBoundsOpacityTextBox = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.boundsColorSelectButton = new System.Windows.Forms.Button();
            this.mapBoundsColorTextBox = new System.Windows.Forms.TextBox();
            this.mapBoundsColorPreview = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.generateBoundsCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.riversUseDefaultColorCheckBox = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.mapRiversOpacityTextBox = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.riversColorSelectButton = new System.Windows.Forms.Button();
            this.mapRiversColorTextBox = new System.Windows.Forms.TextBox();
            this.riversColorPreview = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.generateRiversCheckBox = new System.Windows.Forms.CheckBox();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.drawMapBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.riversColorColorDialog = new System.Windows.Forms.ColorDialog();
            this.boundsColorDialog = new System.Windows.Forms.ColorDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.drawFixturesBelowWaterCheckBox = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.mapTreeTransparencyTextBox = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.treesAsImages = new System.Windows.Forms.RadioButton();
            this.treesAsShadedModel = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.drawTreesCheckBox = new System.Windows.Forms.CheckBox();
            this.drawFixturesCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.renderButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.enableLogCheckBox = new System.Windows.Forms.CheckBox();
            this.enableResultPreview = new System.Windows.Forms.CheckBox();
            this.skipIfFileExistsCheckbox = new System.Windows.Forms.CheckBox();
            this.directoryPatternTextBox = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.filePatternTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mapQualityTextBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.fileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutSizerPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mapPreview = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.currentMapLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatus.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthTextBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector3TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector2TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector1TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZScaleTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMaxTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMinTextBox)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBoundsOpacityTextBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapRiversOpacityTextBox)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTreeTransparencyTextBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapQualityTextBox)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatus
            // 
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressBar,
            this.queueProcessedLabel,
            this.toolStripStatusLabel2,
            this.queueTotalLabel,
            this.currentMapLabel,
            this.statusLabel});
            this.mainStatus.Location = new System.Drawing.Point(2, 789);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(977, 22);
            this.mainStatus.TabIndex = 0;
            this.mainStatus.Text = "statusStrip1";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // queueProcessedLabel
            // 
            this.queueProcessedLabel.Name = "queueProcessedLabel";
            this.queueProcessedLabel.Size = new System.Drawing.Size(13, 17);
            this.queueProcessedLabel.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(-4, 3, -4, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel2.Text = "/";
            // 
            // queueTotalLabel
            // 
            this.queueTotalLabel.Name = "queueTotalLabel";
            this.queueTotalLabel.Size = new System.Drawing.Size(13, 17);
            this.queueTotalLabel.Text = "0";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(2, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(977, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createShapedNIFToolStripMenuItem,
            this.cachesToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(46, 20);
            this.toolStripMenuItem1.Text = "&Tools";
            // 
            // createShapedNIFToolStripMenuItem
            // 
            this.createShapedNIFToolStripMenuItem.Name = "createShapedNIFToolStripMenuItem";
            this.createShapedNIFToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.createShapedNIFToolStripMenuItem.Text = "Create &shaped NIF";
            this.createShapedNIFToolStripMenuItem.Visible = false;
            this.createShapedNIFToolStripMenuItem.Click += new System.EventHandler(this.createShapedNIFToolStripMenuItem_Click);
            // 
            // cachesToolStripMenuItem
            // 
            this.cachesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearfixturesPolygonCacheToolStripMenuItem,
            this.clearheightmapsToolStripMenuItem});
            this.cachesToolStripMenuItem.Name = "cachesToolStripMenuItem";
            this.cachesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.cachesToolStripMenuItem.Text = "&Caches";
            // 
            // clearfixturesPolygonCacheToolStripMenuItem
            // 
            this.clearfixturesPolygonCacheToolStripMenuItem.Name = "clearfixturesPolygonCacheToolStripMenuItem";
            this.clearfixturesPolygonCacheToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.clearfixturesPolygonCacheToolStripMenuItem.Text = "Delete &fixture polygons";
            this.clearfixturesPolygonCacheToolStripMenuItem.Click += new System.EventHandler(this.clearfixturesPolygonCacheToolStripMenuItem_Click);
            // 
            // clearheightmapsToolStripMenuItem
            // 
            this.clearheightmapsToolStripMenuItem.Name = "clearheightmapsToolStripMenuItem";
            this.clearheightmapsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.clearheightmapsToolStripMenuItem.Text = "Delete &heightmaps";
            this.clearheightmapsToolStripMenuItem.Click += new System.EventHandler(this.clearheightmapsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dawnOfLightToolStripMenuItem,
            this.reportABugToolStripMenuItem,
            this.aboutMapCreatorToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // dawnOfLightToolStripMenuItem
            // 
            this.dawnOfLightToolStripMenuItem.Image = global::MapCreator.Properties.Resources.dol_small;
            this.dawnOfLightToolStripMenuItem.Name = "dawnOfLightToolStripMenuItem";
            this.dawnOfLightToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.dawnOfLightToolStripMenuItem.Text = "Dawn of Light";
            this.dawnOfLightToolStripMenuItem.Click += new System.EventHandler(this.dawnOfLightToolStripMenuItem_Click);
            // 
            // reportABugToolStripMenuItem
            // 
            this.reportABugToolStripMenuItem.Name = "reportABugToolStripMenuItem";
            this.reportABugToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.reportABugToolStripMenuItem.Text = "Report a bug";
            this.reportABugToolStripMenuItem.Click += new System.EventHandler(this.reportABugToolStripMenuItem_Click);
            // 
            // aboutMapCreatorToolStripMenuItem
            // 
            this.aboutMapCreatorToolStripMenuItem.Name = "aboutMapCreatorToolStripMenuItem";
            this.aboutMapCreatorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aboutMapCreatorToolStripMenuItem.Text = "&About MapCreator";
            this.aboutMapCreatorToolStripMenuItem.Click += new System.EventHandler(this.aboutMapCreatorToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.widthTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map settings";
            // 
            // widthTextBox
            // 
            this.widthTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.widthTextBox.Location = new System.Drawing.Point(70, 17);
            this.widthTextBox.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.ReadOnly = true;
            this.widthTextBox.Size = new System.Drawing.Size(68, 20);
            this.widthTextBox.TabIndex = 13;
            this.widthTextBox.Value = global::MapCreator.Properties.Settings.Default.mapWidth;
            this.widthTextBox.ValueChanged += new System.EventHandler(this.widthTextBox_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dimensions";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.createBackgroundCheckBox);
            this.groupBox2.Controls.Add(this.generateLightmapCheckBox);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.heightmapZVector3TextBox);
            this.groupBox2.Controls.Add(this.heightmapZVector2TextBox);
            this.groupBox2.Controls.Add(this.heightmapZVector1TextBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.heightmapZScaleTextBox);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.heightmapLightMaxTextBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.heightmapLightMinTextBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 294);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 149);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Background";
            // 
            // createBackgroundCheckBox
            // 
            this.createBackgroundCheckBox.AutoSize = true;
            this.createBackgroundCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapGenerateBackground;
            this.createBackgroundCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createBackgroundCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapGenerateBackground", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.createBackgroundCheckBox.Location = new System.Drawing.Point(9, 19);
            this.createBackgroundCheckBox.Name = "createBackgroundCheckBox";
            this.createBackgroundCheckBox.Size = new System.Drawing.Size(117, 17);
            this.createBackgroundCheckBox.TabIndex = 26;
            this.createBackgroundCheckBox.Text = "Create background";
            this.createBackgroundCheckBox.UseVisualStyleBackColor = true;
            // 
            // generateLightmapCheckBox
            // 
            this.generateLightmapCheckBox.AutoSize = true;
            this.generateLightmapCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapGenerateHeightmap;
            this.generateLightmapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateLightmapCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapGenerateHeightmap", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.generateLightmapCheckBox.Location = new System.Drawing.Point(132, 19);
            this.generateLightmapCheckBox.Name = "generateLightmapCheckBox";
            this.generateLightmapCheckBox.Size = new System.Drawing.Size(146, 17);
            this.generateLightmapCheckBox.TabIndex = 25;
            this.generateLightmapCheckBox.Text = "Generate height/lightmap";
            this.generateLightmapCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(116, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(198, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "exaggeration factor applied to heightfield";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(88, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Min";
            // 
            // heightmapZVector3TextBox
            // 
            this.heightmapZVector3TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapZVector3", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapZVector3TextBox.DecimalPlaces = 2;
            this.heightmapZVector3TextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.heightmapZVector3TextBox.Location = new System.Drawing.Point(268, 116);
            this.heightmapZVector3TextBox.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.heightmapZVector3TextBox.Name = "heightmapZVector3TextBox";
            this.heightmapZVector3TextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapZVector3TextBox.TabIndex = 22;
            this.heightmapZVector3TextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapZVector3;
            // 
            // heightmapZVector2TextBox
            // 
            this.heightmapZVector2TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapZVector2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapZVector2TextBox.DecimalPlaces = 2;
            this.heightmapZVector2TextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.heightmapZVector2TextBox.Location = new System.Drawing.Point(193, 116);
            this.heightmapZVector2TextBox.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.heightmapZVector2TextBox.Name = "heightmapZVector2TextBox";
            this.heightmapZVector2TextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapZVector2TextBox.TabIndex = 21;
            this.heightmapZVector2TextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapZVector2;
            // 
            // heightmapZVector1TextBox
            // 
            this.heightmapZVector1TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapZVector1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapZVector1TextBox.DecimalPlaces = 2;
            this.heightmapZVector1TextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.heightmapZVector1TextBox.Location = new System.Drawing.Point(118, 116);
            this.heightmapZVector1TextBox.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.heightmapZVector1TextBox.Name = "heightmapZVector1TextBox";
            this.heightmapZVector1TextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapZVector1TextBox.TabIndex = 20;
            this.heightmapZVector1TextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapZVector1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Light direction vector";
            // 
            // heightmapZScaleTextBox
            // 
            this.heightmapZScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapZScale", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapZScaleTextBox.DecimalPlaces = 2;
            this.heightmapZScaleTextBox.Location = new System.Drawing.Point(118, 42);
            this.heightmapZScaleTextBox.Name = "heightmapZScaleTextBox";
            this.heightmapZScaleTextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapZScaleTextBox.TabIndex = 18;
            this.heightmapZScaleTextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapZScale;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Z-Scale";
            // 
            // heightmapLightMaxTextBox
            // 
            this.heightmapLightMaxTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapMaxLight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapLightMaxTextBox.DecimalPlaces = 2;
            this.heightmapLightMaxTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.heightmapLightMaxTextBox.Location = new System.Drawing.Point(226, 92);
            this.heightmapLightMaxTextBox.Name = "heightmapLightMaxTextBox";
            this.heightmapLightMaxTextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapLightMaxTextBox.TabIndex = 16;
            this.heightmapLightMaxTextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapMaxLight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(193, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Max";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Light spectrum";
            // 
            // heightmapLightMinTextBox
            // 
            this.heightmapLightMinTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapMinLight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapLightMinTextBox.DecimalPlaces = 2;
            this.heightmapLightMinTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.heightmapLightMinTextBox.Location = new System.Drawing.Point(118, 92);
            this.heightmapLightMinTextBox.Name = "heightmapLightMinTextBox";
            this.heightmapLightMinTextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapLightMinTextBox.TabIndex = 13;
            this.heightmapLightMinTextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapMinLight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.selectMapsButton);
            this.groupBox5.Controls.Add(this.selectedMapsListBox);
            this.groupBox5.Controls.Add(this.selecetedMapsResetButton);
            this.groupBox5.Controls.Add(this.selectedMapsCounterLabel);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(350, 123);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Maps";
            // 
            // selectMapsButton
            // 
            this.selectMapsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectMapsButton.Location = new System.Drawing.Point(244, 92);
            this.selectMapsButton.Name = "selectMapsButton";
            this.selectMapsButton.Size = new System.Drawing.Size(100, 25);
            this.selectMapsButton.TabIndex = 5;
            this.selectMapsButton.Text = "Select maps...";
            this.selectMapsButton.UseVisualStyleBackColor = true;
            this.selectMapsButton.Click += new System.EventHandler(this.selectMapsButton_Click);
            // 
            // selectedMapsListBox
            // 
            this.selectedMapsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedMapsListBox.IntegralHeight = false;
            this.selectedMapsListBox.Location = new System.Drawing.Point(6, 19);
            this.selectedMapsListBox.Name = "selectedMapsListBox";
            this.selectedMapsListBox.Size = new System.Drawing.Size(338, 67);
            this.selectedMapsListBox.TabIndex = 4;
            // 
            // selecetedMapsResetButton
            // 
            this.selecetedMapsResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selecetedMapsResetButton.Location = new System.Drawing.Point(196, 92);
            this.selecetedMapsResetButton.Name = "selecetedMapsResetButton";
            this.selecetedMapsResetButton.Size = new System.Drawing.Size(42, 25);
            this.selecetedMapsResetButton.TabIndex = 3;
            this.selecetedMapsResetButton.Text = "Clear";
            this.selecetedMapsResetButton.UseVisualStyleBackColor = true;
            this.selecetedMapsResetButton.Click += new System.EventHandler(this.selecetedMapsResetButton_Click);
            // 
            // selectedMapsCounterLabel
            // 
            this.selectedMapsCounterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedMapsCounterLabel.AutoSize = true;
            this.selectedMapsCounterLabel.Location = new System.Drawing.Point(93, 98);
            this.selectedMapsCounterLabel.Name = "selectedMapsCounterLabel";
            this.selectedMapsCounterLabel.Size = new System.Drawing.Size(13, 13);
            this.selectedMapsCounterLabel.TabIndex = 2;
            this.selectedMapsCounterLabel.Text = "0";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 98);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(81, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Selected Maps:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.excludeBoundsFromMapCheckbox);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.mapBoundsOpacityTextBox);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.boundsColorSelectButton);
            this.groupBox4.Controls.Add(this.mapBoundsColorTextBox);
            this.groupBox4.Controls.Add(this.mapBoundsColorPreview);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.generateBoundsCheckBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 132);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(350, 100);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bounds";
            // 
            // excludeBoundsFromMapCheckbox
            // 
            this.excludeBoundsFromMapCheckbox.AutoSize = true;
            this.excludeBoundsFromMapCheckbox.Checked = global::MapCreator.Properties.Settings.Default.removeBoundsFromMap;
            this.excludeBoundsFromMapCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "removeBoundsFromMap", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.excludeBoundsFromMapCheckbox.Location = new System.Drawing.Point(123, 19);
            this.excludeBoundsFromMapCheckbox.Name = "excludeBoundsFromMapCheckbox";
            this.excludeBoundsFromMapCheckbox.Size = new System.Drawing.Size(83, 17);
            this.excludeBoundsFromMapCheckbox.TabIndex = 29;
            this.excludeBoundsFromMapCheckbox.Text = "Transparent";
            this.excludeBoundsFromMapCheckbox.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(120, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "#";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(206, 73);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 13);
            this.label19.TabIndex = 27;
            this.label19.Text = "%";
            // 
            // mapBoundsOpacityTextBox
            // 
            this.mapBoundsOpacityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapBoundsOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapBoundsOpacityTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.mapBoundsOpacityTextBox.Location = new System.Drawing.Point(135, 69);
            this.mapBoundsOpacityTextBox.Name = "mapBoundsOpacityTextBox";
            this.mapBoundsOpacityTextBox.Size = new System.Drawing.Size(69, 20);
            this.mapBoundsOpacityTextBox.TabIndex = 26;
            this.mapBoundsOpacityTextBox.Value = global::MapCreator.Properties.Settings.Default.mapBoundsOpacity;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 72);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Transparency";
            // 
            // boundsColorSelectButton
            // 
            this.boundsColorSelectButton.Location = new System.Drawing.Point(212, 41);
            this.boundsColorSelectButton.Name = "boundsColorSelectButton";
            this.boundsColorSelectButton.Size = new System.Drawing.Size(91, 23);
            this.boundsColorSelectButton.TabIndex = 4;
            this.boundsColorSelectButton.Text = "Choose color...";
            this.boundsColorSelectButton.UseVisualStyleBackColor = true;
            this.boundsColorSelectButton.Click += new System.EventHandler(this.boundsColorSelectButton_Click);
            // 
            // mapBoundsColorTextBox
            // 
            this.mapBoundsColorTextBox.Location = new System.Drawing.Point(135, 43);
            this.mapBoundsColorTextBox.MaxLength = 6;
            this.mapBoundsColorTextBox.Name = "mapBoundsColorTextBox";
            this.mapBoundsColorTextBox.Size = new System.Drawing.Size(71, 20);
            this.mapBoundsColorTextBox.TabIndex = 3;
            this.mapBoundsColorTextBox.TextChanged += new System.EventHandler(this.mapBoundsColorTextBox_TextChanged);
            this.mapBoundsColorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mapBoundsColorTextBox_KeyDown);
            // 
            // mapBoundsColorPreview
            // 
            this.mapBoundsColorPreview.BackColor = global::MapCreator.Properties.Settings.Default.mapBoundsColor;
            this.mapBoundsColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapBoundsColorPreview.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::MapCreator.Properties.Settings.Default, "mapBoundsColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapBoundsColorPreview.Location = new System.Drawing.Point(81, 42);
            this.mapBoundsColorPreview.Name = "mapBoundsColorPreview";
            this.mapBoundsColorPreview.Size = new System.Drawing.Size(33, 48);
            this.mapBoundsColorPreview.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 46);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Color";
            // 
            // generateBoundsCheckBox
            // 
            this.generateBoundsCheckBox.AutoSize = true;
            this.generateBoundsCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapGenerateBounds;
            this.generateBoundsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateBoundsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapGenerateBounds", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.generateBoundsCheckBox.Location = new System.Drawing.Point(9, 19);
            this.generateBoundsCheckBox.Name = "generateBoundsCheckBox";
            this.generateBoundsCheckBox.Size = new System.Drawing.Size(89, 17);
            this.generateBoundsCheckBox.TabIndex = 0;
            this.generateBoundsCheckBox.Text = "Draw bounds";
            this.generateBoundsCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.riversUseDefaultColorCheckBox);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.mapRiversOpacityTextBox);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.riversColorSelectButton);
            this.groupBox3.Controls.Add(this.mapRiversColorTextBox);
            this.groupBox3.Controls.Add(this.riversColorPreview);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.generateRiversCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 449);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 122);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rivers";
            // 
            // riversUseDefaultColorCheckBox
            // 
            this.riversUseDefaultColorCheckBox.AutoSize = true;
            this.riversUseDefaultColorCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapRiverColorUseDefault;
            this.riversUseDefaultColorCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapRiverColorUseDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.riversUseDefaultColorCheckBox.Location = new System.Drawing.Point(81, 98);
            this.riversUseDefaultColorCheckBox.Name = "riversUseDefaultColorCheckBox";
            this.riversUseDefaultColorCheckBox.Size = new System.Drawing.Size(143, 17);
            this.riversUseDefaultColorCheckBox.TabIndex = 29;
            this.riversUseDefaultColorCheckBox.Text = "game client defines color";
            this.riversUseDefaultColorCheckBox.UseVisualStyleBackColor = true;
            this.riversUseDefaultColorCheckBox.CheckedChanged += new System.EventHandler(this.riverUseColorDefault_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(120, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "#";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(206, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "%";
            // 
            // mapRiversOpacityTextBox
            // 
            this.mapRiversOpacityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapRiverOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapRiversOpacityTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.mapRiversOpacityTextBox.Location = new System.Drawing.Point(135, 72);
            this.mapRiversOpacityTextBox.Name = "mapRiversOpacityTextBox";
            this.mapRiversOpacityTextBox.Size = new System.Drawing.Size(69, 20);
            this.mapRiversOpacityTextBox.TabIndex = 26;
            this.mapRiversOpacityTextBox.Value = global::MapCreator.Properties.Settings.Default.mapRiverOpacity;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Transparency";
            // 
            // riversColorSelectButton
            // 
            this.riversColorSelectButton.Location = new System.Drawing.Point(212, 43);
            this.riversColorSelectButton.Name = "riversColorSelectButton";
            this.riversColorSelectButton.Size = new System.Drawing.Size(91, 23);
            this.riversColorSelectButton.TabIndex = 4;
            this.riversColorSelectButton.Text = "Choose color...";
            this.riversColorSelectButton.UseVisualStyleBackColor = true;
            this.riversColorSelectButton.Click += new System.EventHandler(this.riverColorSelectButton_Click);
            // 
            // mapRiversColorTextBox
            // 
            this.mapRiversColorTextBox.Location = new System.Drawing.Point(135, 45);
            this.mapRiversColorTextBox.MaxLength = 6;
            this.mapRiversColorTextBox.Name = "mapRiversColorTextBox";
            this.mapRiversColorTextBox.Size = new System.Drawing.Size(71, 20);
            this.mapRiversColorTextBox.TabIndex = 3;
            this.mapRiversColorTextBox.TextChanged += new System.EventHandler(this.riversColorTextBox_TextChanged);
            this.mapRiversColorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riversColorTextBox_KeyDown);
            // 
            // riversColorPreview
            // 
            this.riversColorPreview.BackColor = global::MapCreator.Properties.Settings.Default.mapRiverColor;
            this.riversColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.riversColorPreview.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::MapCreator.Properties.Settings.Default, "mapRiverColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.riversColorPreview.Location = new System.Drawing.Point(81, 44);
            this.riversColorPreview.Name = "riversColorPreview";
            this.riversColorPreview.Size = new System.Drawing.Size(33, 48);
            this.riversColorPreview.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Color";
            // 
            // generateRiversCheckBox
            // 
            this.generateRiversCheckBox.AutoSize = true;
            this.generateRiversCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapGenerateRivers;
            this.generateRiversCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateRiversCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapGenerateRivers", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.generateRiversCheckBox.Location = new System.Drawing.Point(9, 19);
            this.generateRiversCheckBox.Name = "generateRiversCheckBox";
            this.generateRiversCheckBox.Size = new System.Drawing.Size(79, 17);
            this.generateRiversCheckBox.TabIndex = 0;
            this.generateRiversCheckBox.Text = "Draw rivers";
            this.generateRiversCheckBox.UseVisualStyleBackColor = true;
            // 
            // logListBox
            // 
            this.logListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.logListBox.FormattingEnabled = true;
            this.logListBox.IntegralHeight = false;
            this.logListBox.Location = new System.Drawing.Point(0, 0);
            this.logListBox.Name = "logListBox";
            this.logListBox.ScrollAlwaysVisible = true;
            this.logListBox.Size = new System.Drawing.Size(261, 215);
            this.logListBox.TabIndex = 3;
            this.logListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.logListBox_DrawItem);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Result image";
            // 
            // drawMapBackgroundWorker
            // 
            this.drawMapBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.drawMapBackgroundWorker_DoWork);
            this.drawMapBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.drawMapBackgroundWorker_RunWorkerCompleted);
            // 
            // riversColorColorDialog
            // 
            this.riversColorColorDialog.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox6);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutSizerPanel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(712, 765);
            this.flowLayoutPanel1.TabIndex = 33;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.drawFixturesBelowWaterCheckBox);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.mapTreeTransparencyTextBox);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.treesAsImages);
            this.groupBox6.Controls.Add(this.treesAsShadedModel);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.drawTreesCheckBox);
            this.groupBox6.Controls.Add(this.drawFixturesCheckBox);
            this.groupBox6.Location = new System.Drawing.Point(3, 577);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(350, 180);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Fixtures";
            // 
            // drawFixturesBelowWaterCheckBox
            // 
            this.drawFixturesBelowWaterCheckBox.AutoSize = true;
            this.drawFixturesBelowWaterCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapDrawBuildingsBelowWater;
            this.drawFixturesBelowWaterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawFixturesBelowWaterCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapDrawBuildingsBelowWater", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.drawFixturesBelowWaterCheckBox.Location = new System.Drawing.Point(6, 19);
            this.drawFixturesBelowWaterCheckBox.Name = "drawFixturesBelowWaterCheckBox";
            this.drawFixturesBelowWaterCheckBox.Size = new System.Drawing.Size(172, 17);
            this.drawFixturesBelowWaterCheckBox.TabIndex = 29;
            this.drawFixturesBelowWaterCheckBox.Text = "Draw fixtures below water level";
            this.drawFixturesBelowWaterCheckBox.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(186, 133);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 13);
            this.label25.TabIndex = 28;
            this.label25.Text = "%";
            // 
            // mapTreeTransparencyTextBox
            // 
            this.mapTreeTransparencyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapTreeTransparency", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapTreeTransparencyTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.mapTreeTransparencyTextBox.Location = new System.Drawing.Point(116, 128);
            this.mapTreeTransparencyTextBox.Name = "mapTreeTransparencyTextBox";
            this.mapTreeTransparencyTextBox.Size = new System.Drawing.Size(69, 20);
            this.mapTreeTransparencyTextBox.TabIndex = 6;
            this.mapTreeTransparencyTextBox.Value = global::MapCreator.Properties.Settings.Default.mapTreeTransparency;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(17, 130);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(93, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "Tree transparency";
            // 
            // treesAsImages
            // 
            this.treesAsImages.AutoSize = true;
            this.treesAsImages.Checked = global::MapCreator.Properties.Settings.Default.treesAsImages;
            this.treesAsImages.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "treesAsImages", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.treesAsImages.Location = new System.Drawing.Point(20, 105);
            this.treesAsImages.Name = "treesAsImages";
            this.treesAsImages.Size = new System.Drawing.Size(136, 17);
            this.treesAsImages.TabIndex = 4;
            this.treesAsImages.TabStop = true;
            this.treesAsImages.Text = "with image replacement";
            this.treesAsImages.UseVisualStyleBackColor = true;
            this.treesAsImages.CheckedChanged += new System.EventHandler(this.treesAsImages_CheckedChanged);
            // 
            // treesAsShadedModel
            // 
            this.treesAsShadedModel.AutoSize = true;
            this.treesAsShadedModel.Checked = global::MapCreator.Properties.Settings.Default.treesAsShadedModel;
            this.treesAsShadedModel.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "treesAsShadedModel", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.treesAsShadedModel.Location = new System.Drawing.Point(20, 85);
            this.treesAsShadedModel.Name = "treesAsShadedModel";
            this.treesAsShadedModel.Size = new System.Drawing.Size(105, 17);
            this.treesAsShadedModel.TabIndex = 3;
            this.treesAsShadedModel.Text = "as shaded model";
            this.treesAsShadedModel.UseVisualStyleBackColor = true;
            this.treesAsShadedModel.CheckedChanged += new System.EventHandler(this.treesAsShadedModel_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please see fixtures.xml for more settings";
            // 
            // drawTreesCheckBox
            // 
            this.drawTreesCheckBox.AutoSize = true;
            this.drawTreesCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapDrawTrees;
            this.drawTreesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawTreesCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapDrawTrees", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.drawTreesCheckBox.Location = new System.Drawing.Point(6, 65);
            this.drawTreesCheckBox.Name = "drawTreesCheckBox";
            this.drawTreesCheckBox.Size = new System.Drawing.Size(77, 17);
            this.drawTreesCheckBox.TabIndex = 1;
            this.drawTreesCheckBox.Text = "Draw trees";
            this.drawTreesCheckBox.UseVisualStyleBackColor = true;
            this.drawTreesCheckBox.CheckedChanged += new System.EventHandler(this.drawTreesCheckBox_CheckedChanged);
            // 
            // drawFixturesCheckBox
            // 
            this.drawFixturesCheckBox.AutoSize = true;
            this.drawFixturesCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapDrawBuildings;
            this.drawFixturesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawFixturesCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapDrawBuildings", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.drawFixturesCheckBox.Location = new System.Drawing.Point(6, 42);
            this.drawFixturesCheckBox.Name = "drawFixturesCheckBox";
            this.drawFixturesCheckBox.Size = new System.Drawing.Size(174, 17);
            this.drawFixturesCheckBox.TabIndex = 0;
            this.drawFixturesCheckBox.Text = "Draw fixtures above water level";
            this.drawFixturesCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.renderButton);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Location = new System.Drawing.Point(359, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 306);
            this.panel1.TabIndex = 34;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(3, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 33);
            this.button1.TabIndex = 34;
            this.button1.Text = "Stop!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // renderButton
            // 
            this.renderButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.renderButton.Location = new System.Drawing.Point(184, 269);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(160, 33);
            this.renderButton.TabIndex = 33;
            this.renderButton.Text = "Start";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.enableLogCheckBox);
            this.groupBox7.Controls.Add(this.enableResultPreview);
            this.groupBox7.Controls.Add(this.skipIfFileExistsCheckbox);
            this.groupBox7.Controls.Add(this.directoryPatternTextBox);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.filePatternTextBox);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.mapQualityTextBox);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.fileTypeComboBox);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(350, 251);
            this.groupBox7.TabIndex = 35;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "File settings";
            // 
            // enableLogCheckBox
            // 
            this.enableLogCheckBox.AutoSize = true;
            this.enableLogCheckBox.Checked = global::MapCreator.Properties.Settings.Default.enableLog;
            this.enableLogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableLogCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "enableLog", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableLogCheckBox.Location = new System.Drawing.Point(77, 219);
            this.enableLogCheckBox.Name = "enableLogCheckBox";
            this.enableLogCheckBox.Size = new System.Drawing.Size(96, 17);
            this.enableLogCheckBox.TabIndex = 23;
            this.enableLogCheckBox.Text = "Enable logging";
            this.enableLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableResultPreview
            // 
            this.enableResultPreview.AutoSize = true;
            this.enableResultPreview.Checked = global::MapCreator.Properties.Settings.Default.enableResultPreview;
            this.enableResultPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableResultPreview.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "enableResultPreview", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableResultPreview.Location = new System.Drawing.Point(77, 196);
            this.enableResultPreview.Name = "enableResultPreview";
            this.enableResultPreview.Size = new System.Drawing.Size(170, 17);
            this.enableResultPreview.TabIndex = 22;
            this.enableResultPreview.Text = "Enable preview of result image";
            this.enableResultPreview.UseVisualStyleBackColor = true;
            // 
            // skipIfFileExistsCheckbox
            // 
            this.skipIfFileExistsCheckbox.AutoSize = true;
            this.skipIfFileExistsCheckbox.Checked = global::MapCreator.Properties.Settings.Default.skipIfFileExists;
            this.skipIfFileExistsCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "skipIfFileExists", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.skipIfFileExistsCheckbox.Location = new System.Drawing.Point(77, 173);
            this.skipIfFileExistsCheckbox.Name = "skipIfFileExistsCheckbox";
            this.skipIfFileExistsCheckbox.Size = new System.Drawing.Size(100, 17);
            this.skipIfFileExistsCheckbox.TabIndex = 21;
            this.skipIfFileExistsCheckbox.Text = "Skip if file exists";
            this.skipIfFileExistsCheckbox.UseVisualStyleBackColor = true;
            // 
            // directoryPatternTextBox
            // 
            this.directoryPatternTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryPatternTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MapCreator.Properties.Settings.Default, "targetDirectoryPattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.directoryPatternTextBox.Location = new System.Drawing.Point(74, 98);
            this.directoryPatternTextBox.Name = "directoryPatternTextBox";
            this.directoryPatternTextBox.Size = new System.Drawing.Size(270, 20);
            this.directoryPatternTextBox.TabIndex = 20;
            this.directoryPatternTextBox.Text = global::MapCreator.Properties.Settings.Default.targetDirectoryPattern;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 101);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(49, 13);
            this.label26.TabIndex = 19;
            this.label26.Text = "Directory";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Location = new System.Drawing.Point(74, 121);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(270, 107);
            this.label23.TabIndex = 18;
            this.label23.Text = "{id} = Zone ID; {name} = Zone name; {realm} = Realm of the zone; {expanision} = E" +
    "xpansion of the zone; {type} = Zone type; {size} = Map size;";
            // 
            // filePatternTextBox
            // 
            this.filePatternTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePatternTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MapCreator.Properties.Settings.Default, "mapFilePattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.filePatternTextBox.Location = new System.Drawing.Point(74, 72);
            this.filePatternTextBox.Name = "filePatternTextBox";
            this.filePatternTextBox.Size = new System.Drawing.Size(270, 20);
            this.filePatternTextBox.TabIndex = 17;
            this.filePatternTextBox.Text = global::MapCreator.Properties.Settings.Default.mapFilePattern;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Filename";
            // 
            // mapQualityTextBox
            // 
            this.mapQualityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapQuality", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapQualityTextBox.Location = new System.Drawing.Point(74, 46);
            this.mapQualityTextBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mapQualityTextBox.Name = "mapQualityTextBox";
            this.mapQualityTextBox.Size = new System.Drawing.Size(68, 20);
            this.mapQualityTextBox.TabIndex = 15;
            this.mapQualityTextBox.Value = global::MapCreator.Properties.Settings.Default.mapQuality;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Quality";
            // 
            // fileTypeComboBox
            // 
            this.fileTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MapCreator.Properties.Settings.Default, "mapType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fileTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileTypeComboBox.FormattingEnabled = true;
            this.fileTypeComboBox.Items.AddRange(new object[] {
            "PNG",
            "JPEG"});
            this.fileTypeComboBox.Location = new System.Drawing.Point(74, 19);
            this.fileTypeComboBox.Name = "fileTypeComboBox";
            this.fileTypeComboBox.Size = new System.Drawing.Size(270, 21);
            this.fileTypeComboBox.TabIndex = 1;
            this.fileTypeComboBox.Text = global::MapCreator.Properties.Settings.Default.mapType;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type";
            // 
            // flowLayoutSizerPanel
            // 
            this.flowLayoutSizerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutSizerPanel.Location = new System.Drawing.Point(359, 309);
            this.flowLayoutSizerPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.flowLayoutSizerPanel.Name = "flowLayoutSizerPanel";
            this.flowLayoutSizerPanel.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutSizerPanel.Size = new System.Drawing.Size(350, 2);
            this.flowLayoutSizerPanel.TabIndex = 36;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mapPreview);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 546);
            this.panel2.TabIndex = 4;
            // 
            // mapPreview
            // 
            this.mapPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPreview.ImageLocation = "";
            this.mapPreview.Location = new System.Drawing.Point(0, 16);
            this.mapPreview.Margin = new System.Windows.Forms.Padding(10, 10, 20, 10);
            this.mapPreview.Name = "mapPreview";
            this.mapPreview.Size = new System.Drawing.Size(261, 530);
            this.mapPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mapPreview.TabIndex = 3;
            this.mapPreview.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(2, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(977, 765);
            this.splitContainer1.SplitterDistance = 712;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            this.splitContainer2.Panel1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::MapCreator.Properties.Settings.Default, "enableResultPreview", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer2.Panel1.Enabled = global::MapCreator.Properties.Settings.Default.enableResultPreview;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.logListBox);
            this.splitContainer2.Panel2.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::MapCreator.Properties.Settings.Default, "enableLog", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer2.Panel2.Enabled = global::MapCreator.Properties.Settings.Default.enableLog;
            this.splitContainer2.Size = new System.Drawing.Size(261, 765);
            this.splitContainer2.SplitterDistance = 546;
            this.splitContainer2.TabIndex = 0;
            // 
            // currentMapLabel
            // 
            this.currentMapLabel.Name = "currentMapLabel";
            this.currentMapLabel.Size = new System.Drawing.Size(24, 17);
            this.currentMapLabel.Text = "| - |";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 811);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(1000, 850);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 5, 0);
            this.Text = "MapCreator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthTextBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector3TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector2TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector1TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZScaleTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMaxTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMinTextBox)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBoundsOpacityTextBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapRiversOpacityTextBox)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTreeTransparencyTextBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapQualityTextBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPreview)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox mapPreview;
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker drawMapBackgroundWorker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown heightmapLightMinTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown heightmapLightMaxTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown heightmapZScaleTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown heightmapZVector3TextBox;
        private System.Windows.Forms.NumericUpDown heightmapZVector2TextBox;
        private System.Windows.Forms.NumericUpDown heightmapZVector1TextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox generateLightmapCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown mapRiversOpacityTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button riversColorSelectButton;
        private System.Windows.Forms.TextBox mapRiversColorTextBox;
        private System.Windows.Forms.Panel riversColorPreview;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox generateRiversCheckBox;
        private System.Windows.Forms.ColorDialog riversColorColorDialog;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox riversUseDefaultColorCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown mapBoundsOpacityTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button boundsColorSelectButton;
        private System.Windows.Forms.TextBox mapBoundsColorTextBox;
        private System.Windows.Forms.Panel mapBoundsColorPreview;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox generateBoundsCheckBox;
        private System.Windows.Forms.ColorDialog boundsColorDialog;
        private System.Windows.Forms.NumericUpDown widthTextBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button selecetedMapsResetButton;
        private System.Windows.Forms.Label selectedMapsCounterLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ListBox selectedMapsListBox;
        private System.Windows.Forms.Button selectMapsButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox drawFixturesCheckBox;
        private System.Windows.Forms.Button renderButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox drawTreesCheckBox;
        private System.Windows.Forms.CheckBox excludeBoundsFromMapCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton treesAsImages;
        private System.Windows.Forms.RadioButton treesAsShadedModel;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown mapQualityTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox fileTypeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox filePatternTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dawnOfLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMapCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportABugToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem createShapedNIFToolStripMenuItem;
        private System.Windows.Forms.Panel flowLayoutSizerPanel;
        private System.Windows.Forms.NumericUpDown mapTreeTransparencyTextBox;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ToolStripMenuItem cachesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearfixturesPolygonCacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearheightmapsToolStripMenuItem;
        private System.Windows.Forms.TextBox directoryPatternTextBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox skipIfFileExistsCheckbox;
        private System.Windows.Forms.CheckBox createBackgroundCheckBox;
        private System.Windows.Forms.CheckBox enableResultPreview;
        private System.Windows.Forms.CheckBox enableLogCheckBox;
        private System.Windows.Forms.ToolStripStatusLabel queueTotalLabel;
        private System.Windows.Forms.ToolStripStatusLabel queueProcessedLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.CheckBox drawFixturesBelowWaterCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripStatusLabel currentMapLabel;
    }
}

