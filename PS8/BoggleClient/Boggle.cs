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
    public partial class Boggle : Form
    {
        public Boggle()
        {
            InitializeComponent();
        }

        private void register_Click(object sender, EventArgs e)
        {
            //RegisterEvent?.Invoke();
        }

        private void time_Click(object sender, EventArgs e)
        {
            //JoinGameEvent?.Invoke();
        }

        private void word_Click(object sender, EventArgs e)
        {
            //SubmitWordEvent?.Invoke();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HelpEvent?.Invoke();
        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ExitGameEvent?.Invoke();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ClosedEvent?.Invoke();
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
                //if (/*is registered*/)
                {
                    time.Enabled = true;
                }
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
                //if (/*is in active game*/)
                {
                    word.Enabled = true;
                }
            }
        }
    }
}
