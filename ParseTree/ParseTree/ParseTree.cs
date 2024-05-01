using System.Collections;
using System.Linq.Expressions;
using System.Text;

namespace ParseTree;

public class ParseTree
{
    private Node? head = new Node();

    private void ReadSymbol(string expression, Node node)
    {
        foreach (var symbol in expression)
        {
            if ((symbol is '+' or '-' or '/' or '*') && node.Symbol is null)
            {
                node.Symbol = symbol;
                break;
            }
        }
    }

    private void ReadExpression(string expression, Node operand)
    {
        string? firstNumber = null;
        string? secondNumber = null;
        double? number1 = null;
        double? number2 = null;
        var space = false;
        var countBkt = 0;
        var i = 1;

        ReadSymbol(expression, operand);

        while (i < expression.Length - 1)
        {
            if (countBkt == 0 && expression[i] != '(' && expression[i] != ')' && char.IsDigit(expression[i]))
            {
                if (!space && firstNumber is null)
                {
                    if (number1 is null)
                    {
                        number1 = int.Parse(expression[i].ToString());
                    }
                    else
                    {
                        number1 = (number1 * 10) + int.Parse(expression[i].ToString());
                    }

                    space = (expression[i + 1] is ' ');
                }
                else if (space && secondNumber is null)
                {
                    if (number2 is null)
                    {
                        number2 = int.Parse(expression[i].ToString());
                    }
                    else
                    {
                        number2 = (number2 * 10) + int.Parse(expression[i].ToString());
                    }
                }
            }

            if ((countBkt is not 0 && (char.IsDigit(expression[i]) || expression[i] is '+' or '-' or '/' or '*' or ' ')) || expression[i] is '(' || expression[i] is ')')
            {
                if (!space && number1 is null)
                {
                    firstNumber += expression[i];
                }
                else if (space && number2 is null)
                {
                    secondNumber += expression[i];
                }

                if (expression[i] is '(')
                {
                    countBkt++;
                }
                else if (expression[i] is ')')
                {
                    countBkt--;

                    if (countBkt == 0 && (expression[i + 1] is ' '))
                    {
                        space = true;
                    }
                }
            }

            i++;
        }

        if (number1 is not null)
        {
            var operandNew1 = new Node()
            {
                Result = number1,
            };
            operand.SubOperand1 = operandNew1;
        }
        else if (firstNumber is not null)
        {
            var operandNew1 = new Node();
            ReadExpression(firstNumber, operandNew1);
            operand.SubOperand1 = operandNew1;
        }

        if (number2 is not null)
        {
            var operandNew2 = new Node()
            {
                Result = number2,
            };
            operand.SubOperand2 = operandNew2;
        }
        else if (secondNumber is not null)
        {
            var operandNew2 = new Node();
            ReadExpression(secondNumber, operandNew2);
            operand.SubOperand2 = operandNew2;
        }
    }

    private string? WriteExpression(Node operand)
    {
        if (operand.Symbol is null)
        {
            throw new ArgumentException("Expression is number");
        }

        string? expression = null;
        string? subExpression;

        if (operand.SubOperand1.Symbol is null && operand.SubOperand2.Symbol is null && operand.Symbol is not null)
        {
            expression = $"( {operand.Symbol} {operand.SubOperand1.Result} {operand.SubOperand2.Result} )";
        }
        else if (operand.SubOperand1.Symbol is not null && operand.SubOperand2.Symbol is null && operand.Symbol is not null)
        {
            subExpression = WriteExpression(operand.SubOperand1);
            expression = $"( {operand.Symbol} {subExpression} {operand.SubOperand2.Result} )";
        }
        else if (operand.SubOperand1.Symbol is null && operand.SubOperand2.Symbol is not null && operand.Symbol is not null)
        {
            subExpression = WriteExpression(operand.SubOperand2);
            expression = $"( {operand.Symbol} {operand.SubOperand1.Result} {subExpression} )";
        }
        else if (operand.SubOperand1.Symbol is not null && operand.SubOperand2.Symbol is not null && operand.Symbol is not null)
        {
            var subExpression1 = WriteExpression(operand.SubOperand1);
            var subExpression2 = WriteExpression(operand.SubOperand2);
            expression = $"( {operand.Symbol} {subExpression1} {subExpression2} )";
        }

        return expression;
    }

    /// <summary>
    /// The value of the tree according to the input expression.
    /// </summary>
    /// <param name="expression">Input string with expression.</param>
    public void CreatTree(string expression)
    {
        ReadExpression(expression, head);
    }

    /// <summary>
    /// Displays result the expression used to construct the tree.
    /// </summary>
    /// <returns>Result expression.</returns>
    public double ResultExpression()
    {
        if (head.GetResult() is null)
        {
            return 0;
        }
        else
        {
            return (double)head.GetResult();
        }
    }

    /// <summary>
    /// Displays the expression used to construct the tree.
    /// </summary>
    /// <returns>Expression.</returns>
    public string? ExpressionWrite()
    {
        return WriteExpression(head);
    }
}