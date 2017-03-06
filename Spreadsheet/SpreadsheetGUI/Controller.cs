using Formulas;
using SS;
using System;
using System.IO;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    class Controller
    {
        /// <summary>
        /// The view this controller is currently managing and has hooked event handlers with.
        /// </summary>
        ISpreadsheetView view;

        /// <summary>
        /// The current data backing the view.
        /// </summary>
        Spreadsheet ss;

        /// <summary>
        /// TODO: Might not need given the FileSaveDialog handles file overwriting.
        /// File the Spreadsheet data was last saved to. May not need.
        /// </summary>
        FileInfo spreadsheetFile;

        /// <summary>
        /// Creates a new controller that is used to listen to the given view.
        /// </summary>
        /// <param name="view"></param>
        public Controller(ISpreadsheetView view):this(view, new Spreadsheet())
        {
        }

        /// <summary>
        /// Creates a controller object that uses the specified view and sheet.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="sheet"></param>
        public Controller(ISpreadsheetView view, Spreadsheet sheet)
        {
            this.view = view;
            ss = sheet;
            view.CloseEvent += HandleCloseEvent;
            view.SaveEvent += HandleSaveEvent;
            view.OpenEvent += HandleOpenEvent;
            view.CellSelectedEvent += HandleCellSelectedEvent;
            view.CellContentsChanged += HandleCellContentsChanged;
            view.HelpEvent += HandleHelpEvent;
        }

        void HandleHelpEvent(int index)
        {
            switch (index)
            {
                case 0:
                    MessageBox.Show("To navigate cells either:\r\nA) Left-click on the desired cell.\r\nB) While editing in the content edit box, use the arrow keys.");
                    break;
                case 1:
                    MessageBox.Show("To edit cell contents click in the text-box on the Cell Details panel and enter the desired contents. When finished press enter to finalize changes.");
                    break;
            }
        }

        // TODO: Really inefficient and clunky. Works but needs fixing.
        void HandleCellContentsChanged(string cellName, string contents)
        {
            try
            {
                ss.SetContentsOfCell(cellName, contents);
                object val = ss.GetCellValue(cellName);
                object cont = ss.GetCellContents(cellName);
                view.SetCellValueText(val.ToString());
                view.SetCellContentsText(cont is Formula ? "=" + cont.ToString() : cont.ToString());
                view.SetCellValue(cellName, val.ToString());
                foreach (string s in ss.GetNamesOfAllNonemptyCells())
                {
                    object cellVal = ss.GetCellValue(s);
                    if(cellVal is FormulaError)
                    {
                        view.SetCellValue(s, ((FormulaError)cellVal).Reason);
                    } else
                    {
                        view.SetCellValue(s, cellVal.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                if(e is CircularException)
                {
                    MessageBox.Show("A circular dependency has been detected.", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    MessageBox.Show(e.Message, "Error detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void HandleCellSelectedEvent(string name)
        {
            object cont = ss.GetCellContents(name);
            object val = ss.GetCellValue(name);
            view.SetCellContentsText(cont is Formula ? "=" + cont.ToString() : cont.ToString());
            view.SetCellNameText(name);
            view.SetCellValueText(val.ToString());
        }

        void HandleOpenEvent(FileInfo file)
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TODO: May not need to check spreadSheetFile since the FileSaveDialog handles file overwriting.
        void HandleSaveEvent(FileInfo file)
        {
            if (spreadsheetFile == null)
            {
                spreadsheetFile = file;
            } else
            {
                if (file.Exists && !file.FullName.Equals(spreadsheetFile.FullName))
                {
                    DialogResult r = MessageBox.Show("File " + file.Name + " is not the file that this spreadsheet was most recently saved to. Would you like to overwrite?", "Spreadsheet Saving", MessageBoxButtons.YesNo);
                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    spreadsheetFile = file;
                }
            }
            try
            {
                StreamWriter writer = new StreamWriter(file.FullName);
                ss.Save(writer);
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void HandleCloseEvent(FormClosingEventArgs args)
        {
            if (ss.Changed == true)
            {
                DialogResult r = MessageBox.Show("The spreadsheet has been changed. Closing now will result in the loss of data. Do you still wish to exit?", "Spreadsheet Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (r == DialogResult.No)
                {
                    args.Cancel = true;
                }
            }
        }
    }
}
