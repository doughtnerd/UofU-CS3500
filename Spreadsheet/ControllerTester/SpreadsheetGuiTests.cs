using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;
using System.Windows.Forms;

namespace ControllerTester
{
    public class ViewStub : ISpreadsheetView
    {
        public event Action<string, string> CellContentsChanged;
        public event Action<string> CellSelectedEvent;
        public event Action<FormClosingEventArgs> CloseEvent;
        public event Action<int> HelpEvent;
        public event Action<FileInfo> OpenEvent;
        public event Action<FileInfo> SaveEvent;

        public string CellContentsText { get; set; }
        public string CellNameText { get; set; }
        public string CellValueText { get; set; }

        public ViewStub()
        {
        }

        public void SetCellContentsText(string s)
        {
            throw new NotImplementedException();
        }

        public void SetCellNameText(string s)
        {
            throw new NotImplementedException();
        }

        public bool SetCellValue(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void SetCellValueText(string s)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class SpreadsheetGuiTests
    {
        #region SpreadsheetUtils Tests

        [TestMethod]
        public void SimpleCellNameToCoords()
        {
            string cellName = "a1";
            int x;
            int y;
            SpreadsheetUtils.CellNameToCoords(cellName, out x, out y);
            Assert.AreEqual(1, x);
            Assert.AreEqual(1, y);
        }

        [TestMethod]
        public void ZCellNameToCoords()
        {
            string cellName = "z26";
            int x;
            int y;
            SpreadsheetUtils.CellNameToCoords(cellName, out x, out y);
            Assert.AreEqual(26, x);
            Assert.AreEqual(26, y);
        }

        [TestMethod]
        public void DoubleLetterCellNameToCoords1()
        {
            string cellName = "aa26";
            int x;
            int y;
            SpreadsheetUtils.CellNameToCoords(cellName, out x, out y);
            Assert.AreEqual(27, x);
            Assert.AreEqual(26, y);
        }

        [TestMethod]
        public void DoubleLetterCellNameToCoords2()
        {
            string cellName = "zz26";
            int x;
            int y;
            SpreadsheetUtils.CellNameToCoords(cellName, out x, out y);
            Assert.AreEqual(702, x);
            Assert.AreEqual(26, y);
        }

        [TestMethod]
        public void DoubleLetterCellNameToCoords3()
        {
            string cellName = "az26";
            int x;
            int y;
            SpreadsheetUtils.CellNameToCoords(cellName, out x, out y);
            Assert.AreEqual(52, x);
            Assert.AreEqual(26, y);
        }

        [TestMethod]
        public void DoubleLetterCellNameToCoords4()
        {
            string cellName = "ab26";
            int x;
            int y;
            SpreadsheetUtils.CellNameToCoords(cellName, out x, out y);
            Assert.AreEqual(28, x);
            Assert.AreEqual(26, y);
        }

        [TestMethod]
        public void SimpleCellNameFromCoords()
        {
            int x = 1;
            int y = 20;
            string cellName = SpreadsheetUtils.CellNameFromCoords(x, y);
            Assert.AreEqual("A20", cellName);
        }

        #endregion
    }
}
