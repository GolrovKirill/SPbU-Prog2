namespace UniqueList;

public class Node<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Node{T}"/> class.
    /// </summary>
    /// <param name="element">Data node.</param>
    /// <param name="index">Index node in list.</param>
    public Node(T element, int index)
    {
        Data = element;
        Index = index;
    }

    /// <summary>
    /// Gets or sets save data node.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Gets or sets save index node in list.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets create new node.
    /// </summary>
    public Node<T>? Next { get; set; }
}