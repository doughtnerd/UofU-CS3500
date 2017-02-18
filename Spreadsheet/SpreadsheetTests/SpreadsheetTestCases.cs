using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulas;
using System.Collections.Generic;

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

        [TestMethod]
        public void Empty1()
        {
            Spreadsheet sheet = new Spreadsheet();
            Assert.AreEqual("", sheet.GetCellContents("a1"));
        }

        [TestMethod]
        public void Empty2()
        {
            Spreadsheet sheet = new Spreadsheet();
            IEnumerator<string> iterator = sheet.GetNamesOfAllNonemptyCells().GetEnumerator();
            Assert.IsFalse(iterator.MoveNext());
        }

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

        [TestMethod]
        public void Test7()
        {
            Spreadsheet sheet = new Spreadsheet();
            ISet<string> set = sheet.SetCellContents("b1", new Formula("a1*2"));
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
            set = sheet.SetCellContents("c1", new Formula("b1+a1"));
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
    }
}
