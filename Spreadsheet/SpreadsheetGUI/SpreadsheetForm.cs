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
            spreadsheetPanel1.SelectionChanged += HandleSelectionChange;

        }

        public event Action<FormClosingEventArgs> CloseEvent;
        public event Action<FileInfo> OpenEvent;
        public event Action<FileInfo> SaveEvent;
        public event Action<string> CellSelectedEvent;
        public event Action<string, string> CellContentsChanged;

        /// <summary>
        /// TODO:Remove, currently unused.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the named cell to the given value.
        /// </summary>
        /// <param name="name">Name of the cell in the view to set.</param>
        /// <param name="value">Value the the cell should dispaly</param>
        /// <returns>True if the cell was successfully set on the view, false otherwise.</returns>
        public bool SetCellValue(string name, string value)
        {
            int x;
            int y;
            CellNameToCoords(name, out x, out y);
            if(x==0 || y == 0)
            {
                return false;
            }
            spreadsheetPanel1.SetValue(x-1, y-1, value);
            return true;
        }

        /// <summary>
        /// Sets the text of the cell contents display box to the given string.
        /// </summary>
        /// <param name="s">Value the contents box should contain.</param>
        public void SetCellContentsText(string s)
        {
            this.cellContentsTextBox.Text = s;
        }

        /// <summary>
        /// Sets the text of the cell value display box to the given string.
        /// </summary>
        /// <param name="s">The value the text box should contain.</param>
        public void SetCellValueText(string s)
        {
            this.cellValueTextBox.Text = s;
        }

        /// <summary>
        /// Sets the text of the cell name display box to the given string.
        /// </summary>
        /// <param name="s"></param>
        public void SetCellNameText(string s)
        {
            this.cellNameTextBox.Text = s;
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

        private void HandleSelectionChange(SpreadsheetPanel panel)
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

        private void cellContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            HandleContentKeyPress(sender, e);
            HandleArrowKeyPress(sender, e);
        }

        private void spreadsheet_KeyDown(object sender, KeyEventArgs e)
        {
            HandleArrowKeyPress(sender, e);
        }

        private void HandleContentKeyPress(object sender, KeyEventArgs e)
        {
            int x;
            int y;
            spreadsheetPanel1.GetSelection(out x, out y);
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string selectedCellName = CellNameFromCoords(x + 1, y + 1);
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    string theText = textBox.Text;
                    CellContentsChanged?.Invoke(selectedCellName, theText);
                }
            }
        }

        private void HandleArrowKeyPress(object sender, KeyEventArgs e)
        {
            int x;
            int y;
            spreadsheetPanel1.GetSelection(out x, out y);
            switch (e.KeyCode)
            {
                case Keys.Up:
                    y -= 1;
                    HandleArrowKeySelection(x, y);
                    break;
                case Keys.Right:
                    x += 1;
                    HandleArrowKeySelection(x, y);
                    break;
                case Keys.Down:
                    y += 1;
                    HandleArrowKeySelection(x, y);
                    break;
                case Keys.Left:
                    x -= 1;
                    HandleArrowKeySelection(x, y);
                    break;
            }
        }

        private void HandleArrowKeySelection(int x, int y)
        {
            if(spreadsheetPanel1.SetSelection(x, y))
            {
                CellSelectedEvent?.Invoke(CellNameFromCoords(x + 1, y + 1));
            }
        }
    }
}
