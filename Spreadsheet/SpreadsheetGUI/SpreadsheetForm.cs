using SSGui;
using System;
using System.IO;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class SpreadsheetForm : Form, ISpreadsheetView
    {
        /// <summary>
        /// Creates a new SpreadsheetForm.
        /// </summary>
        public SpreadsheetForm()
        {
            InitializeComponent();
            spreadsheetPanel1.SelectionChanged += HandleSelectionChange;
        }

        /// <summary>
        /// Notifies subscribers that a close event has been fired and passes the event args.
        /// </summary>
        public event Action<FormClosingEventArgs> CloseEvent;

        /// <summary>
        /// Notifies subscribers of an open event and passes the selected file to open.
        /// </summary>
        public event Action<FileInfo> OpenEvent;

        /// <summary>
        /// Notifies subscribers a save event has been fired and passes the selected save file.
        /// </summary>
        public event Action<FileInfo> SaveEvent;

        /// <summary>
        /// Notifies subscribers that a cell has been selected and passes the name of the selected cell.
        /// </summary>
        public event Action<string> CellSelectedEvent;

        /// <summary>
        /// Notifies subscribers that a cell's contents has been changed and passes the cell name and the contents entered.
        /// </summary>
        public event Action<string, string> CellContentsChanged;

        /// <summary>
        /// Notifies subscribers that a help menu item has been selected and passes the index of the menu item selected.
        /// </summary>
        public event Action<int> HelpEvent;

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
            SpreadsheetUtils.CellNameToCoords(name, out x, out y);
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
            string selectedCellName = SpreadsheetUtils.CellNameFromCoords(x + 1, y + 1);
            CellSelectedEvent?.Invoke(selectedCellName);
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
                string selectedCellName = SpreadsheetUtils.CellNameFromCoords(x + 1, y + 1);
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
            if (!e.Control && !e.Shift && !e.Alt)
            {
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
        }

        private void HandleArrowKeySelection(int x, int y)
        {
            if(spreadsheetPanel1.SetSelection(x, y))
            {
                CellSelectedEvent?.Invoke(SpreadsheetUtils.CellNameFromCoords(x + 1, y + 1));
            }
        }

        private void navigatingCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleHelpMenuClick(sender);
        }

        private void editingCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleHelpMenuClick(sender);
        }

        private void HandleHelpMenuClick(object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                HelpEvent?.Invoke(index);
            }
        }
    }
}
