using UniqueList;

namespace UniqueList.Tests;

[TestFixture]
[TestOf(typeof(UniqueList<>))]
public class UniqueListTest
{
    private static bool AreEqual(UniqueList<int> list, IReadOnlyList<int> array)
    {
        if (list.Count != array.Count)
        {
            return false;
        }
        
        var cnt = 0;
        
        foreach (var element in list)
        {
            if (element != array[cnt])
            {
                return false;
            }

            cnt++;
        }

        return true;
    }
    
    [Test]
    public void TestUniqueListAdd_andRemove()
    {
        var arr = new int[] { 1, 2, 3, 4, 5 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }
        
        foreach (var element in list)
        {
            list.UniqueRemove(element);
        }
        
        Assert.That(list.IsEmpty);
    }
    
    [Test]
    public void TestUniqueListAdd()
    {
        var arr = new int[] { 1, 2, 3, 4, 5 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }
        
        Assert.That(AreEqual(list, arr));
    }
    
    [Test]
    public void TestUniqueListChange()
    {
        var arr = new int[] { 1, 3, 5, 7, 9 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }
        
        cnt = 0;
        
        foreach (var element in list)
        {
            list.UniqueChange(element + 1, cnt);
            cnt++;
        }
        
        var res = new int[] { 2, 4, 6, 8, 10 };
        
        Assert.That(AreEqual(list, res));
    }
    
    [Test]
    public void TestUniqueListExceptionChange()
    {
        var arr = new int[] { 1, 2, 3, 4, 5 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }
        
        Assert.Throws<ListExceptionsChange>(() => list.UniqueChange(2, 0));
        Assert.Throws<ListExceptionsChange>(() => list.UniqueChange(3, 1));
    }
    
    [Test]
    public void TestUniqueListExceptionAdd()
    {
        var arr = new int[] { 1, 2, 3, 4, 5 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }
        
        Assert.Throws<ListExceptionsAdd>(() => list.UniqueAdd(2, 0));
        Assert.Throws<ListExceptionsAdd>(() => list.UniqueAdd(3, 1));
    }
    
    [Test]
    public void TestUniqueListExceptionRemove()
    {
        var arr = new int[] { 1, 2, 3, 4, 5 };
        var list = new UniqueList<int>();
        var cnt = 0;

        foreach (var element in arr)
        {
            list.UniqueAdd(element, cnt);
            cnt++;
        }
        
        Assert.Throws<ListExceptionsRemove>(() => list.UniqueRemove(7));
    }
}