namespace MapCreator
{
    partial class SelectMapsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMapsForm));
            this.mapsTreeView = new MWControls.MWTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectMapsCounterLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectMapsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.addAllButton = new System.Windows.Forms.Button();
            this.selectedMapsListBox = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.saveNewPresetButton = new System.Windows.Forms.Button();
            this.newPresetTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.savePresetButton = new System.Windows.Forms.Button();
            this.deletePresetButton = new System.Windows.Forms.Button();
            this.loadPresetButton = new System.Windows.Forms.Button();
            this.presetsComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapsTreeView
            // 
            this.mapsTreeView.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("mapsTreeView.CheckedNodes")));
            this.mapsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapsTreeView.HideSelection = false;
            this.mapsTreeView.Location = new System.Drawing.Point(3, 3);
            this.mapsTreeView.Name = "mapsTreeView";
            this.mapsTreeView.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("mapsTreeView.SelNodes")));
            this.mapsTreeView.Size = new System.Drawing.Size(281, 583);
            this.mapsTreeView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectMapsCounterLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.selectMapsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 651);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 49);
            this.panel1.TabIndex = 1;
            // 
            // selectMapsCounterLabel
            // 
            this.selectMapsCounterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectMapsCounterLabel.AutoSize = true;
            this.selectMapsCounterLabel.Location = new System.Drawing.Point(505, 19);
            this.selectMapsCounterLabel.Name = "selectMapsCounterLabel";
            this.selectMapsCounterLabel.Size = new System.Drawing.Size(13, 13);
            this.selectMapsCounterLabel.TabIndex = 2;
            this.selectMapsCounterLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selected maps:";
            // 
            // selectMapsButton
            // 
            this.selectMapsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectMapsButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectMapsButton.Location = new System.Drawing.Point(553, 14);
            this.selectMapsButton.Name = "selectMapsButton";
            this.selectMapsButton.Size = new System.Drawing.Size(90, 23);
            this.selectMapsButton.TabIndex = 0;
            this.selectMapsButton.Text = "OK";
            this.selectMapsButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.mapsTreeView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectedMapsListBox, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(655, 589);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.removeAllButton);
            this.panel2.Controls.Add(this.removeButton);
            this.panel2.Controls.Add(this.addButton);
            this.panel2.Controls.Add(this.addAllButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(290, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(74, 583);
            this.panel2.TabIndex = 1;
            // 
            // removeAllButton
            // 
            this.removeAllButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.removeAllButton.Location = new System.Drawing.Point(9, 322);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(57, 23);
            this.removeAllButton.TabIndex = 3;
            this.removeAllButton.Text = "<<";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.removeButton.Location = new System.Drawing.Point(9, 293);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(57, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "<";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addButton.Location = new System.Drawing.Point(9, 264);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(57, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = ">";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addAllButton
            // 
            this.addAllButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addAllButton.Location = new System.Drawing.Point(9, 235);
            this.addAllButton.Name = "addAllButton";
            this.addAllButton.Size = new System.Drawing.Size(57, 23);
            this.addAllButton.TabIndex = 0;
            this.addAllButton.Text = ">>";
            this.addAllButton.UseVisualStyleBackColor = true;
            this.addAllButton.Click += new System.EventHandler(this.addAllButton_Click);
            // 
            // selectedMapsListBox
            // 
            this.selectedMapsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedMapsListBox.FormattingEnabled = true;
            this.selectedMapsListBox.IntegralHeight = false;
            this.selectedMapsListBox.Location = new System.Drawing.Point(370, 3);
            this.selectedMapsListBox.Name = "selectedMapsListBox";
            this.selectedMapsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedMapsListBox.Size = new System.Drawing.Size(282, 583);
            this.selectedMapsListBox.TabIndex = 2;
            this.selectedMapsListBox.DataSourceChanged += new System.EventHandler(this.selectedMapsListBox_DataSourceChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.saveNewPresetButton);
            this.panel3.Controls.Add(this.newPresetTextBox);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.savePresetButton);
            this.panel3.Controls.Add(this.deletePresetButton);
            this.panel3.Controls.Add(this.loadPresetButton);
            this.panel3.Controls.Add(this.presetsComboBox);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 589);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(655, 62);
            this.panel3.TabIndex = 3;
            // 
            // saveNewPresetButton
            // 
            this.saveNewPresetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveNewPresetButton.Location = new System.Drawing.Point(589, 33);
            this.saveNewPresetButton.Name = "saveNewPresetButton";
            this.saveNewPresetButton.Size = new System.Drawing.Size(54, 23);
            this.saveNewPresetButton.TabIndex = 7;
            this.saveNewPresetButton.Text = "Save";
            this.saveNewPresetButton.UseVisualStyleBackColor = true;
            this.saveNewPresetButton.Click += new System.EventHandler(this.saveNewPresetButton_Click);
            // 
            // newPresetTextBox
            // 
            this.newPresetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newPresetTextBox.Location = new System.Drawing.Point(81, 35);
            this.newPresetTextBox.Name = "newPresetTextBox";
            this.newPresetTextBox.Size = new System.Drawing.Size(502, 20);
            this.newPresetTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "New preset";
            // 
            // savePresetButton
            // 
            this.savePresetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savePresetButton.Location = new System.Drawing.Point(533, 6);
            this.savePresetButton.Name = "savePresetButton";
            this.savePresetButton.Size = new System.Drawing.Size(50, 23);
            this.savePresetButton.TabIndex = 4;
            this.savePresetButton.Text = "Save";
            this.savePresetButton.UseVisualStyleBackColor = true;
            this.savePresetButton.Click += new System.EventHandler(this.savePresetButton_Click);
            // 
            // deletePresetButton
            // 
            this.deletePresetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePresetButton.Location = new System.Drawing.Point(589, 6);
            this.deletePresetButton.Name = "deletePresetButton";
            this.deletePresetButton.Size = new System.Drawing.Size(54, 23);
            this.deletePresetButton.TabIndex = 3;
            this.deletePresetButton.Text = "Delete";
            this.deletePresetButton.UseVisualStyleBackColor = true;
            this.deletePresetButton.Click += new System.EventHandler(this.deletePresetButton_Click);
            // 
            // loadPresetButton
            // 
            this.loadPresetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadPresetButton.Location = new System.Drawing.Point(477, 6);
            this.loadPresetButton.Name = "loadPresetButton";
            this.loadPresetButton.Size = new System.Drawing.Size(50, 23);
            this.loadPresetButton.TabIndex = 2;
            this.loadPresetButton.Text = "Load";
            this.loadPresetButton.UseVisualStyleBackColor = true;
            this.loadPresetButton.Click += new System.EventHandler(this.loadPresetButton_Click);
            // 
            // presetsComboBox
            // 
            this.presetsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.presetsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.presetsComboBox.FormattingEnabled = true;
            this.presetsComboBox.Location = new System.Drawing.Point(81, 8);
            this.presetsComboBox.Name = "presetsComboBox";
            this.presetsComboBox.Size = new System.Drawing.Size(390, 21);
            this.presetsComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Presets";
            // 
            // SelectMapsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectMapsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select maps";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MWControls.MWTreeView mapsTreeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button selectMapsButton;
        private System.Windows.Forms.Label selectMapsCounterLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button addAllButton;
        private System.Windows.Forms.ListBox selectedMapsListBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button deletePresetButton;
        private System.Windows.Forms.Button loadPresetButton;
        private System.Windows.Forms.ComboBox presetsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveNewPresetButton;
        private System.Windows.Forms.TextBox newPresetTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button savePresetButton;
    }
}