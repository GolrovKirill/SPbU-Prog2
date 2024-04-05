namespace StackCalculator;

/// <inheritdoc />
public class Array : InterfaceStack
{
    private double[] stack = [0];

    private int size = 1;

    private int idtop = -1;

    /// <inheritdoc/>
    public double Pop()
    {
        if (this.Count())
        {
            throw new InvalidOperationException("Try take element from empty stack");
        }

        this.idtop -= 1;
        return this.stack[this.idtop + 1];
    }

    /// <inheritdoc/>
    public void Push(double element)
    {
        this.ArrResize();
        this.idtop += 1;
        this.stack[this.idtop] = element;
    }

    /// <inheritdoc/>
    public bool Count()
    {
        return this.idtop == -1;
    }

    private void ArrResize()
    {
        if ((this.idtop + 1) >= this.size)
        {
            this.size += 1;
            System.Array.Resize(ref this.stack, this.size);
        }
    }
}