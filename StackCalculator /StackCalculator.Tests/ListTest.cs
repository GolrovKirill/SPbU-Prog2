namespace StackCalculator.Tests;

[TestFixture]
[TestOf(typeof(List))]
public class ListTest
{
    private const double ComparisonAccuracy = 1e-12;

    private static IEnumerable<List> Initialize()
    {
        yield return new List();
    }

    [TestCaseSource(nameof(Initialize))]
    public void PushCorrect(List stackList)
    {
        stackList.Push(1);
        Assert.That(stackList != null);
    }

    [TestCaseSource(nameof(Initialize))]
    public void Push_and_CountCorrect(List stackList)
    {
        stackList.Push(1);
        Assert.That(!stackList.Count());
    }

    [TestCaseSource(nameof(Initialize))]
    public void CountCorrect(List stackList)
    {
        Assert.That(stackList.Count());
    }

    [TestCaseSource(nameof(Initialize))]
    public void PopCorrect(List stackList)
    {
        stackList.Push(1);
        Assert.That(Math.Abs(stackList.Pop() - 1) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void PopNullCorrect(List stackList)
    {
        var str = Assert.Throws<InvalidOperationException>(() => stackList.Pop());
        Assert.That(str.Message, Is.EqualTo("Try take element from empty stack"));
    }

    [TestCaseSource(nameof(Initialize))]
    public void StackCorrect(List stackList)
    {
        stackList.Push(1);
        stackList.Push(2);
        var first = stackList.Pop();
        var second = stackList.Pop();
        Assert.That((Math.Abs(first - 2) < ComparisonAccuracy) && (Math.Abs(second - 1) < ComparisonAccuracy));
    }

    [TestCaseSource(nameof(Initialize))]
    public void PushBigNumberCorrect(List stackList)
    {
        const double num = 1.8e307;
        stackList.Push(num);
        Assert.That(Math.Abs(stackList.Pop() - num) < ComparisonAccuracy);
    }
}