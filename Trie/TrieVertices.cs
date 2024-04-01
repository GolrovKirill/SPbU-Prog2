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
        /// Fills in the fields of the subvertices
        /// </summary>
        public Vertex()
        {
            Flag = false;
            Prefix = 0;
            SubVertices = new Dictionary<char, Vertex>();
        } 
        
        /// <summary>
        /// Shows whether the top is the end of someone's word
        /// </summary>
        public bool Flag { get; set; } 
        
        /// <summary>
        /// Used to find a sub-vertex
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>If there is such a subvertex with this symbol true, otherwise false</returns>
        public Vertex FindSubVertex(char symbol)
        {
            if (SubVertices.TryGetValue(symbol, out var value))
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
    protected Vertex root = new Vertex(); 
    
    /// <summary>
    /// Adding a word to Trie
    /// </summary>
    /// <param name="element"></param>
    /// <returns>If you manage to add a word to Trie then true, otherwise false</returns>
    public bool Add(string element) 
    {
        if (Contains(element))
        {
            return false;
        }
        
        Vertex current = root;
        
        foreach (var symbol in element)
        {
            var subvertex = current.FindSubVertex(symbol);
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
    /// <param name="element"></param>
    /// <returns>If it contains a word in Trie then true, otherwise false</returns>
    public bool Contains(string element)
    {
        Vertex current = root;
        
        foreach (var symbol in element)
        {
            var subvertex = current.FindSubVertex(symbol);
            
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
    /// <param name="element"></param>
    /// <returns>If you succeed in removing a word from Trie then true, otherwise false</returns>
    public bool Remove(string element)
    {
        if (!Contains(element))
        {
            return false;
        }
        
        Vertex current = root;
        
        foreach (var symbol in element)
        {
            var subvertex = current.FindSubVertex(symbol);
            current.Prefix -= 1;
            current = subvertex;
        }

        current.Flag = false;
        return !current.Flag;
    }
    
    /// <summary>
    /// Counts the number of words starting with a given prefix in Trie
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns>Word count</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        Vertex current = root;
        
        foreach (var symbol in prefix)
        {
            var subvertex = current.FindSubVertex(symbol);
            
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
    /// <returns>Word count</returns>
    public int Size()
    {
        return root.Prefix;
    }
}