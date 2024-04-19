namespace StackCalculator.Tests;

[TestFixture]
[TestOf(typeof(Array))]
public class ArrayTest
{
    private const double ComparisonAccuracy = 1e-12;

    private static IEnumerable<Array<double>> Initialize()
    {
        yield return new Array<double>();
    }

    [TestCaseSource(nameof(Initialize))]
    public void PushCorrect(Array<double> stackArray)
    {
        stackArray.Push(1);
        Assert.That(stackArray != null);
    }

    [TestCaseSource(nameof(Initialize))]
    public void Push_and_CountCorrect(Array<double> stackArray)
    {
        stackArray.Push(1);
        Assert.That(!stackArray.Count());
    }

    [TestCaseSource(nameof(Initialize))]
    public void CountCorrect(Array<double> stackArray)
    {
        Assert.That(stackArray.Count());
    }

    [TestCaseSource(nameof(Initialize))]
    public void PopCorrect(Array<double> stackArray)
    {
        stackArray.Push(1);
        Assert.That(Math.Abs(stackArray.Pop() - 1) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void PopNullCorrect(Array<double> stackArray)
    {
        var str = Assert.Throws<InvalidOperationException>(() => stackArray.Pop());
        Assert.That(str.Message, Is.EqualTo("Try take element from empty stack"));
    }

    [TestCaseSource(nameof(Initialize))]
    public void StackCorrect(Array<double> stackArray)
    {
        stackArray.Push(1);
        stackArray.Push(2);
        var first = stackArray.Pop();
        var second = stackArray.Pop();
        Assert.That((Math.Abs(first - 2) < ComparisonAccuracy) && (Math.Abs(second - 1) < ComparisonAccuracy));
    }

    [TestCaseSource(nameof(Initialize))]
    public void PushBigNumberCorrect(Array<double> stackArray)
    {
        const double num = 1.8e307;
        stackArray.Push(num);
        Assert.That(Math.Abs(stackArray.Pop() - num) < ComparisonAccuracy);
    }
}