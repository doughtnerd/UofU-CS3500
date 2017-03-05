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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(422, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // cellValueTextBox
            // 
            this.cellValueTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellValueTextBox.Location = new System.Drawing.Point(267, 36);
            this.cellValueTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.cellValueTextBox.Name = "cellValueTextBox";
            this.cellValueTextBox.ReadOnly = true;
            this.cellValueTextBox.Size = new System.Drawing.Size(138, 20);
            this.cellValueTextBox.TabIndex = 0;
            // 
            // cellValueLabel
            // 
            this.cellValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellValueLabel.AutoSize = true;
            this.cellValueLabel.Location = new System.Drawing.Point(209, 39);
            this.cellValueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cellValueLabel.Name = "cellValueLabel";
            this.cellValueLabel.Size = new System.Drawing.Size(54, 13);
            this.cellValueLabel.TabIndex = 1;
            this.cellValueLabel.Text = "Cell Value";
            // 
            // cellNameTextBox
            // 
            this.cellNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellNameTextBox.Location = new System.Drawing.Point(267, 17);
            this.cellNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.cellNameTextBox.Name = "cellNameTextBox";
            this.cellNameTextBox.ReadOnly = true;
            this.cellNameTextBox.Size = new System.Drawing.Size(138, 20);
            this.cellNameTextBox.TabIndex = 2;
            // 
            // cellNameLabel
            // 
            this.cellNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cellNameLabel.AutoSize = true;
            this.cellNameLabel.Location = new System.Drawing.Point(209, 20);
            this.cellNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cellNameLabel.Name = "cellNameLabel";
            this.cellNameLabel.Size = new System.Drawing.Size(55, 13);
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
            this.cellDetailsEditGroupBox.Location = new System.Drawing.Point(6, 23);
            this.cellDetailsEditGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.cellDetailsEditGroupBox.Name = "cellDetailsEditGroupBox";
            this.cellDetailsEditGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.cellDetailsEditGroupBox.Size = new System.Drawing.Size(416, 62);
            this.cellDetailsEditGroupBox.TabIndex = 3;
            this.cellDetailsEditGroupBox.TabStop = false;
            this.cellDetailsEditGroupBox.Text = "Cell Details";
            // 
            // cellContentsTextBox
            // 
            this.cellContentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellContentsTextBox.Location = new System.Drawing.Point(4, 16);
            this.cellContentsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.cellContentsTextBox.Multiline = true;
            this.cellContentsTextBox.Name = "cellContentsTextBox";
            this.cellContentsTextBox.Size = new System.Drawing.Size(201, 45);
            this.cellContentsTextBox.TabIndex = 0;
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel1.Location = new System.Drawing.Point(6, 89);
            this.spreadsheetPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(416, 268);
            this.spreadsheetPanel1.TabIndex = 1;
            // 
            // SpreadsheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 357);
            this.Controls.Add(this.cellDetailsEditGroupBox);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(438, 396);
            this.Name = "SpreadsheetForm";
            this.Text = "Spreadsheet";
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
    }
}

