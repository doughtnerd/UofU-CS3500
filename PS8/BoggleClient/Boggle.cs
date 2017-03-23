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
        public Boggle()
        {
            InitializeComponent();
        }

        public event Action<string> PlayWordEvent;
        public event Action<string, string> RegisterEvent;
        public event Action<long> JoinGameEvent;
        public event Action CancelJoinEvent;

        private void register_Click(object sender, EventArgs e)
        {
            if (this.register.Text.Equals("Register"))
            {
                this.register.Text = "Cancel";
                RegisterEvent?.Invoke(this.serverTextBox.Text, this.usernameTextbox.Text);
                this.register.Text = "Register";
            }
            else
            {
                this.register.Text = "Register";
            }
        }

        private void time_Click(object sender, EventArgs e)
        {
            long result;
            if(long.TryParse(this.timeTextBox.Text, out result))
            {
                if (this.time.Text.Equals("Join Game"))
                {
                    this.time.Text = "Cancel";
                    JoinGameEvent?.Invoke(result);
                }
                else
                {
                    this.time.Text = "Join Game";
                    CancelJoinEvent?.Invoke();
                }
            }
            else
            {
                MessageBox.Show("Invalid duration!", "Game Duration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void word_Click(object sender, EventArgs e)
        {
            PlayWordEvent?.Invoke(this.wordTextBox.Text);
        }

        private void usernameTextbox_TextChanged(object sender, EventArgs e)
        {
            if (usernameTextbox.TextLength == 0 || serverTextBox.TextLength == 0)
            {
                register.Enabled = false;
            }
            else
            {
                register.Enabled = true;
            }
        }

        private void serverTextBox_TextChanged(object sender, EventArgs e)
        {
            if (serverTextBox.TextLength == 0 || usernameTextbox.TextLength == 0)
            {
                register.Enabled = false;
            }
            else
            {
                register.Enabled = true;
            }
        }

        private void timeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (timeTextBox.TextLength == 0)
            {
                time.Enabled = false;
            }
            else
            {
                //if (userToken != null)
                //{
                    time.Enabled = true;
                //}
            }
        }

        private void wordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (wordTextBox.TextLength == 0)
            {
                word.Enabled = false;
            }
            else
            {
                word.Enabled = true;
            }
        }

        public void BuildMainMenu()
        {
            this.mainMenu.Visible = true;
        }

        public void HideMainMenu()
        {
            this.mainMenu.Visible = false;
        }

        public void BuildGame()
        {
            this.game.Visible = true;
        }

        public void HideGame()
        {
            this.game.Visible = false;
        }

        public void BuildEndGame()
        {
            this.endGame.Visible = true;
        }

        public void HideEndGame()
        {
            this.endGame.Visible = false;
        }

        public void startGame()
        {

        }

        public void startGameData(dynamic data, int playerNumber)
        {
            this.boardLabel1 = data.Board.ElementAt(0);
            this.boardLabel2 = data.Board.ElementAt(1);
            this.boardLabel3 = data.Board.ElementAt(2);
            this.boardLabel4 = data.Board.ElementAt(3);
            this.boardLabel5 = data.Board.ElementAt(4);
            this.boardLabel6 = data.Board.ElementAt(5);
            this.boardLabel7 = data.Board.ElementAt(6);
            this.boardLabel8 = data.Board.ElementAt(7);
            this.boardLabel9 = data.Board.ElementAt(8);
            this.boardLabel10 = data.Board.ElementAt(9);
            this.boardLabel11 = data.Board.ElementAt(10);
            this.boardLabel12 = data.Board.ElementAt(11);
            this.boardLabel13 = data.Board.ElementAt(12);
            this.boardLabel14 = data.Board.ElementAt(13);
            this.boardLabel15 = data.Board.ElementAt(14);
            this.boardLabel16 = data.Board.ElementAt(15);
            this.timeLeft = data.TimeLimit;
            if (playerNumber == 1)
            {
                this.playerLabel = data.Player1.NickName;
                this.playerScore = data.Player1.Score;
                this.opponentLabel = data.PLayer2.NickName;
                this.opponentScore = data.Player2.Score;
            }
            else if (playerNumber == 2)
            {
                this.playerLabel = data.Player2.NickName;
                this.playerScore = data.Player2.Score;
                this.opponentLabel = data.PLayer1.NickName;
                this.opponentScore = data.Player1.Score;
            }
            this.game.Refresh();
        }

        public void updateGameData(dynamic data, int playerNumber)
        {
            this.timeLeft = data.TimeLeft;
            if (playerNumber == 1)
            {
                this.playerScore = data.Player1.Score;
                this.opponentScore = data.Player2.Score;
            }
            else if (playerNumber == 2)
            {
                this.playerScore = data.Player2.Score;
                this.opponentScore = data.Player1.Score;
            }
            this.game.Refresh();
        }

        public void endGameData(dynamic data, int playerNumber)
        {
            if (playerNumber == 1)
            {
                this.playerLabel = data.Player1.NickName;
                this.playerEnd = data.Player1.WordsPlayed;
                this.opponentLabel = data.PLayer2.NickName;
                this.opponentEnd = data.Player2.WordsPlayed;
            }
            else if (playerNumber == 2)
            {
                this.playerLabel = data.Player2.NickName;
                this.playerEnd = data.Player2.Wordsplayed;
                this.opponentLabel = data.PLayer1.NickName;
                this.opponentEnd = data.Player1.Wordsplayed;
            }
            this.endGame.Refresh();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game.");
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Main menu.");
        }

        private void helpToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("End game.");
        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideEndGame();
            BuildMainMenu();
        }
    }
}
