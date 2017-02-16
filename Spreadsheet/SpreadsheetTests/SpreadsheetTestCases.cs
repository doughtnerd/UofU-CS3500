using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulas;

namespace SS
{
    [TestClass]
    public class SpreadsheetTestCases
    {
        [TestMethod]
        public void Constructor1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
        }

        [TestMethod]
        public void Constructor2()
        {
            Spreadsheet sheet = new Spreadsheet();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, new Formula());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("Z");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("X07", new Formula());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("hello", "");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("Z", 0.0);
        }
    }
}
