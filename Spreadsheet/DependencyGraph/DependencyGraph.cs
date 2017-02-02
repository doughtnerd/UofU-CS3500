// Skeleton implementation written by Joe Zachary for CS 3500, January 2017.

using System;
using System.Collections.Generic;

namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If t is a string, the set of all strings s such that the dependency (s,t) is in DG 
    ///    is called the dependees of t, which we will denote as dependees(t).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of this class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// All of the methods below require their string parameters to be non-null.  This means that 
    /// the behavior of the method is undefined when a string parameter is null.  
    ///
    /// IMPORTANT IMPLEMENTATION NOTE
    /// 
    /// The simplest way to describe a DependencyGraph and its methods is as a set of dependencies, 
    /// as discussed above.
    /// 
    /// However, physically representing a DependencyGraph as, say, a set of ordered pairs will not
    /// yield an acceptably efficient representation.  DO NOT USE SUCH A REPRESENTATION.
    /// 
    /// You'll need to be more clever than that.  Design a representation that is both easy to work
    /// with as well acceptably efficient according to the guidelines in the PS3 writeup. Some of
    /// the test cases with which you will be graded will create massive DependencyGraphs.  If you
    /// build an inefficient DependencyGraph this week, you will be regretting it for the next month.
    /// </summary>
    public class DependencyGraph
    {

        private Dictionary<int, Dependency> dependencies;

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            dependencies = new Dictionary<int, Dependency>();
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return dependencies.Count; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependents(string s)
        {
            return dependencies[s.GetHashCode()].getDependents().Count > 0;
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependees(string s)
        {
            return dependencies[s.GetHashCode()].getDependees().Count > 0;
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            return dependencies[s.GetHashCode()].getDependents().Values;
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            return dependencies[s.GetHashCode()].getDependees().Values;
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void AddDependency(string s, string t)
        {
            if (!dependencies.ContainsKey(s.GetHashCode()))
            {
                dependencies.Add(s.GetHashCode(), new Dependency(s));
            }
            if (!dependencies[s.GetHashCode()].checkDependents(t))
            {
                dependencies[s.GetHashCode()].addDependent(t);
            }
            if (!dependencies.ContainsKey(t.GetHashCode()))
            {
                dependencies.Add(t.GetHashCode(), new Dependency(t));
            }
            if (!dependencies[t.GetHashCode()].checkDependees(s))
            {
                dependencies[t.GetHashCode()].addDependee(s);
            }
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            if (dependencies.ContainsKey(s.GetHashCode()))
            {
                if (dependencies[s.GetHashCode()].checkDependents(t))
                {
                    dependencies[s.GetHashCode()].removeDependent(t);
                    dependencies[t.GetHashCode()].removeDependee(s);
                }
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            dependencies[s.GetHashCode()].getDependents().Clear();
            IEnumerator<string> iterator = newDependents.GetEnumerator();
            while (iterator.MoveNext())
            {
                AddDependency(s, iterator.Current);
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependees(string t, IEnumerable<string> newDependees)
        {
            dependencies[t.GetHashCode()].getDependees().Clear();
            IEnumerator<string> iterator = newDependees.GetEnumerator();
            while (iterator.MoveNext())
            {
                AddDependency(iterator.Current, t);
            }
        }

        private class Dependency
        {
            private string dependency;
            private Dictionary<int, string> dependents;
            private Dictionary<int, string> dependees;
            public Dependency(string s)
            {
                dependency = s;
                dependents = new Dictionary<int, string>();
                dependees = new Dictionary<int, string>();
            }
            public void addDependent(string s)
            {
                dependents.Add(s.GetHashCode(), s);
            }
            public void addDependee(string s)
            {
                dependees.Add(s.GetHashCode(), s);
            }
            public void removeDependent(string s)
            {
                dependents.Remove(s.GetHashCode());
            }
            public void removeDependee(string s)
            {
                dependees.Remove(s.GetHashCode());
            }
            public Dictionary<int, string> getDependents()
            {
                return dependents;
            }
            public Dictionary<int, string> getDependees()
            {
                return dependees;
            }
            public bool checkDependents(string s)
            {
                return dependents.ContainsKey(s.GetHashCode());
            }
            public bool checkDependees(string s)
            {
                return dependees.ContainsKey(s.GetHashCode());
            }
        }
    }
}
