using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace StackCalculator.Tests;

[TestFixture]
[TestOf(typeof(Array))]
public class ArrayTest
{
    private static IEnumerable<Array> Initialize()
    {
        yield return new Array();
    }

    [TestCaseSource(nameof(Initialize))]
    public void PushCorrect(Array stackArray)
    {
        stackArray.Push(1);
        Assert.That(stackArray != null);
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void Push_and_CountCorrect(Array stackArray)
    {
        stackArray.Push(1);
        Assert.That(!stackArray.Count());
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void CountCorrect(Array stackArray)
    {
        Assert.That(stackArray.Count());
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void PopCorrect(Array stackArray)
    {
        stackArray.Push(1);
        Assert.That(Math.Abs(stackArray.Pop() - 1) < 1e12);
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void PopNullCorrect(Array stackArray)
    {
        var str = Assert.Throws<InvalidOperationException>(() => stackArray.Pop());
        Assert.That(str.Message, Is.EqualTo("Невозможно совершить это действие со стеком"));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void StackCorrect(Array stackArray)
    {
        stackArray.Push(1);
        stackArray.Push(2);
        var first = stackArray.Pop();
        var second = stackArray.Pop();
        Assert.That((Math.Abs(first - 2) < 1e12) && (Math.Abs(second - 1) < 1e12));
    }
    
    [TestCaseSource(nameof(Initialize))]
    public void PushBigNumberCorrect(Array stackArray)
    {
        stackArray.Push(1.8e307);
        Assert.That(Math.Abs(stackArray.Pop() - (1.8e307)) < 1e12);
    }
}