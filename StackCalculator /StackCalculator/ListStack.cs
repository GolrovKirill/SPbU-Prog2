namespace StackCalculator;

/// <inheritdoc />
public class ListStack<T> : InterfaceStack<T>
{
    private List<T?> stack = [];

    /// <inheritdoc/>
    public T? Pop()
    {
        if (Count())
        {
            throw new InvalidOperationException("Try take element from empty stack");
        }

        var num = stack[^1];
        stack.Remove(stack.Last());
        return num;
    }

    /// <inheritdoc/>
    public void Push(T? element)
    {
        stack.Add(element);
    }

    /// <inheritdoc/>
    public bool Count()
    {
        return stack.Count == 0;
    }
}