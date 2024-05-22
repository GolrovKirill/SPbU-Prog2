using Buble;

namespace Buble.Tests;


public class BubleComparerTest
{

    [Test]
    public void BubbleSortInt()
    {
        var list = new List<int>() { 5, 4, 3, 2, 1 };
        
        SortBuble<int>.BubbleSort(list, Comparer<int>.Default);


        var result = new List<int>() { 1, 2, 3, 4, 5 };
        Assert.That(list, Is.EqualTo(result));
    }

    [Test]
    public void BubbbleSortString()
    {
        var list = new List<string>() { "ha", "haha", "hah" };


        SortBuble<string>.BubbleSort(list, Comparer<string>.Default);


        var result = new List<string>() { "ha", "hah", "haha" };
        Assert.That(list, Is.EqualTo(result));
    }
}