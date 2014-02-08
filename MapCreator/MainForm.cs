using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ImageMagick;

namespace MapCreator
{

    public partial class MainForm : Form
    {
        /// <summary>
        /// Self reference
        /// </summary>
        private static MainForm _self = null;

        /// <summary>
        /// The Zones to draw
        /// </summary>
        private List<ZoneSelection> m_selectedZones = new List<ZoneSelection>();

        /// <summary>
        /// The Zones to draw
        /// </summary>
        public List<ZoneSelection> SelectedZones
        {
            get { return m_selectedZones; }
            set { m_selectedZones = value; }
        }

        /// <summary>
        /// Current Game Expansion
        /// </summary>
        private GameExpansion m_expansion = GameExpansion.Classic;

        /// <summary>
        /// Current Game Expansion
        /// </summary>
        public GameExpansion Expansion
        {
            get { return m_expansion; }
            set { m_expansion = value; }
        }

        /// <summary>
        /// The target map size
        /// </summary>
        public int TargetMapSize
        {
            get {
                return Convert.ToInt32(this.widthTextBox.Value);
            }
            set {
                this.widthTextBox.Value = Convert.ToDecimal(value);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            // Language settings
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            InitializeComponent();
            _self = this;
            Initialize();

            // Load last selected zones
            if (!string.IsNullOrEmpty(Properties.Settings.Default.lastCreatedMaps))
            {
                foreach (string zoneId in Properties.Settings.Default.lastCreatedMaps.Split(','))
                {
                    try
                    {
                        SelectedZones.Add(DataWrapper.GetZoneSelectionByZoneId(zoneId));
                    }
                    catch { }
                }

                selectedMapsListBox.DataSource = null;
                selectedMapsListBox.DataSource = SelectedZones;
                selectedMapsCounterLabel.Text = SelectedZones.Count.ToString();
            }
        }

        public void Initialize()
        {
            // Set river color
            Color mapRiversColor = Properties.Settings.Default.mapRiverColor;
            mapRiversColorTextBox.Text = string.Format("{0}{1}{2}", mapRiversColor.R.ToString("X2"), mapRiversColor.G.ToString("X2"), mapRiversColor.B.ToString("X2"));

            Color mapBoundsColor = Properties.Settings.Default.mapBoundsColor;
            mapBoundsColorTextBox.Text = string.Format("{0}{1}{2}", mapBoundsColor.R.ToString("X2"), mapBoundsColor.G.ToString("X2"), mapBoundsColor.B.ToString("X2"));
        }

        /// <summary>
        /// Zone Selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectMapsButton_Click(object sender, EventArgs e)
        {
            SelectMapsForm form = new SelectMapsForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedZones = form.SelectedZones.ToList();
                selectedMapsListBox.DataSource = null;
                selectedMapsListBox.DataSource = SelectedZones;
                selectedMapsCounterLabel.Text = SelectedZones.Count.ToString();

                Properties.Settings.Default.lastCreatedMaps = string.Join(",", SelectedZones.Select(z => z.Id));
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Reset selected zones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selecetedMapsResetButton_Click(object sender, EventArgs e)
        {
            SelectedZones.Clear();
            selectedMapsListBox.DataSource = null;
            selectedMapsListBox.DataSource = SelectedZones;
            selectedMapsCounterLabel.Text = SelectedZones.Count.ToString();
        }

        #region Logging

        /// <summary>
        /// Log Levels
        /// </summary>
        public enum LogLevel
        {
            normal = 1,
            success = 2,
            notice = 3,
            warning = 4,
            error = 5
        }

        /// <summary>
        /// Logs somthing
        /// </summary>
        /// <param name="text"></param>
        /// <param name="logLevel"></param>
        public static void Log(string text, LogLevel logLevel = LogLevel.normal)
        {
            _self.LogText(text, logLevel);
        }

        /// <summary>
        /// LogLevels per line for drawing
        /// </summary>
        public List<LogLevel> logListBoxLogLevels = new List<LogLevel>();

        /// <summary>
        /// LogText delegate
        /// </summary>
        /// <param name="text"></param>
        /// <param name="logLevel"></param>
        private delegate void LogDelegate(string text, LogLevel logLevel);

        /// <summary>
        /// Adds text to the LogBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="logLevel"></param>
        public void LogText(string text, LogLevel logLevel = LogLevel.normal)
        {
            if (InvokeRequired)
            {
                this.Invoke(new LogDelegate(LogText), text, logLevel);
                return;
            }

            // Cut on 3000 rows
            if (logListBox.Items.Count == 3000)
            {
                logListBox.Items.Clear();
                logListBoxLogLevels.Clear();
            }

            logListBox.Items.Add(string.Format("{0}:{1}:{2}  {3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, text));
            logListBoxLogLevels.Add(logLevel);
            logListBox.SelectedIndex = logListBox.Items.Count - 1;
            logListBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Draws ListBox items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null || listBox.Items.Count == 0) return;

            e.DrawBackground();

            Brush newBrush;
            switch (logListBoxLogLevels[e.Index])
            {
                case LogLevel.success:
                    newBrush = Brushes.LimeGreen;
                    break;
                case LogLevel.notice:
                    newBrush = Brushes.CornflowerBlue;
                    break;
                case LogLevel.warning:
                    newBrush = Brushes.Orange;
                    break;
                case LogLevel.error:
                    newBrush = Brushes.Red;
                    break;
                default:
                    newBrush = Brushes.Black;
                    break;
            }

            e.Graphics.DrawString((listBox).Items[e.Index].ToString(), e.Font, newBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        #endregion

        #region StatusBar

        /// <summary>
        /// The delegate method for InitProgressBar to stay thread save
        /// </summary>
        /// <param name="label"></param>
        private delegate void InitProgressBarDelegate(string label);

        /// <summary>
        /// This method sets the Progressbar to zero and sets the label to a defined value
        /// Requires an Invoke check to stay thread save
        /// </summary>
        /// <param name="label"></param>
        private void InitProgressBar(string label)
        {
            if (!InvokeRequired)
            {
                statusLabel.Text = label;
                statusProgressBar.Style = ProgressBarStyle.Blocks;
                statusProgressBar.Value = 0;
            }
            else Invoke(new InitProgressBarDelegate(InitProgressBar), label);
        }

        /// <summary>
        /// The delegate method for InitProgressBar to stay thread save
        /// </summary>
        /// <param name="label"></param>
        private delegate void InitProgressBarMarqueeDelegate(string label);

        /// <summary>
        /// This method sets the Progressbar to a Marquee effect (we don't know long the action takes) and sets the label to a defined value
        /// Requires an Invoke check to stay thread save
        /// </summary>
        /// <param name="label"></param>
        private void InitProgressBarMarquee(string label)
        {
            if (!InvokeRequired)
            {
                statusLabel.Text = label;
                statusProgressBar.Style = ProgressBarStyle.Marquee;
            }
            else Invoke(new InitProgressBarMarqueeDelegate(InitProgressBarMarquee), label);
        }

        /// <summary>
        /// The delegate method for SetProgressBarValue to stay thread save
        /// </summary>
        /// <param name="value"></param>
        private delegate void SetProgressBarValueDelegate(int value);

        /// <summary>
        /// This method sets the status of the ProgressBar to the defined percentage value.
        /// Requires an Invoke check to stay thread save
        /// </summary>
        /// <param name="percentValue"></param>
        private void SetProgressBarValue(int percentValue)
        {
            if (!InvokeRequired)
            {
                statusProgressBar.Value = percentValue;
            }
            else Invoke(new SetProgressBarValueDelegate(SetProgressBarValue), percentValue);
        }

        /// <summary>
        /// The delegate method for ResetProgressBar to stay thread save
        /// </summary>
        private delegate void ResetProgressBarDelegate();

        /// <summary>
        /// This method resets the ProgressBar and sets the label to "Ready"
        /// Requires an Invoke check to stay thread save
        /// </summary>
        private void ResetProgressBar()
        {
            if (!InvokeRequired)
            {
                statusLabel.Text = "Ready";
                statusProgressBar.Style = ProgressBarStyle.Blocks;
                statusProgressBar.Value = 0;
            }
            else Invoke(new ResetProgressBarDelegate(ResetProgressBar));
        }

        public static void ProgressReset()
        {
            _self.ResetProgressBar();
        }

        public static void ProgressStartMarquee(string label)
        {
            _self.InitProgressBarMarquee(label);
        }

        public static void ProgressStart(string label)
        {
            _self.InitProgressBar(label);
        }

        public static void ProgressUpdate(int percent)
        {
            if (percent < 0) percent = 0;
            _self.SetProgressBarValue(percent);
        }

        #endregion

        #region UI Actions

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferencesForm form = new PreferencesForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Initialize();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private delegate void HandleRenderButtonDelegate(bool enabled);

        private void HandleRenderButton(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HandleRenderButtonDelegate(HandleRenderButton), enabled);
                return;
            }

            this.renderButton.Enabled = enabled;
        }

        #region River Color

        private void riverColorSelectButton_Click(object sender, EventArgs e)
        {
            if (riversColorColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mapRiversColorTextBox.Text = string.Format("{0}{1}{2}", riversColorColorDialog.Color.R.ToString("X2"), riversColorColorDialog.Color.G.ToString("X2"), riversColorColorDialog.Color.B.ToString("X2"));
                Properties.Settings.Default.mapRiverColor = riversColorColorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void riverUseColorDefault_CheckedChanged(object sender, EventArgs e)
        {
            mapRiversColorTextBox.Enabled = !riversUseDefaultColorCheckBox.Checked;
            riversColorSelectButton.Enabled = !riversUseDefaultColorCheckBox.Checked;
        }

        private void riversColorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mapRiversColorTextBox.Text.Length == 6)
            {
                try
                {
                    riversColorPreview.BackColor = ColorTranslator.FromHtml("#" + mapRiversColorTextBox.Text);
                    Properties.Settings.Default.mapRiverColor = ColorTranslator.FromHtml("#" + mapRiversColorTextBox.Text);
                    Properties.Settings.Default.Save();
                }
                catch { }
            }
        }

        private void riversColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            riversColorTextBox_TextChanged(null, null);
        }

        #endregion

        #region Bounds color

        private void boundsColorSelectButton_Click(object sender, EventArgs e)
        {
            boundsColorDialog.Color = Properties.Settings.Default.mapBoundsColor;
            if (boundsColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mapBoundsColorTextBox.Text = string.Format("{0}{1}{2}", boundsColorDialog.Color.R.ToString("X2"), boundsColorDialog.Color.G.ToString("X2"), boundsColorDialog.Color.B.ToString("X2"));
                Properties.Settings.Default.mapBoundsColor = boundsColorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void mapBoundsColorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mapRiversColorTextBox.Text.Length == 6)
            {
                try
                {
                    mapBoundsColorPreview.BackColor = ColorTranslator.FromHtml("#" + mapBoundsColorTextBox.Text);
                    Properties.Settings.Default.mapBoundsColor = ColorTranslator.FromHtml("#" + mapBoundsColorTextBox.Text);
                    Properties.Settings.Default.Save();
                }
                catch { }
            }
        }

        private void mapBoundsColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            mapBoundsColorTextBox_TextChanged(null, null);
        }

        #endregion

        #endregion

        /// <summary>
        /// Start rendering the zone(s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderButton_Click(object sender, EventArgs e)
        {
            if (MpkWrapper.CheckGamePath())
            {
                Log("Game found...", LogLevel.notice);

                if (SelectedZones.Count == 0)
                {
                    Log("Please select at least one Zone to render.", LogLevel.error);
                }
                else
                {
                    HandleRenderButton(false);

                    foreach (ZoneSelection zone in SelectedZones)
                    {
                        Log(string.Format("Rendering {0} ({1})...", zone.Name, zone.Id), LogLevel.notice);
                        drawMapBackgroundWorker.RunWorkerAsync(zone.Id);

                        while (drawMapBackgroundWorker.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }

                    HandleRenderButton(true);
                }
            }
        }

        /// <summary>
        /// Load Image Delegate
        /// </summary>
        /// <param name="filename"></param>
        private delegate void LoadImageDelegate(string filename);

        /// <summary>
        /// Loads an image to the preview
        /// </summary>
        /// <param name="filename"></param>
        private void LoadImage(string filename)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new LoadImageDelegate(LoadImage), filename);
                return;
            }

            mapPreview.ImageLocation = filename;
        }

        /// <summary>
        /// Render selected Zones as Background Worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawMapBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string ZoneId = (string)e.Argument;

            // Start BackgroundWorker
            Log(string.Format("Start creating map for zone {0} ...", ZoneId), LogLevel.notice);

            // The Target File
            FileInfo mapFile = new FileInfo(string.Format("{0}\\maps\\zone{1}_{2}.jpg", Application.StartupPath, ZoneId, TargetMapSize));
            if (!Directory.Exists(mapFile.DirectoryName))
            {
                Directory.CreateDirectory(mapFile.DirectoryName);
            }

            bool lightmap = generateLightmapCheckBox.Checked;
            double lightmapZScale = Convert.ToDouble(heightmapZScaleTextBox.Value);
            double lightmapLightMin = Convert.ToDouble(heightmapLightMinTextBox.Value);
            double lightmapLightMax = Convert.ToDouble(heightmapLightMaxTextBox.Value);
            double[] lightmapZVector = new double[] { Convert.ToDouble(heightmapZVector1TextBox.Value), Convert.ToDouble(heightmapZVector2TextBox.Value), Convert.ToDouble(heightmapZVector3TextBox.Value) };

            bool rivers = generateRiversCheckBox.Checked;
            bool riversUseDefaultColor = riversUseDefaultColorCheckBox.Checked;
            Color riversColor = Properties.Settings.Default.mapRiverColor;
            int riverOpacity = Convert.ToInt32(mapRiversOpacityTextBox.Value);

            bool bounds = generateBoundsCheckBox.Checked;
            Color boundsColor = Properties.Settings.Default.mapBoundsColor;
            int boundsOpacity = Convert.ToInt32(mapBoundsOpacityTextBox.Text);

            bool fixtures = drawFixturesCheckBox.Checked;

            // Generate the map
            using (ZoneConfiguration conf = new ZoneConfiguration(ZoneId, TargetMapSize))
            {
                // Create Background
                MapBackground background = new MapBackground(conf);

                using (ImageMagick.MagickImage map = background.Draw())
                {
                    if (map != null)
                    {
                        // Create lightmap
                        if (lightmap)
                        {
                            MapLightmap lightmapGenerator = new MapLightmap(conf);
                            lightmapGenerator.ZScale = lightmapZScale;
                            lightmapGenerator.LightMin = lightmapLightMin;
                            lightmapGenerator.LightMax = lightmapLightMax;
                            lightmapGenerator.ZVector = lightmapZVector;
                            lightmapGenerator.RecalculateLights();
                            lightmapGenerator.Draw(map);
                        }

                        // We need this for fixtures
                        MapRiver river = new MapRiver(conf);

                        MapFixtures fixturesGenerator = null;
                        if (fixtures)
                        {
                            fixturesGenerator = new MapFixtures(conf, river.Rivers);
                        }

                        // Draw Fixtures below water
                        if (fixtures)
                        {
                            fixturesGenerator.Draw(map, true);
                        }

                        // Create Rivers
                        if (rivers)
                        {
                            river.RiverColor = riversColor;
                            river.RiverOpacity = riverOpacity;
                            river.UseDefaultColors = riversUseDefaultColor;
                            river.Draw(map);
                        }

                        // Draw Fixtures above water
                        if (fixtures)
                        {
                            fixturesGenerator.Draw(map, false);
                        }

                        if (fixturesGenerator != null)
                        {
                            fixturesGenerator.Dispose();
                        }

                        // Create bounds
                        if (bounds)
                        {
                            MapBounds mapBounds = new MapBounds(conf);
                            mapBounds.BoundsColor = boundsColor;
                            mapBounds.BoundsOpacity = boundsOpacity;
                            mapBounds.Draw(map);
                        }

                        Log(string.Format("Writing map {0} ...", mapFile.Name));
                        map.Quality = 100;
                        map.Write(mapFile.FullName);
                    }
                }
            }

            if (mapFile.Exists)
            {
                Log("Finished without errors!", LogLevel.success);
                LoadImage(mapFile.FullName);
                ProgressReset();
            }
            else
            {
                Log("Errors during progress!", LogLevel.error);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Width < 800)
            {
                splitContainer1.SplitterDistance = flowLayoutPanel1.Width;
            }
        }

    }

}
