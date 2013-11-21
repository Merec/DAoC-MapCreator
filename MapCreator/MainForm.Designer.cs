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
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.heightTextBox = new System.Windows.Forms.NumericUpDown();
            this.widthTextBox = new System.Windows.Forms.NumericUpDown();
            this.typesComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.expansionsComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.realmsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zonesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.renderButton = new System.Windows.Forms.Button();
            this.mapPreview = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
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
            this.mainStatus.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthTextBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector3TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector2TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector1TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZScaleTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMaxTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMinTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBoundsOpacityTextBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapRiversOpacityTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainStatus
            // 
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressBar,
            this.statusLabel});
            this.mainStatus.Location = new System.Drawing.Point(0, 786);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(698, 22);
            this.mainStatus.TabIndex = 0;
            this.mainStatus.Text = "statusStrip1";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(100, 16);
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
            this.settingsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(698, 24);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.heightTextBox);
            this.groupBox1.Controls.Add(this.widthTextBox);
            this.groupBox1.Controls.Add(this.typesComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.expansionsComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.realmsComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.zonesComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.MinimumSize = new System.Drawing.Size(306, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 161);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map settings";
            // 
            // heightTextBox
            // 
            this.heightTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightTextBox.Location = new System.Drawing.Point(220, 129);
            this.heightTextBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.heightTextBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.ReadOnly = true;
            this.heightTextBox.Size = new System.Drawing.Size(100, 20);
            this.heightTextBox.TabIndex = 14;
            this.heightTextBox.Value = global::MapCreator.Properties.Settings.Default.mapWidth;
            // 
            // widthTextBox
            // 
            this.widthTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.widthTextBox.Location = new System.Drawing.Point(70, 129);
            this.widthTextBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.widthTextBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(100, 20);
            this.widthTextBox.TabIndex = 13;
            this.widthTextBox.Value = global::MapCreator.Properties.Settings.Default.mapWidth;
            // 
            // typesComboBox
            // 
            this.typesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.typesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typesComboBox.FormattingEnabled = true;
            this.typesComboBox.Location = new System.Drawing.Point(70, 73);
            this.typesComboBox.Name = "typesComboBox";
            this.typesComboBox.Size = new System.Drawing.Size(333, 21);
            this.typesComboBox.TabIndex = 12;
            this.typesComboBox.SelectedIndexChanged += new System.EventHandler(this.typesComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Type";
            // 
            // expansionsComboBox
            // 
            this.expansionsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.expansionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.expansionsComboBox.FormattingEnabled = true;
            this.expansionsComboBox.Location = new System.Drawing.Point(70, 46);
            this.expansionsComboBox.Name = "expansionsComboBox";
            this.expansionsComboBox.Size = new System.Drawing.Size(333, 21);
            this.expansionsComboBox.TabIndex = 10;
            this.expansionsComboBox.SelectedIndexChanged += new System.EventHandler(this.expansionsComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Expansion";
            // 
            // realmsComboBox
            // 
            this.realmsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.realmsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.realmsComboBox.FormattingEnabled = true;
            this.realmsComboBox.Location = new System.Drawing.Point(70, 19);
            this.realmsComboBox.Name = "realmsComboBox";
            this.realmsComboBox.Size = new System.Drawing.Size(333, 21);
            this.realmsComboBox.TabIndex = 8;
            this.realmsComboBox.SelectedIndexChanged += new System.EventHandler(this.realmsComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Realm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width";
            // 
            // zonesComboBox
            // 
            this.zonesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zonesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zonesComboBox.FormattingEnabled = true;
            this.zonesComboBox.Location = new System.Drawing.Point(70, 100);
            this.zonesComboBox.Name = "zonesComboBox";
            this.zonesComboBox.Size = new System.Drawing.Size(333, 21);
            this.zonesComboBox.TabIndex = 1;
            this.zonesComboBox.SelectedIndexChanged += new System.EventHandler(this.zonesComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zone";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBox2.Location = new System.Drawing.Point(12, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 133);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Background";
            // 
            // generateLightmapCheckBox
            // 
            this.generateLightmapCheckBox.AutoSize = true;
            this.generateLightmapCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapGenerateHeightmap;
            this.generateLightmapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateLightmapCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapGenerateHeightmap", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.generateLightmapCheckBox.Location = new System.Drawing.Point(9, 19);
            this.generateLightmapCheckBox.Name = "generateLightmapCheckBox";
            this.generateLightmapCheckBox.Size = new System.Drawing.Size(112, 17);
            this.generateLightmapCheckBox.TabIndex = 25;
            this.generateLightmapCheckBox.Text = "Generate lightmap";
            this.generateLightmapCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(193, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(198, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "exaggeration factor applied to heightfield";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(88, 77);
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
            this.heightmapZVector3TextBox.Location = new System.Drawing.Point(268, 98);
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
            this.heightmapZVector2TextBox.Location = new System.Drawing.Point(193, 98);
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
            this.heightmapZVector1TextBox.Location = new System.Drawing.Point(118, 98);
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
            this.label11.Location = new System.Drawing.Point(6, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Light direction vector";
            // 
            // heightmapZScaleTextBox
            // 
            this.heightmapZScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapHeightmapZScale", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.heightmapZScaleTextBox.DecimalPlaces = 2;
            this.heightmapZScaleTextBox.Location = new System.Drawing.Point(118, 48);
            this.heightmapZScaleTextBox.Name = "heightmapZScaleTextBox";
            this.heightmapZScaleTextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapZScaleTextBox.TabIndex = 18;
            this.heightmapZScaleTextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapZScale;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 51);
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
            this.heightmapLightMaxTextBox.Location = new System.Drawing.Point(226, 74);
            this.heightmapLightMaxTextBox.Name = "heightmapLightMaxTextBox";
            this.heightmapLightMaxTextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapLightMaxTextBox.TabIndex = 16;
            this.heightmapLightMaxTextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapMaxLight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(193, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Max";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 77);
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
            this.heightmapLightMinTextBox.Location = new System.Drawing.Point(118, 74);
            this.heightmapLightMinTextBox.Name = "heightmapLightMinTextBox";
            this.heightmapLightMinTextBox.Size = new System.Drawing.Size(69, 20);
            this.heightmapLightMinTextBox.TabIndex = 13;
            this.heightmapLightMinTextBox.Value = global::MapCreator.Properties.Settings.Default.mapHeightmapMinLight;
            // 
            // renderButton
            // 
            this.renderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.renderButton.Location = new System.Drawing.Point(300, 525);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(115, 23);
            this.renderButton.TabIndex = 6;
            this.renderButton.Text = "Render";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // mapPreview
            // 
            this.mapPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPreview.Location = new System.Drawing.Point(10, 22);
            this.mapPreview.Margin = new System.Windows.Forms.Padding(10);
            this.mapPreview.Name = "mapPreview";
            this.mapPreview.Size = new System.Drawing.Size(250, 737);
            this.mapPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mapPreview.TabIndex = 3;
            this.mapPreview.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.logListBox);
            this.splitContainer1.Panel1.Controls.Add(this.renderButton);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1MinSize = 424;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.mapPreview);
            this.splitContainer1.Size = new System.Drawing.Size(698, 762);
            this.splitContainer1.SplitterDistance = 424;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.mapBoundsOpacityTextBox);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.boundsColorSelectButton);
            this.groupBox4.Controls.Add(this.mapBoundsColorTextBox);
            this.groupBox4.Controls.Add(this.mapBoundsColorPreview);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.generateBoundsCheckBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 415);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(409, 100);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bounds";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(109, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "#";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(180, 73);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 13);
            this.label19.TabIndex = 27;
            this.label19.Text = "%";
            // 
            // mapBoundsOpacityTextBox
            // 
            this.mapBoundsOpacityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapBoundsOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapBoundsOpacityTextBox.Location = new System.Drawing.Point(109, 70);
            this.mapBoundsOpacityTextBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.label20.Size = new System.Drawing.Size(43, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Opacity";
            // 
            // boundsColorSelectButton
            // 
            this.boundsColorSelectButton.Location = new System.Drawing.Point(201, 41);
            this.boundsColorSelectButton.Name = "boundsColorSelectButton";
            this.boundsColorSelectButton.Size = new System.Drawing.Size(60, 23);
            this.boundsColorSelectButton.TabIndex = 4;
            this.boundsColorSelectButton.Text = "Select";
            this.boundsColorSelectButton.UseVisualStyleBackColor = true;
            this.boundsColorSelectButton.Click += new System.EventHandler(this.boundsColorSelectButton_Click);
            // 
            // mapBoundsColorTextBox
            // 
            this.mapBoundsColorTextBox.Location = new System.Drawing.Point(124, 43);
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
            this.mapBoundsColorPreview.Location = new System.Drawing.Point(70, 42);
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
            this.generateBoundsCheckBox.Size = new System.Drawing.Size(108, 17);
            this.generateBoundsCheckBox.TabIndex = 0;
            this.generateBoundsCheckBox.Text = "Generate bounds";
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
            this.groupBox3.Location = new System.Drawing.Point(12, 309);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 100);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rivers";
            // 
            // riversUseDefaultColorCheckBox
            // 
            this.riversUseDefaultColorCheckBox.AutoSize = true;
            this.riversUseDefaultColorCheckBox.Checked = global::MapCreator.Properties.Settings.Default.mapRiverColorUseDefault;
            this.riversUseDefaultColorCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MapCreator.Properties.Settings.Default, "mapRiverColorUseDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.riversUseDefaultColorCheckBox.Location = new System.Drawing.Point(268, 45);
            this.riversUseDefaultColorCheckBox.Name = "riversUseDefaultColorCheckBox";
            this.riversUseDefaultColorCheckBox.Size = new System.Drawing.Size(78, 17);
            this.riversUseDefaultColorCheckBox.TabIndex = 29;
            this.riversUseDefaultColorCheckBox.Text = "use default";
            this.riversUseDefaultColorCheckBox.UseVisualStyleBackColor = true;
            this.riversUseDefaultColorCheckBox.CheckedChanged += new System.EventHandler(this.riverUseColorDefault_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(109, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "#";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(180, 73);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "%";
            // 
            // mapRiversOpacityTextBox
            // 
            this.mapRiversOpacityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MapCreator.Properties.Settings.Default, "mapRiverOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapRiversOpacityTextBox.Location = new System.Drawing.Point(109, 70);
            this.mapRiversOpacityTextBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mapRiversOpacityTextBox.Name = "mapRiversOpacityTextBox";
            this.mapRiversOpacityTextBox.Size = new System.Drawing.Size(69, 20);
            this.mapRiversOpacityTextBox.TabIndex = 26;
            this.mapRiversOpacityTextBox.Value = global::MapCreator.Properties.Settings.Default.mapRiverOpacity;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Opacity";
            // 
            // riversColorSelectButton
            // 
            this.riversColorSelectButton.Location = new System.Drawing.Point(201, 41);
            this.riversColorSelectButton.Name = "riversColorSelectButton";
            this.riversColorSelectButton.Size = new System.Drawing.Size(60, 23);
            this.riversColorSelectButton.TabIndex = 4;
            this.riversColorSelectButton.Text = "Select";
            this.riversColorSelectButton.UseVisualStyleBackColor = true;
            this.riversColorSelectButton.Click += new System.EventHandler(this.riverColorSelectButton_Click);
            // 
            // mapRiversColorTextBox
            // 
            this.mapRiversColorTextBox.Location = new System.Drawing.Point(124, 43);
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
            this.riversColorPreview.Location = new System.Drawing.Point(70, 42);
            this.riversColorPreview.Name = "riversColorPreview";
            this.riversColorPreview.Size = new System.Drawing.Size(33, 48);
            this.riversColorPreview.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 46);
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
            this.generateRiversCheckBox.Size = new System.Drawing.Size(98, 17);
            this.generateRiversCheckBox.TabIndex = 0;
            this.generateRiversCheckBox.Text = "Generate rivers";
            this.generateRiversCheckBox.UseVisualStyleBackColor = true;
            // 
            // logListBox
            // 
            this.logListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.logListBox.FormattingEnabled = true;
            this.logListBox.IntegralHeight = false;
            this.logListBox.Location = new System.Drawing.Point(12, 554);
            this.logListBox.Name = "logListBox";
            this.logListBox.ScrollAlwaysVisible = true;
            this.logListBox.Size = new System.Drawing.Size(409, 205);
            this.logListBox.TabIndex = 3;
            this.logListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.logListBox_DrawItem);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Preview";
            // 
            // drawMapBackgroundWorker
            // 
            this.drawMapBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.drawMapBackgroundWorker_DoWork);
            // 
            // riversColorColorDialog
            // 
            this.riversColorColorDialog.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 808);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "MapCreator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthTextBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector3TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector2TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZVector1TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapZScaleTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMaxTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightmapLightMinTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPreview)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBoundsOpacityTextBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapRiversOpacityTextBox)).EndInit();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox zonesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox mapPreview;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button renderButton;
        private System.Windows.Forms.ComboBox typesComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox expansionsComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox realmsComboBox;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.NumericUpDown heightTextBox;
        private System.Windows.Forms.NumericUpDown widthTextBox;
    }
}

