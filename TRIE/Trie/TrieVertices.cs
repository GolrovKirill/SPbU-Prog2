namespace Trie;

public class TrieVertices
{
    private class Vertex
    {
        /// <summary>
        /// Gets or sets stores the number of words starting with a prefix ending with this vertex.
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        /// Gets or sets creates a dictionary of subversives.
        /// </summary>
        public Dictionary<char, Vertex> SubVertices { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// Fills in the fields of the subversives.
        /// </summary>
        protected internal Vertex()
        {
            this.Flag = false;
            this.Prefix = 0;
            this.SubVertices = new Dictionary<char, Vertex>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether shows whether the top is the end of someone's word.
        /// </summary>
        public bool Flag { get; set; }

        /// <summary>
        /// Used to find a sub-vertex.
        /// </summary>
        /// <returns>If there is such a subverted with this symbol true, otherwise false.</returns>
        public Vertex? FindSubVertex(char symbol)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(symbol);
            return this.SubVertices.TryGetValue(symbol, out var value) ? value : null;
        }
    }

    /// <summary>
    /// Initializing the root.
    /// </summary>
    private Vertex? root = new();

    /// <summary>
    /// Adding a word to Trie.
    /// </summary>
    /// <returns>If you manage to add a word to Trie then true, otherwise false.</returns>
    public bool Add(string element)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (this.Contains(element))
        {
            return false;
        }

        var current = this.root;

        foreach (var symbol in element)
        {
            var subverted = current?.FindSubVertex(symbol);
            if (current != null)
            {
                current.Prefix += 1;

                if (subverted == null)
                {
                    subverted = new Vertex();
                    current.SubVertices.Add(symbol, subverted);
                }
            }

            current = subverted;
        }

        current.Flag = true;
        return current.Flag;
    }

    /// <summary>
    /// Checks if Trie contains a word.
    /// </summary>
    /// <returns>If it contains a word in Trie then true, otherwise false.</returns>
    public bool Contains(string element)
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
    public bool Remove(string element)
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
    public int HowManyStartsWithPrefix(string prefix)
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
}