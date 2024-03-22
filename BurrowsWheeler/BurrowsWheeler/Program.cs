namespace BurrowsWheeler;

static class Program
{
    static void Main( )
    {
        string? s = Console.ReadLine();
        string strin = BWT.Coding(s);
        string strout = BWT.Decoding((strin));
        
        Console.WriteLine(strin);
        Console.WriteLine(strout);
    }
}