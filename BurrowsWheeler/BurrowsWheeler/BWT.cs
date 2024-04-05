namespace BurrowsWheeler;

public static class BWT
{
    private const int AlphabetSize = 255;

    /// <summary>
    /// Method <c>Coding</c> codes the string.
    /// </summary>
    /// <param name="s">String.</param>
    /// <returns>String with $.</returns>
    public static string Coding(string? s)
    {
        s += "$";
        var arr = new string[s.Length];
        for (int i = 0; i < s.Length; ++i)
        {
            arr[i] = s.Substring(i, s.Length - i);
        }

        LexicoSort.Sort(arr);

        s = "$" + s;
        var str = string.Empty;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < s.Length; j++)
            {
                if (arr[i] == s.Substring(j, s.Length - j))
                {
                    str = string.Concat(str, s[j - 1]);
                }
            }
        }

        return str;
    }

    /// <summary>
    /// Method <c>Decoding</c> decodes the string codes BWT.
    /// </summary>
    /// <param name="s">Codes string.</param>
    /// <returns>Decodes string.</returns>
    public static string Decoding(string? s)
    {
        var quantity = new int[AlphabetSize];
        for (int i = 0; i < s.Length; i++)
        {
            quantity[Convert.ToByte(s[i])] += 1;
        }

        var arr = new int[AlphabetSize];
        int j = Convert.ToByte('$');
        for (int i = j + 1; i < quantity.Length; i++)
        {
            if (quantity[i] != 0)
            {
                arr[i] = arr[j] + quantity[j];
                j = i;
            }
        }

        int[] p = new int[s.Length];
        for (int i = 0; i < p.Length; i++)
        {
            p[i] = arr[Convert.ToByte(s[i])];
            arr[Convert.ToByte(s[i])] += 1;
        }

        var str = string.Empty;
        var id = s.IndexOf('$');
        for (int i = 0; i < (s.Length - 1); ++i)
        {
            str = s[p[id]] + str;
            id = p[id];
        }

        return str;
    }
}
