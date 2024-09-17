namespace LZW;

/// <summary>
/// Builds a dictionary based on the trie structure.
/// </summary>
public class TrieEncoder
{
    public class Vertex
    {
        /// <summary>
        /// Gets number for LZW.
        /// </summary>
        public int NumberValue { get; }

        /// <summary>
        /// Gets or sets stores the number of words starting with a prefix ending with this vertex.
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        /// Gets or sets creates a dictionary of subvertices.
        /// </summary>
        public Dictionary<byte, Vertex> SubVertices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// Fills in the fields of the subvertices.
        /// </summary>
        public Vertex(int number)
        {
            this.NumberValue = number;
            this.Flag = false;
            this.Prefix = 0;
            this.SubVertices = new Dictionary<byte, Vertex>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether shows whether the top is the end of someone's word.
        /// </summary>
        public bool Flag { get; set; }

        /// <summary>
        /// Used to find a sub-vertex.
        /// </summary>
        /// <returns>If there is such a subvertex with this symbol true, otherwise false.</returns>
        public Vertex? FindSubVertex(byte symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            return this.SubVertices.GetValueOrDefault(symbol);
        }
    }

    private Vertex? root = new Vertex(-1);

    /// <summary>
    /// Adding a word to Trie.
    /// </summary>
    /// <returns>If you manage to add a word to Trie then true, otherwise false.</returns>
    public bool Add(List<byte> element, int number)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (this.Contains(element))
        {
            return false;
        }

        var current = this.root;

        foreach (var symbol in element)
        {
            var subvertex = current?.FindSubVertex(symbol);
            current.Prefix += 1;

            if (subvertex == null)
            {
                subvertex = new Vertex(number);
                current.SubVertices.Add(symbol, subvertex);
            }

            current = subvertex;
        }

        current.Flag = true;
        return current.Flag;
    }

    /// <summary>
    /// Checks if Trie contains a word.
    /// </summary>
    /// <returns>If it contains a word in Trie then true, otherwise false.</returns>
    private bool Contains(IReadOnlyCollection<byte> element)
    {
        ArgumentNullException.ThrowIfNull(element);

        var current = this.root;

        foreach (var subvertex in element.Select(symbol => current?.FindSubVertex(symbol)))
        {
            if (subvertex == null)
            {
                return false;
            }

            current = subvertex;
        }

        return current.Flag;
    }

    /// <summary>
    /// Removes a word from a Trie.
    /// </summary>
    /// <returns>If you succeed in removing a word from Trie then true, otherwise false.</returns>
    public bool Remove(List<byte> element)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (!this.Contains(element))
        {
            return false;
        }

        var current = this.root;

        foreach (var subvertex in element.Select(symbol => current?.FindSubVertex(symbol)))
        {
            current.Prefix -= 1;
            current = subvertex;
        }

        current.Flag = false;
        return !current.Flag;
    }

    /// <summary>
    /// Counts the number of words starting with a given prefix in Trie.
    /// </summary>
    /// <returns>Word count.</returns>
    public int HowManyStartsWithPrefix(List<byte> prefix)
    {
        ArgumentNullException.ThrowIfNull(prefix);

        var current = this.root;

        foreach (var subvertex in prefix.Select(symbol => current?.FindSubVertex(symbol)))
        {
            if (subvertex == null)
            {
                return 0;
            }

            current = subvertex;
        }

        return current.Prefix;
    }

    /// <summary>
    /// Counts how many words there are in a Trie.
    /// </summary>
    /// <returns>Word count.</returns>
    public int Size()
    {
        return this.root.Prefix;
    }

    /// <summary>
    /// Get number of list bites.
    /// </summary>
    /// <returns>Number of element.</returns>
    public int GetNumberValue(List<byte> element)
    {
        ArgumentNullException.ThrowIfNull(element);

        var current = this.root;

        foreach (var subvertex in element.Select(symbol => current?.FindSubVertex(symbol)))
        {
            if (subvertex == null)
            {
                return -1;
            }

            current = subvertex;
        }

        return current.NumberValue;
    }
}