namespace StackCalculator.Tests;

[TestFixture]
[TestOf(typeof(List))]
public class CalculatorTest
{
    private const double ComparisonAccuracy = 1e-12;

    private static IEnumerable<Calculator> Initialize()
    {
        yield return new Calculator(new List());
        yield return new Calculator(new Array());
    }

    [TestCaseSource(nameof(Initialize))]
    public void EmptyStringCorrect(Calculator calculator)
    {
        // arrange
        var exp = "   ";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & res == 0);
    }

    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectMultiplication(Calculator calculator)
    {
        // arrange
        var exp = "1 2 3 4 5 6 * * * * *";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - 720) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectSum(Calculator calculator)
    {
        // arrange
        var exp = "1,3411 -0,412 0,001 + +";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - 0.9301) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectDivision(Calculator calculator)
    {
        // arrange
        var exp = "5 -0,25 125 / /";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - (-100)) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectSubtraction(Calculator calculator)
    {
        // arrange
        var exp = "0,342 4343 -1234,23 - -";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(reply & Math.Abs(res - (-5577.572)) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void OneNumberOneOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "1 *";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 2) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void ZeroNumberOneOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "*";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 3) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void OperationNoCorrect(Calculator calculator)
    {
        // arrange
        var exp = "1 ^";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 4) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void ThreeNumberOneOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "1 2 3 -";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 6) < ComparisonAccuracy);
    }

    [TestCaseSource(nameof(Initialize))]
    public void ZeroNumberZeroOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "0 5 /";

        // assert
        var (res, reply) = calculator.CalculatorOperation(exp);
        Assert.That(!reply & Math.Abs(res - 1) < ComparisonAccuracy);
    }
}