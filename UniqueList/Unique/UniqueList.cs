namespace Unique;

/// <summary>
/// List unique elements.
/// </summary>
/// <typeparam name="T">Type elements.</typeparam>
public class UniqueList<T> : MyList<T>
{
    private (bool, int) Contains(T element)
    {
        var current = head;

        while (current != null && current.Data != null)
        {
            if (current.Data.Equals(element))
            {
                return (true, current.Index);
            }

            current = current.Next;
        }

        return (false, 0);
    }

    /// <summary>
    /// Add new unique element in list.
    /// </summary>
    /// <param name="element">Adding element.</param>
    /// <param name="newIndex">Index element in list.</param>
    /// <exception cref="ListExceptionsAdd">Return exception if element contains in list.</exception>
    public override void Add(T element, int newIndex)
    {
        var (res, index) = Contains(element);

        if (res)
        {
            throw new ListExceptionsAdd("This element is in list");
        }

        base.Add(element, newIndex);
    }

    /// <summary>
    /// Remove unique element in list.
    /// </summary>
    /// <param name="element">Removing element.</param>
    /// <exception cref="ListExceptionsRemove">Return exception if element no contains in list.</exception>
    public override void Remove(T element)
    {
        var (res, index) = Contains(element);

        if (!res)
        {
            throw new ListExceptionsRemove("This element have not in list");
        }

        base.Remove(element);
    }

    /// <summary>
    /// Change unique element in list.
    /// </summary>
    /// <param name="element">Changing element.</param>
    /// <param name="newIndex">Index element in list.</param>
    /// <exception cref="ListExceptionsChange">Return exception if element contains in list.</exception>
    public override void Change(T element, int newIndex)
    {
        var (res, index) = Contains(element);

        if (res)
        {
            throw new ListExceptionsChange("This element have in list");
        }

        base.Change(element, newIndex);
    }
}