namespace Trie;

public class TrieVertices
{
    public class Vertex
    {
        /// <summary>
        /// Stores the number of words starting with a prefix ending with this vertex
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        /// Creates a dictionary of subvertices
        /// </summary>
        public Dictionary<char, Vertex> SubVertices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// Fills in the fields of the subvertices
        /// </summary>
        public Vertex()
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
        /// Used to find a sub-vertex
        /// </summary>
        /// <returns>If there is such a subvertex with this symbol true, otherwise false</returns>
        public Vertex? FindSubVertex(char symbol)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(symbol);
            if (this.SubVertices.TryGetValue(symbol, out var value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Initializing the root
    /// </summary>
    protected Vertex? root = new Vertex();

    /// <summary>
    /// Adding a word to Trie
    /// </summary>
    /// <returns>If you manage to add a word to Trie then true, otherwise false.</returns>
    public bool Add(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        if (this.Contains(element))
        {
            return false;
        }

        Vertex? current = this.root;

        foreach (var symbol in element)
        {
            var subvertex = current?.FindSubVertex(symbol);
            current.Prefix += 1;

            if (subvertex == null)
            {
                subvertex = new Vertex();
                current.SubVertices.Add(symbol, subvertex);
            }

            current = subvertex;
        }

        current.Flag = true;
        return current.Flag;
    }

    /// <summary>
    /// Checks if Trie contains a word
    /// </summary>
    /// <returns>If it contains a word in Trie then true, otherwise false</returns>
    public bool Contains(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        Vertex? current = this.root;

        foreach (var symbol in element)
        {
            var subvertex = current?.FindSubVertex(symbol);

            if (subvertex == null)
            {
                return false;
            }

            current = subvertex;
        }

        return current.Flag;
    }

    /// <summary>
    /// Removes a word from a Trie
    /// </summary>
    /// <returns>If you succeed in removing a word from Trie then true, otherwise false.</returns>
    public bool Remove(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        if (!this.Contains(element))
        {
            return false;
        }

        Vertex? current = this.root;

        foreach (var symbol in element)
        {
            var subvertex = current?.FindSubVertex(symbol);
            current.Prefix -= 1;
            current = subvertex;
        }

        current.Flag = false;
        return !current.Flag;
    }

    /// <summary>
    /// Counts the number of words starting with a given prefix in Trie
    /// </summary>
    /// <returns>Word count.</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        if (prefix == null)
        {
            throw new ArgumentNullException(nameof(prefix));
        }

        Vertex? current = this.root;

        foreach (var symbol in prefix)
        {
            var subvertex = current?.FindSubVertex(symbol);

            if (subvertex == null)
            {
                return 0;
            }

            current = subvertex;
        }

        return current.Prefix;
    }

    /// <summary>
    /// Counts how many words there are in a Trie
    /// </summary>
    /// <returns>Word count.</returns>
    public int Size()
    {
        return this.root.Prefix;
    }
}