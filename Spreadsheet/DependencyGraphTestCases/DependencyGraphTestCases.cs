using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Dependencies
{
    /// <summary>
    /// Tests the dependency graph class.
    /// </summary>
    [TestClass]
    public class DependencyGraphTestCases
    {
        //[TestMethod]
        //public void Test()
        //{
        //    int a = "a".GetHashCode(), b = "b".GetHashCode(), c = "c".GetHashCode(), d = "d".GetHashCode();
        //    DependencyGraph dg = new DependencyGraph();
        //    dg.AddDependency("a", "b");
        //    dg.AddDependency("a", "c");
        //    dg.AddDependency("a", "d");
        //    dg.AddDependency("b", "a");
        //    dg.AddDependency("b", "c");
        //    dg.AddDependency("b", "d");
        //    dg.AddDependency("c", "a");
        //    dg.AddDependency("c", "b");
        //    dg.AddDependency("c", "d");
        //    dg.AddDependency("d", "a");
        //    dg.AddDependency("d", "b");
        //    dg.AddDependency("d", "c");
        //}

        /// <summary>
        /// Tests constructor.
        /// </summary>
        [TestMethod]
        public void Constructor()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if the correct size is returned when the dependency graph is empty.
        /// </summary>
        [TestMethod]
        public void SizeZero()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.AreEqual(0, dg.Size);
        }
        /// <summary>
        /// Tests if the correct size is returned.
        /// </summary>
        [TestMethod]
        public void Size()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            Assert.AreEqual(6, dg.Size);
        }
        /// <summary>
        /// Tests if a dependency has dependents when it has dependents.
        /// </summary>
        [TestMethod]
        public void HasDependentsTrue()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            Assert.IsTrue(dg.HasDependents("a"));
        }
        /// <summary>
        /// Tests if a dependency has dependents when it does not have dependents.
        /// </summary>
        [TestMethod]
        public void HasDependentsFalse()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            Assert.IsFalse(dg.HasDependents("d"));
        }
        /// <summary>
        /// Tests if a dependency has dependees when it has dependees.
        /// </summary>
        [TestMethod]
        public void HasDependeesTrue()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            Assert.IsTrue(dg.HasDependees("d"));
        }
        /// <summary>
        /// Tests if a dependency has dependees when it does not have dependees.
        /// </summary>
        [TestMethod]
        public void HasDependeesFalse()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            Assert.IsFalse(dg.HasDependees("a"));
        }
        /// <summary>
        /// Tests if dependents of a dependency are properly returned when there are none.
        /// </summary>
        [TestMethod]
        public void GetDependentsNone()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            List<string> expected = new List<string>();
            IEnumerator<string> iterator1 = expected.GetEnumerator();
            IEnumerator<string> iterator2 = dg.GetDependents("d").GetEnumerator();
            while (iterator2.MoveNext()) {
                if(!iterator1.MoveNext())
                {
                    Assert.Fail();
                }
                Assert.AreEqual(iterator1.Current, iterator2.Current);
            }
        }
        /// <summary>
        /// Tests if dependents of a dependency are properly returned.
        /// </summary>
        [TestMethod]
        public void GetDependents()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            List<string> expected = new List<string>();
            expected.Add("b");
            expected.Add("c");
            expected.Add("d");
            IEnumerator<string> iterator1 = expected.GetEnumerator();
            IEnumerator<string> iterator2 = dg.GetDependents("a").GetEnumerator();
            while (iterator2.MoveNext())
            {
                if (!iterator1.MoveNext())
                {
                    Assert.Fail();
                }
                Assert.AreEqual(iterator1.Current, iterator2.Current);
            }
        }
        /// <summary>
        /// Tests if dependees of a dependency are properly returned when there are none.
        /// </summary>
        [TestMethod]
        public void GetDependeesNone()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            List<string> expected = new List<string>();
            IEnumerator<string> iterator1 = expected.GetEnumerator();
            IEnumerator<string> iterator2 = dg.GetDependees("a").GetEnumerator();
            while (iterator2.MoveNext())
            {
                if (!iterator1.MoveNext())
                {
                    Assert.Fail();
                }
                Assert.AreEqual(iterator1.Current, iterator2.Current);
            }
        }
        /// <summary>
        /// Tests if dependees of a dependency are properly returned.
        /// </summary>
        [TestMethod]
        public void GetDependees()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            List<string> expected = new List<string>();
            expected.Add("a");
            expected.Add("b");
            expected.Add("c");
            IEnumerator<string> iterator1 = expected.GetEnumerator();
            IEnumerator<string> iterator2 = dg.GetDependees("d").GetEnumerator();
            while (iterator2.MoveNext())
            {
                if (!iterator1.MoveNext())
                {
                    Assert.Fail();
                }
                Assert.AreEqual(iterator1.Current, iterator2.Current);
            }
        }
        /// <summary>
        /// Tests if a dependency is properly added if dependency does exist.
        /// </summary>
        [TestMethod]
        public void AddDependencyExists()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            dg.AddDependency("a", "d");
            IEnumerator<string> iterator1 = dg.GetDependents("a").GetEnumerator();
            int count1 = 0;
            while (iterator1.MoveNext())
            {
                count1++;
            }
            IEnumerator<string> iterator2 = dg.GetDependees("d").GetEnumerator();
            int count2 = 0;
            while (iterator2.MoveNext())
            {
                count2++;
            }
            Assert.AreEqual(3, count1);
            Assert.AreEqual(3, count2);
        }
        /// <summary>
        /// Tests if a dependency is properly added if dependency does not exist.
        /// </summary>
        [TestMethod]
        public void AddDependency()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            dg.AddDependency("b", "a");
            IEnumerator<string> iterator1 = dg.GetDependents("b").GetEnumerator();
            int count1 = 0;
            while (iterator1.MoveNext())
            {
                count1++;
            }
            IEnumerator<string> iterator2 = dg.GetDependees("a").GetEnumerator();
            int count2 = 0;
            while (iterator2.MoveNext())
            {
                count2++;
            }
            Assert.AreEqual(3, count1);
            Assert.AreEqual(1, count2);
        }
        /// <summary>
        /// Tests if a dependency is properly removed if dependency does exist.
        /// </summary>
        [TestMethod]
        public void RemoveDependencyDoesNotExist()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            dg.RemoveDependency("b", "a");
            IEnumerator<string> iterator1 = dg.GetDependents("b").GetEnumerator();
            int count1 = 0;
            while (iterator1.MoveNext())
            {
                count1++;
            }
            IEnumerator<string> iterator2 = dg.GetDependees("a").GetEnumerator();
            int count2 = 0;
            while (iterator2.MoveNext())
            {
                count2++;
            }
            Assert.AreEqual(2, count1);
            Assert.AreEqual(0, count2);
        }
        /// <summary>
        /// Tests if a dependency is properly removed if dependency does not exist.
        /// </summary>
        [TestMethod]
        public void RemoveDependency()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            dg.RemoveDependency("a", "d");
            IEnumerator<string> iterator1 = dg.GetDependents("a").GetEnumerator();
            int count1 = 0;
            while (iterator1.MoveNext())
            {
                count1++;
            }
            IEnumerator<string> iterator2 = dg.GetDependees("d").GetEnumerator();
            int count2 = 0;
            while (iterator2.MoveNext())
            {
                count2++;
            }
            Assert.AreEqual(2, count1);
            Assert.AreEqual(2, count2);
        }
        /// <summary>
        /// Tests if dependents are properly replaced.
        /// </summary>
        [TestMethod]
        public void ReplaceDependents()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            List<string> toAdd = new List<string>();
            toAdd.Add("e");
            toAdd.Add("f");
            toAdd.Add("g");
            toAdd.Add("h");
            toAdd.Add("i");
            dg.ReplaceDependents("a", toAdd);
            IEnumerator<string> iterator = dg.GetDependents("a").GetEnumerator();
            int count = 0;
            while (iterator.MoveNext())
            {
                count++;
            }
            Assert.AreEqual(5, count);
        }
        /// <summary>
        /// Tests if dependees are properly replaced.
        /// </summary>
        [TestMethod]
        public void ReplaceDependees()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "d");
            dg.AddDependency("b", "c");
            dg.AddDependency("b", "d");
            dg.AddDependency("c", "d");
            List<string> toAdd = new List<string>();
            toAdd.Add("e");
            toAdd.Add("f");
            toAdd.Add("g");
            toAdd.Add("h");
            toAdd.Add("i");
            dg.ReplaceDependees("d", toAdd);
            IEnumerator<string> iterator = dg.GetDependees("d").GetEnumerator();
            int count = 0;
            while (iterator.MoveNext())
            {
                count++;
            }
            Assert.AreEqual(5, count);
        }

        /// <summary>
        /// Tests for null throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullThrows()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency(null, null);
            dg.RemoveDependency(null, null);
            dg.GetDependees(null);
            dg.GetDependents(null);
            dg.HasDependees(null);
            dg.HasDependents(null);
            dg.ReplaceDependees(null, null);
            dg.ReplaceDependents(null, null);
            DependencyGraph dg2 = new DependencyGraph(null);
        }

        /// <summary>
        /// Tests new constructor.
        /// </summary>
        [TestMethod]
        public void NewConstructor()
        {
            DependencyGraph dg1 = new DependencyGraph();
            dg1.AddDependency("a", "b");
            dg1.AddDependency("a", "c");
            dg1.AddDependency("a", "d");
            dg1.AddDependency("b", "c");
            dg1.AddDependency("b", "d");
            dg1.AddDependency("c", "d");
            DependencyGraph dg2 = new DependencyGraph(dg1);
            dg1.RemoveDependency("a", "c");
            Assert.AreEqual(5, dg1.Size);
            Assert.AreEqual(6, dg2.Size);
            dg2.AddDependency("c", "e");
            dg2.AddDependency("c", "f");
            Assert.AreEqual(5, dg1.Size);
            Assert.AreEqual(8, dg2.Size);
        }

        /// <summary>
        /// Tests operation of a large dependency graph.
        /// </summary>
        [TestMethod]
        public void StressTest()
        {
            DependencyGraph dg = new DependencyGraph();
            Random random = new Random();
            for (int i = 0; i < 100000; i++)
            {
                char char1 = (char)((random.NextDouble() * 25) + 97);
                char char2 = (char)((random.NextDouble() * 25) + 97);
                dg.AddDependency("" + char1, "" + char2);
            }
            List<string> toReplace = new List<string>();
            toReplace.Add("a");
            toReplace.Add("b");
            toReplace.Add("c");
            for (int i = 0; i < 100000; i++)
            {
                char char1 = (char)((random.NextDouble() * 25) + 97);
                char char2 = (char)((random.NextDouble() * 25) + 97);
                if (dg.HasDependents("" + char1))
                {
                    dg.ReplaceDependents("" + char1, toReplace);
                }
                if (dg.HasDependees("" + char1))
                {
                    dg.ReplaceDependees("" + char1, toReplace);
                }
                dg.RemoveDependency("" + char1, "" + char2);
            }
            for (int i = 0; i < 100000; i++)
            {
                char char1 = (char)((random.NextDouble() * 25) + 97);
                char char2 = (char)((random.NextDouble() * 25) + 97);
                dg.RemoveDependency("" + char1, "" + char2);
            }
            for (int i = 0; i < 100000; i++)
            {
                char char1 = (char)((random.NextDouble() * 25) + 97);
                char char2 = (char)((random.NextDouble() * 25) + 97);
                dg.AddDependency("" + char1, "" + char2);
            }
            toReplace = new List<string>();
            toReplace.Add("a");
            toReplace.Add("b");
            toReplace.Add("c");
            for (int i = 0; i < 100000; i++)
            {
                char char1 = (char)((random.NextDouble() * 25) + 97);
                char char2 = (char)((random.NextDouble() * 25) + 97);
                if (dg.HasDependents("" + char1))
                {
                    dg.ReplaceDependents("" + char1, toReplace);
                }
                if (dg.HasDependees("" + char1))
                {
                    dg.ReplaceDependees("" + char1, toReplace);
                }
                dg.RemoveDependency("" + char1, "" + char2);
            }
            for (int i = 0; i < 100000; i++)
            {
                char char1 = (char)((random.NextDouble() * 25) + 97);
                char char2 = (char)((random.NextDouble() * 25) + 97);
                dg.RemoveDependency("" + char1, "" + char2);
            }
        }
    }
}
