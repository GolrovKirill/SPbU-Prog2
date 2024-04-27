namespace BurrowsWheeler;

internal static class LexicoSort
{
    /// <summary>
    /// Method <c>Sort</c> sorts string array.
    /// </summary>
    /// <param name="arr">String.</param>
    public static void Sort(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < (arr.Length - 1); j++)
            {
                if (string.CompareOrdinal(arr[j], arr[j + 1]) > 0)
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }
    }
}