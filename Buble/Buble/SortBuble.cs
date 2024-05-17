using System.Collections;

namespace Buble;

public static class SortBuble<T>
{
    /// <summary>
    /// Sorts the list based on the entered generic.
    /// </summary>
    /// <param name="list">Input list for sorted.</param>
    /// <param name="comparer">Low how sorted.</param>
    public static void BubbleSort(List<T> list, IComparer<T> comparer)
    {
        for (var i = 0; i < list.Count; i++)
        {
            for (var j = i + 1; j < list.Count; j++)
            {
                if (comparer.Compare(list[j], list[i]) < 0)
                {
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }
        }
    }
}


