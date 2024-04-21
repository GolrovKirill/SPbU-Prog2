namespace StackCalculator.Tests;

using Moq;

[TestFixture]
public class CalculatorTest
{
    private const double ComparisonAccuracy = 1e-12;

    [Test]
    public void MockTest()
    {
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.SetupSequence(x => x.Count())
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(true);

        mock.SetupSequence(x => x.Pop())
            .Returns(2)
            .Returns(2)
            .Returns(4);

        const string expression = "2 2 *";

        var (res, reply) = calculator.CalculatorOperation(expression);
        Assert.That(reply & Math.Abs(res - 4) < ComparisonAccuracy);
    }

    private static IEnumerable<Calculator> Initialize()
    {
        yield return new Calculator(new ListStack<double>());
        yield return new Calculator(new Array<double>());
    }

    [Test]
    public void EmptyStringCorrect()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.Setup(x => x.Count())
            .Returns(true);

        mock.Setup(x => x.Pop())
            .Returns(0);

        const string exp = "   ";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & res == 0);
    }

    [Test]
    public void CalculatorCorrectMultiplication()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.SetupSequence(x => x.Count())
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(true);

        mock.SetupSequence(x => x.Pop())
            .Returns(6)
            .Returns(5)
            .Returns(30)
            .Returns(4)
            .Returns(120)
            .Returns(3)
            .Returns(360)
            .Returns(2)
            .Returns(720)
            .Returns(1)
            .Returns(720);

        var exp = "1 2 3 4 5 6 * * * * *";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - 720) < ComparisonAccuracy);
    }

    [Test]
    public void CalculatorCorrectSum()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.SetupSequence(x => x.Count())
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(true);

        mock.SetupSequence(x => x.Pop())
            .Returns(0.001)
            .Returns(-0.412)
            .Returns(-0.411)
            .Returns(1.3411)
            .Returns(0.9301);

        var exp = "1,3411 -0,412 0,001 + +";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - 0.9301) < ComparisonAccuracy);
    }

    [Test]
    public void CalculatorCorrectDivision()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.SetupSequence(x => x.Count())
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(true);

        mock.SetupSequence(x => x.Pop())
            .Returns(125)
            .Returns(-0.25)
            .Returns(-500)
            .Returns(5)
            .Returns(-100);

        var exp = "5 -0,25 125 / /";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - (-100)) < ComparisonAccuracy);
    }

    [Test]
    public void CalculatorCorrectSubtraction()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.SetupSequence(x => x.Count())
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(true);

        mock.SetupSequence(x => x.Pop())
            .Returns(-1234.23)
            .Returns(4343)
            .Returns(-5577.23)
            .Returns(0.342)
            .Returns(-5577.572);

        var exp = "0,342 4343 -1234,23 - -";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - (-5577.572)) < ComparisonAccuracy);
    }

    [Test]
    public void OneNumberOneOperationCorrect()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.SetupSequence(x => x.Count())
            .Returns(false)
            .Returns(true);

        var exp = "1 *";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 2) < ComparisonAccuracy);
    }

    [Test]
    public void ZeroNumberOneOperationCorrect()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        mock.Setup(x => x.Count())
            .Returns(true);

        var exp = "*";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 3) < ComparisonAccuracy);
    }

    [Test]
    public void OperationNoCorrect()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        var exp = "1 ^";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 4) < ComparisonAccuracy);
    }

    [Test]
    public void ThreeNumberOneOperationCorrect()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        var exp = "1 2 3 -";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 6) < ComparisonAccuracy);
    }

    [Test]
    public void ZeroNumberZeroOperationCorrect()
    {
        // arrange
        var mock = new Mock<InterfaceStack<double>>();
        var calculator = new Calculator(mock.Object);

        var exp = "0 5 /";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 1) < ComparisonAccuracy);
    }
}