namespace StackCalculator;

/// <summary>
/// Creat interface for works with stack.
/// </summary>
public interface InterfaceStack
{
    /// <summary>
    /// Return and clean the element in stack.
    /// </summary>
    /// <returns>Number.</returns>
    double Pop();

    /// <summary>
    /// Inputs element in stack.
    /// </summary>
    /// <param name="element">Number.</param>
    void Push(double element);

    /// <summary>
    /// Write stack have element.
    /// </summary>
    /// <returns>true if there is at least one element, otherwise false.</returns>
    bool Count();
}