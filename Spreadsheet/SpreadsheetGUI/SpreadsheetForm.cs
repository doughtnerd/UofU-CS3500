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
        }

        public event Action<FormClosingEventArgs> CloseEvent;
        public event Action<FileInfo> OpenEvent;
        public event Action<FileInfo> SaveEvent;

        public bool GetCellValue(string name, out string contents)
        {
            throw new NotImplementedException();
        }

        public bool SetCellValue(string name, string value)
        {
            throw new NotImplementedException();
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
    }
}
