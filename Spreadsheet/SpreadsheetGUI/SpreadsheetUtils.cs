using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    public static class SpreadsheetUtils
    {
        /// <summary>
        /// Takes a cell name and returns 1 indexed coordinates for the given name.
        /// </summary>
        public static void CellNameToCoords(string name, out int x, out int y)
        {
            x = 0;
            y = 0;
            name = name.ToUpper();
            int i = 0;
            while (char.IsLetter(name[i]))
            {
                x += i==0 ? ((int)name[i] - 64) : ((int)name[i] - 64) * (26*i);
                i++;
            }
            y = int.Parse(name.Substring(i, name.Length - i));
        }

        /// <summary>
        /// Returns a spreadsheet cell name based off of the x and y values passed.
        /// x and y values are to be 1-based.
        /// Ex. x = 1 = a, x = 2 = b, x = 26 = z, etc.
        /// Ex. x = 26 & y=22 = Z22.
        /// </summary>
        public static string CellNameFromCoords(int x, int y)
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

            return columnName + y;
        }
    }
}
