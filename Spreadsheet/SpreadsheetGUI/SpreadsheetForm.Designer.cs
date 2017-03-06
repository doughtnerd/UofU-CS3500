namespace SpreadsheetGUI
{
    partial class SpreadsheetForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellValueTextBox = new System.Windows.Forms.TextBox();
            this.cellValueLabel = new System.Windows.Forms.Label();
            this.cellNameTextBox = new System.Windows.Forms.TextBox();
            this.cellNameLabel = new System.Windows.Forms.Label();
            this.cellDetailsEditGroupBox = new System.Windows.Forms.GroupBox();
            this.cellContentsTextBox = new System.Windows.Forms.TextBox();
            this.spreadsheetPanel1 = new SSGui.SpreadsheetPanel();
            this.navigatingCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editingCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.cellDetailsEditGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(844, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 38);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigatingCellsToolStripMenuItem,
            this.editingCellsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(77, 38);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // cellValueTextBox
            // 
            this.cellValueTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellValueTextBox.Location = new System.Drawing.Point(502, 69);
            this.cellValueTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cellValueTextBox.Name = "cellValueTextBox";
            this.cellValueTextBox.ReadOnly = true;
            this.cellValueTextBox.Size = new System.Drawing.Size(304, 31);
            this.cellValueTextBox.TabIndex = 0;
            this.cellValueTextBox.TabStop = false;
            // 
            // cellValueLabel
            // 
            this.cellValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellValueLabel.AutoSize = true;
            this.cellValueLabel.Location = new System.Drawing.Point(384, 75);
            this.cellValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cellValueLabel.Name = "cellValueLabel";
            this.cellValueLabel.Size = new System.Drawing.Size(110, 25);
            this.cellValueLabel.TabIndex = 1;
            this.cellValueLabel.Text = "Cell Value";
            // 
            // cellNameTextBox
            // 
            this.cellNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellNameTextBox.Location = new System.Drawing.Point(502, 33);
            this.cellNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cellNameTextBox.Name = "cellNameTextBox";
            this.cellNameTextBox.ReadOnly = true;
            this.cellNameTextBox.Size = new System.Drawing.Size(304, 31);
            this.cellNameTextBox.TabIndex = 2;
            this.cellNameTextBox.TabStop = false;
            // 
            // cellNameLabel
            // 
            this.cellNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellNameLabel.AutoSize = true;
            this.cellNameLabel.Location = new System.Drawing.Point(384, 38);
            this.cellNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cellNameLabel.Name = "cellNameLabel";
            this.cellNameLabel.Size = new System.Drawing.Size(111, 25);
            this.cellNameLabel.TabIndex = 3;
            this.cellNameLabel.Text = "Cell Name";
            // 
            // cellDetailsEditGroupBox
            // 
            this.cellDetailsEditGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellDetailsEditGroupBox.Controls.Add(this.cellValueLabel);
            this.cellDetailsEditGroupBox.Controls.Add(this.cellNameLabel);
            this.cellDetailsEditGroupBox.Controls.Add(this.cellValueTextBox);
            this.cellDetailsEditGroupBox.Controls.Add(this.cellNameTextBox);
            this.cellDetailsEditGroupBox.Controls.Add(this.cellContentsTextBox);
            this.cellDetailsEditGroupBox.Location = new System.Drawing.Point(12, 44);
            this.cellDetailsEditGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cellDetailsEditGroupBox.Name = "cellDetailsEditGroupBox";
            this.cellDetailsEditGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cellDetailsEditGroupBox.Size = new System.Drawing.Size(832, 119);
            this.cellDetailsEditGroupBox.TabIndex = 3;
            this.cellDetailsEditGroupBox.TabStop = false;
            this.cellDetailsEditGroupBox.Text = "Cell Details";
            // 
            // cellContentsTextBox
            // 
            this.cellContentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellContentsTextBox.Location = new System.Drawing.Point(8, 31);
            this.cellContentsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cellContentsTextBox.Multiline = true;
            this.cellContentsTextBox.Name = "cellContentsTextBox";
            this.cellContentsTextBox.Size = new System.Drawing.Size(364, 83);
            this.cellContentsTextBox.TabIndex = 0;
            this.cellContentsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cellContentsTextBox_KeyDown);
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel1.Location = new System.Drawing.Point(12, 171);
            this.spreadsheetPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(832, 515);
            this.spreadsheetPanel1.TabIndex = 1;
            this.spreadsheetPanel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spreadsheet_KeyDown);
            // 
            // navigatingCellsToolStripMenuItem
            // 
            this.navigatingCellsToolStripMenuItem.Name = "navigatingCellsToolStripMenuItem";
            this.navigatingCellsToolStripMenuItem.Size = new System.Drawing.Size(288, 38);
            this.navigatingCellsToolStripMenuItem.Text = "Navigating Cells";
            this.navigatingCellsToolStripMenuItem.Click += new System.EventHandler(this.navigatingCellsToolStripMenuItem_Click);
            // 
            // editingCellsToolStripMenuItem
            // 
            this.editingCellsToolStripMenuItem.Name = "editingCellsToolStripMenuItem";
            this.editingCellsToolStripMenuItem.Size = new System.Drawing.Size(288, 38);
            this.editingCellsToolStripMenuItem.Text = "Editing Cells";
            this.editingCellsToolStripMenuItem.Click += new System.EventHandler(this.editingCellsToolStripMenuItem_Click);
            // 
            // SpreadsheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 687);
            this.Controls.Add(this.cellDetailsEditGroupBox);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(850, 696);
            this.Name = "SpreadsheetForm";
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadsheetForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cellDetailsEditGroupBox.ResumeLayout(false);
            this.cellDetailsEditGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private SSGui.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.Label cellNameLabel;
        private System.Windows.Forms.TextBox cellNameTextBox;
        private System.Windows.Forms.Label cellValueLabel;
        private System.Windows.Forms.TextBox cellValueTextBox;
        private System.Windows.Forms.GroupBox cellDetailsEditGroupBox;
        private System.Windows.Forms.TextBox cellContentsTextBox;
        private System.Windows.Forms.ToolStripMenuItem navigatingCellsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editingCellsToolStripMenuItem;
    }
}

