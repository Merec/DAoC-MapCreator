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
        /// Current Zone ID
        /// </summary>
        private string m_zoneId = "084";

        /// <summary>
        /// Current Zone ID
        /// </summary>
        public string ZoneId
        {
            get { return m_zoneId; }
            set { m_zoneId = value; }
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
            InitializeComponent();
            _self = this;
            Initialize();

            MagickImage test = new MagickImage();
            test.Dispose();
        }

        public void Initialize()
        {
            List<string> realms = DataWrapper.GetRealms();
            realmsComboBox.DataSource = new BindingSource(realms, null);

            // Set river color
            Color mapRiversColor = Properties.Settings.Default.mapRiverColor;
            mapRiversColorTextBox.Text = string.Format("{0}{1}{2}", mapRiversColor.R.ToString("X2"), mapRiversColor.G.ToString("X2"), mapRiversColor.B.ToString("X2"));

            Color mapBoundsColor = Properties.Settings.Default.mapBoundsColor;
            mapBoundsColorTextBox.Text = string.Format("{0}{1}{2}", mapBoundsColor.R.ToString("X2"), mapBoundsColor.G.ToString("X2"), mapBoundsColor.B.ToString("X2"));

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

        private delegate void SetZoneListDelegate(Dictionary<int, string> zones);

        /// <summary>
        /// Add Zones to ComboBox
        /// </summary>
        /// <param name="zones"></param>
        private void SetZoneList(Dictionary<int, string> zones)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetZoneListDelegate(SetZoneList), zones);
                return;
            }

            zonesComboBox.Items.Clear();
            try
            {
                zonesComboBox.DataSource = new BindingSource(zones, null);
                zonesComboBox.ValueMember = "Key";
                zonesComboBox.DisplayMember = "Value";
            }
            catch
            {
                zonesComboBox.DataSource = null;
                zonesComboBox.Items.Add("empty");
            } 
        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            if (MpkWrapper.CheckGamePath())
            {
                renderButton.Enabled = false;
                drawMapBackgroundWorker.RunWorkerAsync();
            }
        }

        private delegate void LoadImageDelegate(string filename);

        private void LoadImage(string filename)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new LoadImageDelegate(LoadImage), filename);
                return;
            }

            mapPreview.ImageLocation = filename;
        }

        private void realmsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reset in reverse order
            zonesComboBox.DataSource = null;
            typesComboBox.DataSource = null;

            BindingSource source = new BindingSource(
                DataWrapper.GetExpansionsByRealm((string)realmsComboBox.SelectedValue),
                null
            );
            expansionsComboBox.DataSource = source;
        }

        private void expansionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reset in reverse order
            zonesComboBox.DataSource = null;

            BindingSource source = new BindingSource(
                DataWrapper.GetZoneTypesByRealmAndExpansion((string)realmsComboBox.SelectedValue, (string)expansionsComboBox.SelectedValue),
                null
            );
            typesComboBox.DataSource = source;

            // Set the current expansion
            string selectedValue = ((string)expansionsComboBox.SelectedItem).ToLower().Replace(" ", string.Empty);
            foreach (string expansion in Enum.GetNames(typeof(GameExpansion)))
            {
                if (selectedValue == expansion.ToLower())
                {
                    Expansion = (GameExpansion)Enum.Parse(typeof(GameExpansion), expansion, true);
                }
            }

        }

        private void typesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typesComboBox.SelectedValue == null)
            {
                zonesComboBox.DataSource = null;
            }

            Dictionary<string, string> zones = DataWrapper.GetZonesByRealmAndExpansionAndType(
                (string)realmsComboBox.SelectedValue,
                (string)expansionsComboBox.SelectedValue, 
                (string)typesComboBox.SelectedValue
            );

            zonesComboBox.DataSource = new BindingSource(zones, null);
            zonesComboBox.ValueMember = "Key";
            zonesComboBox.DisplayMember = "Value";
        }

        private void zonesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zonesComboBox.SelectedValue != null && zonesComboBox.SelectedValue is string)
            {
                ZoneId = (string)zonesComboBox.SelectedValue;
            }
        }

        private void drawMapBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // The Target File
            FileInfo mapFile = new FileInfo(string.Format("{0}\\maps\\zone{1}.jpg", Application.StartupPath, ZoneId));
            if (!Directory.Exists(mapFile.DirectoryName))
            {
                Directory.CreateDirectory(mapFile.DirectoryName);
            }

            Log(string.Format("Start creating map for zone {0} ...", ZoneId), LogLevel.notice);

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

                        // Create Rivers
                        if (rivers)
                        {
                            MapRiver river = new MapRiver(conf);
                            river.RiverColor = riversColor;
                            river.RiverOpacity = riverOpacity;
                            river.UseDefaultColors = riversUseDefaultColor;
                            river.Draw(map);
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

            HandleRenderButton(true);
        }

    }
}
