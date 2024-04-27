namespace Trie;

using static Trie.TrieVertices;

public class Program
{
    private static void Main()
    {
        var trie = new TrieVertices();
        Console.WriteLine("Start Trie");
        var run = false;

        while (!run)
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

                    Console.WriteLine(!trie.Add(str) ? "Error Add" : "Successfully Add");

                    break;

                case "2":
                    Console.Write("Enter a word: ");
                    str = Console.ReadLine();

                    if (string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("Input string empty");
                        break;
                    }

                    Console.WriteLine(!trie.Contains(str) ? "Not Found" : "Found");

                    break;

                case "3":
                    Console.Write("Enter a word: ");
                    str = Console.ReadLine();

                    if (string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("Input string empty");
                        break;
                    }

                    Console.WriteLine(!trie.Remove(str) ? "Error Remove" : "Successfully Remove");

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
                    run = true;
                    break;

                default:
                    Console.WriteLine("Input Error");
                    break;
            }
        }
    }
}