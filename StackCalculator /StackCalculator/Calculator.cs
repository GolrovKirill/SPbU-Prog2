using System.Diagnostics;
using System.Text;

namespace StackCalculator;

public class Calculator
{
    private readonly InterfaceStack stack;
    
    public Calculator(InterfaceStack stack)
    {
        this.stack = stack;
    }

    public double CalculatorOperation(string str)
    {
        var exp = str.Split();
        foreach (var element in exp)
        {
            if (double.TryParse(element, out double num))
            {
                this.stack.Push(num);
            }

            else if (element == "+" || element == "-" || element == "*" || element == "/")
            {
                if (!this.stack.Count())
                {
                    double num1 = this.stack.Pop();
                    if (!this.stack.Count())
                    {
                        double num2 = this.stack.Pop();
                        var (num3, rez) = Operation(num1, num2, element);
                        
                        if (rez) { this.stack.Push(num3); }
                    }
                    else { throw new InvalidOperationException("В стеке только одно число, невозможно совершить эту операцию");}
                }
                else { throw new InvalidOperationException("Стек пуст, невозможно совершить операцию");}
            }
            
            else {throw new InvalidOperationException("Неизвестный символ");}
        }
        
        if (!this.stack.Count())
        {
            double tmp = this.stack.Pop();
            if (!this.stack.Count())
            {
                throw new InvalidOperationException("В стеке осталось больше одного числа");
            }

            return tmp;
        }
        
        throw new InvalidOperationException("Стек пуст, программе нечего возвращать");
    }
    
    private (double, bool) Operation(double num1, double num2, string element)
    {
        switch (element)
        {
            case "+":
                return ((num1 + num2), true);
            case "-":
                return ((num1 - num2), true);
            case "*":
                return ((num1 * num2), true);
            case "/":
                return (Math.Abs(num2) < 1e-12) ?  throw new InvalidOperationException("Делние на 0") : ((num1 / num2), true);
            default:
                return (-1, false);
        }
    }
}
