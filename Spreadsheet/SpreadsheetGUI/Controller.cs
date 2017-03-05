using SS;
using System;
using System.Collections.Generic;
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

        public Controller(ISpreadsheetView view)
        {
            this.view = view;
            view.CloseEvent += HandleCloseEvent;
            ss = new Spreadsheet();
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
