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

        /*
         * NOTE: I was thinking about renaming some of the methods to have more
         * consistent naming accross the class. I can do this once you've fixed 
         * the summaries of the methods. I will be working on this too but we 
         * also need to make it so no overwrite warning is displayed if the user
         * is attempting to save to the spreadsheet's most recent save file.
         */

        /// <summary>
        /// New in the file menu was clicked.
        /// Creates a new spreadsheet in a new window.
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetGUIApplicationContext.GetContext().RunNew();
        }

        /// <summary>
        /// Exit in the file menu was clicked.
        /// Closes the current spreadsheet window.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open in the file menu was clicked.
        /// Creates a window allowing the user to search for a spreadsheet file to open.
        /// </summary>
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

        /// <summary>
        /// Save in the file menu was clicked.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Spreadsheet Files|*.ss|All Files|*.*";
            dialog.Title = "Save Spreadsheet to File";
            dialog.OverwritePrompt = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveEvent?.Invoke(new FileInfo(dialog.FileName));
            }
        }

        /// <summary>
        /// Current spreadsheet window is closing.
        /// </summary>
        private void SpreadsheetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseEvent?.Invoke(e);
        }

        /// <summary>
        /// Selected cell was changed.
        /// </summary>
        private void HandleSelectionChange(SpreadsheetPanel panel)
        {
            int x;
            int y;
            panel.GetSelection(out x, out y);
            string selectedCellName = SpreadsheetUtils.CellNameFromCoords(x + 1, y + 1);
            CellSelectedEvent?.Invoke(selectedCellName);
        }

        // TODO Read updated comment
        /// <summary>
        /// Event that's fired when a user presses a key while focus is on the cell contents edit textbox.
        /// It's what allows the user to press enter in order to change a cell and use the arrow keys to navigate.
        /// </summary>
        private void cellContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            HandleEnterKeyPress(sender, e);
            HandleArrowKeyPress(sender, e);
        }

        //TODO:Read updated comment
        /// <summary>
        /// Handles a key press event for the enter key when a key press event is detected in the cell contents edit box.
        /// </summary>
        /// <param name="sender">The element that was listening for the event.</param>
        /// <param name="e">The key events for the detected key</param>
        private void HandleEnterKeyPress(object sender, KeyEventArgs e)
        {
            //Checks that the enter key was the key pressed in the event.
            if (e.KeyCode == Keys.Enter)
            {
                int x;
                int y;
                
                //This gets the selected cell coordinates out of the view.
                spreadsheetPanel1.GetSelection(out x, out y);

                //Disables the enter key being used as a carriage return within the text box.
                e.SuppressKeyPress = true;

                //Turns the above coords into a usable cell name.
                string selectedCellName = SpreadsheetUtils.CellNameFromCoords(x + 1, y + 1);

                //Since the cell contents edit box is the 'sender' of the event, we want the text inside of it.
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    string contents = textBox.Text;

                    //Fire the CellContentsChanged event with the cell name and new contents.
                    //As long as the controller has an event hooked into the event, it will handle the rest.
                    CellContentsChanged?.Invoke(selectedCellName, contents);
                }
            }
        }

        //TODO:Read updated comment
        /// <summary>
        /// Enables using the arrow keys to navigate cells.
        /// </summary>
        private void HandleArrowKeyPress(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Shift && !e.Alt)
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
        }

        //TODO:Read updated comment
        /// <summary>
        /// Called by the HandleArrowKeyPress method and handles ensuring that the x & y values are valid for the view and if so, fires the cell selected event.
        /// As long as the controller has a handler hooked to the event it will be notified that it needs to update the contents box, the cell name box, and the cell value box.
        /// </summary>
        private void HandleArrowKeySelection(int x, int y)
        {
            if(spreadsheetPanel1.SetSelection(x, y))
            {
                CellSelectedEvent?.Invoke(SpreadsheetUtils.CellNameFromCoords(x + 1, y + 1));
            }
        }

        /// <summary>
        /// Navigating cells in help menu was clicked.
        /// </summary>
        private void navigatingCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleHelpMenuClick(sender);
        }

        /// <summary>
        /// Editing cells in help menu was clicked.
        /// </summary>
        private void editingCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleHelpMenuClick(sender);
        }

        /// <summary>
        /// Help menu item was clicked.
        /// </summary>
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
