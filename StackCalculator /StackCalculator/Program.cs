namespace StackCalculator;

using System.Linq.Expressions;
using static StackCalculator.Calculator;

internal class Program
{
    private const double ComparisonAccuracy = 1e-12;

    private static void Main(string[] args)
    {
        var stackArray = new Calculator(new Array());
        var stackList = new Calculator(new List());
        var end = true;

        while (end)
        {
            Console.WriteLine("Input expression or press enter for end program:");
            var expression = Console.ReadLine();

            if (expression != string.Empty)
            {
                var (replyArray, resultArray) = stackArray.CalculatorOperation(expression);
                var (replyList, resultList) = stackList.CalculatorOperation(expression);

                if (resultList & resultArray)
                {
                    Console.WriteLine(Math.Abs(replyArray - replyList) < ComparisonAccuracy
                        ? $"{replyArray}"
                        : "Error, the result in the array stack is not equal to the result in the list stack");
                    Console.WriteLine(replyArray);
                    Console.WriteLine(replyList);
                }
                else if (!resultList & !resultArray)
                {
                    switch (replyArray)
                    {
                        case 0:
                            Console.WriteLine("String with expression Empty");
                            break;
                        case 1:
                            Console.WriteLine("In expression division by zero");
                            break;
                        case 2:
                            Console.WriteLine("There is only one number on the stack, it is impossible to perform this operation");
                            break;
                        case 3:
                            Console.WriteLine("Stack is empty, no operation can be performed");
                            break;
                        case 4:
                            Console.WriteLine("An unknown symbol");
                            stackArray = new Calculator(new Array());
                            stackList = new Calculator(new List());
                            break;
                        case 5:
                            Console.WriteLine("The stack is empty, the program has nothing to return");
                            break;
                        case 6:
                            Console.WriteLine("There is more than one number left on the stack");
                            break;
                    }
                }
            }
            else
            {
                end = false;
            }
        }
    }
}