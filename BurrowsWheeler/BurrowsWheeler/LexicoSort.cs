namespace BurrowsWheeler;

static class LexicoSort
{
    public static void Sort(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < (arr.Length - 1); j++)
            {
                if (String.CompareOrdinal(arr[j], arr[j + 1]) > 0)
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }
    }
}