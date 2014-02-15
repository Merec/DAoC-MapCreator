namespace MapCreator
{
    partial class ShapedNifForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nifSize = new System.Windows.Forms.NumericUpDown();
            this.selectedFileTextBox = new System.Windows.Forms.TextBox();
            this.selectFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nifSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NIF File";
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(873, 10);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Create";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // selectFileDialog
            // 
            this.selectFileDialog.Filter = "NIF File|*.nif";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(936, 514);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Size";
            // 
            // nifSize
            // 
            this.nifSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nifSize.Location = new System.Drawing.Point(63, 38);
            this.nifSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nifSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nifSize.Name = "nifSize";
            this.nifSize.Size = new System.Drawing.Size(120, 20);
            this.nifSize.TabIndex = 5;
            this.nifSize.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // selectedFileTextBox
            // 
            this.selectedFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFileTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MapCreator.Properties.Settings.Default, "shapedNifLastFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.selectedFileTextBox.Location = new System.Drawing.Point(63, 12);
            this.selectedFileTextBox.Name = "selectedFileTextBox";
            this.selectedFileTextBox.Size = new System.Drawing.Size(770, 20);
            this.selectedFileTextBox.TabIndex = 1;
            this.selectedFileTextBox.Text = global::MapCreator.Properties.Settings.Default.shapedNifLastFile;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFileButton.Location = new System.Drawing.Point(839, 10);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(28, 23);
            this.selectFileButton.TabIndex = 6;
            this.selectFileButton.Text = "...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // ShapedNifForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 597);
            this.Controls.Add(this.selectFileButton);
            this.Controls.Add(this.nifSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.selectedFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "ShapedNifForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ShapedNifForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nifSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox selectedFileTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.OpenFileDialog selectFileDialog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nifSize;
        private System.Windows.Forms.Button selectFileButton;
    }
}