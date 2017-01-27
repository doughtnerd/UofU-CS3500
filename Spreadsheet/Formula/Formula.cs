// Skeleton written by Joe Zachary for CS 3500, January 2017

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Formulas
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public class Formula
    {
        private string formula;
        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using C#-like syntax for double/int literals), 
        /// variable symbols (a letter followed by zero or more letters and/or digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// Examples of a valid parameter to this constructor are:
        ///     "2.5e9 + x5 / 17"
        ///     "(5 * 2) + 8"
        ///     "x*y-2+35/9"
        ///     
        /// Examples of invalid parameters are:
        ///     "_"
        ///     "-5.3"
        ///     "2 5 + 3"
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula)
        {
            IEnumerator<string> iterator = GetTokens(formula).GetEnumerator();
            int numberOfOpeningParentheses = 0, numberOfClosingParentheses = 0;
            bool checkNextType1 = false, checkNextType2 = false;
            double test;
            if (!iterator.MoveNext())
            {
                throw new FormulaFormatException("More information needed.");
            }
            
            if (!(double.TryParse(iterator.Current, out test) || Char.IsLetter(iterator.Current.ToCharArray()[0]) || iterator.Current.Equals("(")))
            {
                throw new FormulaFormatException("Invalid first character.");
            }
            while (iterator.MoveNext())
            {
                if (double.TryParse(iterator.Current, out test))
                {
                    //is number
                }
                else if (iterator.Current.Length != 1)
                {
                    throw new FormulaFormatException("Invalid character.");
                }
                if (checkNextType1)
                {
                    if (!(double.TryParse(iterator.Current, out test) || Char.IsLetter(iterator.Current.ToCharArray()[0]) || iterator.Current.Equals('(')))
                    {
                        throw new FormulaFormatException("Expected a number, a variable, or an opening parentheses to follow an opening parentheses or an operator.");
                    }
                }
                if (checkNextType2)
                {
                    if (!(iterator.Current.Equals('+') || iterator.Current.Equals('-') || iterator.Current.Equals('*') || iterator.Current.Equals('/') || iterator.Current.Equals(')')))
                    {
                        throw new FormulaFormatException("Expected an operator or a closing parentheses to follow a number, a variable, or a closing parentheses.");
                    }
                }
                if (iterator.Current.Equals('+') || iterator.Current.Equals('-') || iterator.Current.Equals('*') || iterator.Current.Equals('/'))
                {
                    checkNextType1 = true;
                }
                if (double.TryParse(iterator.Current, out test) || Char.IsLetter(iterator.Current.ToCharArray()[0]))
                {
                    checkNextType2 = true;
                }
                if (iterator.Current.Equals('('))
                {
                    checkNextType1 = true;
                    numberOfOpeningParentheses++;
                }
                if (iterator.Current.Equals(')'))
                {
                    checkNextType2 = true;
                    numberOfClosingParentheses++;
                }
                if (numberOfClosingParentheses > numberOfOpeningParentheses)
                {
                    throw new FormulaFormatException("Number of closing parentheses exceeded number of opening parentheses.");
                }
            }
            if (numberOfOpeningParentheses != numberOfClosingParentheses)
            {
                throw new FormulaFormatException("Number of opening parentheses does not match number of closing parentheses.");
            }
            if (!(double.TryParse(iterator.Current, out test) || Char.IsLetter(iterator.Current.ToCharArray()[0]) || iterator.Current.Equals(")")))
            {
                throw new FormulaFormatException("Invalid last character.");
            }
            this.formula = formula;
        }
        /// <summary>
        /// Evaluates this Formula, using the Lookup delegate to determine the values of variables.  (The
        /// delegate takes a variable name as a parameter and returns its value (if it has one) or throws
        /// an UndefinedVariableException (otherwise).  Uses the standard precedence rules when doing the evaluation.
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, its value is returned.  Otherwise, throws a FormulaEvaluationException  
        /// with an explanatory Message.
        /// </summary>
        public double Evaluate(Lookup lookup)
        {
            IEnumerator<string> iterator = GetTokens(formula).GetEnumerator();
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            while (iterator.MoveNext())
            {
                double value;
                if (Double.TryParse(iterator.Current, out value))
                {
                    string top = operators.Peek();
                    if (top.Equals("*") || top.Equals("/"))
                    {
                        double first = values.Pop();
                        if (top.Equals("*"))
                        {
                            operators.Pop();
                            values.Push(first * value);
                        }
                        else if (top.Equals("/"))
                        {
                            operators.Pop();
                            values.Push(first / value);
                        }
                    }
                    else
                    {
                        values.Push(value);
                    }
                }
                else if (Char.IsLetter(iterator.Current.ToCharArray()[0]))
                {
                    value = lookup(iterator.Current);
                    string top = operators.Peek();
                    if (top.Equals("*") || top.Equals("/"))
                    {
                        double first = values.Pop();
                        if (top.Equals("*"))
                        {
                            operators.Pop();
                            values.Push(first * value);
                        }
                        else if (top.Equals("/"))
                        {
                            operators.Pop();
                            values.Push(first / value);
                        }
                    }
                    else
                    {
                        values.Push(value);
                    }
                }
                else if (iterator.Current.Equals("+") || iterator.Current.Equals("-"))
                {
                    string top = operators.Peek();
                    if (top.Equals("+") || top.Equals("-"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        if (top.Equals("+"))
                        {
                            values.Push(first + second);
                        }
                        else if (top.Equals("-"))
                        {
                            values.Push(first - second);
                        }
                    }
                    operators.Push(iterator.Current);
                }
                else if (iterator.Current.Equals("*") || iterator.Current.Equals("/"))
                {
                    operators.Push(iterator.Current);
                }
                else if (iterator.Current.Equals("("))
                {
                    operators.Push(iterator.Current);
                }
                else if (iterator.Current.Equals(")"))
                {
                    string top = operators.Peek();
                    if (top.Equals("+") || top.Equals("-"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        if (top.Equals("+"))
                        {
                            values.Push(first + second);
                        }
                        else if (top.Equals("-"))
                        {
                            values.Push(first - second);
                        }
                    }
                    operators.Pop();
                    if (top.Equals("*") || top.Equals("/"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        if (top.Equals("*"))
                        {
                            values.Push(first * second);
                        }
                        else if (top.Equals("/"))
                        {
                            values.Push(first / second);
                        }
                    }
                }
            }
            if (operators.Count != 0)
            {
                string top = operators.Pop();
                double second = values.Pop();
                double first = values.Pop();
                if (top.Equals("+"))
                {
                    return first + second;
                }
                else if (top.Equals("-"))
                {
                    return first - second;
                }
            }
            return values.Pop();
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of a letter followed by
        /// zero or more digits and/or letters, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z][0-9a-zA-Z]*";
            // PLEASE NOTE:  I have added white space to this regex to make it more readable.
            // When the regex is used, it is necessary to include a parameter that says
            // embedded white space should be ignored.  See below for an example of this.
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern.  It contains embedded white space that must be ignored when
            // it is used.  See below for an example of this.
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            // PLEASE NOTE:  Notice the second parameter to Split, which says to ignore embedded white space
            /// in the pattern.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// A Lookup method is one that maps some strings to double values.  Given a string,
    /// such a function can either return a double (meaning that the string maps to the
    /// double) or throw an UndefinedVariableException (meaning that the string is unmapped 
    /// to a value. Exactly how a Lookup method decides which strings map to doubles and which
    /// don't is up to the implementation of the method.
    /// </summary>
    public delegate double Lookup(string var);

    /// <summary>
    /// Used to report that a Lookup delegate is unable to determine the value
    /// of a variable.
    /// </summary>
    [Serializable]
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Constructs an UndefinedVariableException containing whose message is the
        /// undefined variable.
        /// </summary>
        /// <param name="variable"></param>
        public UndefinedVariableException(String variable)
            : base(variable)
        {
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the parameter to the Formula constructor.
    /// </summary>
    [Serializable]
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    [Serializable]
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message) : base(message)
        {
        }
    }
}
