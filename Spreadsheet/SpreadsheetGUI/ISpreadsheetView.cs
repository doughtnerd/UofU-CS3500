using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    interface ISpreadsheetView
    {
        event Action<FileInfo> SaveEvent;
        event Action<FileInfo> OpenEvent;
        event Action CloseEvent;

        bool GetCellValue(string name, out string contents);

        bool SetCellValue(string name, string value);
    }
}
