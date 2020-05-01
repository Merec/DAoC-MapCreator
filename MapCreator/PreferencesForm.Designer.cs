namespace MapCreator
{
    partial class PreferencesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.targetMapBrowseButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.targetMapPathTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gamePathBrowseButton = new System.Windows.Forms.Button();
            this.gamePathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gamePathFileBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.targetMapPathBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.targetMapBrowseButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.targetMapPathTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.gamePathBrowseButton);
            this.groupBox1.Controls.Add(this.gamePathTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paths";
            // 
            // targetMapBrowseButton
            // 
            this.targetMapBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetMapBrowseButton.Location = new System.Drawing.Point(537, 68);
            this.targetMapBrowseButton.Name = "targetMapBrowseButton";
            this.targetMapBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.targetMapBrowseButton.TabIndex = 7;
            this.targetMapBrowseButton.Text = "browse";
            this.targetMapBrowseButton.UseVisualStyleBackColor = true;
            this.targetMapBrowseButton.Click += new System.EventHandler(this.targetMapBrowseButton_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(98, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(433, 32);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select the base directory for created maps. This is altered with the dynamic dire" +
    "ctory pattern in the main window. Leave empty for the directory of the program.";
            // 
            // targetMapPathTextBox
            // 
            this.targetMapPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetMapPathTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MapCreator.Properties.Settings.Default, "targetMapPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.targetMapPathTextBox.Location = new System.Drawing.Point(101, 70);
            this.targetMapPathTextBox.Name = "targetMapPathTextBox";
            this.targetMapPathTextBox.Size = new System.Drawing.Size(430, 20);
            this.targetMapPathTextBox.TabIndex = 5;
            this.targetMapPathTextBox.Text = global::MapCreator.Properties.Settings.Default.targetMapPath;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target directory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select the main directory of your Dark Age of Camloet installation.";
            // 
            // gamePathBrowseButton
            // 
            this.gamePathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePathBrowseButton.Location = new System.Drawing.Point(537, 17);
            this.gamePathBrowseButton.Name = "gamePathBrowseButton";
            this.gamePathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.gamePathBrowseButton.TabIndex = 2;
            this.gamePathBrowseButton.Text = "browse";
            this.gamePathBrowseButton.UseVisualStyleBackColor = true;
            this.gamePathBrowseButton.Click += new System.EventHandler(this.gamePathBrowseButton_Click);
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePathTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MapCreator.Properties.Settings.Default, "game_path", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.gamePathTextBox.Location = new System.Drawing.Point(101, 19);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(430, 20);
            this.gamePathTextBox.TabIndex = 1;
            this.gamePathTextBox.Text = global::MapCreator.Properties.Settings.Default.game_path;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Client Path";
            // 
            // gamePathFileBrowser
            // 
            this.gamePathFileBrowser.Description = "Select the location of your DAoC installtion.";
            this.gamePathFileBrowser.ShowNewFolderButton = false;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(555, 146);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 146);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 181);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button gamePathBrowseButton;
        private System.Windows.Forms.TextBox gamePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog gamePathFileBrowser;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button targetMapBrowseButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox targetMapPathTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog targetMapPathBrowser;
    }
}