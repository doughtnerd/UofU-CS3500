using SSGui;
using System;
using System.IO;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class SpreadsheetForm : Form, ISpreadsheetView
    {
        public SpreadsheetForm()
        {
            InitializeComponent();
            spreadsheetPanel1.SelectionChanged += HandleCellSelected;

        }

        public event Action<FormClosingEventArgs> CloseEvent;
        public event Action<FileInfo> OpenEvent;
        public event Action<FileInfo> SaveEvent;
        public event Action<string> CellSelectedEvent;
        public event Action<string, string> CellContentsChanged;

        public bool GetCellValue(string name, out string value)
        {
            int x;
            int y;
            CellNameToCoords(name, out x, out y);
            if (x == 0 || y == 0)
            {
                value = "";
                return false;
            } else
            {
                spreadsheetPanel1.GetValue(x, y, out value);
                return true;
            }

        }

        public bool SetCellValue(string name, string value)
        {
            int x;
            int y;
            CellNameToCoords(name, out x, out y);
            //Console.WriteLine("Cell: " + x + ", " + y + " ("+name+") set to " + value);
            if(x==0 || y == 0)
            {
                return false;
            }
            spreadsheetPanel1.SetValue(x-1, y-1, value);
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetGUIApplicationContext.GetContext().RunNew();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Spreadsheet Files|*.ss|All Files|*.*";
            dialog.Title = "Select a Spreadsheet File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OpenEvent?.Invoke(new FileInfo(dialog.FileName));
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Spreadsheet Files|*.ss|All Files|*.*";
            dialog.Title = "Save Spreadsheet to File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveEvent?.Invoke(new FileInfo(dialog.FileName));
            }
        }

        private void SpreadsheetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseEvent?.Invoke(e);
        }

        private void HandleCellSelected(SpreadsheetPanel panel)
        {
            int x;
            int y;
            panel.GetSelection(out x, out y);
            string selectedCellName = CellNameFromCoords(x + 1, y + 1);
            CellSelectedEvent?.Invoke(selectedCellName);
        }

        private static void CellNameToCoords(string name, out int x, out int y)
        {
            x = 0;
            y = 0;
            name = name.ToUpper();
            for(int i = 0; i < name.Length; i++)
            {
                if (char.IsLetter(name[i]))
                {
                    x += (int)name[i] - 64;
                } else
                {
                    y = int.Parse(name.Substring(i, name.Length - 1));
                }
            }
        }

        /// <summary>
        /// Returns a spreadsheet cell name based off of the x and y values passed.
        /// </summary>
        private static string CellNameFromCoords(int x, int y)
        {
            int dividend = x;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName +y;
        }

        public void SetCellContentsText(string s)
        {
            this.cellContentsTextBox.Text = s;
        }

        private void cellContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int x;
                int y;
                spreadsheetPanel1.GetSelection(out x, out y);
                string selectedCellName = CellNameFromCoords(x + 1, y + 1);
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    string theText = textBox.Text;
                    CellContentsChanged?.Invoke(selectedCellName, theText);
                }
            }
        }

        public void SetCellValueText(string s)
        {
            this.cellValueTextBox.Text = s;
        }

        public void SetCellNameText(string s)
        {
            this.cellNameTextBox.Text = s;
        }

        private void spreadsheetPanel1_KeyDown(object sender, KeyEventArgs e)
        {
            int x;
            int y;
            spreadsheetPanel1.GetSelection(out x, out y);
            if(e.KeyCode == Keys.Down)
            {
                y += 1;
            }
            if(e.KeyCode == Keys.Right)
            {
                x += 1;
            }
            if(e.KeyCode == Keys.Left)
            {
                x -= 1;
            }
            if (e.KeyCode == Keys.Up)
            {
                y -= 1;
            }
            spreadsheetPanel1.SetSelection(x, y);
            //CellSelectedEvent?.Invoke(CellNameFromCoords(x+1,y+1));
        }
    }
}
