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
    public partial class ShapedNifForm : Form
    {
        public ShapedNifForm()
        {
            InitializeComponent();
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            string file = Properties.Settings.Default.shapedNifLastFile;
            if (!string.IsNullOrEmpty(selectedFileTextBox.Text))
            {
                file = selectedFileTextBox.Text;
            }

            selectFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(file);
            selectFileDialog.FileName = System.IO.Path.GetFileName(file);

            if (selectFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedFileTextBox.Text = selectFileDialog.FileName;
                selectFileDialog.FileName = selectFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }
    }
}
