namespace ParseTree;

public class Node
{
    /// <summary>
    /// If operand is integer save number, else save result suboperands.
    /// </summary>
    public double? Result { get; set; }

    /// <summary>
    /// Counts the result of the operands.
    /// </summary>
    /// <returns>Result operation.</returns>
    /// <exception cref="ArgumentException"> If no operation or operand is null.</exception>
    public double? GetResult()
    {
        if (Symbol is null)
        {
            throw new ArgumentException("No operation");
        }

        if (SubOperand1 is not null && SubOperand2 is not null)
        {
            var symbol1 = SubOperand1.Symbol is not null;
            var symbol2 = SubOperand2.Symbol is not null;

            if (symbol1)
            {
                SubOperand1.Result = SubOperand1.GetResult();
            }

            if (symbol2)
            {
                SubOperand2.Result = SubOperand2.GetResult();
            }

            switch (Symbol)
            {
                case '+':
                    return SubOperand1.Result + SubOperand2.Result;
                case '-':
                    return SubOperand1.Result - SubOperand2.Result;
                case '/':
                    return (SubOperand2.Result == 0)
                        ? throw new ArgumentException("Division by zero")
                        : SubOperand1.Result / SubOperand2.Result;
                case '*':
                    return SubOperand1.Result * SubOperand2.Result;
                default:
                    throw new ArgumentException("Invalid character in Symbol");
            }
        }
        else
        {
            throw new ArgumentException("One or two elements are missing for the operation");
        }
    }

    /// <summary>
    /// Operation.
    /// </summary>
    public char? Symbol { get; set; }

    /// <summary>
    /// Next operand, first in expression.
    /// </summary>
    public Node? SubOperand1 { get; set; }

    /// <summary>
    /// Next operand, second in expression.
    /// </summary>
    public Node? SubOperand2 { get; set; }
}