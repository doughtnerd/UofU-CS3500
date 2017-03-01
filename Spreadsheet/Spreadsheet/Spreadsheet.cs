using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Formulas;
using Dependencies;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace SS
{
    /// <summary>
    /// A Spreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of a regular expression (called IsValid below) and an infinite 
    /// number of named cells.
    /// 
    /// A string is a valid cell name if and only if (1) s consists of one or more letters, 
    /// followed by a non-zero digit, followed by zero or more digits AND (2) the C#
    /// expression IsValid.IsMatch(s.ToUpper()) is true.
    /// 
    /// For example, "A15", "a15", "XY32", and "BC7" are valid cell names, so long as they also
    /// are accepted by IsValid.  On the other hand, "Z", "X07", and "hello" are not valid cell 
    /// names, regardless of IsValid.
    /// 
    /// Any valid incoming cell name, whether passed as a parameter or embedded in a formula,
    /// must be normalized by converting all letters to upper case before it is used by this 
    /// this spreadsheet.  For example, the Formula "x3+a5" should be normalize to "X3+A5" before 
    /// use.  Similarly, all cell names and Formulas that are returned or written to a file must also
    /// be normalized.
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  
    /// In addition to a name, each cell has a contents and a value.  The distinction is
    /// important, and it is important that you understand the distinction and use
    /// the right term when writing code, writing comments, and asking questions.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In an empty spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError.
    /// The value of a Formula, of course, can depend on the values of variables.  The value 
    /// of a Formula variable is the value of the spreadsheet cell it names (if that cell's 
    /// value is a double) or is undefined (otherwise).  If a Formula depends on an undefined
    /// variable or on a division by zero, its value is a FormulaError.  Otherwise, its value
    /// is a double, as specified in Formula.Evaluate.
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Holds the created cells of the spreadsheet.
        /// </summary>
        private Dictionary<int, Cell> cells;

        /// <summary>
        /// Tracks the dependencies between cells of the spreadsheet.
        /// </summary>
        private DependencyGraph dg;

        /// <summary>
        /// Represents a valid cell name.
        /// </summary>
        private Regex validCellName = new Regex(@"^[a-zA-Z]+[1-9][0-9]*$");

        /// <summary>
        /// Tracks if the spreadsheet was modified.
        /// </summary>
        private bool _Changed;

        /// <summary>
        /// The spreadsheet's validator for cell names.
        /// </summary>
        private Regex _IsValid;

        /// <summary>
        /// Creates an empty Spreadsheet whose IsValid regular expression accepts every string.
        /// </summary>
        public Spreadsheet()
        {
            cells = new Dictionary<int, Cell>();
            dg = new DependencyGraph();
            IsValid = new Regex(@"^.*$");
            Changed = false;
        }

        /// <summary>
        /// Creates an empty Spreadsheet whose IsValid regular expression is provided as the parameter.
        /// </summary>
        public Spreadsheet(Regex isValid)
        {
            cells = new Dictionary<int, Cell>();
            dg = new DependencyGraph();
            IsValid = isValid;
            Changed = false;
        }

        /// <summary>
        /// Creates a Spreadsheet that is a duplicate of the spreadsheet saved in source.
        /// 
        /// See the AbstractSpreadsheet.Save method and Spreadsheet.xsd for the file format specification.
        /// 
        /// If there is a problem reading the source, throws an IOException.
        /// 
        /// Else if the contents of source are not consistent with the schema in Spreadsheet.xsd,
        /// throws a SpreadsheetReadException.
        /// 
        /// Else if the IsValid string contained in source is not a valid C# regular expression, throws
        /// a SpreadsheetReadException. (If the exception is not thrown, this regex is reffered to
        /// below as oldIsValid.)
        /// 
        /// Else if there is a duplicate cell name in the source, throws a SpreadsheetReadException.
        /// (Two cell names are duplicates if they are identical after being converted to upper case.)
        /// 
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a
        /// SpreadsheetReadException. (Use oldIsValid in place of IsValid in the definition of
        /// cell name validity.)
        /// 
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a
        /// SpreadsheetVersionException. (Use newIsValid in place of IsValid in the definition of
        /// cell name validity.)
        /// 
        /// Else if there's a formula that causes circular dependency, throws a SpreadsheetReadException.
        /// 
        /// Else, create a Spreadsheet that is a duplicate of the one encoded in source except that
        /// the new Spreadsheet's IsValid regular expression should be newIsValid.
        /// </summary>
        public Spreadsheet(TextReader source, Regex newIsValid)
        {
            cells = new Dictionary<int, Cell>();
            dg = new DependencyGraph();
            Regex oldIsValid = null;
            // Create the XmlSchemaSet class.  Anything with the namespace "urn:spreadsheet-schema" will
            // be validated against Spreadsheet.xsd.
            XmlSchemaSet sc = new XmlSchemaSet();

            // NOTE: To read Spreadsheet.xsd this way, it must be stored in the same folder with the
            // executable.  To arrange this, I set the "Copy to Output Directory" propery of Spreadsheet.xsd to
            // "Copy If Newer", which will copy Spreadsheet.xsd as part of each build (if it has changed
            // since the last build).
            sc.Add(null, "Spreadsheet.xsd");

            // Configure validation.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            settings.ValidationEventHandler += ValidationCallback;

            using (XmlReader reader = XmlReader.Create(source, settings))
            {
                //try
                //{
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "spreadsheet":
                                    try
                                    {
                                        oldIsValid = new Regex(reader["IsValid"]);
                                    }
                                    catch (Exception e)
                                    {
                                        throw new SpreadsheetReadException(e.Message);
                                    }
                                    break;

                                case "cell":
                                    if (cells.ContainsKey(reader["name"].ToUpper().GetHashCode()))
                                    {
                                        throw new SpreadsheetReadException("Duplicate cell names in file.");
                                    }
                                    try
                                    {
                                        IsValid = oldIsValid;
                                        SetContentsOfCell(reader["name"], reader["contents"]);
                                    }
                                    catch (Exception e)
                                    {
                                        throw new SpreadsheetReadException(e.Message);
                                    }
                                    try
                                    {
                                        IsValid = newIsValid;
                                        SetContentsOfCell(reader["name"], reader["contents"]);
                                    }
                                    catch (Exception e)
                                    {
                                        throw new SpreadsheetVersionException(e.Message);
                                    }
                                    break;
                            }

                        }
                    }
                //}
                //catch (Exception)
                //{
                //    throw new IOException();
                //}
            }
            Changed = false;
        }

        // Throw any validation errors.
        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            throw new SpreadsheetReadException(e.Message);
        }

        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed
        {
            get
            {
                return _Changed;
            }
            protected set
            {
                _Changed = value;
            }
        }

        /// <summary>
        /// Represents a validator for cell names.
        /// </summary>
        public Regex IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                _IsValid = value;
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            if (name == null || !validCellName.IsMatch(name) || !IsValid.IsMatch(name.ToUpper()))
            {
                throw new InvalidNameException();
            }
            if (cells.ContainsKey(name.ToUpper().GetHashCode()))
            {
                return cells[name.ToUpper().GetHashCode()].Content;
            }
            return "";
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            if (name == null || !validCellName.IsMatch(name) || !IsValid.IsMatch(name.ToUpper()))
            {
                throw new InvalidNameException();
            }
            if (cells.ContainsKey(name.ToUpper().GetHashCode()))
            {
                return cells[name.ToUpper().GetHashCode()].Value;
            }
            return "";
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            List<string> list = new List<string>();
            foreach (Cell c in cells.Values)
            {
                if (!c.Content.Equals(""))
                {
                    list.Add(c.Name);
                }
            }
            return list;
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to dest using an XML format.
        /// The XML elements should be structured as follows:
        ///
        /// <spreadsheet IsValid="IsValid regex goes here">
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        /// </spreadsheet>
        ///
        /// The value of the IsValid attribute should be IsValid.ToString()
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.
        /// If the cell contains a string, the string (without surrounding double quotes) should be written as the contents.
        /// If the cell contains a double d, d.ToString() should be written as the contents.
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        ///
        /// If there are any problems writing to dest, the method should throw an IOException.
        /// </summary>
        public override void Save(TextWriter dest)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create(dest, settings))
            {
                try
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("", "spreadsheet", "urn:spreadsheet-schema");
                    writer.WriteAttributeString("IsValid", IsValid.ToString());
                    foreach (Cell c in cells.Values)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteAttributeString("name", c.Name);
                        if (c.Content.GetType() == typeof(string))
                        {
                            writer.WriteAttributeString("contents", ((string)c.Content));
                        }
                        else if (c.Content.GetType() == typeof(double))
                        {
                            writer.WriteAttributeString("contents", ((double)c.Content).ToString());
                        }
                        else if (c.Content.GetType() == typeof(Formula))
                        {
                            writer.WriteAttributeString("contents", "=" + ((Formula)c.Content).ToString());
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                catch (Exception)
                {
                    throw new IOException();
                }
            }
            Changed = false;
        }

        /// <summary>
        /// Requires that all of the variables in formula are valid cell names.
        /// 
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, Formula formula)
        {
            if (name == null || !validCellName.IsMatch(name) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            HashSet<string> set = new HashSet<string>();
            IEnumerator<string> iterator = GetCellsToRecalculate(name).GetEnumerator();
            while (iterator.MoveNext())
            {
                set.Add(iterator.Current);
            }
            // check for circular dependency
            iterator = formula.GetVariables().GetEnumerator();
            while (iterator.MoveNext())
            {
                if (validCellName.IsMatch(iterator.Current) && IsValid.IsMatch(iterator.Current))
                {
                    if (set.Contains(iterator.Current))
                    {
                        throw new CircularException();
                    }
                }
                else
                {
                    throw new InvalidNameException();
                }
            }
            // update dependency graph as needed
            // clear relationship between cell (name) and its old dependents
            HashSet<string> set2 = new HashSet<string>();
            iterator = dg.GetDependents(name).GetEnumerator();
            while (iterator.MoveNext())
            {
                set2.Add(iterator.Current);
            }
            iterator = set2.GetEnumerator();
            while (iterator.MoveNext())
            {
                dg.RemoveDependency(name, iterator.Current);
            }
            // add relationships between cell (name) and its new dependents
            iterator = formula.GetVariables().GetEnumerator();
            while (iterator.MoveNext())
            {
                dg.AddDependency(name, iterator.Current);
            }
            // update cell contents and value
            if (!cells.ContainsKey(name.GetHashCode()))
            {
                try
                {
                    cells.Add(name.GetHashCode(), new Cell(name, formula, formula.Evaluate(s => ((double)cells[s.ToUpper().GetHashCode()].Value))));
                }
                catch (Exception e)
                {
                    cells.Add(name.GetHashCode(), new Cell(name, formula, new FormulaError(e.Message)));
                }
            }
            else
            {
                cells[name.GetHashCode()].Content = formula;
                try
                {
                    cells[name.GetHashCode()].Value = formula.Evaluate(s => ((double)cells[s.ToUpper().GetHashCode()].Value));
                }
                catch (Exception e)
                {
                    cells[name.GetHashCode()].Value = new FormulaError(e.Message);
                }
            }
            //update value of other cells
            iterator = set.GetEnumerator();
            iterator.MoveNext();
            while (iterator.MoveNext())
            {
                try
                {
                    cells[iterator.Current.GetHashCode()].Value = ((Formula)cells[iterator.Current.GetHashCode()].Content).Evaluate(s => ((double)cells[s.ToUpper().GetHashCode()].Value));
                }
                catch (Exception e)
                {
                    cells[iterator.Current.GetHashCode()].Value = new FormulaError(e.Message);
                }
            }
            Changed = true;
            return set;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }
            if (name == null || !validCellName.IsMatch(name) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            HashSet<string> set = new HashSet<string>();
            IEnumerator<string> iterator = GetCellsToRecalculate(name).GetEnumerator();
            while (iterator.MoveNext())
            {
                set.Add(iterator.Current);
            }
            // update dependency graph as needed
            // clear relationship between cell (name) and its dependents
            HashSet<string> set2 = new HashSet<string>();
            iterator = dg.GetDependents(name).GetEnumerator();
            while (iterator.MoveNext())
            {
                set2.Add(iterator.Current);
            }
            iterator = set2.GetEnumerator();
            while (iterator.MoveNext())
            {
                dg.RemoveDependency(name, iterator.Current);
            }
            // update cell contents and value
            if (!cells.ContainsKey(name.GetHashCode()))
            {
                cells.Add(name.GetHashCode(), new Cell(name, text, text));
            }
            else
            {
                cells[name.GetHashCode()].Content = text;
                cells[name.GetHashCode()].Value = text;
            }
            //update value of other cells
            iterator = set.GetEnumerator();
            iterator.MoveNext();
            while (iterator.MoveNext())
            {
                try
                {
                    cells[iterator.Current.GetHashCode()].Value = ((Formula)cells[iterator.Current.GetHashCode()].Content).Evaluate(s => ((double)cells[s.ToUpper().GetHashCode()].Value));
                }
                catch (Exception e)
                {
                    cells[iterator.Current.GetHashCode()].Value = new FormulaError(e.Message);
                }
            }
            Changed = true;
            return set;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, double number)
        {
            if (name == null || !validCellName.IsMatch(name) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            HashSet<string> set = new HashSet<string>();
            IEnumerator<string> iterator = GetCellsToRecalculate(name).GetEnumerator();
            while (iterator.MoveNext())
            {
                set.Add(iterator.Current);
            }
            // update dependency graph as needed
            // clear relationship between cell (name) and its dependents
            HashSet<string> set2 = new HashSet<string>();
            iterator = dg.GetDependents(name).GetEnumerator();
            while (iterator.MoveNext())
            {
                set2.Add(iterator.Current);
            }
            iterator = set2.GetEnumerator();
            while (iterator.MoveNext())
            {
                dg.RemoveDependency(name, iterator.Current);
            }
            // update cell contents and value
            if (!cells.ContainsKey(name.GetHashCode()))
            {
                cells.Add(name.GetHashCode(), new Cell(name, number, number));
            }
            else
            {
                cells[name.GetHashCode()].Content = number;
                cells[name.GetHashCode()].Value = number;
            }
            //update value of other cells
            iterator = set.GetEnumerator();
            iterator.MoveNext();
            while (iterator.MoveNext())
            {
                try
                {
                    cells[iterator.Current.GetHashCode()].Value = ((Formula)cells[iterator.Current.GetHashCode()].Content).Evaluate(s => ((double)cells[s.ToUpper().GetHashCode()].Value));
                }
                catch (Exception e)
                {
                    cells[iterator.Current.GetHashCode()].Value = new FormulaError(e.Message);
                }
            }
            Changed = true;
            return set;
        }

        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        ///
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        ///
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor with s => s.ToUpper() as the normalizer and a validator that
        /// checks that s is a valid cell name as defined in the AbstractSpreadsheet
        /// class comment.  There are then three possibilities:
        ///
        ///   (1) If the remainder of content cannot be parsed into a Formula, a
        ///       Formulas.FormulaFormatException is thrown.
        ///
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///
        ///   (3) Otherwise, the contents of the named cell becomes f.
        ///
        /// Otherwise, the contents of the named cell becomes content.
        ///
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        ///
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            if (name == null || !validCellName.IsMatch(name) || !IsValid.IsMatch(name.ToUpper()))
            {
                throw new InvalidNameException();
            }
            double parse;
            if (double.TryParse(content, out parse))
            {
                return SetCellContents(name.ToUpper(), parse);
            }
            if (content.StartsWith("="))
            {
                Formula f = new Formula(content.Remove(0, 1), s => s.ToUpper(), s => (validCellName.IsMatch(s) && IsValid.IsMatch(s)));
                return SetCellContents(name.ToUpper(), f);
            }
            return SetCellContents(name.ToUpper(), content);
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            if (!validCellName.IsMatch(name) || !IsValid.IsMatch(name.ToUpper()))
            {
                throw new InvalidNameException();
            }
            return dg.GetDependees(name.ToUpper());
        }

        /// <summary>
        /// Represents a cell of a spreadsheet.
        /// </summary>
        private class Cell
        {
            /// <summary>
            /// Content of cell.
            /// </summary>
            private object _Content;

            /// <summary>
            /// Name of cell.
            /// </summary>
            private string _Name;

            /// <summary>
            /// Value of cell.
            /// </summary>
            private object _Value;

            /// <summary>
            /// Creates a cell with the provided name and content.
            /// </summary>
            public Cell(string cellName, object cellContent, object cellValue)
            {
                Name = cellName.ToUpper();
                Content = cellContent;
                Value = cellValue;
            }

            /// <summary>
            /// Holds the content of the cell.
            /// </summary>
            public object Content {
                get
                {
                    return _Content;
                }
                set
                {
                    _Content = value;
                }
            }

            /// <summary>
            /// Holds the valid name of the cell.
            /// </summary>
            public string Name
            {
                get
                {
                    return _Name;
                }
                set
                {
                    _Name = value;
                }
            }

            /// <summary>
            /// Holds the value of the cell.
            /// </summary>
            public object Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    _Value = value;
                }
            }
        }
    }
}
