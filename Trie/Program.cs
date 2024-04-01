using static Trie.TrieVertices;

namespace Trie;

class Program
{
    static void Main(string[] args)
    {
        var trie = new TrieVertices();
        Console.WriteLine("Start Trie");
        bool end = true;

        while (end)
        {
            Console.Write("WriteLine: Add - 1, Contains - 2, Remove - 3, StartsWithPrefix - 4, Size - 5, Exit - 6   ");
            var cnt = Console.ReadLine();
            
            switch (cnt)
            {
                case "1":
                    Console.Write("Enter a word: ");
                    var str = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("Input string empty");
                        break;
                    }

                    if (!trie.Add(str))
                    {
                        Console.WriteLine("Error Add");
                    }
                    else
                    {
                        Console.WriteLine("Successfully Add");
                    }
                    
                    break;
                
                case "2":
                    Console.Write("Enter a word: ");
                    str = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("Input string empty");
                        break;
                    }

                    if (!trie.Contains(str))
                    {
                        Console.WriteLine("Not Found");
                    }
                    else
                    {
                        Console.WriteLine("Found");
                    }
                    
                    break;
                
                case "3":
                    Console.Write("Enter a word: ");
                    str = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("Input string empty");
                        break;
                    }

                    if (!trie.Remove(str))
                    {
                        Console.WriteLine("Error Remove");
                    }
                    else
                    {
                        Console.WriteLine("Successfully Remove"); 
                    }
                    
                    break;
                
                case "4":
                    Console.Write("Enter a word: ");
                    str = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("Input string empty");
                        break;
                    }
                    
                    Console.WriteLine(trie.HowManyStartsWithPrefix(str));
                    break;
                
                case "5":
                    Console.WriteLine(trie.Size());
                    break;
                
                case "6":
                    end = false;
                    break;
                
                default:
                    Console.WriteLine("Input Error");
                    break;
            }
        }
    }
}