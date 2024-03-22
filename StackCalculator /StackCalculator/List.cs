namespace StackCalculator;

public class List : InterfaceStack
{
    private List<double> stack = new List<double>();
    
    public double Pop()
    {
        if (this.Count())
        {
            throw new InvalidOperationException("Невозможно совершить это действие со стеком");
        }

        double num = this.stack[this.stack.Count() - 1];
        this.stack.RemoveAt(this.stack.Count() - 1);
        return num;
    }

    public void Push(double element)
    {
        this.stack.Add(element);
    }

    public bool Count()
    {
        if (!this.stack.Any())
        {
            return true;
        }

        return false;
    }
}