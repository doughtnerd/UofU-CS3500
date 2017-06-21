using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulas;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace SS
{
    /// <summary>
    /// Variety of tests for the spreadsheet project.
    /// </summary>
    [TestClass]
    public class SpreadsheetTestCases
    {
        /// <summary>
        /// Tests that an AbstractSpreadsheet object can be made.
        /// </summary>
        [TestMethod]
        public void Constructor1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
        }

        /// <summary>
        /// Tests that the constructor doesn't fail.
        /// </summary>
        [TestMethod]
        public void Constructor2()
        {
            Spreadsheet sheet = new Spreadsheet();
        }

        /// <summary>
        /// Tests that the second constructor doesn't fail.
        /// </summary>
        [TestMethod]
        public void Constructor3()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
        }

        /// <summary>
        /// Tests that the thrid constructor doesn't fail.
        /// </summary>
        [TestMethod]
        public void Constructor4()
        {
            Spreadsheet sheet = new Spreadsheet(File.OpenText("../../SampleSavedSpreadsheet.xml"), new Regex("^[a-zA-Z][0-9]$"));
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue(null);
        }


        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "");
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "");
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("", null);
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null6()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "0.0");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("Z");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue("0Z");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X07", "");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("hello", "");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1A", "0.0");
        }

        /// <summary>
        /// Tests that a cell can't have circular dependency.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Invalid6()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("c1", "=b1+a1");
            sheet.SetContentsOfCell("b1", "=a1*2");
            sheet.SetContentsOfCell("a1", "=c1/3");
        }

        /// <summary>
        /// Tests that a cell can't depend on itself.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Invalid7()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=a1/3");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid8()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.GetCellContents("Zhel4");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid9()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.GetCellValue("Zz9");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid10()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.SetContentsOfCell("Xy47", "");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid11()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.SetContentsOfCell("b59", "");
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid12()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.SetContentsOfCell("A18", "0.0");
        }

        /// <summary>
        /// Tests that a cell can't have circular dependency.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Invalid13()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.SetContentsOfCell("c1", "=b1+a1");
            sheet.SetContentsOfCell("b1", "=a1*2");
            sheet.SetContentsOfCell("a1", "=c1/3");
        }

        /// <summary>
        /// Tests that a cell can't depend on itself.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Invalid14()
        {
            Spreadsheet sheet = new Spreadsheet(new Regex("^[a-zA-Z][0-9]$"));
            sheet.SetContentsOfCell("a1", "=a1/3");
        }

        /// <summary>
        /// Tests GetCellContents on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty1()
        {
            Spreadsheet sheet = new Spreadsheet();
            Assert.AreEqual("", sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetCellValue on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty2()
        {
            Spreadsheet sheet = new Spreadsheet();
            Assert.AreEqual("", sheet.GetCellValue("a1"));
        }

        /// <summary>
        /// Tests GetNamesOfAllNonemptyCells on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty3()
        {
            Spreadsheet sheet = new Spreadsheet();
            IEnumerator<string> iterator = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            Assert.IsFalse(iterator.MoveNext());
        }

        /// <summary>
        /// Tests SetContentsOfCell (formula) on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty4()
        {
            Spreadsheet sheet = new Spreadsheet();
            List<string> expected = new List<string>();
            expected.Add("A1");
            IEnumerator<string> iterator1 = sheet.SetContentsOfCell("a1", "2 + 3").GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetContentsOfCell (string) on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty5()
        {
            Spreadsheet sheet = new Spreadsheet();
            List<string> expected = new List<string>();
            expected.Add("A1");
            IEnumerator<string> iterator1 = sheet.SetContentsOfCell("a1", "hello").GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetContentsOfCell (double) on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty6()
        {
            Spreadsheet sheet = new Spreadsheet();
            List<string> expected = new List<string>();
            expected.Add("A1");
            IEnumerator<string> iterator1 = sheet.SetContentsOfCell("a1", "0.125").GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests Changed on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty7()
        {
            Spreadsheet sheet = new Spreadsheet();
            Assert.IsFalse(sheet.Changed);
        }


        /// <summary>
        /// Tests GetCellContents for formulas.
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=2 + 3");
            Assert.AreEqual("2+3", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetContentsOfCell("a1", "=10 / 5");
            Assert.AreEqual("10/5", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetContentsOfCell("a1", "= (5 - 3) * 8");
            Assert.AreEqual("(5-3)*8", ((Formula)sheet.GetCellContents("a1")).ToString());
        }

        /// <summary>
        /// Tests GetCellContents for strings.
        /// </summary>
        [TestMethod]
        public void Test2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", (string)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "world");
            Assert.AreEqual("world", (string)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "!");
            Assert.AreEqual("!", (string)sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetCellContents for doubles.
        /// </summary>
        [TestMethod]
        public void Test3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "0.125");
            Assert.AreEqual(0.125, (double)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "0.5");
            Assert.AreEqual(0.5, (double)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "1.25");
            Assert.AreEqual(1.25, (double)sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetCellContents for a variety.
        /// </summary>
        [TestMethod]
        public void Test4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=2 + 3");
            Assert.AreEqual("2+3", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", (string)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "0.125");
            Assert.AreEqual(0.125, (double)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "=10 / 5");
            Assert.AreEqual("10/5", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetContentsOfCell("a1", "world");
            Assert.AreEqual("world", (string)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "0.5");
            Assert.AreEqual(0.5, (double)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "=(5 - 3) * 8");
            Assert.AreEqual("(5-3)*8", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetContentsOfCell("a1", "!");
            Assert.AreEqual("!", (string)sheet.GetCellContents("a1"));
            sheet.SetContentsOfCell("a1", "1.25");
            Assert.AreEqual(1.25, (double)sheet.GetCellContents("a1"));
        }


        /// <summary>
        /// Tests GetCellValue for formulas.
        /// </summary>
        [TestMethod]
        public void Test5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=2 + 3");
            Assert.AreEqual(5.0, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "=10 / 5");
            Assert.AreEqual(2.0, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "= (5 - 3) * 8");
            Assert.AreEqual(16.0, sheet.GetCellValue("a1"));
        }

        /// <summary>
        /// Tests GetCellValue for strings.
        /// </summary>
        [TestMethod]
        public void Test6()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "world");
            Assert.AreEqual("world", sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "!");
            Assert.AreEqual("!", sheet.GetCellValue("a1"));
        }

        /// <summary>
        /// Tests GetCellValue for doubles.
        /// </summary>
        [TestMethod]
        public void Test7()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "0.125");
            Assert.AreEqual(0.125, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "0.5");
            Assert.AreEqual(0.5, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "1.25");
            Assert.AreEqual(1.25, sheet.GetCellValue("a1"));
        }


        /// <summary>
        /// Tests GetCellValue for a variety.
        /// </summary>
        [TestMethod]
        public void Test8()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=2 + 3");
            Assert.AreEqual(5.0, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "0.125");
            Assert.AreEqual(0.125, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "=10 / 5");
            Assert.AreEqual(2.0, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "world");
            Assert.AreEqual("world", sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "0.5");
            Assert.AreEqual(0.5, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "=(5 - 3) * 8");
            Assert.AreEqual(16.0, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "!");
            Assert.AreEqual("!", sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("a1", "1.25");
            Assert.AreEqual(1.25, sheet.GetCellValue("a1"));
        }

        /// <summary>
        /// Tests GetNamesOfAllNonemptyCells for similar cell names.
        /// </summary>
        [TestMethod]
        public void Test9()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=1");
            sheet.SetContentsOfCell("a2", "hello");
            sheet.SetContentsOfCell("a3", "0.125");
            sheet.SetContentsOfCell("a4", "=1");
            sheet.SetContentsOfCell("a5", "hello");
            sheet.SetContentsOfCell("a6", "0.125");
            sheet.SetContentsOfCell("a7", "=1");
            sheet.SetContentsOfCell("a8", "hello");
            sheet.SetContentsOfCell("a9", "0.125");
            List<string> expected = new List<string>();
            expected.Add("A1");
            expected.Add("A2");
            expected.Add("A3");
            expected.Add("A4");
            expected.Add("A5");
            expected.Add("A6");
            expected.Add("A7");
            expected.Add("A8");
            expected.Add("A9");
            IEnumerator<string> iterator1 = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests GetNamesOfAllNonemptyCells for a variety of cell names.
        /// </summary>
        [TestMethod]
        public void Test10()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "=1");
            sheet.SetContentsOfCell("b1", "hello");
            sheet.SetContentsOfCell("c1", "0.125");
            sheet.SetContentsOfCell("d1", "=1");
            sheet.SetContentsOfCell("e1", "hello");
            sheet.SetContentsOfCell("f1", "0.125");
            sheet.SetContentsOfCell("g1", "=1");
            sheet.SetContentsOfCell("h1", "hello");
            sheet.SetContentsOfCell("i1", "0.125");
            List<string> expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D1");
            expected.Add("E1");
            expected.Add("F1");
            expected.Add("G1");
            expected.Add("H1");
            expected.Add("I1");
            IEnumerator<string> iterator1 = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetContentsOfCell for formulas.
        /// </summary>
        [TestMethod]
        public void Test11()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetContentsOfCell("c1", "=b1+a1");
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("C1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetContentsOfCell("b1", "=a1*2");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("B1");
            expected.Add("C1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetContentsOfCell("a1", "=2+5");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetContentsOfCell for strings.
        /// </summary>
        [TestMethod]
        public void Test12()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetContentsOfCell("b1", "a1*2");
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("B1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetContentsOfCell("c1", "b1+a1");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("C1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetContentsOfCell("a1", "2+5");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetContentsOfCell for doubles.
        /// </summary>
        [TestMethod]
        public void Test13()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetContentsOfCell("b1", "0.125");
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("B1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetContentsOfCell("c1", "0.5");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("C1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetContentsOfCell("a1", "1.5");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Big test to test changes with large spreadsheets.
        /// </summary>
        [TestMethod]
        public void Test14()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "870.569");
            sheet.SetContentsOfCell("b1", "=a1");
            sheet.SetContentsOfCell("c1", "=b1");
            sheet.SetContentsOfCell("d1", "=c1");
            sheet.SetContentsOfCell("e1", "=d1");
            sheet.SetContentsOfCell("f1", "=e1");
            sheet.SetContentsOfCell("g1", "=f1");
            sheet.SetContentsOfCell("h1", "=g1");
            sheet.SetContentsOfCell("i1", "=h1");
            sheet.SetContentsOfCell("j1", "=i1");
            sheet.SetContentsOfCell("k1", "=j1");
            sheet.SetContentsOfCell("l1", "=k1");
            sheet.SetContentsOfCell("m1", "=l1");
            sheet.SetContentsOfCell("n1", "=m1");
            sheet.SetContentsOfCell("o1", "=n1");
            sheet.SetContentsOfCell("p1", "=o1");
            sheet.SetContentsOfCell("q1", "=p1");
            sheet.SetContentsOfCell("r1", "=q1");
            sheet.SetContentsOfCell("s1", "=r1");
            sheet.SetContentsOfCell("t1", "=s1");
            sheet.SetContentsOfCell("u1", "=t1");
            sheet.SetContentsOfCell("v1", "=u1");
            sheet.SetContentsOfCell("w1", "=v1");
            sheet.SetContentsOfCell("x1", "=w1");
            sheet.SetContentsOfCell("y1", "=x1");
            sheet.SetContentsOfCell("z1", "=y1");
            ISet<string> set = sheet.SetContentsOfCell("a1", "90.87");
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D1");
            expected.Add("E1");
            expected.Add("F1");
            expected.Add("G1");
            expected.Add("H1");
            expected.Add("I1");
            expected.Add("J1");
            expected.Add("K1");
            expected.Add("L1");
            expected.Add("M1");
            expected.Add("N1");
            expected.Add("O1");
            expected.Add("P1");
            expected.Add("Q1");
            expected.Add("R1");
            expected.Add("S1");
            expected.Add("T1");
            expected.Add("U1");
            expected.Add("V1");
            expected.Add("W1");
            expected.Add("X1");
            expected.Add("Y1");
            expected.Add("Z1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator2.MoveNext() && iterator1.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            sheet.SetContentsOfCell("f1", "hello");
            sheet.SetContentsOfCell("j1", "=e1");
            set = sheet.SetContentsOfCell("a1", "90.87");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D1");
            expected.Add("E1");
            expected.Add("J1");
            expected.Add("K1");
            expected.Add("L1");
            expected.Add("M1");
            expected.Add("N1");
            expected.Add("O1");
            expected.Add("P1");
            expected.Add("Q1");
            expected.Add("R1");
            expected.Add("S1");
            expected.Add("T1");
            expected.Add("U1");
            expected.Add("V1");
            expected.Add("W1");
            expected.Add("X1");
            expected.Add("Y1");
            expected.Add("Z1");
            iterator2 = expected.GetEnumerator();
            while (iterator2.MoveNext() && iterator1.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            sheet.SetContentsOfCell("y1", "0.983");
            sheet.SetContentsOfCell("z1", "=x1");
            set = sheet.SetContentsOfCell("a1", "90.87");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D1");
            expected.Add("E1");
            expected.Add("J1");
            expected.Add("K1");
            expected.Add("L1");
            expected.Add("M1");
            expected.Add("N1");
            expected.Add("O1");
            expected.Add("P1");
            expected.Add("Q1");
            expected.Add("R1");
            expected.Add("S1");
            expected.Add("T1");
            expected.Add("U1");
            expected.Add("V1");
            expected.Add("W1");
            expected.Add("X1");
            expected.Add("Z1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            sheet.SetContentsOfCell("p1", "hello");
            sheet.SetContentsOfCell("v1", "=o1");
            set = sheet.SetContentsOfCell("a1", "90.87");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D1");
            expected.Add("E1");
            expected.Add("J1");
            expected.Add("K1");
            expected.Add("L1");
            expected.Add("M1");
            expected.Add("N1");
            expected.Add("O1");
            expected.Add("V1");
            expected.Add("W1");
            expected.Add("X1");
            expected.Add("Z1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            iterator1 = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            expected = new List<string>();
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D1");
            expected.Add("E1");
            expected.Add("F1");
            expected.Add("G1");
            expected.Add("H1");
            expected.Add("I1");
            expected.Add("J1");
            expected.Add("K1");
            expected.Add("L1");
            expected.Add("M1");
            expected.Add("N1");
            expected.Add("O1");
            expected.Add("P1");
            expected.Add("Q1");
            expected.Add("R1");
            expected.Add("S1");
            expected.Add("T1");
            expected.Add("U1");
            expected.Add("V1");
            expected.Add("W1");
            expected.Add("X1");
            expected.Add("Y1");
            expected.Add("Z1");
            iterator2 = expected.GetEnumerator();
            while (iterator2.MoveNext() && iterator1.MoveNext())
            {
                Assert.IsTrue(expected.Contains(iterator1.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            Assert.AreEqual(90.87, sheet.GetCellValue("a1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("b1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("c1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("d1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("e1"));
            Assert.AreEqual("hello", sheet.GetCellValue("f1"));
            Assert.IsTrue(sheet.GetCellValue("g1").GetType() == typeof(FormulaError));
            Assert.IsTrue(sheet.GetCellValue("h1").GetType() == typeof(FormulaError));
            Assert.IsTrue(sheet.GetCellValue("i1").GetType() == typeof(FormulaError));
            Assert.AreEqual(90.87, sheet.GetCellValue("j1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("k1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("l1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("m1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("n1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("o1"));
            Assert.AreEqual("hello", sheet.GetCellValue("p1"));
            Assert.IsTrue(sheet.GetCellValue("q1").GetType() == typeof(FormulaError));
            Assert.IsTrue(sheet.GetCellValue("r1").GetType() == typeof(FormulaError));
            Assert.IsTrue(sheet.GetCellValue("s1").GetType() == typeof(FormulaError));
            Assert.IsTrue(sheet.GetCellValue("t1").GetType() == typeof(FormulaError));
            Assert.IsTrue(sheet.GetCellValue("u1").GetType() == typeof(FormulaError));
            Assert.AreEqual(90.87, sheet.GetCellValue("v1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("w1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("x1"));
            Assert.AreEqual(0.983, sheet.GetCellValue("y1"));
            Assert.AreEqual(90.87, sheet.GetCellValue("z1"));
        }

        /// <summary>
        /// Tests GetCellValue for variable formulas.
        /// </summary>
        [TestMethod]
        public void Test15()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("An1", "=a1 *N1   +16/2");
            Assert.AreEqual("A1*N1+16/2", ((Formula)sheet.GetCellContents("an1")).ToString());
            Assert.IsTrue(sheet.GetCellValue("aN1").GetType() == typeof(FormulaError));
            sheet.SetContentsOfCell("a1", "= 7 - 3");
            Assert.AreEqual("7-3", ((Formula)sheet.GetCellContents("a1")).ToString());
            Assert.IsTrue(sheet.GetCellValue("aN1").GetType() == typeof(FormulaError));
            Assert.AreEqual(4.0, sheet.GetCellValue("a1"));
            sheet.SetContentsOfCell("C1", "=b1/3-1");
            Assert.AreEqual("B1/3-1", ((Formula)sheet.GetCellContents("c1")).ToString());
            Assert.IsTrue(sheet.GetCellValue("aN1").GetType() == typeof(FormulaError));
            Assert.AreEqual(4.0, sheet.GetCellValue("a1"));
            Assert.IsTrue(sheet.GetCellValue("c1").GetType() == typeof(FormulaError));
            sheet.SetContentsOfCell("B1", "9");
            Assert.AreEqual(9.0, sheet.GetCellContents("b1"));
            Assert.IsTrue(sheet.GetCellValue("aN1").GetType() == typeof(FormulaError));
            Assert.AreEqual(4.0, sheet.GetCellValue("a1"));
            Assert.AreEqual(2.0, sheet.GetCellValue("C1"));
            Assert.AreEqual(9.0, sheet.GetCellValue("b1"));
            sheet.SetContentsOfCell("n1", "=b1 + 1");
            Assert.AreEqual("B1+1", ((Formula)sheet.GetCellContents("n1")).ToString());
            Assert.AreEqual(48.0, sheet.GetCellValue("an1"));
            Assert.AreEqual(4.0, sheet.GetCellValue("a1"));
            Assert.AreEqual(2.0, sheet.GetCellValue("C1"));
            Assert.AreEqual(9.0, sheet.GetCellValue("b1"));
            Assert.AreEqual(10.0, sheet.GetCellValue("N1"));
        }

        /// <summary>
        /// Test stuff.
        /// </summary>
        [TestMethod]
        public void Test16()
        {
            Spreadsheet sheet = new Spreadsheet(File.OpenText("../../SampleSavedSpreadsheet.xml"), new Regex("^[a-zA-Z][0-9]$"));
            Assert.AreEqual(1.5, sheet.GetCellValue("a1"));
            Assert.AreEqual(8.0, sheet.GetCellValue("A2"));
            Assert.AreEqual(35.0, sheet.GetCellValue("A3"));
            Assert.AreEqual("Hello", sheet.GetCellValue("b2"));
        }

        /// <summary>
        /// Test more stuff.
        /// </summary>
        [TestMethod]
        public void Test17()
        {
            Spreadsheet sheet = new Spreadsheet();
            StreamWriter writer = File.CreateText("../../SpreadsheetTest.xml");
            Assert.IsFalse(sheet.Changed);
            sheet.SetContentsOfCell("An1", "=a1 *N1   +16/2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("a1", "= 7 - 3");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("C1", "=b1/3-1");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("B1", "9");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("n1", "=b1 + 1");
            Assert.IsTrue(sheet.Changed);
            sheet.Save(writer);
            Assert.IsFalse(sheet.Changed);
        }
    }
}
