namespace BurrowsWheeler;

public static class BWT
{
    public static string Coding(string? s)
    {
        s += "$";
        string[] arr = new string[s.Length];
        for (int i = 0; i < s.Length; ++i)
        {
            arr[i] = s.Substring(i, s.Length - i);
        }
        LexicoSort.Sort(arr); //Строим массив подстрок и сортируем его в лексикографическом порядке

        s = "$" + s;
        string str = "";
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < s.Length; j++)
            {
                if (arr[i] == s.Substring(j, s.Length - j))
                {
                    str = string.Concat(str, s[j - 1]); //Берём предшествующие подстрокам символы и собираем их в строку
                }
            }
        }
        return str; //Возвращаем закодировонную строку 
    }

    
    
    
    
    
    public static string Decoding(string? s)
    {
        int[] quantity = new int[255];
        for (int i = 0; i < s.Length; i++)
        {
            quantity[Convert.ToByte(s[i])] += 1; // Посчитали сколько раз встречается каждый символ
        }

        int[] arr = new int[255];
        int j = Convert.ToByte('$');
        for (int i = (j + 1); i < quantity.Length; i++)
        {
            if (quantity[i] != 0)
            {
                arr[i] = arr[j] + quantity[j]; // массив позиций элементов в отсортированной строке 
                j = i;
            }
        }

        int[] p = new int[s.Length];
        for (int i = 0; i < p.Length; i++)
        {
            p[i] = (arr[Convert.ToByte(s[i])]); // строка чисел выражающая перестановки для s
            arr[Convert.ToByte(s[i])] += 1; // выполнив которые декодируем s
        }

        string str = "";
        int id = (s.IndexOf("$", StringComparison.Ordinal));
        for (int i = 0; i < (s.Length - 1); ++i)
        {
            str =  s[p[id]] + str; // выполнили перестановку
            id = p[id];
        }
        return str; // Возвращаем декодированную строку
        
    }
}
