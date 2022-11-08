using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class ExprCalculator
    {
        static char[] operators = new char[] { '-', '+', '/', '*' };

        public static float Calculate(string s)
        {
            float operand = 0;
            float n = 0;
            Stack<object> stack = new Stack<object>();

            for (int i = s.Length - 1; i >= 0; i--)
            {
                char ch = s[i];
                if (Char.IsDigit(ch))
                {
                    // Forming the operand - in reverse order.
                    operand = (float)Math.Pow(10, n) * (float)(ch - '0') + operand;
                    n += 1;
                }
                else
                {
                    if (n != 0)
                    {
                        // Save the operand on the stack
                        // As we encounter some non-digit.
                        stack.Push(operand);
                        n = 0;
                        operand = 0;
                    }
                    if (ch == '(')
                    {
                        float res = EvaluateBrackets(stack);
                        stack.Pop();

                        // Append the evaluated result to the stack.
                        // This result could be of a sub-expression within the parenthesis.
                        stack.Push(res);
                    }
                    else
                    {
                        // For other non-digits just push onto the stack.
                        stack.Push(ch);
                    }
                }
            }

            //Push the last operand to stack, if any.
            if (n != 0)
            {
                stack.Push(operand);
            }

            // Evaluate any left overs in the stack.
            return EvaluateBrackets(stack);
        }

        static float EvaluateBrackets(Stack<object> stack)
        {
            // If stack is empty or the expression starts with
            // a symbol, then append 0 to the stack.
            // i.e. [1, '-', 2, '-'] becomes [1, '-', 2, '-', 0]
            if (stack.Count == 0 || IsSymbol(stack.Peek()))
            {
                stack.Push(0);
            }

            Stack<object> forwardStack = new Stack<object>();

            // Evaluate the expression till we get corresponding ')'
            while (stack.Count > 0 && !IsClosingBracket(stack.Peek()))
            {
                if (IsOperator(stack.Peek()))
                {
                    char oper = (char)stack.Pop();
                    if (oper == '/')
                    {
                        float operand = (float)forwardStack.Pop();
                        operand /= (float)stack.Pop();
                        forwardStack.Push(operand);
                    }
                    else if (oper == '*')
                    {
                        float operand = (float)forwardStack.Pop();
                        operand *= (float)stack.Pop();
                        forwardStack.Push(operand);
                    }
                    else
                    {
                        forwardStack.Push(oper);
                    }
                }
                else
                {
                    forwardStack.Push(stack.Pop());
                }
            }

            while (forwardStack.Count > 0)
            {
                stack.Push(forwardStack.Pop());
            }

            float res = (float)stack.Pop();

            // Evaluate the expression till we get corresponding ')'
            while (stack.Count > 0 && !IsClosingBracket(stack.Peek()))
            {
                char sign = (char)stack.Pop();

                if (sign == '+')
                {
                    res += (float)stack.Pop();
                }
                else
                {
                    res -= (float)stack.Pop();
                }
            }
            return res;
        }

        static bool IsOperand(object obj)
        {
            return obj is float;
        }

       static bool IsOperator(object obj)
        {
            return obj is char && operators.Contains((char)obj);
        }

        static bool IsClosingBracket(object obj)
        {
            return obj is char && (char)obj == ')';
        }

        static bool IsSymbol(object obj)
        {
            return obj is char;
        }
    }
}
