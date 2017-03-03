using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    class SpreadsheetGUIApplicationContext : ApplicationContext
    {
        private int windowCount = 0;

        private static SpreadsheetGUIApplicationContext context;

        private SpreadsheetGUIApplicationContext() { }


        public static SpreadsheetGUIApplicationContext GetContext()
        {
            if (context == null)
            {
                context = new SpreadsheetGUIApplicationContext();
            }
            return context;
        }

        public void RunNew()
        {
            SpreadsheetForm form = new SpreadsheetForm();
            new Controller(form);

            windowCount++;

            form.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            form.Show();
        }
    }
}
