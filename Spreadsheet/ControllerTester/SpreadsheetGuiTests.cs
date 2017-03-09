using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Diagnostics;

namespace SpreadsheetGUI
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
        public string CellCoordinates { get; set; }

        public void SetCellContentsText(string s)
        {
            CellContentsText = s;
        }

        public void SetCellNameText(string s)
        {
            CellNameText = s;
        }

        public bool SetCellValue(string name, string value)
        {
            int x, y;
            SpreadsheetUtils.CellNameToCoords(name, out x, out y);
            if (x == 0 || y == 0)
            {
                return false;
            }
            return true;
        }

        public void SetCellValueText(string s)
        {
            CellValueText = s;
        }

        public void CellContentsChanged_(string cell, string content)
        {
            CellContentsChanged?.Invoke(cell, content);
        }

        public void CellSelected_(string cell)
        {
            int x, y;
            SpreadsheetUtils.CellNameToCoords(cell, out x, out y);
            CellCoordinates = x + "," + y;
            CellSelectedEvent?.Invoke(cell);
        }

        public void Close_(FormClosingEventArgs args)
        {
            CloseEvent?.Invoke(args);
        }

        public void Help_(int index)
        {
            HelpEvent?.Invoke(index);
        }

        public void Open_(FileInfo fileInfo)
        {
            OpenEvent?.Invoke(fileInfo);
        }

        public void Save_(FileInfo fileInfo)
        {
            SaveEvent?.Invoke(fileInfo);
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

        #region Controller Tests

        /// <summary>
        /// Tests that the cell selected event is handled.
        /// Tests for correct cell selection.
        /// </summary>
        [TestMethod]
        public void Test()
        {
            ViewStub vs = new ViewStub();
            Controller controller = new Controller(vs);
            vs.CellSelected_("A1");
            Assert.IsTrue(vs.CellCoordinates.Equals("1,1"));
            Assert.IsTrue(vs.CellNameText.Equals("A1"));
            vs.CellSelected_("B1");
            Assert.IsTrue(vs.CellCoordinates.Equals("2,1"));
            Assert.IsTrue(vs.CellNameText.Equals("B1"));
            vs.CellSelected_("D26");
            Assert.IsTrue(vs.CellCoordinates.Equals("4,26"));
            Assert.IsTrue(vs.CellNameText.Equals("D26"));
            vs.CellSelected_("B1");
            Assert.IsTrue(vs.CellCoordinates.Equals("2,1"));
            Assert.IsTrue(vs.CellNameText.Equals("B1"));
            vs.CellSelected_("P47");
            Assert.IsTrue(vs.CellCoordinates.Equals("16,47"));
            Assert.IsTrue(vs.CellNameText.Equals("P47"));
            vs.CellSelected_("Z99");
            Assert.IsTrue(vs.CellCoordinates.Equals("26,99"));
            Assert.IsTrue(vs.CellNameText.Equals("Z99"));
            vs.CellSelected_("A1");
            Assert.IsTrue(vs.CellCoordinates.Equals("1,1"));
            Assert.IsTrue(vs.CellNameText.Equals("A1"));
        }

        /// <summary>
        /// Tests that cell contents changed is handled correctly.
        /// Tests that fields are changed correctly when cell contents have been changed (formulas).
        /// </summary>
        [TestMethod]
        public void Test2()
        {
            ViewStub vs = new ViewStub();
            Controller controller = new Controller(vs);
            vs.CellContentsChanged_("A91", "=1+7");
            Assert.IsTrue(vs.CellContentsText.Equals("=1+7"));
            Assert.IsTrue(vs.CellValueText.Equals("8"));
            vs.CellContentsChanged_("B27", "=A91+2");
            Assert.IsTrue(vs.CellContentsText.Equals("=A91+2"));
            Assert.IsTrue(vs.CellValueText.Equals("10"));
            vs.CellContentsChanged_("C31", "=B27/5");
            Assert.IsTrue(vs.CellContentsText.Equals("=B27/5"));
            Assert.IsTrue(vs.CellValueText.Equals("2"));
            vs.CellContentsChanged_("D15", "=C31-2");
            Assert.IsTrue(vs.CellContentsText.Equals("=C31-2"));
            Assert.IsTrue(vs.CellValueText.Equals("0"));
            vs.CellContentsChanged_("E1", "=4/D15");
            Assert.IsTrue(vs.CellContentsText.Equals("=4/D15"));
            Assert.IsTrue(vs.CellValueText.Equals("SS.FormulaError"));
            vs.CellContentsChanged_("F1", "=A100");
            Assert.IsTrue(vs.CellContentsText.Equals("=A100"));
            Assert.IsTrue(vs.CellValueText.Equals("SS.FormulaError"));
        }

        /// <summary>
        /// Tests that cell contents changed is handled correctly.
        /// Tests that fields are changed correctly when cell contents have been changed (doubles).
        /// </summary>
        [TestMethod]
        public void Test3()
        {
            ViewStub vs = new ViewStub();
            Controller controller = new Controller(vs);
            vs.CellContentsChanged_("A91", "8.0");
            Assert.IsTrue(vs.CellContentsText.Equals("8"));
            Assert.IsTrue(vs.CellValueText.Equals("8"));
            vs.CellContentsChanged_("B27", "16");
            Assert.IsTrue(vs.CellContentsText.Equals("16"));
            Assert.IsTrue(vs.CellValueText.Equals("16"));
            vs.CellContentsChanged_("C31", "98");
            Assert.IsTrue(vs.CellContentsText.Equals("98"));
            Assert.IsTrue(vs.CellValueText.Equals("98"));
            vs.CellContentsChanged_("D15", "31");
            Assert.IsTrue(vs.CellContentsText.Equals("31"));
            Assert.IsTrue(vs.CellValueText.Equals("31"));
            vs.CellContentsChanged_("E1", "467.93");
            Assert.IsTrue(vs.CellContentsText.Equals("467.93"));
            Assert.IsTrue(vs.CellValueText.Equals("467.93"));
            vs.CellContentsChanged_("F1", "100");
            Assert.IsTrue(vs.CellContentsText.Equals("100"));
            Assert.IsTrue(vs.CellValueText.Equals("100"));
        }

        /// <summary>
        /// Tests that cell contents changed is handled correctly.
        /// Tests that fields are changed correctly when cell contents have been changed (strings).
        /// </summary>
        [TestMethod]
        public void Test4()
        {
            ViewStub vs = new ViewStub();
            Controller controller = new Controller(vs);
            vs.CellContentsChanged_("A91", "hello");
            Assert.IsTrue(vs.CellContentsText.Equals("hello"));
            Assert.IsTrue(vs.CellValueText.Equals("hello"));
            vs.CellContentsChanged_("B27", "TeST");
            Assert.IsTrue(vs.CellContentsText.Equals("TeST"));
            Assert.IsTrue(vs.CellValueText.Equals("TeST"));
            vs.CellContentsChanged_("C31", "what98");
            Assert.IsTrue(vs.CellContentsText.Equals("what98"));
            Assert.IsTrue(vs.CellValueText.Equals("what98"));
            vs.CellContentsChanged_("D15", "world!");
            Assert.IsTrue(vs.CellContentsText.Equals("world!"));
            Assert.IsTrue(vs.CellValueText.Equals("world!"));
            vs.CellContentsChanged_("E1", "4r67");
            Assert.IsTrue(vs.CellContentsText.Equals("4r67"));
            Assert.IsTrue(vs.CellValueText.Equals("4r67"));
            vs.CellContentsChanged_("F1", "1O00.OiI");
            Assert.IsTrue(vs.CellContentsText.Equals("1O00.OiI"));
            Assert.IsTrue(vs.CellValueText.Equals("1O00.OiI"));
        }

        /// <summary>
        /// Tests that close events are handled properly.
        /// </summary>
        [TestMethod]
        public void Test5()
        {
            ViewStub vs = new ViewStub();
            Controller controller = new Controller(vs);
            try
            {
                vs.Close_(new FormClosingEventArgs(new CloseReason(), false));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /*
         * Feel free to uncomment however:
         * 
         * The following tests will create pop-up windows. The spreadsheet windows will close themselves after
         * 4 seconds however the message box windows will remain open until the "OK" button has been clicked.
         */

        ///// <summary>
        ///// Tests that help events are handled properly.
        ///// </summary>
        //[TestMethod]
        //public void Test6()
        //{
        //    ViewStub vs = new ViewStub();
        //    Controller controller = new Controller(vs);
        //    try
        //    {
        //        vs.Help_(0);
        //        vs.Help_(1);
        //    }
        //    catch (Exception)
        //    {
        //        Assert.Fail();
        //    }
        //}

        ///// <summary>
        ///// Tests that open and close events are handled properly.
        ///// </summary>
        //[TestMethod]
        //public void Test7()
        //{
        //    ViewStub vs = new ViewStub();
        //    Controller controller = new Controller(vs);
        //    try
        //    {
        //        Stopwatch sw = new Stopwatch();
        //        vs.Open_(new FileInfo("test"));
        //        sw.Start();
        //        while (sw.ElapsedMilliseconds < 2000)
        //        {
        //        }
        //        sw.Stop();
        //        vs.Close_(new FormClosingEventArgs(new CloseReason(), false));
        //    }
        //    catch (Exception)
        //    {
        //        Assert.Fail();
        //    }
        //}

        ///// <summary>
        ///// Tests that open, edits, save, and close are handled properly.
        ///// </summary>
        //[TestMethod]
        //public void Test8()
        //{
        //    ViewStub vs = new ViewStub();
        //    Controller controller = new Controller(vs);
        //    try
        //    {
        //        Stopwatch sw = new Stopwatch();
        //        vs.Open_(new FileInfo("test"));
        //        vs.CellContentsChanged_("A1", "=1+7");
        //        vs.Save_(new FileInfo("test"));
        //        sw.Start();
        //        while (sw.ElapsedMilliseconds < 2000)
        //        {
        //        }
        //        sw.Stop();
        //        vs.Close_(new FormClosingEventArgs(new CloseReason(), false));
        //    }
        //    catch (Exception)
        //    {
        //        Assert.Fail();
        //    }
        //}

        #endregion 
    }
}
