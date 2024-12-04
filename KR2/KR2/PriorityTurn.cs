namespace KR2;

public class PriorityTurn<T>
{
    private class Node(T data, int priority)
    {
        public T Data { get; } = data;

        public int Priority { get; } = priority;

        public Node? NextNode { get; set; }
    }

    private Node? head;

    private int maxPriority;

    private Node? maxPriorityNode;

    /// <summary>
    /// Create new node.
    /// </summary>
    /// <param name="data">Value for new node.</param>
    /// <param name="priority">Priority for new node.</param>
    public void Enqueue(T data, int priority)
    {
        var node = new Node(data, priority);

        if (head == null)
        {
            head = node;
            maxPriority = priority;
            maxPriorityNode = node;
        }
        else
        {
            var currentNode = head;

            while (currentNode != null && currentNode.Priority <= priority)
            {
                while (currentNode != null && currentNode.Priority < priority)
                {
                    currentNode = currentNode.NextNode;
                }

                if (currentNode.Priority == priority)
                {
                    node.NextNode = currentNode.NextNode;
                    currentNode.NextNode = node;
                }
                else if (currentNode.Priority > priority)
                {
                    node.NextNode = currentNode;
                    LastNode(currentNode.Priority).NextNode = node;
                }
            }

            if (priority > maxPriority)
            {
                maxPriority = priority;
                maxPriorityNode = node;
            }
        }
    }

    /// <summary>
    /// Returns node with max value and delint this node.
    /// </summary>
    /// <returns>Value node.</returns>
    public T Dequeue()
    {
        var current = maxPriorityNode;
        LastNode(current.Priority).NextNode = maxPriorityNode.NextNode;

        return maxPriorityNode.Data;
    }

    /// <summary>
    /// Check turn is empty or no.
    /// </summary>
    /// <returns>True is empty, else false.</returns>
    public bool Empty() => head == null;

    private Node? LastNode(int priority)
    {
        var current = head;
        Node? last = null;

        if (current == null)
        {
            return null;
        }

        while (current != null && current.Priority <= priority)
        {
            last = current;
            current = current.NextNode;
        }

        return last;
    }
}
