using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapCreator
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void gamePathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void gamePathBrowseButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gamePathTextBox.Text))
            {
                gamePathFileBrowser.SelectedPath = gamePathTextBox.Text;
            }

            if (gamePathFileBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gamePathTextBox.Text = gamePathFileBrowser.SelectedPath;
            }
        }

    }
}
