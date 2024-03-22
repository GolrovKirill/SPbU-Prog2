namespace StackCalculator;

public class Array : InterfaceStack
{
    private double[] stack = new double[1] {0};
    
    private int size = 1;

    private int idtop = -1;
    
    public double Pop()
    {
        if (Count())
        {
            throw new InvalidOperationException("Невозможно совершить это действие со стеком");
        }

        this.idtop -= 1;
        return this.stack[(this.idtop + 1)];
    }
    
    public void Push(double element)
    {
        this.ArrResize();
        this.idtop += 1;
        this.stack[this.idtop] = element;
    }
    public bool Count()
    {
        if (this.idtop == -1)
        {
            return true;
        }

        return false;
    }
    private void ArrResize() // изменение размера стека
    {
        if ((this.idtop + 1) >= this.size)
        {
            this.size += 1;
            System.Array.Resize(ref this.stack, this.size);
        }
    }
}