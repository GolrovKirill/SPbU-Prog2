namespace MapFilterFold;

public class MapFilterFold
{
    /// <summary>
    /// Functions for Map method.
    /// </summary>
    /// <typeparam name="TK">Type input value.</typeparam>
    /// <typeparam name="T">Type output value.</typeparam>
    /// <returns>Value witn new type.</returns>
    public delegate T FunctionsMap<in TK, out T>(TK value);

    /// <summary>
    /// Functions for Filter method.
    /// </summary>
    /// <typeparam name="T">Type input value.</typeparam>
    /// <returns>True if value need, else false.</returns>
    public delegate bool FunctionsFilter<in T>(T value);

    /// <summary>
    /// Functions for Fold method.
    /// </summary>
    /// <typeparam name="TK">Type input value.</typeparam>
    /// <returns>Current value.</returns>
    public delegate TK FunctionsFold<TK>(TK value, TK current);

    /// <summary>
    /// Create new list with output values FunctionsMap.
    /// </summary>
    /// <param name="list">Input list.</param>
    /// <param name="func">Functions return new value.</param>
    /// <typeparam name="TK">Type value in input list.</typeparam>
    /// <typeparam name="T">Type new value in output list.</typeparam>
    /// <returns>List with value new type.</returns>
    public static List<T> Map<TK, T>(List<TK> list,  FunctionsMap<TK, T> func)
    {
        List<T> resList = [];

        foreach (var element in list)
        {
            resList.Add(func(element));
        }

        return resList;
    }

    /// <summary>
    /// Create new list with need values from FunctionsFilter.
    /// </summary>
    /// <param name="list">Input list.</param>
    /// <param name="func">Functions return true if value need, else false.</param>
    /// <typeparam name="T">Type value.</typeparam>
    /// <returns>List with value need type.</returns>
    public static List<T> Filter<T>(List<T> list, FunctionsFilter<T> func)
    {
        List<T> resList = [];

        foreach (var element in list)
        {
            if (func(element))
            {
                resList.Add(element);
            }
        }

        return resList;
    }

    /// <summary>
    /// Return  saveValue after use FunctionsFold on list.
    /// </summary>
    /// <param name="list">Input list.</param>
    /// <param name="saveValue">Value after use FunctionsFold.</param>
    /// <param name="func">Updates value saveValue.</param>
    /// <typeparam name="TK">Type saveValue and element from list.</typeparam>
    /// <returns>Value after use FunctionsFold and element from list.</returns>
    public static TK Fold<TK>(List<TK> list, TK saveValue, FunctionsFold<TK> func)
    {
        foreach (var element in list)
        {
            saveValue = func(element, saveValue);
        }

        return saveValue;
    }
}