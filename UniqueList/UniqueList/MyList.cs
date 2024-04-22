namespace UniqueList;

using System.Collections;

public class MyList<T> : IEnumerable<T>
{
    protected Node<T>? head;

    private int cnt;

    /// <summary>
    /// Add new element in list.
    /// </summary>
    /// <param name="element">Adding element.</param>
    /// <param name="index">Index element in list.</param>
    public void Add(T element, int index)
    {
        if (index > cnt)
        {
            throw new AggregateException("Out of index");
        }

        var node = new Node<T>(element, index);

        if (head == null)
        {
            head = node;
        }
        else
        {
            var currentNode = head;

            while (currentNode.Next != null || currentNode == head)
            {
                while (currentNode.Index + 1 < index)
                {
                    currentNode = currentNode.Next;
                }

                if (currentNode.Index + 1 == index)
                {
                    node.Next = currentNode.Next;
                    currentNode.Next = node;
                    currentNode = node.Next;
                    if (currentNode == null)
                    {
                        break;
                    }

                    continue;
                }

                currentNode.Index += 1;
                currentNode = currentNode.Next;
            }
        }

        cnt++;
    }

    /// <summary>
    /// Remove element in list.
    /// </summary>
    /// <param name="index">Index element in list.</param>
    public void Remove(int index)
    {
        if (index >= cnt)
        {
            throw new AggregateException("Out of index");
        }

        Node<T>? previous = null;
        var currentNode = head.Next;

        if (index == 0)
        {
            head = currentNode;
            while (currentNode != null)
            {
                currentNode.Index -= 1;
                currentNode = currentNode.Next;
            }
        }
        else
        {
            currentNode = head;

            while (currentNode != null)
            {
                while (currentNode.Index < index)
                {
                    previous = currentNode;
                    currentNode = currentNode.Next;
                }

                if (currentNode.Index == index)
                {
                    previous.Next = currentNode.Next;
                    currentNode = currentNode.Next;
                    if (currentNode == null)
                    {
                        break;
                    }
                }

                currentNode.Index -= 1;
                currentNode = currentNode.Next;
            }
        }

        cnt--;
    }

    /// <summary>
    /// Change element in list.
    /// </summary>
    /// <param name="element">Changing element.</param>
    /// <param name="index">Index element in list.</param>
    public void Change(T element, int index)
    {
        if (index >= cnt)
        {
            throw new AggregateException("Out of index");
        }

        if (head != null && index == 0)
        {
            head.Data = element;
        }
        else
        {
            var currentNode = head;

            while (currentNode.Index <= index)
            {
                while (currentNode.Index < index)
                {
                    currentNode = currentNode.Next;
                }

                if (currentNode.Index != index)
                {
                    continue;
                }

                currentNode.Data = element;
                break;
            }
        }
    }

    /// <summary>
    /// Method for use foreach list.
    /// </summary>
    /// <returns>Node list.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        var currentNode = head;
        while (currentNode != null)
        {
            yield return currentNode.Data;
            currentNode = currentNode.Next;
        }
    }

    /// <summary>
    /// Method for use foreach list.
    /// </summary>
    /// <returns>Enumerable list.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }

    /// <summary>
    /// Gets return count element in list.
    /// </summary>
    public int Count => cnt;

    /// <summary>
    /// Gets a value indicating whether return true if list is empty, else return false.
    /// </summary>
    public bool IsEmpty => cnt == 0;
}