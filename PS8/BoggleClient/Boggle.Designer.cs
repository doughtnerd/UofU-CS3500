namespace BoggleClient
{
    partial class Boggle
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
            this.register = new System.Windows.Forms.Button();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.boardLabel1 = new System.Windows.Forms.Label();
            this.boardLabel2 = new System.Windows.Forms.Label();
            this.boardLabel3 = new System.Windows.Forms.Label();
            this.boardLabel4 = new System.Windows.Forms.Label();
            this.boardLabel5 = new System.Windows.Forms.Label();
            this.boardLabel6 = new System.Windows.Forms.Label();
            this.boardLabel7 = new System.Windows.Forms.Label();
            this.boardLabel8 = new System.Windows.Forms.Label();
            this.boardLabel9 = new System.Windows.Forms.Label();
            this.boardLabel10 = new System.Windows.Forms.Label();
            this.boardLabel11 = new System.Windows.Forms.Label();
            this.boardLabel12 = new System.Windows.Forms.Label();
            this.boardLabel13 = new System.Windows.Forms.Label();
            this.boardLabel14 = new System.Windows.Forms.Label();
            this.boardLabel15 = new System.Windows.Forms.Label();
            this.boardLabel16 = new System.Windows.Forms.Label();
            this.word = new System.Windows.Forms.Button();
            this.wordTextBox = new System.Windows.Forms.TextBox();
            this.wordLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.time = new System.Windows.Forms.Button();
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.timeLeft = new System.Windows.Forms.Label();
            this.playerLabel = new System.Windows.Forms.Label();
            this.opponentLabel = new System.Windows.Forms.Label();
            this.playerScore = new System.Windows.Forms.Label();
            this.opponentScore = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerEnd = new System.Windows.Forms.ListBox();
            this.opponentEnd = new System.Windows.Forms.ListBox();
            this.playerLabel2 = new System.Windows.Forms.Label();
            this.opponentLabel2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // register
            // 
            this.register.Enabled = false;
            this.register.Location = new System.Drawing.Point(104, 127);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(75, 23);
            this.register.TabIndex = 2;
            this.register.Text = "Register";
            this.register.UseVisualStyleBackColor = true;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(104, 75);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(100, 20);
            this.usernameTextbox.TabIndex = 0;
            this.usernameTextbox.TextChanged += new System.EventHandler(this.usernameTextbox_TextChanged);
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(104, 101);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(100, 20);
            this.serverTextBox.TabIndex = 1;
            this.serverTextBox.TextChanged += new System.EventHandler(this.serverTextBox_TextChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(43, 78);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 120;
            this.usernameLabel.Text = "Username";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(60, 104);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(38, 13);
            this.serverLabel.TabIndex = 119;
            this.serverLabel.Text = "Server";
            // 
            // boardLabel1
            // 
            this.boardLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel1.Location = new System.Drawing.Point(281, 95);
            this.boardLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel1.Name = "boardLabel1";
            this.boardLabel1.Size = new System.Drawing.Size(25, 25);
            this.boardLabel1.TabIndex = 118;
            this.boardLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel2
            // 
            this.boardLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel2.Location = new System.Drawing.Point(306, 95);
            this.boardLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel2.Name = "boardLabel2";
            this.boardLabel2.Size = new System.Drawing.Size(25, 25);
            this.boardLabel2.TabIndex = 117;
            this.boardLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel3
            // 
            this.boardLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel3.Location = new System.Drawing.Point(331, 95);
            this.boardLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel3.Name = "boardLabel3";
            this.boardLabel3.Size = new System.Drawing.Size(25, 25);
            this.boardLabel3.TabIndex = 116;
            this.boardLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel4
            // 
            this.boardLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel4.Location = new System.Drawing.Point(356, 95);
            this.boardLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel4.Name = "boardLabel4";
            this.boardLabel4.Size = new System.Drawing.Size(25, 25);
            this.boardLabel4.TabIndex = 115;
            this.boardLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel5
            // 
            this.boardLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel5.Location = new System.Drawing.Point(281, 120);
            this.boardLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel5.Name = "boardLabel5";
            this.boardLabel5.Size = new System.Drawing.Size(25, 25);
            this.boardLabel5.TabIndex = 114;
            this.boardLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel6
            // 
            this.boardLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel6.Location = new System.Drawing.Point(306, 120);
            this.boardLabel6.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel6.Name = "boardLabel6";
            this.boardLabel6.Size = new System.Drawing.Size(25, 25);
            this.boardLabel6.TabIndex = 113;
            this.boardLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel7
            // 
            this.boardLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel7.Location = new System.Drawing.Point(331, 120);
            this.boardLabel7.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel7.Name = "boardLabel7";
            this.boardLabel7.Size = new System.Drawing.Size(25, 25);
            this.boardLabel7.TabIndex = 112;
            this.boardLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel8
            // 
            this.boardLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel8.Location = new System.Drawing.Point(356, 120);
            this.boardLabel8.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel8.Name = "boardLabel8";
            this.boardLabel8.Size = new System.Drawing.Size(25, 25);
            this.boardLabel8.TabIndex = 111;
            this.boardLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel9
            // 
            this.boardLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel9.Location = new System.Drawing.Point(281, 145);
            this.boardLabel9.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel9.Name = "boardLabel9";
            this.boardLabel9.Size = new System.Drawing.Size(25, 25);
            this.boardLabel9.TabIndex = 110;
            this.boardLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel10
            // 
            this.boardLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel10.Location = new System.Drawing.Point(306, 145);
            this.boardLabel10.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel10.Name = "boardLabel10";
            this.boardLabel10.Size = new System.Drawing.Size(25, 25);
            this.boardLabel10.TabIndex = 109;
            this.boardLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel11
            // 
            this.boardLabel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel11.Location = new System.Drawing.Point(331, 145);
            this.boardLabel11.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel11.Name = "boardLabel11";
            this.boardLabel11.Size = new System.Drawing.Size(25, 25);
            this.boardLabel11.TabIndex = 108;
            this.boardLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel12
            // 
            this.boardLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel12.Location = new System.Drawing.Point(356, 145);
            this.boardLabel12.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel12.Name = "boardLabel12";
            this.boardLabel12.Size = new System.Drawing.Size(25, 25);
            this.boardLabel12.TabIndex = 107;
            this.boardLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel13
            // 
            this.boardLabel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel13.Location = new System.Drawing.Point(281, 170);
            this.boardLabel13.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel13.Name = "boardLabel13";
            this.boardLabel13.Size = new System.Drawing.Size(25, 25);
            this.boardLabel13.TabIndex = 106;
            this.boardLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel14
            // 
            this.boardLabel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel14.Location = new System.Drawing.Point(306, 170);
            this.boardLabel14.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel14.Name = "boardLabel14";
            this.boardLabel14.Size = new System.Drawing.Size(25, 25);
            this.boardLabel14.TabIndex = 105;
            this.boardLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel15
            // 
            this.boardLabel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel15.Location = new System.Drawing.Point(331, 170);
            this.boardLabel15.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel15.Name = "boardLabel15";
            this.boardLabel15.Size = new System.Drawing.Size(25, 25);
            this.boardLabel15.TabIndex = 104;
            this.boardLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel16
            // 
            this.boardLabel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel16.Location = new System.Drawing.Point(356, 170);
            this.boardLabel16.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel16.Name = "boardLabel16";
            this.boardLabel16.Size = new System.Drawing.Size(25, 25);
            this.boardLabel16.TabIndex = 103;
            this.boardLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // word
            // 
            this.word.Enabled = false;
            this.word.Location = new System.Drawing.Point(290, 241);
            this.word.Name = "word";
            this.word.Size = new System.Drawing.Size(75, 23);
            this.word.TabIndex = 6;
            this.word.Text = "Submit";
            this.word.UseVisualStyleBackColor = true;
            this.word.Click += new System.EventHandler(this.word_Click);
            // 
            // wordTextBox
            // 
            this.wordTextBox.Location = new System.Drawing.Point(290, 217);
            this.wordTextBox.Name = "wordTextBox";
            this.wordTextBox.Size = new System.Drawing.Size(100, 20);
            this.wordTextBox.TabIndex = 5;
            this.wordTextBox.TextChanged += new System.EventHandler(this.wordTextBox_TextChanged);
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(251, 220);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(33, 13);
            this.wordLabel.TabIndex = 102;
            this.wordLabel.Text = "Word";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(20, 159);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(78, 13);
            this.timeLabel.TabIndex = 101;
            this.timeLabel.Text = "Game Duration";
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new System.Drawing.Point(104, 156);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeTextBox.TabIndex = 3;
            this.timeTextBox.TextChanged += new System.EventHandler(this.timeTextBox_TextChanged);
            // 
            // time
            // 
            this.time.Enabled = false;
            this.time.Location = new System.Drawing.Point(104, 182);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(75, 23);
            this.time.TabIndex = 4;
            this.time.Text = "Join Game";
            this.time.UseVisualStyleBackColor = true;
            this.time.Click += new System.EventHandler(this.time_Click);
            // 
            // timeLeftLabel
            // 
            this.timeLeftLabel.AutoSize = true;
            this.timeLeftLabel.Location = new System.Drawing.Point(306, 48);
            this.timeLeftLabel.Name = "timeLeftLabel";
            this.timeLeftLabel.Size = new System.Drawing.Size(51, 13);
            this.timeLeftLabel.TabIndex = 100;
            this.timeLeftLabel.Text = "Time Left";
            // 
            // timeLeft
            // 
            this.timeLeft.AutoSize = true;
            this.timeLeft.Location = new System.Drawing.Point(321, 68);
            this.timeLeft.Name = "timeLeft";
            this.timeLeft.Size = new System.Drawing.Size(26, 13);
            this.timeLeft.TabIndex = 4;
            this.timeLeft.Text = "time";
            // 
            // playerLabel
            // 
            this.playerLabel.AutoSize = true;
            this.playerLabel.Location = new System.Drawing.Point(264, 48);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(36, 13);
            this.playerLabel.TabIndex = 3;
            this.playerLabel.Text = "Player";
            // 
            // opponentLabel
            // 
            this.opponentLabel.AutoSize = true;
            this.opponentLabel.Location = new System.Drawing.Point(363, 48);
            this.opponentLabel.Name = "opponentLabel";
            this.opponentLabel.Size = new System.Drawing.Size(54, 13);
            this.opponentLabel.TabIndex = 2;
            this.opponentLabel.Text = "Opponent";
            // 
            // playerScore
            // 
            this.playerScore.AutoSize = true;
            this.playerScore.Location = new System.Drawing.Point(267, 68);
            this.playerScore.Name = "playerScore";
            this.playerScore.Size = new System.Drawing.Size(33, 13);
            this.playerScore.TabIndex = 1;
            this.playerScore.Text = "score";
            // 
            // opponentScore
            // 
            this.opponentScore.AutoSize = true;
            this.opponentScore.Location = new System.Drawing.Point(374, 68);
            this.opponentScore.Name = "opponentScore";
            this.opponentScore.Size = new System.Drawing.Size(33, 13);
            this.opponentScore.TabIndex = 0;
            this.opponentScore.Text = "score";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(687, 24);
            this.menuStrip1.TabIndex = 121;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.exitGameToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // exitGameToolStripMenuItem
            // 
            this.exitGameToolStripMenuItem.Name = "exitGameToolStripMenuItem";
            this.exitGameToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.exitGameToolStripMenuItem.Text = "Exit Game";
            this.exitGameToolStripMenuItem.Click += new System.EventHandler(this.exitGameToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // playerEnd
            // 
            this.playerEnd.FormattingEnabled = true;
            this.playerEnd.Location = new System.Drawing.Point(454, 58);
            this.playerEnd.Name = "playerEnd";
            this.playerEnd.Size = new System.Drawing.Size(88, 199);
            this.playerEnd.TabIndex = 3;
            this.playerEnd.TabStop = false;
            // 
            // opponentEnd
            // 
            this.opponentEnd.FormattingEnabled = true;
            this.opponentEnd.Location = new System.Drawing.Point(563, 58);
            this.opponentEnd.Name = "opponentEnd";
            this.opponentEnd.Size = new System.Drawing.Size(88, 199);
            this.opponentEnd.TabIndex = 2;
            this.opponentEnd.TabStop = false;
            // 
            // playerLabel2
            // 
            this.playerLabel2.AutoSize = true;
            this.playerLabel2.Location = new System.Drawing.Point(477, 37);
            this.playerLabel2.Name = "playerLabel2";
            this.playerLabel2.Size = new System.Drawing.Size(36, 13);
            this.playerLabel2.TabIndex = 1;
            this.playerLabel2.Text = "Player";
            // 
            // opponentLabel2
            // 
            this.opponentLabel2.AutoSize = true;
            this.opponentLabel2.Location = new System.Drawing.Point(576, 37);
            this.opponentLabel2.Name = "opponentLabel2";
            this.opponentLabel2.Size = new System.Drawing.Size(54, 13);
            this.opponentLabel2.TabIndex = 0;
            this.opponentLabel2.Text = "Opponent";
            // 
            // Boggle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 277);
            this.Controls.Add(this.opponentLabel2);
            this.Controls.Add(this.playerLabel2);
            this.Controls.Add(this.opponentEnd);
            this.Controls.Add(this.playerEnd);
            this.Controls.Add(this.opponentScore);
            this.Controls.Add(this.playerScore);
            this.Controls.Add(this.opponentLabel);
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.timeLeft);
            this.Controls.Add(this.timeLeftLabel);
            this.Controls.Add(this.time);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.wordLabel);
            this.Controls.Add(this.wordTextBox);
            this.Controls.Add(this.word);
            this.Controls.Add(this.boardLabel16);
            this.Controls.Add(this.boardLabel15);
            this.Controls.Add(this.boardLabel14);
            this.Controls.Add(this.boardLabel13);
            this.Controls.Add(this.boardLabel12);
            this.Controls.Add(this.boardLabel11);
            this.Controls.Add(this.boardLabel10);
            this.Controls.Add(this.boardLabel9);
            this.Controls.Add(this.boardLabel8);
            this.Controls.Add(this.boardLabel7);
            this.Controls.Add(this.boardLabel6);
            this.Controls.Add(this.boardLabel5);
            this.Controls.Add(this.boardLabel4);
            this.Controls.Add(this.boardLabel3);
            this.Controls.Add(this.boardLabel2);
            this.Controls.Add(this.boardLabel1);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.serverTextBox);
            this.Controls.Add(this.usernameTextbox);
            this.Controls.Add(this.register);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Boggle";
            this.Text = "Boggle";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button register;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.Label boardLabel1;
        private System.Windows.Forms.Label boardLabel2;
        private System.Windows.Forms.Label boardLabel3;
        private System.Windows.Forms.Label boardLabel4;
        private System.Windows.Forms.Label boardLabel5;
        private System.Windows.Forms.Label boardLabel6;
        private System.Windows.Forms.Label boardLabel7;
        private System.Windows.Forms.Label boardLabel8;
        private System.Windows.Forms.Label boardLabel9;
        private System.Windows.Forms.Label boardLabel10;
        private System.Windows.Forms.Label boardLabel11;
        private System.Windows.Forms.Label boardLabel12;
        private System.Windows.Forms.Label boardLabel13;
        private System.Windows.Forms.Label boardLabel14;
        private System.Windows.Forms.Label boardLabel15;
        private System.Windows.Forms.Label boardLabel16;
        private System.Windows.Forms.Button word;
        private System.Windows.Forms.TextBox wordTextBox;
        private System.Windows.Forms.Label wordLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.Button time;
        private System.Windows.Forms.Label timeLeftLabel;
        private System.Windows.Forms.Label timeLeft;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label opponentLabel;
        private System.Windows.Forms.Label playerScore;
        private System.Windows.Forms.Label opponentScore;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ListBox playerEnd;
        private System.Windows.Forms.ListBox opponentEnd;
        private System.Windows.Forms.Label playerLabel2;
        private System.Windows.Forms.Label opponentLabel2;
    }
}

