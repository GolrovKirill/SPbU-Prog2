// <copyright file="TrieVerticesTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Trie.Tests;

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Trie;

[TestFixture]
[TestOf(typeof(TrieVertices))]
public class TrieVerticesTest
{
    private TrieVertices trie = new();

    public static IEnumerable<string[]> TestCasesWithRepeatedWords
    {
        get
        {
            yield return new string[] { "wor$", "world", "war", "WAR", "WaRhAmEr40000@#№", "warning!!!!", "(00000)", "    ", " ", string.Empty, "кукук", "пам пам пам" };
        }
    }

    /// <summary>
    /// Test standard and empty elements Add in Trie.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TesttAdd(string[] elements)
    {
        foreach (var element in elements)
        {
            this.trie.Add(element);

            Assert.That(this.trie.Contains(element));
        }
    }

    /// <summary>
    /// Test standard and empty elements Remove in Trie.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TestRemove(string[] elements)
    {
        foreach (var element in elements)
        {
            this.trie.Add(element);
        }

        foreach (var element in elements)
        {
            this.trie.Remove(element);

            Assert.That(!this.trie.Contains(element));
        }
    }

    /// <summary>
    /// If elements no added and try removing there. Must be error.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TestFalseRemove(string[] elements)
    {
        foreach (var element in elements)
        {
            Assert.That(!this.trie.Remove(element));
        }
    }

    /// <summary>
    /// Test standard and empty elements Size in True.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TestSize(string[] elements)
    {
        foreach (var element in elements)
        {
            this.trie.Add(element);
        }

        Assert.That(this.trie.Size() == 11);
    }

    /// <summary>
    /// Test standard and empty elements HowManyStartsWithPrefix in Trie.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TestPrefix(string[] elements)
    {
        foreach (var element in elements)
        {
            this.trie.Add(element);
        }

        Assert.That(this.trie.HowManyStartsWithPrefix("w") == 4);
    }

    /// <summary>
    /// If try added double elements. All elements must be added once.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TestDoubleAdd(string[] elements)
    {
        foreach (var element in elements)
        {
            this.trie.Add(element);
            this.trie.Add(element);
        }

        Assert.That(this.trie.Size() == 11);
    }

    /// <summary>
    /// If try removed double elements. All elements must be removed once.
    /// </summary>
    [TestCaseSource(nameof(TestCasesWithRepeatedWords))]
    public void TestDoubleRemove(string[] elements)
    {
        foreach (var element in elements)
        {
            this.trie.Add(element);
        }

        foreach (var element in elements)
        {
            this.trie.Remove(element);
            this.trie.Remove(element);
        }

        foreach (var element in elements)
        {
            this.trie.Add(element);
        }

        Assert.That(this.trie.Size() == 11);
    }
}