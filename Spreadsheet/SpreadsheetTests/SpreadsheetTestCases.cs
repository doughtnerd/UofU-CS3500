using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulas;
using System.Collections.Generic;

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
            sheet.SetCellContents(null, new Formula());
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, "");
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("", null);
        }

        /// <summary>
        /// Tests that null input is correctly dealt with.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, 0.0);
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
            sheet.SetCellContents("X07", new Formula());
        }

        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("hello", "");
        }
        
        /// <summary>
        /// Tests that cell name is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Invalid4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("Z", 0.0);
        }

        /// <summary>
        /// Tests that a cell can't have circular dependency.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Invalid5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("c1", new Formula("b1+a1"));
            sheet.SetCellContents("b1", new Formula("a1*2"));
            sheet.SetCellContents("a1", new Formula("c1/3"));
        }

        /// <summary>
        /// Tests that a cell can't depend on itself.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Invalid6()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", new Formula("a1/3"));
        }

        /// <summary>
        /// Tests GetCellContent on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty1()
        {
            Spreadsheet sheet = new Spreadsheet();
            Assert.AreEqual("", sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetNamesOfAllNonemptyCells on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty2()
        {
            Spreadsheet sheet = new Spreadsheet();
            IEnumerator<string> iterator = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            Assert.IsFalse(iterator.MoveNext());
        }

        /// <summary>
        /// Tests SetCellContents (formula) on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty3()
        {
            Spreadsheet sheet = new Spreadsheet();
            List<string> expected = new List<string>();
            expected.Add("a1");
            IEnumerator<string> iterator1 = sheet.SetCellContents("a1", new Formula("2 + 3")).GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetCellContents (string) on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty4()
        {
            Spreadsheet sheet = new Spreadsheet();
            List<string> expected = new List<string>();
            expected.Add("a1");
            IEnumerator<string> iterator1 = sheet.SetCellContents("a1", "hello").GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetCellContents (double) on an empty spreadsheet.
        /// </summary>
        [TestMethod]
        public void Empty5()
        {
            Spreadsheet sheet = new Spreadsheet();
            List<string> expected = new List<string>();
            expected.Add("a1");
            IEnumerator<string> iterator1 = sheet.SetCellContents("a1", 0.125).GetEnumerator();
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.AreEqual(iterator2.Current, iterator1.Current);
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests GetCellContents for formulas.
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", new Formula("2 + 3"));
            Assert.AreEqual("2+3", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetCellContents("a1", new Formula("10 / 5"));
            Assert.AreEqual("10/5", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetCellContents("a1", new Formula("(5 - 3) * 8"));
            Assert.AreEqual("(5-3)*8", ((Formula)sheet.GetCellContents("a1")).ToString());
        }

        /// <summary>
        /// Tests GetCellContents for strings.
        /// </summary>
        [TestMethod]
        public void Test2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", "hello");
            Assert.AreEqual("hello", (string)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", "world");
            Assert.AreEqual("world", (string)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", "!");
            Assert.AreEqual("!", (string)sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetCellContents for doubles.
        /// </summary>
        [TestMethod]
        public void Test3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", 0.125);
            Assert.AreEqual(0.125, (double)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", 0.5);
            Assert.AreEqual(0.5, (double)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", 1.25);
            Assert.AreEqual(1.25, (double)sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetCellContents for a variety.
        /// </summary>
        [TestMethod]
        public void Test4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", new Formula("2 + 3"));
            Assert.AreEqual("2+3", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetCellContents("a1", "hello");
            Assert.AreEqual("hello", (string)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", 0.125);
            Assert.AreEqual(0.125, (double)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", new Formula("10 / 5"));
            Assert.AreEqual("10/5", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetCellContents("a1", "world");
            Assert.AreEqual("world", (string)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", 0.5);
            Assert.AreEqual(0.5, (double)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", new Formula("(5 - 3) * 8"));
            Assert.AreEqual("(5-3)*8", ((Formula)sheet.GetCellContents("a1")).ToString());
            sheet.SetCellContents("a1", "!");
            Assert.AreEqual("!", (string)sheet.GetCellContents("a1"));
            sheet.SetCellContents("a1", 1.25);
            Assert.AreEqual(1.25, (double)sheet.GetCellContents("a1"));
        }

        /// <summary>
        /// Tests GetNamesOfAllNonemptyCells for similar cell names.
        /// </summary>
        [TestMethod]
        public void Test5()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", new Formula());
            sheet.SetCellContents("a2", "hello");
            sheet.SetCellContents("a3", 0.125);
            sheet.SetCellContents("a4", new Formula());
            sheet.SetCellContents("a5", "hello");
            sheet.SetCellContents("a6", 0.125);
            sheet.SetCellContents("a7", new Formula());
            sheet.SetCellContents("a8", "hello");
            sheet.SetCellContents("a9", 0.125);
            List<string> expected = new List<string>();
            expected.Add("a1");
            expected.Add("a2");
            expected.Add("a3");
            expected.Add("a4");
            expected.Add("a5");
            expected.Add("a6");
            expected.Add("a7");
            expected.Add("a8");
            expected.Add("a9");
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
        public void Test6()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", new Formula());
            sheet.SetCellContents("b1", "hello");
            sheet.SetCellContents("c1", 0.125);
            sheet.SetCellContents("d1", new Formula());
            sheet.SetCellContents("e1", "hello");
            sheet.SetCellContents("f1", 0.125);
            sheet.SetCellContents("g1", new Formula());
            sheet.SetCellContents("h1", "hello");
            sheet.SetCellContents("i1", 0.125);
            List<string> expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            expected.Add("d1");
            expected.Add("e1");
            expected.Add("f1");
            expected.Add("g1");
            expected.Add("h1");
            expected.Add("i1");
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
        /// Tests SetCellContents for formulas.
        /// </summary>
        [TestMethod]
        public void Test7()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetCellContents("c1", new Formula("b1+a1"));
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("c1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetCellContents("b1", new Formula("a1*2"));
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("b1");
            expected.Add("c1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetCellContents("a1", new Formula("2+5"));
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetCellContents for strings.
        /// </summary>
        [TestMethod]
        public void Test8()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetCellContents("b1", "a1*2");
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("b1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetCellContents("c1", "b1+a1");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("c1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetCellContents("a1", "2+5");
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }

        /// <summary>
        /// Tests SetCellContents for doubles.
        /// </summary>
        [TestMethod]
        public void Test9()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetCellContents("b1", 0.125);
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("b1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetCellContents("c1", 0.5);
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("c1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            set = sheet.SetCellContents("a1", 1.5);
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
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
        public void Test10()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("a1", 870.569);
            sheet.SetCellContents("b1", new Formula("a1"));
            sheet.SetCellContents("c1", new Formula("b1"));
            sheet.SetCellContents("d1", new Formula("c1"));
            sheet.SetCellContents("e1", new Formula("d1"));
            sheet.SetCellContents("f1", new Formula("e1"));
            sheet.SetCellContents("g1", new Formula("f1"));
            sheet.SetCellContents("h1", new Formula("g1"));
            sheet.SetCellContents("i1", new Formula("h1"));
            sheet.SetCellContents("j1", new Formula("i1"));
            sheet.SetCellContents("k1", new Formula("j1"));
            sheet.SetCellContents("l1", new Formula("k1"));
            sheet.SetCellContents("m1", new Formula("l1"));
            sheet.SetCellContents("n1", new Formula("m1"));
            sheet.SetCellContents("o1", new Formula("n1"));
            sheet.SetCellContents("p1", new Formula("o1"));
            sheet.SetCellContents("q1", new Formula("p1"));
            sheet.SetCellContents("r1", new Formula("q1"));
            sheet.SetCellContents("s1", new Formula("r1"));
            sheet.SetCellContents("t1", new Formula("s1"));
            sheet.SetCellContents("u1", new Formula("t1"));
            sheet.SetCellContents("v1", new Formula("u1"));
            sheet.SetCellContents("w1", new Formula("v1"));
            sheet.SetCellContents("x1", new Formula("w1"));
            sheet.SetCellContents("y1", new Formula("x1"));
            sheet.SetCellContents("z1", new Formula("y1"));
            ISet<string> set = sheet.SetCellContents("a1", 90.87);
            IEnumerator<string> iterator1 = set.GetEnumerator();
            List<string> expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            expected.Add("d1");
            expected.Add("e1");
            expected.Add("f1");
            expected.Add("g1");
            expected.Add("h1");
            expected.Add("i1");
            expected.Add("j1");
            expected.Add("k1");
            expected.Add("l1");
            expected.Add("m1");
            expected.Add("n1");
            expected.Add("o1");
            expected.Add("p1");
            expected.Add("q1");
            expected.Add("r1");
            expected.Add("s1");
            expected.Add("t1");
            expected.Add("u1");
            expected.Add("v1");
            expected.Add("w1");
            expected.Add("x1");
            expected.Add("y1");
            expected.Add("z1");
            IEnumerator<string> iterator2 = expected.GetEnumerator();
            while (iterator2.MoveNext() && iterator1.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            sheet.SetCellContents("f1", "hello");
            sheet.SetCellContents("j1", new Formula("e1"));
            set = sheet.SetCellContents("a1", 90.87);
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            expected.Add("d1");
            expected.Add("e1");
            expected.Add("j1");
            expected.Add("k1");
            expected.Add("l1");
            expected.Add("m1");
            expected.Add("n1");
            expected.Add("o1");
            expected.Add("p1");
            expected.Add("q1");
            expected.Add("r1");
            expected.Add("s1");
            expected.Add("t1");
            expected.Add("u1");
            expected.Add("v1");
            expected.Add("w1");
            expected.Add("x1");
            expected.Add("y1");
            expected.Add("z1");
            iterator2 = expected.GetEnumerator();
            while (iterator2.MoveNext() && iterator1.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            sheet.SetCellContents("y1", 0.983);
            sheet.SetCellContents("z1", new Formula("x1"));
            set = sheet.SetCellContents("a1", 90.87);
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            expected.Add("d1");
            expected.Add("e1");
            expected.Add("j1");
            expected.Add("k1");
            expected.Add("l1");
            expected.Add("m1");
            expected.Add("n1");
            expected.Add("o1");
            expected.Add("p1");
            expected.Add("q1");
            expected.Add("r1");
            expected.Add("s1");
            expected.Add("t1");
            expected.Add("u1");
            expected.Add("v1");
            expected.Add("w1");
            expected.Add("x1");
            expected.Add("z1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            sheet.SetCellContents("p1", "hello");
            sheet.SetCellContents("v1", new Formula("o1"));
            set = sheet.SetCellContents("a1", 90.87);
            iterator1 = set.GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            expected.Add("d1");
            expected.Add("e1");
            expected.Add("j1");
            expected.Add("k1");
            expected.Add("l1");
            expected.Add("m1");
            expected.Add("n1");
            expected.Add("o1");
            expected.Add("v1");
            expected.Add("w1");
            expected.Add("x1");
            expected.Add("z1");
            iterator2 = expected.GetEnumerator();
            while (iterator1.MoveNext() && iterator2.MoveNext())
            {
                Assert.IsTrue(set.Contains(iterator2.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
            iterator1 = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            expected = new List<string>();
            expected.Add("a1");
            expected.Add("b1");
            expected.Add("c1");
            expected.Add("d1");
            expected.Add("e1");
            expected.Add("f1");
            expected.Add("g1");
            expected.Add("h1");
            expected.Add("i1");
            expected.Add("j1");
            expected.Add("k1");
            expected.Add("l1");
            expected.Add("m1");
            expected.Add("n1");
            expected.Add("o1");
            expected.Add("p1");
            expected.Add("q1");
            expected.Add("r1");
            expected.Add("s1");
            expected.Add("t1");
            expected.Add("u1");
            expected.Add("v1");
            expected.Add("w1");
            expected.Add("x1");
            expected.Add("y1");
            expected.Add("z1");
            iterator2 = expected.GetEnumerator();
            while (iterator2.MoveNext() && iterator1.MoveNext())
            {
                Assert.IsTrue(expected.Contains(iterator1.Current));
            }
            Assert.IsFalse(iterator1.MoveNext());
            Assert.IsFalse(iterator2.MoveNext());
        }
    }
}
