namespace SkipList;

using System.Collections;

public class SkipList<T> : IList<T>

    where T : IComparable<T>
{
    private const int MaxLevel = 16;

    private readonly Node nil = new(default, default, default);

    private readonly Random random = new();

    private Node downHead;

    private int version;

    private Node head;

    private class Node(T? value, Node? next, Node? down)
    {
        public T? Value { get; set; } = value;

        public Node? Next { get; set; } = next;

        public Node? Down { get; set; } = down;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class.
    /// </summary>
    public SkipList()
    {
        head = new Node(default, nil, default);
        var current = head;
        for (var i = 0; i < MaxLevel - 1; ++i)
        {
            current.Down = new Node(default, nil, default);
            current = current.Down;
        }

        downHead = current;
    }

    /// <inheritdoc/>
    public int Count { get; private set; }

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var current = downHead;

            for (var i = 0; i < index + 1; ++i)
            {
                current = current.Next ?? throw new InvalidOperationException("Exception input index");
            }

            if (current.Value is null)
            {
                throw new InvalidOperationException("Exception input index");
            }

            return current.Value;
        }
        set => throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public void Add(T value)
    {
        var newElement = InsertValue(head, value);
        if (newElement is not null)
        {
            head.Next = new Node(value, nil, newElement);
        }

        ++version;
        ++Count;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        head = new Node(default, nil, default);
        var current = head;
        for (var i = 0; i < MaxLevel - 1; ++i)
        {
            current.Down = new Node(default, nil, default);
        }

        downHead = current;
        ++version;
        Count = 0;
    }

    /// <inheritdoc/>
    public bool Contains(T value)
    {
        var foundValue = FindValue(head, value);
        if (foundValue == downHead)
        {
            return false;
        }

        return value.CompareTo(foundValue.Value) == 0;
    }

    /// <inheritdoc/>
    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("The size exceeds the space for copying");
        }

        var current = downHead.Next;

        while (current != nil)
        {
            if (current is null || current.Value is null)
            {
                throw new InvalidOperationException("The current item being copied is null");
            }

            array[arrayIndex] = current.Value;

            ++arrayIndex;
            current = current.Next;
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
        => new Enumerator(this);

    /// <inheritdoc/>
    public int IndexOf(T value)
    {
        var current = downHead.Next;

        var index = 0;
        while (current != nil)
        {
            if (current is null)
            {
                throw new InvalidOperationException("The desired element is null");
            }

            if (value.CompareTo(current.Value) == 0)
            {
                return index;
            }

            current = current.Next;

            ++index;
        }

        return -1;
    }

    /// <inheritdoc/>
    public void Insert(int index, T value)
        => throw new NotSupportedException();

    /// <inheritdoc/>
    public bool Remove(T value)
    {
        var success = false;
        RemoveValue(head, value, ref success);

        ++version;
        if (success)
        {
            --Count;
        }

        return success;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
        => Remove(this[index]);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => new Enumerator(this);

    private void RemoveValue(Node element, T value, ref bool wasDelete)
    {
        if (element.Next is null)
        {
            throw new InvalidOperationException("Next element is null");
        }

        while (element.Next != nil && value.CompareTo(element.Next.Value) > 0)
        {
            element = element.Next;

            if (element.Next is null)
            {
                throw new InvalidOperationException("Next element is null");
            }
        }

        if (element.Down is not null)
        {
            RemoveValue(element.Down, value, ref wasDelete);
        }

        if (element.Next == nil || value.CompareTo(element.Next.Value) != 0)
        {
            return;
        }

        element.Next = element.Next.Next;
        wasDelete = true;
    }

    private Node? InsertValue(Node result, T value)
    {
        if (result.Next is null)
        {
            throw new InvalidOperationException("Next element is null");
        }

        while (result.Next != nil && value.CompareTo(result.Next.Value) > 0)
        {
            result = result.Next;

            if (result.Next is null)
            {
                throw new InvalidOperationException("Next element is null");
            }
        }

        var downElement = result.Down is null ? null : InsertValue(result.Down, value);

        if (downElement == null && result.Down is not null)
        {
            return null;
        }

        result.Next = new Node(value, result.Next, downElement);

        return FlipCoin() ? result.Next : null;
    }

    private Node FindValue(Node result, T value)
    {
        while (true)
        {
            if (result.Next is null)
            {
                throw new InvalidOperationException("Next element is null");
            }

            while (result.Next != nil && value.CompareTo(result.Next.Value) >= 0)
            {
                result = result.Next;

                if (result.Next is null)
                {
                    throw new InvalidOperationException("Next element is null");
                }
            }

            if (result.Down is null)
            {
                return result;
            }

            result = result.Down;
        }
    }

    private bool FlipCoin() => random.Next() % 2 == 0;

    private class Enumerator : IEnumerator<T>
    {
        private readonly SkipList<T>? skipList;

        private readonly int version;

        private Node? current;

        public Enumerator(SkipList<T> skiplist)
        {
            current = skiplist.downHead;
            this.skipList = skiplist;
            version = this.skipList.version;
        }

        /// <inheritdoc/>
        public object Current
        {
            get
            {
                if (current is null || current.Value is null)
                {
                    throw new InvalidOperationException("The current item being copied is null");
                }

                return current.Value;
            }
        }

        /// <inheritdoc/>
        T IEnumerator<T>.Current
        {
            get
            {
                if (current is null || current.Value is null)
                {
                    throw new InvalidOperationException("The current item being copied is null");
                }

                return current.Value;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
            => GC.SuppressFinalize(this);

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (current is null || skipList is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            if (version != skipList.version)
            {
                throw new InvalidOperationException("Can't modify skiplist during iteration");
            }

            if (current.Next == skipList.nil)
            {
                current = skipList.downHead;
                return false;
            }

            current = current.Next;

            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            if (skipList is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            if (version != skipList.version)
            {
                throw new InvalidOperationException("Can't modify skiplist during iteration");
            }

            current = skipList.downHead;
        }
    }
}
