using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dependencies
{
    /// <summary>
    /// Tests the dependency graph class.
    /// </summary>
    [TestClass]
    public class DependencyGraphTestCases
    {
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
            //dg.AddDependency("a", "b");
            //dg.AddDependency("a", "c");
            //dg.AddDependency("a", "d");
            //dg.AddDependency("b", "a");
            //dg.AddDependency("b", "c");
            //dg.AddDependency("b", "d");
            //dg.AddDependency("c", "a");
            //dg.AddDependency("c", "b");
            //dg.AddDependency("c", "d");
            //dg.AddDependency("d", "a");
            //dg.AddDependency("d", "b");
            //dg.AddDependency("d", "c");
            Assert.AreEqual(4, dg.Size);
        }
        /// <summary>
        /// Tests if a dependency has dependents when it has dependents.
        /// </summary>
        [TestMethod]
        public void HasDependentsTrue()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency has dependents when it does not have dependents.
        /// </summary>
        [TestMethod]
        public void HasDependentsFalse()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency has dependees when it has dependees.
        /// </summary>
        [TestMethod]
        public void HasDependeesTrue()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency has dependees when it does not have dependees.
        /// </summary>
        [TestMethod]
        public void HasDependeesFalse()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if dependents of a dependency are properly returned when there are none.
        /// </summary>
        [TestMethod]
        public void GetDependentsNone()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if dependents of a dependency are properly returned.
        /// </summary>
        [TestMethod]
        public void GetDependents()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if dependees of a dependency are properly returned when there are none.
        /// </summary>
        [TestMethod]
        public void GetDependeesNone()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if dependees of a dependency are properly returned.
        /// </summary>
        [TestMethod]
        public void GetDependees()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency is properly added if dependency does exist.
        /// </summary>
        [TestMethod]
        public void AddDependencyExists()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency is properly added if dependency does not exist.
        /// </summary>
        [TestMethod]
        public void AddDependency()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency is properly removed if dependency does exist.
        /// </summary>
        [TestMethod]
        public void RemoveDependencyExists()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if a dependency is properly removed if dependency does not exist.
        /// </summary>
        [TestMethod]
        public void RemoveDependency()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if dependents are properly replaced.
        /// </summary>
        [TestMethod]
        public void ReplaceDependents()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests if dependees are properly replaced.
        /// </summary>
        [TestMethod]
        public void ReplaceDependees()
        {
            DependencyGraph dg = new DependencyGraph();
        }
        /// <summary>
        /// Tests operation of a large dependency graph.
        /// </summary>
        [TestMethod]
        public void StressTest()
        {
            DependencyGraph dg = new DependencyGraph();
        }
    }
}
