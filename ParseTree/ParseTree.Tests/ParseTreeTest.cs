using ParseTree;

namespace ParseTree.Tests;

[TestFixture]
[TestOf(typeof(ParseTree))]
public class ParseTreeTest
{
    private const double ComparisonAccuracy = 1e-12;

    [Test]
    public void TestFile1()
    {
        var expression = CorrectionString.Correction(CorrectionString.ReadPath("../../../Tests/test1.txt"));

        var tree = new ParseTree();
        tree.CreatTree(expression);

        Assert.That(Math.Abs((tree.ResultExpression()! - 6)!) < ComparisonAccuracy);
    }

    [Test]
    public void TestFile2()
    {
        Assert.Throws<ArgumentException>(() => CorrectionString.Correction(CorrectionString.ReadPath("../../../Tests/test3.txt")));
    }

    [Test]
    public void TestWithoutFile()
    {
        var expression = CorrectionString.Correction("( * ( + 1 2 ) ( - 5 ( / 21 7 ) ) )");

        var tree = new ParseTree();
        tree.CreatTree(expression);

        Assert.That(Math.Abs((tree.ResultExpression()! - 6)!) < ComparisonAccuracy);
    }

    [Test]
    public void TestWithoutFileWithExeptionOperandCount()
    {
        Assert.Throws<ArgumentException>(() => CorrectionString.Correction("( * ( + 1 2 4 ) ( - 5 ( / 21 7 ) ) )"));
    }

    [Test]
    public void TestWithoutFileWithExeptionBracket()
    {
        Assert.Throws<ArgumentException>(() => CorrectionString.Correction("( * ( + 1 2 ) ( - 5  / 21 7 ) ) )"));
    }

    [Test]
    public void TestWithoutFileWithExeptionCountOperation()
    {
        Assert.Throws<ArgumentException>(() => CorrectionString.Correction("( * ( + + + 1 2 ) ( - 5  / 21 7 ) ) )"));
    }

    [Test]
    public void TestFile1Write()
    {
        var expression = CorrectionString.Correction(CorrectionString.ReadPath("../../../Tests/test1.txt"));

        var tree = new ParseTree();
        tree.CreatTree(expression);

        Assert.That(tree.ExpressionWrite(), Is.EqualTo("( * ( + 1 2 ) ( - 5 ( / 21 7 ) ) )"));
    }
}