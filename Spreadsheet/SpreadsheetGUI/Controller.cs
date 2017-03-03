using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    class Controller
    {
        ISpreadsheetView view;

        public Controller(ISpreadsheetView view)
        {
            this.view = view;
        }
    }
}
