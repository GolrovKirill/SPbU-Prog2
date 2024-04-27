namespace StackCalculator;

using System.Diagnostics;
using System.Text;

/// <summary>
/// Class <c>Calculator</c> considers the expression.
/// </summary>
public class Calculator
{
    private const double ComparisonAccuracy = 1e-12;

    private readonly InterfaceStack<double> stack;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calculator"/> class.
    /// </summary>
    /// <param name="stack">Stack.</param>
    public Calculator(InterfaceStack<double> stack)
    {
        this.stack = stack;
    }

    /// <summary>
    /// Method <c>CalculatorOperation</c> return result the expression.
    /// </summary>
    /// <param name="str">Expression.</param>
    /// <returns>true if expression correct and result this expression.</returns>
    public (double, bool) CalculatorOperation(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return (0, false);
        }

        var exp = str.Split();
        foreach (var element in exp)
        {
            if (double.TryParse(element, out var num))
            {
                this.stack.Push(num);
            }
            else if (element is "+" or "-" or "*" or "/")
            {
                if (!this.stack.Count())
                {
                    var num1 = this.stack.Pop();
                    if (!this.stack.Count())
                    {
                        var num2 = this.stack.Pop();
                        var (num3, rez) = Operation(num1, num2, element);

                        if (rez)
                        {
                            this.stack.Push(num3);
                        }
                        else
                        {
                            return (1, false);
                        }
                    }
                    else
                    {
                        return (2, false);
                    }
                }
                else
                {
                    return (3, false);
                }
            }
            else
            {
                return (4, false);
            }
        }

        if (this.stack.Count())
        {
            return (5, false);
        }

        var tmp = this.stack.Pop();
        return !this.stack.Count() ? (6, false) : (tmp, true);
    }

    private static (double, bool) Operation(double num1, double num2, string element)
    {
        switch (element)
        {
            case "+":
                return (num1 + num2, true);
            case "-":
                return (num1 - num2, true);
            case "*":
                return (num1 * num2, true);
            case "/":
                return Math.Abs(num2) < ComparisonAccuracy ? (0, false) : (num1 / num2, true);

            default:
                return (-1, false);
        }
    }
}
