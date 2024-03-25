using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace StackCalculator.Tests;

[TestFixture]
[TestOf(typeof(List))]
public class CalculatorTest
{
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
        //assert
        var str = Assert.Throws<ArgumentException>(() => calculator.CalculatorOperation(exp));
        Assert.That(str.Message, Is.EqualTo("Пустая строка"));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectMultiplication(Calculator calculator)
    {
        // arrange
        var exp = "1 2 3 4 5 6 * * * * *";
        //assert
        Assert.That(Math.Abs(calculator.CalculatorOperation(exp) - 720) < 1e12);
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectSum(Calculator calculator)
    {
        // arrange
        var exp = "1,3411 -0,412 0,001 + +";
        //assert
        Assert.That(Math.Abs(calculator.CalculatorOperation(exp) - 0.9301) < 1e12);
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectDivision(Calculator calculator)
    {
        // arrange
        var exp = "5 -0,25 125 / /";
        //assert
        Assert.That(Math.Abs(calculator.CalculatorOperation(exp) - (-100)) < 1e12);
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void CalculatorCorrectSubtraction(Calculator calculator)
    {
        // arrange
        var exp = "0,342 4343 -1234,23 - -";
        //assert
        Assert.That(Math.Abs(calculator.CalculatorOperation(exp) - (-5577.572)) < 1e12);
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void OneNumberOneOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "1 *";
        //assert
        var str = Assert.Throws<InvalidOperationException>(() => calculator.CalculatorOperation(exp));
        Assert.That(str.Message, Is.EqualTo("В стеке только одно число, невозможно совершить эту операцию"));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void ZeroNumberOneOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "*";
        //assert
        var str = Assert.Throws<InvalidOperationException>(() => calculator.CalculatorOperation(exp));
        Assert.That(str.Message, Is.EqualTo("Стек пуст, невозможно совершить операцию"));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void OperationNoCorrect(Calculator calculator)
    {
        // arrange
        var exp = "1 ^";
        //assert
        var str = Assert.Throws<InvalidOperationException>(() => calculator.CalculatorOperation(exp));
        Assert.That(str.Message, Is.EqualTo("Неизвестный символ"));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void ThreeNumberOneOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "1 2 3 -";
        //assert
        var str = Assert.Throws<InvalidOperationException>(() => calculator.CalculatorOperation(exp));
        Assert.That(str.Message, Is.EqualTo("В стеке осталось больше одного числа"));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void ZeroNumberZeroOperationCorrect(Calculator calculator)
    {
        // arrange
        var exp = "0 5 /";
        //assert
        var str = Assert.Throws<InvalidOperationException>(() => calculator.CalculatorOperation(exp));
        Assert.That(str.Message, Is.EqualTo("Деление на 0"));
    }
}    