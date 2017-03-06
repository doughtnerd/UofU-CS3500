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
        /// <summary>
        /// Notifies subscribers a save event has been fired and passes the selected save file.
        /// </summary>
        event Action<FileInfo> SaveEvent;

        /// <summary>
        /// Notifies subscribers of an open event and passes the selected file to open.
        /// </summary>
        event Action<FileInfo> OpenEvent;

        /// <summary>
        /// Notifies subscribers that a close event has been fired and passes the event args.
        /// </summary>
        event Action<FormClosingEventArgs> CloseEvent;

        /// <summary>
        /// Notifies subscribers that a cell has been selected and passes the name of the selected cell.
        /// </summary>
        event Action<string> CellSelectedEvent;

        /// <summary>
        /// Notifies subscribers that a cell's contents has been changed and passes the cell name and the contents entered.
        /// </summary>
        event Action<string, string> CellContentsChanged;

        /// <summary>
        /// Notifies subscribers that a help menu item has been selected and passes the index of the menu item selected.
        /// </summary>
        event Action<int> HelpEvent;

        /// <summary>
        /// Sets the named cell to the given value.
        /// </summary>
        /// <param name="name">Name of the cell in the view to set.</param>
        /// <param name="value">Value the the cell should dispaly</param>
        /// <returns>True if the cell was successfully set on the view, false otherwise.</returns>
        bool SetCellValue(string name, string value);

        /// <summary>
        /// Sets the text of the cell contents display box to the given string.
        /// </summary>
        /// <param name="s">Value the contents box should contain.</param>
        void SetCellContentsText(string s);

        /// <summary>
        /// Sets the text of the cell value display box to the given string.
        /// </summary>
        /// <param name="s">The value the text box should contain.</param>
        void SetCellValueText(string s);

        /// <summary>
        /// Sets the text of the cell name display box to the given string.
        /// </summary>
        /// <param name="s"></param>
        void SetCellNameText(string s);
    }
}
