namespace StackCalculator;

/// <inheritdoc />
public class ListStack<T> : InterfaceStack<T>
{
    private List<T?> stack = [];

    /// <inheritdoc/>
    public T Pop()
    {
        if (Count())
        {
            throw new InvalidOperationException("Try take element from empty stack");
        }

        var lastNumber = stack[^1];
        stack.Remove(stack.Last());
        return lastNumber;
    }

    /// <inheritdoc/>
    public void Push(T element)
    {
        stack.Add(element);
    }

    /// <inheritdoc/>
    public bool Count()
    {
        return stack.Count == 0;
    }
}