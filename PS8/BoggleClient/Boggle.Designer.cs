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
            this.registerButton = new System.Windows.Forms.Button();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.domainTextBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.domainLabel = new System.Windows.Forms.Label();
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
            this.wordTextBox = new System.Windows.Forms.TextBox();
            this.wordLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.timeLeft = new System.Windows.Forms.Label();
            this.player2Label = new System.Windows.Forms.Label();
            this.player1Score = new System.Windows.Forms.Label();
            this.player2Score = new System.Windows.Forms.Label();
            this.playerOneWords = new System.Windows.Forms.ListBox();
            this.playerTwoWords = new System.Windows.Forms.ListBox();
            this.exitGame = new System.Windows.Forms.Button();
            this.game = new System.Windows.Forms.Panel();
            this.gameStatusLabel = new System.Windows.Forms.Label();
            this.player1Label = new System.Windows.Forms.Label();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.mainMenu = new System.Windows.Forms.Panel();
            this.gameLengthEntry = new System.Windows.Forms.NumericUpDown();
            this.setupBox = new System.Windows.Forms.GroupBox();
            this.gameBox = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joiningAGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.game.SuspendLayout();
            this.gridPanel.SuspendLayout();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameLengthEntry)).BeginInit();
            this.setupBox.SuspendLayout();
            this.gameBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // registerButton
            // 
            this.registerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.registerButton.Enabled = false;
            this.registerButton.Location = new System.Drawing.Point(254, 68);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(75, 23);
            this.registerButton.TabIndex = 2;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextbox.Location = new System.Drawing.Point(77, 14);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(252, 20);
            this.usernameTextbox.TabIndex = 0;
            this.usernameTextbox.TextChanged += new System.EventHandler(this.UsernameTextBox_TextChanged);
            // 
            // domainTextBox
            // 
            this.domainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.domainTextBox.Location = new System.Drawing.Point(77, 42);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.Size = new System.Drawing.Size(252, 20);
            this.domainTextBox.TabIndex = 1;
            this.domainTextBox.TextChanged += new System.EventHandler(this.ServerTextBox_TextChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(16, 17);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 120;
            this.usernameLabel.Text = "Username";
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Location = new System.Drawing.Point(28, 45);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(43, 13);
            this.domainLabel.TabIndex = 119;
            this.domainLabel.Text = "Domain";
            // 
            // boardLabel1
            // 
            this.boardLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel1.Location = new System.Drawing.Point(22, 8);
            this.boardLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel1.Name = "boardLabel1";
            this.boardLabel1.Size = new System.Drawing.Size(25, 25);
            this.boardLabel1.TabIndex = 118;
            this.boardLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel2
            // 
            this.boardLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel2.Location = new System.Drawing.Point(47, 8);
            this.boardLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel2.Name = "boardLabel2";
            this.boardLabel2.Size = new System.Drawing.Size(25, 25);
            this.boardLabel2.TabIndex = 117;
            this.boardLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel3
            // 
            this.boardLabel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel3.Location = new System.Drawing.Point(72, 8);
            this.boardLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel3.Name = "boardLabel3";
            this.boardLabel3.Size = new System.Drawing.Size(25, 25);
            this.boardLabel3.TabIndex = 116;
            this.boardLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel4
            // 
            this.boardLabel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel4.Location = new System.Drawing.Point(97, 8);
            this.boardLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel4.Name = "boardLabel4";
            this.boardLabel4.Size = new System.Drawing.Size(25, 25);
            this.boardLabel4.TabIndex = 115;
            this.boardLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel5
            // 
            this.boardLabel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel5.Location = new System.Drawing.Point(22, 33);
            this.boardLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel5.Name = "boardLabel5";
            this.boardLabel5.Size = new System.Drawing.Size(25, 25);
            this.boardLabel5.TabIndex = 114;
            this.boardLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel6
            // 
            this.boardLabel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel6.Location = new System.Drawing.Point(47, 33);
            this.boardLabel6.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel6.Name = "boardLabel6";
            this.boardLabel6.Size = new System.Drawing.Size(25, 25);
            this.boardLabel6.TabIndex = 113;
            this.boardLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel7
            // 
            this.boardLabel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel7.Location = new System.Drawing.Point(72, 33);
            this.boardLabel7.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel7.Name = "boardLabel7";
            this.boardLabel7.Size = new System.Drawing.Size(25, 25);
            this.boardLabel7.TabIndex = 112;
            this.boardLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel8
            // 
            this.boardLabel8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel8.Location = new System.Drawing.Point(97, 33);
            this.boardLabel8.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel8.Name = "boardLabel8";
            this.boardLabel8.Size = new System.Drawing.Size(25, 25);
            this.boardLabel8.TabIndex = 111;
            this.boardLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel9
            // 
            this.boardLabel9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel9.Location = new System.Drawing.Point(22, 58);
            this.boardLabel9.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel9.Name = "boardLabel9";
            this.boardLabel9.Size = new System.Drawing.Size(25, 25);
            this.boardLabel9.TabIndex = 110;
            this.boardLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel10
            // 
            this.boardLabel10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel10.Location = new System.Drawing.Point(47, 58);
            this.boardLabel10.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel10.Name = "boardLabel10";
            this.boardLabel10.Size = new System.Drawing.Size(25, 25);
            this.boardLabel10.TabIndex = 109;
            this.boardLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel11
            // 
            this.boardLabel11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel11.Location = new System.Drawing.Point(72, 58);
            this.boardLabel11.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel11.Name = "boardLabel11";
            this.boardLabel11.Size = new System.Drawing.Size(25, 25);
            this.boardLabel11.TabIndex = 108;
            this.boardLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel12
            // 
            this.boardLabel12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel12.Location = new System.Drawing.Point(97, 58);
            this.boardLabel12.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel12.Name = "boardLabel12";
            this.boardLabel12.Size = new System.Drawing.Size(25, 25);
            this.boardLabel12.TabIndex = 107;
            this.boardLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel13
            // 
            this.boardLabel13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel13.Location = new System.Drawing.Point(22, 83);
            this.boardLabel13.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel13.Name = "boardLabel13";
            this.boardLabel13.Size = new System.Drawing.Size(25, 25);
            this.boardLabel13.TabIndex = 106;
            this.boardLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel14
            // 
            this.boardLabel14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel14.Location = new System.Drawing.Point(47, 83);
            this.boardLabel14.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel14.Name = "boardLabel14";
            this.boardLabel14.Size = new System.Drawing.Size(25, 25);
            this.boardLabel14.TabIndex = 105;
            this.boardLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel15
            // 
            this.boardLabel15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel15.Location = new System.Drawing.Point(72, 83);
            this.boardLabel15.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel15.Name = "boardLabel15";
            this.boardLabel15.Size = new System.Drawing.Size(25, 25);
            this.boardLabel15.TabIndex = 104;
            this.boardLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardLabel16
            // 
            this.boardLabel16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardLabel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardLabel16.Location = new System.Drawing.Point(97, 83);
            this.boardLabel16.Margin = new System.Windows.Forms.Padding(0);
            this.boardLabel16.Name = "boardLabel16";
            this.boardLabel16.Size = new System.Drawing.Size(25, 25);
            this.boardLabel16.TabIndex = 103;
            this.boardLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wordTextBox
            // 
            this.wordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.wordTextBox.Location = new System.Drawing.Point(140, 204);
            this.wordTextBox.Name = "wordTextBox";
            this.wordTextBox.Size = new System.Drawing.Size(107, 20);
            this.wordTextBox.TabIndex = 5;
            this.wordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wordTextBox_KeyDown);
            // 
            // wordLabel
            // 
            this.wordLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(101, 207);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(33, 13);
            this.wordLabel.TabIndex = 102;
            this.wordLabel.Text = "Word";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(24, 99);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(47, 13);
            this.timeLabel.TabIndex = 101;
            this.timeLabel.Text = "Duration";
            // 
            // joinGameButton
            // 
            this.joinGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.joinGameButton.Enabled = false;
            this.joinGameButton.Location = new System.Drawing.Point(254, 123);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(75, 23);
            this.joinGameButton.TabIndex = 4;
            this.joinGameButton.Text = "Join Game";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
            // 
            // timeLeftLabel
            // 
            this.timeLeftLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.timeLeftLabel.Location = new System.Drawing.Point(104, 31);
            this.timeLeftLabel.Name = "timeLeftLabel";
            this.timeLeftLabel.Size = new System.Drawing.Size(143, 13);
            this.timeLeftLabel.TabIndex = 100;
            this.timeLeftLabel.Text = "Time Left";
            this.timeLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timeLeft
            // 
            this.timeLeft.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.timeLeft.Location = new System.Drawing.Point(104, 54);
            this.timeLeft.Name = "timeLeft";
            this.timeLeft.Size = new System.Drawing.Size(143, 13);
            this.timeLeft.TabIndex = 4;
            this.timeLeft.Text = "time";
            this.timeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player2Label
            // 
            this.player2Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.player2Label.Location = new System.Drawing.Point(261, 31);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(89, 13);
            this.player2Label.TabIndex = 2;
            this.player2Label.Text = "Player 2";
            this.player2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player1Score
            // 
            this.player1Score.Location = new System.Drawing.Point(3, 54);
            this.player1Score.Name = "player1Score";
            this.player1Score.Size = new System.Drawing.Size(89, 13);
            this.player1Score.TabIndex = 1;
            this.player1Score.Text = "score";
            this.player1Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player2Score
            // 
            this.player2Score.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.player2Score.Location = new System.Drawing.Point(261, 54);
            this.player2Score.Name = "player2Score";
            this.player2Score.Size = new System.Drawing.Size(89, 13);
            this.player2Score.TabIndex = 0;
            this.player2Score.Text = "score";
            this.player2Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerOneWords
            // 
            this.playerOneWords.FormattingEnabled = true;
            this.playerOneWords.Location = new System.Drawing.Point(3, 82);
            this.playerOneWords.Name = "playerOneWords";
            this.playerOneWords.Size = new System.Drawing.Size(89, 173);
            this.playerOneWords.TabIndex = 3;
            this.playerOneWords.TabStop = false;
            // 
            // playerTwoWords
            // 
            this.playerTwoWords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playerTwoWords.FormattingEnabled = true;
            this.playerTwoWords.Location = new System.Drawing.Point(262, 82);
            this.playerTwoWords.Name = "playerTwoWords";
            this.playerTwoWords.Size = new System.Drawing.Size(88, 173);
            this.playerTwoWords.TabIndex = 2;
            this.playerTwoWords.TabStop = false;
            // 
            // exitGame
            // 
            this.exitGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.exitGame.Location = new System.Drawing.Point(104, 230);
            this.exitGame.Name = "exitGame";
            this.exitGame.Size = new System.Drawing.Size(143, 23);
            this.exitGame.TabIndex = 122;
            this.exitGame.Text = "Exit Game";
            this.exitGame.UseVisualStyleBackColor = true;
            this.exitGame.Click += new System.EventHandler(this.ExitGameButton_Click);
            // 
            // game
            // 
            this.game.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.game.Controls.Add(this.gameStatusLabel);
            this.game.Controls.Add(this.player1Label);
            this.game.Controls.Add(this.exitGame);
            this.game.Controls.Add(this.playerOneWords);
            this.game.Controls.Add(this.gridPanel);
            this.game.Controls.Add(this.player2Label);
            this.game.Controls.Add(this.playerTwoWords);
            this.game.Controls.Add(this.timeLeftLabel);
            this.game.Controls.Add(this.timeLeft);
            this.game.Controls.Add(this.player2Score);
            this.game.Controls.Add(this.wordLabel);
            this.game.Controls.Add(this.player1Score);
            this.game.Controls.Add(this.wordTextBox);
            this.game.Enabled = false;
            this.game.Location = new System.Drawing.Point(6, 19);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(353, 265);
            this.game.TabIndex = 124;
            // 
            // gameStatusLabel
            // 
            this.gameStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameStatusLabel.Location = new System.Drawing.Point(0, 9);
            this.gameStatusLabel.Name = "gameStatusLabel";
            this.gameStatusLabel.Size = new System.Drawing.Size(353, 13);
            this.gameStatusLabel.TabIndex = 128;
            this.gameStatusLabel.Text = "Game Status: ";
            this.gameStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player1Label
            // 
            this.player1Label.Location = new System.Drawing.Point(3, 31);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(89, 13);
            this.player1Label.TabIndex = 127;
            this.player1Label.Text = "Player 1";
            this.player1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gridPanel
            // 
            this.gridPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gridPanel.Controls.Add(this.boardLabel1);
            this.gridPanel.Controls.Add(this.boardLabel9);
            this.gridPanel.Controls.Add(this.boardLabel10);
            this.gridPanel.Controls.Add(this.boardLabel8);
            this.gridPanel.Controls.Add(this.boardLabel11);
            this.gridPanel.Controls.Add(this.boardLabel7);
            this.gridPanel.Controls.Add(this.boardLabel12);
            this.gridPanel.Controls.Add(this.boardLabel6);
            this.gridPanel.Controls.Add(this.boardLabel13);
            this.gridPanel.Controls.Add(this.boardLabel5);
            this.gridPanel.Controls.Add(this.boardLabel14);
            this.gridPanel.Controls.Add(this.boardLabel4);
            this.gridPanel.Controls.Add(this.boardLabel2);
            this.gridPanel.Controls.Add(this.boardLabel15);
            this.gridPanel.Controls.Add(this.boardLabel16);
            this.gridPanel.Controls.Add(this.boardLabel3);
            this.gridPanel.Location = new System.Drawing.Point(104, 82);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(143, 116);
            this.gridPanel.TabIndex = 126;
            // 
            // mainMenu
            // 
            this.mainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainMenu.Controls.Add(this.gameLengthEntry);
            this.mainMenu.Controls.Add(this.usernameTextbox);
            this.mainMenu.Controls.Add(this.registerButton);
            this.mainMenu.Controls.Add(this.domainTextBox);
            this.mainMenu.Controls.Add(this.joinGameButton);
            this.mainMenu.Controls.Add(this.usernameLabel);
            this.mainMenu.Controls.Add(this.domainLabel);
            this.mainMenu.Controls.Add(this.timeLabel);
            this.mainMenu.Location = new System.Drawing.Point(6, 19);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(353, 149);
            this.mainMenu.TabIndex = 125;
            // 
            // gameLengthEntry
            // 
            this.gameLengthEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameLengthEntry.Location = new System.Drawing.Point(77, 97);
            this.gameLengthEntry.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.gameLengthEntry.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.gameLengthEntry.Name = "gameLengthEntry";
            this.gameLengthEntry.Size = new System.Drawing.Size(252, 20);
            this.gameLengthEntry.TabIndex = 121;
            this.gameLengthEntry.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // setupBox
            // 
            this.setupBox.Controls.Add(this.mainMenu);
            this.setupBox.Location = new System.Drawing.Point(0, 27);
            this.setupBox.Name = "setupBox";
            this.setupBox.Size = new System.Drawing.Size(365, 176);
            this.setupBox.TabIndex = 126;
            this.setupBox.TabStop = false;
            this.setupBox.Text = "Setup";
            // 
            // gameBox
            // 
            this.gameBox.Controls.Add(this.game);
            this.gameBox.Location = new System.Drawing.Point(0, 209);
            this.gameBox.Name = "gameBox";
            this.gameBox.Size = new System.Drawing.Size(365, 290);
            this.gameBox.TabIndex = 127;
            this.gameBox.TabStop = false;
            this.gameBox.Text = "Game";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(365, 24);
            this.menuStrip1.TabIndex = 128;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrationToolStripMenuItem,
            this.joiningAGameToolStripMenuItem,
            this.playingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // registrationToolStripMenuItem
            // 
            this.registrationToolStripMenuItem.Name = "registrationToolStripMenuItem";
            this.registrationToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.registrationToolStripMenuItem.Text = "Registration";
            this.registrationToolStripMenuItem.Click += new System.EventHandler(this.registrationToolStripMenuItem_Click);
            // 
            // joiningAGameToolStripMenuItem
            // 
            this.joiningAGameToolStripMenuItem.Name = "joiningAGameToolStripMenuItem";
            this.joiningAGameToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.joiningAGameToolStripMenuItem.Text = "Joining a Game";
            this.joiningAGameToolStripMenuItem.Click += new System.EventHandler(this.joiningAGameToolStripMenuItem_Click);
            // 
            // playingToolStripMenuItem
            // 
            this.playingToolStripMenuItem.Name = "playingToolStripMenuItem";
            this.playingToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.playingToolStripMenuItem.Text = "Playing";
            this.playingToolStripMenuItem.Click += new System.EventHandler(this.playingToolStripMenuItem_Click);
            // 
            // Boggle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 496);
            this.Controls.Add(this.gameBox);
            this.Controls.Add(this.setupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(381, 535);
            this.MinimumSize = new System.Drawing.Size(381, 535);
            this.Name = "Boggle";
            this.Text = "Boggle";
            this.game.ResumeLayout(false);
            this.game.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameLengthEntry)).EndInit();
            this.setupBox.ResumeLayout(false);
            this.gameBox.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.TextBox domainTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label domainLabel;
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
        private System.Windows.Forms.TextBox wordTextBox;
        private System.Windows.Forms.Label wordLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button joinGameButton;
        private System.Windows.Forms.Label timeLeftLabel;
        private System.Windows.Forms.Label timeLeft;
        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.Label player1Score;
        private System.Windows.Forms.Label player2Score;
        private System.Windows.Forms.ListBox playerOneWords;
        private System.Windows.Forms.ListBox playerTwoWords;
        private System.Windows.Forms.Button exitGame;
        private System.Windows.Forms.Panel game;
        private System.Windows.Forms.Panel mainMenu;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.NumericUpDown gameLengthEntry;
        private System.Windows.Forms.GroupBox setupBox;
        private System.Windows.Forms.GroupBox gameBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.Label gameStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joiningAGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playingToolStripMenuItem;
    }
}

