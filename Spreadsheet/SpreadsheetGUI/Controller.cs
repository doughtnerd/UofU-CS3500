using SS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    class Controller
    {
        ISpreadsheetView view;
        Spreadsheet ss;
        FileInfo spreadsheetFile;

        public Controller(ISpreadsheetView view)
        {
            this.view = view;
            ss = new Spreadsheet();
            view.CloseEvent += HandleCloseEvent;
            view.SaveEvent += HandleSaveEvent;
            view.OpenEvent += HandleOpenEvent;
        }

        public Controller(ISpreadsheetView view, Spreadsheet sheet)
        {
            this.view = view;
            ss = sheet;
            view.CloseEvent += HandleCloseEvent;
            view.SaveEvent += HandleSaveEvent;
            view.OpenEvent += HandleOpenEvent;
            view.CellSelectedEvent += HandleCellSelectedEvent;
        }

        public void HandleCellSelectedEvent(string name)
        {
            view.SetCellContentsText(ss.GetCellContents(name).ToString());
        }

        public void HandleOpenEvent(FileInfo file)
        {
            StreamReader read = new StreamReader(file.FullName);
            Spreadsheet sheet = new Spreadsheet(read, ss.IsValid);
            Controller c;
            SpreadsheetGUIApplicationContext.GetContext().RunNew(sheet, out c);
            foreach(string s in sheet.GetNamesOfAllNonemptyCells())
            {
                c.view.SetCellValue(s, sheet.GetCellValue(s).ToString());
            }
        }

        public void HandleSaveEvent(FileInfo file)
        {
            // If-else statement might be unnecessary since the FileSaveDialog handles checking for overwriting.
            if (spreadsheetFile == null)
            {
                spreadsheetFile = file;
            } else
            {
                if (file.Exists && !file.FullName.Equals(spreadsheetFile.FullName))
                {
                    DialogResult r = MessageBox.Show("File " + file.Name + " already exists. Would you like to overwrite?", "Spreadsheet Saving", MessageBoxButtons.YesNo);
                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    spreadsheetFile = file;
                }
            }
            StreamWriter writer = new StreamWriter(file.FullName);
            ss.Save(writer);
        }

        public void HandleCloseEvent(FormClosingEventArgs args)
        {
            if (ss.Changed == true)
            {
                DialogResult r = MessageBox.Show("The spreadsheet has been changed. Closing now will result in the loss of data. Do you still wish to exit?", "Spreadsheet Closing", MessageBoxButtons.YesNo);
                if (r == DialogResult.No)
                {
                    args.Cancel = true;
                }
            }
        }
    }
}
