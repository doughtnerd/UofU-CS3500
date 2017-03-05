using SSGui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Console.WriteLine("Cell: " + x + ", " + y + " ("+name+") set to " + value);
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
            this.cellNameTextBox.Text = selectedCellName;
            string val;
            if(panel.GetValue(x, y, out val))
            {
                this.cellValueTextBox.Text = val;
                CellSelectedEvent?.Invoke(selectedCellName);
            }
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
    }
}
