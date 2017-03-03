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

        public event Action CloseEvent;
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
    }
}
