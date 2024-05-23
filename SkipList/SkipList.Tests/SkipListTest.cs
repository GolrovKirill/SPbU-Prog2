using SkipList;

namespace SkipList.Tests;

public class SkipListTests
{
    SkipList<int> skiplist;

    [SetUp]
    public void SetUp()
        => skiplist = [];

    [Test]
    public void CountAdd()
    {
        Array.ForEach([1, 2, 4, 8, 16, 32], skiplist.Add);

        const int expectedResultAfterAdd = 6;
        Assert.That(skiplist, Has.Count.EqualTo(expectedResultAfterAdd));
    }

    [Test]
    public void CountRemove()
    {
        Array.ForEach([1, 2, 4, 8, 16, 32], skiplist.Add);
        
        skiplist.Remove(1);
        skiplist.Remove(3);
        skiplist.Remove(8);
        skiplist.Remove(2123);
        
        const int expectedResultAfterRemove = 4;
        Assert.That(skiplist, Has.Count.EqualTo(expectedResultAfterRemove));
    }

    [Test]
    public void IsReadOnly()
        => Assert.That(!skiplist.IsReadOnly);

    [Test]
    public void IndexerAddRemove()
    {
        Array.ForEach([1, 7, 4, 5, 12], skiplist.Add);


        skiplist.Remove(4);
        skiplist.Remove(7);
        skiplist.Remove(10);
        skiplist.Remove(-1);


        var expectedArray = new[] { 1, 5, 12 };
        for (var i = 0; i < expectedArray.Length; ++i)
        {
            Assert.That(skiplist[i], Is.EqualTo(expectedArray[i]));
        }
    }

    [Test]
    public void GetIndex()
    {
        skiplist.Add(1);


        Assert.Throws<ArgumentOutOfRangeException>(() => _ = skiplist[1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = skiplist[-1]);
    }

    [Test]
    public void ContainsEmpty()
        => Assert.That(skiplist, Does.Not.Contain(0));

    [Test]
    public void ContainsAddAndRemove()
    {
        Array.ForEach([9, 7, 5, 12], skiplist.Add);


        skiplist.Remove(9);
        skiplist.Remove(7);


        Assert.That(skiplist, Does.Contain(5));
        Assert.That(skiplist, Does.Contain(12));
        Assert.That(skiplist, Does.Not.Contain(9));
    }

    [Test]
    public void Clear()
    {
        Array.ForEach([9, 7, 5, 12], skiplist.Add);
        
        skiplist.Clear();
        
        Assert.That(skiplist, Has.Count.EqualTo(0));
    }

    [Test]
    public void CopyTo()
    {
        Array.ForEach([1, 7, 4, 5, 12], skiplist.Add);


        var array = new int[7];
        skiplist.CopyTo(array, 2);


        var expectedArray = new int[] { 0, 0, 1, 4, 5, 7, 12 };
        Assert.That(array, Is.EqualTo(expectedArray));
    }

    [Test]
    public void CopyToWithNull()
    {
        int[] array = null!;


        Assert.Throws<ArgumentNullException>(() => skiplist.CopyTo(array, 0));
    }

    [Test]
    public void CopyToSizeException()
    {
        Array.ForEach([1, 7, 4, 5, 12], skiplist.Add);
        var array = new int[7];


        Assert.Throws<ArgumentException>(() => skiplist.CopyTo(array, 6));
    }

    [Test]
    public void CopyToOutOfRangeException()
    {
        Array.ForEach([1, 7, 4, 5, 12], skiplist.Add);
        var array = new int[7];


        Assert.Throws<ArgumentOutOfRangeException>(() => skiplist.CopyTo(array, -1));
    }
    
    [Test]
    public void InsertException()
        => Assert.Throws<NotSupportedException>(() => skiplist.Insert(0, 0));
}