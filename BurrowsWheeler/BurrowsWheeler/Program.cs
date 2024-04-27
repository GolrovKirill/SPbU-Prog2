namespace BurrowsWheeler;

internal static class Program
{
    private static void Main()
    {
        Console.Write("Input the string for coding with BWT:");
        var s = Console.ReadLine();
        var strin = BWT.Coding(s);
        var strout = BWT.Decoding(strin);

        Console.WriteLine(strin);
        Console.WriteLine(strout);
    }
}