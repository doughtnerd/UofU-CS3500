using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public interface ISpreadsheetView
    {
        event Action<FileInfo> SaveEvent;
        event Action<FileInfo> OpenEvent;
        event Action<FormClosingEventArgs> CloseEvent;
        event Action<string> CellSelectedEvent;

        bool GetCellValue(string name, out string contents);

        bool SetCellValue(string name, string value);

        void SetCellContentsText(string s);
    }
}
