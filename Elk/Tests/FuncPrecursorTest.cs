using NUnit.Framework;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk_Test{
public class FuncPrecursorTest{

    Elk.Basic.Parser.FuncPrecursor rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.FuncPrecursor();

    [Test] public void Test_0_Arg(){
        var seq = new Sequence(
            "func", "main", "(", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

    [Test] public void Test_1_Arg(){
        var seq = new Sequence(
            "func", "main", "(", "3", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

    [Test] public void Test_2_Arg(){
        var seq = new Sequence(
            "func", "main", "(", "1", ",", "2", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

    [Test] public void Test_3_Arg(){
        var seq = new Sequence(
            "func", "main", "(", "1", ",", "2", ",", "3", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

}}
