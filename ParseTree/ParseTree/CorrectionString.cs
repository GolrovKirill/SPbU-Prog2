namespace ParseTree;

public class CorrectionString
{
    /// <summary>
    /// Try open and read first line in file.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>First line with expression.</returns>
    /// <exception cref="ArgumentException">Exception open.</exception>
    public static string? ReadPath(string path)
    {
        try
        {
            StreamReader sr = new StreamReader(path);
            return sr.ReadLine();
        }
        catch (Exception e)
        {
            throw new ArgumentException("Exception: " + e.Message);
        }

        throw new ArgumentException("Exception read");
    }

    /// <summary>
    /// Check the input string that it is an expression.
    /// </summary>
    /// <param name="expression">Input string.</param>
    /// <returns>Try correct small exception, else return exception.</returns>
    /// <exception cref="ArgumentException">No correction string.</exception>
    public static string? Correction(string? expression)
    {
        var countBktLeft = 0;
        var countBktRight = 0;
        var countOperation = 0;
        var doubleOperation = 0;
        var countOperandInt = 0;
        string? expressionCorrect = null;

        for (var i = 1; i < expression.Length - 1; i++)
        {
            if (expression[i] is '(' && expression[i + 1] is '+' or '-' or '*' or '/')
            {
                expressionCorrect += "( ";
            }
            else if (char.IsDigit(expression[i]) && expression[i + 1] is ')')
            {
                expressionCorrect += int.Parse(expression[i].ToString()) + ' ';
            }
            else if (expression[i] is '+' or '-' or '*' or '/' && char.IsDigit(expression[i + 1]))
            {
                expressionCorrect += expression[i] + ' ';
            }
            else
            {
                expressionCorrect += expression[i];
            }

            if (expression[i] is '(' || expression[i] is ')')
            {
                countBktLeft += (expression[i] is '(') ? 1 : 0;
                countBktRight += (expression[i] is ')') ? 1 : 0;
                doubleOperation--;
            }

            if (expression[i] is '+' or '-' or '*' or '/')
            {
                countOperation++;
                doubleOperation++;
                if (doubleOperation >= 2)
                {
                    throw new ArgumentException("There is more than one operation in one operand");
                }
            }

            if (char.IsDigit(expression[i]) && expression[i - 1] is ' ')
            {
                countOperandInt++;
            }
        }

        if (countOperation + 1 != countOperandInt)
        {
            throw new ArgumentException($"The number of operations does not match the number of operands {countOperation} : {countOperandInt}");
        }

        if (countBktLeft != countBktRight)
        {
            throw new ArgumentException("The brackets are placed incorrectly");
        }

        return expressionCorrect;
    }
}