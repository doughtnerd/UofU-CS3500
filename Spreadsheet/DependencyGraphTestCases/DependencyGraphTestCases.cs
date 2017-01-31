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
            DependencyGraph test = new DependencyGraph();
        }
        /// <summary>
        /// Tests if the correct size is returned when the dependency graph is empty.
        /// </summary>
        [TestMethod]
        public void SizeZero()
        {
        }
        /// <summary>
        /// Tests if the correct size is returned.
        /// </summary>
        [TestMethod]
        public void Size()
        {
        }
        /// <summary>
        /// Tests if a dependency has dependents when it has dependents.
        /// </summary>
        [TestMethod]
        public void HasDependentsTrue()
        {
        }
        /// <summary>
        /// Tests if a dependency has dependents when it does not have dependents.
        /// </summary>
        [TestMethod]
        public void HasDependentsFalse()
        {
        }
        /// <summary>
        /// Tests if a dependency has dependees when it has dependees.
        /// </summary>
        [TestMethod]
        public void HasDependeesTrue()
        {
        }
        /// <summary>
        /// Tests if a dependency has dependees when it does not have dependees.
        /// </summary>
        [TestMethod]
        public void HasDependeesFalse()
        {
        }
        /// <summary>
        /// Tests if dependents of a dependency are properly returned when there are none.
        /// </summary>
        [TestMethod]
        public void GetDependentsNone()
        {
        }
        /// <summary>
        /// Tests if dependents of a dependency are properly returned.
        /// </summary>
        [TestMethod]
        public void GetDependents()
        {
        }
        /// <summary>
        /// Tests if dependees of a dependency are properly returned when there are none.
        /// </summary>
        [TestMethod]
        public void GetDependeesNone()
        {
        }
        /// <summary>
        /// Tests if dependees of a dependency are properly returned.
        /// </summary>
        [TestMethod]
        public void GetDependees()
        {
        }
        /// <summary>
        /// Tests if a dependency is properly added if dependency does exist.
        /// </summary>
        [TestMethod]
        public void AddDependencyExists()
        {
        }
        /// <summary>
        /// Tests if a dependency is properly added if dependency does not exist.
        /// </summary>
        [TestMethod]
        public void AddDependency()
        {
        }
        /// <summary>
        /// Tests if a dependency is properly removed if dependency does exist.
        /// </summary>
        [TestMethod]
        public void RemoveDependencyExists()
        {
        }
        /// <summary>
        /// Tests if a dependency is properly removed if dependency does not exist.
        /// </summary>
        [TestMethod]
        public void RemoveDependency()
        {
        }
        /// <summary>
        /// Tests if dependents are properly replaced.
        /// </summary>
        [TestMethod]
        public void ReplaceDependents()
        {
        }
        /// <summary>
        /// Tests if dependees are properly replaced.
        /// </summary>
        [TestMethod]
        public void ReplaceDependees()
        {
        }
        /// <summary>
        /// Tests operation of a large dependency graph.
        /// </summary>
        [TestMethod]
        public void StressTest()
        {
        }
    }
}
