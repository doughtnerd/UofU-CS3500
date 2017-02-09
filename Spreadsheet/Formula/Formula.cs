﻿// Skeleton written by Joe Zachary for CS 3500, January 2017

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
    public struct Formula
    {
        /// <summary>
        /// Holds the formula of the Formula as a string.
        /// </summary>
        private string formula;

        /// <summary>
        /// Holds the variables of the formula in an ISet.
        /// </summary>
        private ISet<string> variables;

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
        public Formula(string formula)
        {
            if (formula == null)
            {
                throw new ArgumentNullException();
            }
            this.formula = null;
            variables = new HashSet<string>();
            CreateFormula(formula, x => x, _ => true);
        }

        /// <summary>
        /// Multi parameter constuctor.
        /// </summary>
        public Formula(string formula, Normalizer normalizer, Validator validator)
        {
            if (formula == null || normalizer == null || validator == null)
            {
                throw new ArgumentNullException();
            }
            this.formula = null;
            variables = new HashSet<string>();
            CreateFormula(formula, normalizer, validator);
        }

        /// <summary>
        /// Creates and validates the formula.
        /// </summary>
        private void CreateFormula(string formula, Normalizer normalizer, Validator validator)
        {
            string newFormula = "";
            IEnumerator<string> iterator = GetTokens(formula).GetEnumerator();
            int numberOfOpeningParentheses = 0, numberOfClosingParentheses = 0;
            bool checkNextType1 = false, checkNextType2 = false;
            if (!iterator.MoveNext())
            {
                throw new FormulaFormatException("More tokens needed.");
            }
            if (!(IsDouble(iterator.Current) || IsVariable(iterator.Current) || iterator.Current.Equals("(")))
            {
                throw new FormulaFormatException("Invalid first token.");
            }
            if (IsDouble(iterator.Current))
            {
                //double
                newFormula += iterator.Current;
                checkNextType2 = true;
            }
            else if (IsVariable(iterator.Current))
            {
                //variable
                if (!IsVariable(normalizer(iterator.Current)))
                {
                    throw new FormulaFormatException("Normalizer not a valid variable.");
                }
                if (!validator(normalizer(iterator.Current)))
                {
                    throw new FormulaFormatException("Variable does not match validator.");
                }
                newFormula += normalizer(iterator.Current);
                variables.Add(normalizer(iterator.Current));
                checkNextType2 = true;
            }
            else if (iterator.Current.Equals("("))
            {
                //opening parenthesis
                numberOfOpeningParentheses++;
                newFormula += iterator.Current;
                checkNextType1 = true;
            }
            while (iterator.MoveNext())
            {
                if (IsDouble(iterator.Current))
                {
                    //double
                    if (checkNextType1)
                    {
                        checkNextType1 = false;
                    }
                    else if (checkNextType2)
                    {
                        throw new FormulaFormatException("Invalid token combination.");
                    }
                    newFormula += iterator.Current;
                    checkNextType2 = true;
                }
                else if (IsVariable(iterator.Current))
                {
                    //variable
                    if (checkNextType1)
                    {
                        checkNextType1 = false;
                    }
                    else if (checkNextType2)
                    {
                        throw new FormulaFormatException("Invalid token combination.");
                    }
                    if (!IsVariable(normalizer(iterator.Current)))
                    {
                        throw new FormulaFormatException("Normalizer not a valid variable.");
                    }
                    if (!validator(normalizer(iterator.Current)))
                    {
                        throw new FormulaFormatException("Variable does not match validator.");
                    }
                    newFormula += normalizer(iterator.Current);
                    variables.Add(normalizer(iterator.Current));
                    checkNextType2 = true;
                }
                else if (iterator.Current.Equals("("))
                {
                    //opening parenthesis
                    numberOfOpeningParentheses++;
                    if (checkNextType1)
                    {
                        checkNextType1 = false;
                    }
                    else if (checkNextType2)
                    {
                        throw new FormulaFormatException("Invalid token combination.");
                    }
                    newFormula += iterator.Current;
                    checkNextType1 = true;
                }
                else if (iterator.Current.Equals(")"))
                {
                    //closing parenthesis
                    numberOfClosingParentheses++;
                    if (numberOfClosingParentheses > numberOfOpeningParentheses)
                    {
                        throw new FormulaFormatException("Number of closing parentheses exceeded number of opening parentheses.");
                    }
                    else if (checkNextType1)
                    {
                        throw new FormulaFormatException("Invalid token combination.");
                    }
                    else if (checkNextType2)
                    {
                        checkNextType2 = false;
                    }
                    newFormula += iterator.Current;
                    checkNextType2 = true;
                }
                else if (iterator.Current.Equals("+") || iterator.Current.Equals("-") || iterator.Current.Equals("*") || iterator.Current.Equals("/"))
                {
                    //operator
                    if (checkNextType1)
                    {
                        throw new FormulaFormatException("Invalid token combination.");
                    }
                    if (checkNextType2)
                    {
                        checkNextType2 = false;
                    }
                    newFormula += iterator.Current;
                    checkNextType1 = true;
                }
                else
                {
                    //invalid token
                    throw new FormulaFormatException("Invalid token.");
                }
            }
            if (!(IsDouble(iterator.Current) || IsVariable(iterator.Current) || iterator.Current.Equals(")")))
            {
                throw new FormulaFormatException("Invalid last token.");
            }
            if (numberOfOpeningParentheses != numberOfClosingParentheses)
            {
                throw new FormulaFormatException("Number of opening parentheses does not match number of closing parentheses.");
            }
            this.formula = newFormula;
        }

        /// <summary>
        /// Returns each distinct variable (in normalized form) that appears in the Formula.
        /// </summary>
        public ISet<string> GetVariables()
        {
            return variables;
        }

        /// <summary>
        /// Tests if a string is a double.
        /// </summary>
        private bool IsDouble(string formulaPart)
        {
            double result;
            return double.TryParse(formulaPart, out result);
        }
        /// <summary>
        /// Tests if a string is a variable.
        /// </summary>
        private bool IsVariable(string formulaPart)
        {
            if (char.IsLetter(formulaPart.ToCharArray()[0]))
            {
                foreach (char character in formulaPart)
                {
                    if (!char.IsLetterOrDigit(character))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a string version of the Formula (in normalized form).
        /// </summary>
        public override string ToString()
        {
            if (formula == null)
            {
                return "0";
            }
            return formula;
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
            if (lookup == null)
            {
                throw new ArgumentNullException();
            }
            if (formula == null)
            {
                formula = "0";
            }
            IEnumerator<string> iterator = GetTokens(formula).GetEnumerator();
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            while (iterator.MoveNext())
            {
                if (IsDouble(iterator.Current))
                {
                    if ((operators.Count != 0) && operators.Peek().Equals("*"))
                    {
                        operators.Pop();
                        values.Push(values.Pop() * double.Parse(iterator.Current));
                    }
                    else if ((operators.Count != 0) && operators.Peek().Equals("/"))
                    {
                        if (double.Parse(iterator.Current) == 0)
                        {
                            throw new FormulaEvaluationException("Divide by zero error.");
                        }
                        operators.Pop();
                        values.Push(values.Pop() / double.Parse(iterator.Current));
                    }
                    else
                    {
                        values.Push(double.Parse(iterator.Current));
                    }
                }
                else if (IsVariable(iterator.Current))
                {
                    try
                    {
                        lookup(iterator.Current);
                    }
                    catch (UndefinedVariableException)
                    {
                        throw new FormulaEvaluationException("Undefined variable.");
                    }
                    if ((operators.Count != 0) && operators.Peek().Equals("*"))
                    {
                        operators.Pop();
                        values.Push(values.Pop() * lookup(iterator.Current));
                    }
                    else if ((operators.Count != 0) && operators.Peek().Equals("/"))
                    {
                        if (lookup(iterator.Current) == 0)
                        {
                            throw new FormulaEvaluationException("Divide by zero error.");
                        }
                        operators.Pop();
                        values.Push(values.Pop() / lookup(iterator.Current));
                    }
                    else
                    {
                        values.Push(lookup(iterator.Current));
                    }
                }
                else if (iterator.Current.Equals("+") || iterator.Current.Equals("-"))
                {
                    if ((operators.Count != 0) && operators.Peek().Equals("+"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        operators.Pop();
                        values.Push(first + second);
                    }
                    else if ((operators.Count != 0) && operators.Peek().Equals("-"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        operators.Pop();
                        values.Push(first - second);
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
                    if ((operators.Count != 0) && operators.Peek().Equals("+"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        operators.Pop();
                        values.Push(first + second);
                    }
                    else if ((operators.Count != 0) && operators.Peek().Equals("-"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        operators.Pop();
                        values.Push(first - second);
                    }
                    operators.Pop();
                    if ((operators.Count != 0) && operators.Peek().Equals("*"))
                    {
                        double second = values.Pop();
                        double first = values.Pop();
                        operators.Pop();
                        values.Push(first * second);
                    }
                    else if ((operators.Count != 0) && operators.Peek().Equals("/"))
                    {
                        double second = values.Pop();
                        if (second == 0)
                        {
                            throw new FormulaEvaluationException("Divide by zero error.");
                        }
                        double first = values.Pop();
                        operators.Pop();
                        values.Push(first / second);
                    }
                }
            }
            if (operators.Count != 0)
            {
                if (operators.Peek().Equals("+"))
                {
                    double second = values.Pop();
                    double first = values.Pop();
                    operators.Pop();
                    return first + second;
                }
                else if (operators.Peek().Equals("-"))
                {
                    double second = values.Pop();
                    double first = values.Pop();
                    operators.Pop();
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
    /// Converts variables into a canonical form.
    /// </summary>
    public delegate string Normalizer(string s);

    /// <summary>
    /// Imposes extra restrictions on the validity of a variable, beyond the ones already built
    /// into the Formula definition.
    /// </summary>
    public delegate bool Validator(string s);

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
