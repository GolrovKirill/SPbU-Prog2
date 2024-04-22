namespace UniqueList;


public class Program
{
    private static void Main()
    {
        var arr = new int[] { 1, 2, 3, 4, 5 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }

        list.UniqueAdd(2, 0);
        list.UniqueAdd(3, 1);

        foreach (var element in list)
        {
            Console.Write($"{element} ");
        }
    }
}