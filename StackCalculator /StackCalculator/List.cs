namespace StackCalculator;

/// <inheritdoc />
public class List : InterfaceStack
{
    private List<double> stack = [];

    /// <inheritdoc/>
    public double Pop()
    {
        if (this.Count())
        {
            throw new InvalidOperationException("Try take element from empty stack");
        }

        var num = this.stack[^1];
        this.stack.Remove(this.stack.Last());
        return num;
    }

    /// <inheritdoc/>
    public void Push(double element)
    {
        this.stack.Add(element);
    }

    /// <inheritdoc/>
    public bool Count()
    {
        return this.stack.Count == 0;
    }
}