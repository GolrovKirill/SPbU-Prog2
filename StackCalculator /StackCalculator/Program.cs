using static StackCalculator.Calculator;

namespace StackCalculator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите выражение");

        string? str = Console.ReadLine();
        var stackArray = new Calculator(new Array());
        var stackList = new Calculator(new List());

        if (str != null)
        {
            var rez1 = stackArray.CalculatorOperation(str);
            var rez2 = stackList.CalculatorOperation(str);
            
            if ((Math.Abs(rez1) - (Math.Abs(rez2)) < 1e-12))
            {
                Console.WriteLine(rez1);
            }
            else { throw new InvalidOperationException("Разные ответы"); }
        }
        else {throw new InvalidOperationException("Пустая строка");}
    }
}