using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class Boggle : Form, IBoggleView
    {
        public event Action<string> PlayWordEvent;
        public event Action<string, string> RegisterEvent;
        public event Action<long> JoinGameEvent;
        public event Action CancelJoinEvent;
        public event Action ExitGameEvent;

        public Boggle()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (this.registerButton.Text.Equals("Register"))
            {
                this.registerButton.Text = "Cancel";
                RegisterEvent?.Invoke(this.domainTextBox.Text, this.usernameTextbox.Text);
                this.registerButton.Text = "Register";
            }
            else
            {
                this.registerButton.Text = "Register";
            }
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            long result;
            if(long.TryParse(this.gameLengthEntry.Value.ToString(), out result))
            {
                if (this.joinGameButton.Text.Equals("Join Game"))
                {
                    this.joinGameButton.Text = "Cancel";
                    JoinGameEvent?.Invoke(result);
                }
                else
                {
                    this.joinGameButton.Text = "Join Game";
                    CancelJoinEvent?.Invoke();
                }
            }
            else
            {
                MessageBox.Show("Invalid duration!", "Game Duration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (usernameTextbox.TextLength == 0 || domainTextBox.TextLength == 0)
            {
                registerButton.Enabled = false;
            }
            else
            {
                registerButton.Enabled = true;
            }
        }

        private void ServerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (domainTextBox.TextLength == 0 || usernameTextbox.TextLength == 0)
            {
                registerButton.Enabled = false;
            }
            else
            {
                registerButton.Enabled = true;
            }
        }

        public void SetMainMenuEnabled(bool enabled)
        {
            this.mainMenu.Visible = enabled;
        }

        public void SetGameViewEnabled(bool enabled)
        {
            this.game.Enabled = enabled;
        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitGameEvent?.Invoke();
        }

        /// <summary>
        /// Sets the text of the domain field to the given string.
        /// </summary>
        /// <param name="s"></param>
        public void SetDomain(string s)
        {
            this.domainTextBox.Text = s;
        }

        /// <summary>
        /// This allows access to the view's gamelength entry textfield.
        /// Field can be set to enabled or disabled as necessary.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetJoinGameActive(bool enabled)
        {
            this.gameLengthEntry.Enabled = enabled;
            this.joinGameButton.Enabled = enabled;
        }

        /// <summary>
        /// Controls the domain, username, and register button.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetRegistrationActive(bool enabled)
        {
            this.domainTextBox.Enabled = enabled;
            this.usernameTextbox.Enabled = enabled;
            this.registerButton.Enabled = enabled;
        }

        public void SetGameBoard(string s)
        {
            int i = 0;
            this.boardLabel1.Text = s[0].ToString();
            this.boardLabel2.Text = s[1].ToString();
            this.boardLabel3.Text = s[2].ToString();
            this.boardLabel4.Text = s[3].ToString();
            this.boardLabel5.Text = s[4].ToString();
            this.boardLabel6.Text = s[5].ToString();
            this.boardLabel7.Text = s[6].ToString();
            this.boardLabel8.Text = s[7].ToString();
            this.boardLabel9.Text = s[8].ToString();
            this.boardLabel10.Text = s[9].ToString();
            this.boardLabel11.Text = s[10].ToString();
            this.boardLabel12.Text = s[11].ToString();
            this.boardLabel13.Text = s[12].ToString();
            this.boardLabel14.Text = s[13].ToString();
            this.boardLabel15.Text = s[14].ToString();
            this.boardLabel16.Text = s[15].ToString();
            gridPanel.Refresh();
        }

        private void wordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                TextBox box = sender as TextBox;
                PlayWordEvent?.Invoke(box.Text);
                e.SuppressKeyPress = true;
                box.Text = "";
            }
        }

        public void SetGameStatus(string s)
        {
            this.gameStatusLabel.Text = "Game Status: " + s;
        }

        public void SetTimeLeft(string s)
        {
            this.timeLeft.Text = s;
        }

        public void SetScores(int playerOne, int playerTwo)
        {
            this.player1Score.Text = playerOne.ToString();
            this.player2Score.Text = playerTwo.ToString();
        }

        public void SetWords(string[] playerOne, string[] playerTwo)
        {
            playerOneWords.Items.Clear();
            playerTwoWords.Items.Clear();
            playerOneWords.Items.AddRange(playerOne);
            playerTwoWords.Items.AddRange(playerTwo);
            playerOneWords.Refresh();
            playerTwoWords.Refresh();
        }

        public void SetJoinButtonText(string s)
        {
            this.joinGameButton.Text = s;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To register, enter a username and the server domain to use then press the register button.");
        }

        private void joiningAGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To join a game, enter a positive number in the duration field and press the join game button.");
        }

        private void playingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To play, enter a word into the Word field and press enter to submit.");
        }
    }
}
