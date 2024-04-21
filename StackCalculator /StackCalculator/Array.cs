namespace StackCalculator;

/// <inheritdoc />
public class Array<T> : InterfaceStack<T>
{
    private T?[] stack = new T?[1];

    private int size = 1;

    private int idtop = -1;

    /// <inheritdoc/>
    public T Pop()
    {
        if (Count())
        {
            throw new InvalidOperationException("Try take element from empty stack");
        }

        this.idtop -= 1;

        return stack[idtop + 1] ?? throw new InvalidOperationException("Try take element from empty stack");
    }

    /// <inheritdoc/>
    public void Push(T element)
    {
        ArrResize();
        idtop += 1;

        stack[idtop] = element;
    }

    /// <inheritdoc/>
    public bool Count()
    {
        return idtop == -1;
    }

    private void ArrResize()
    {
        if ((idtop + 1) >= size)
        {
            size += 1;
            Array.Resize(ref stack, size);
        }
    }
}