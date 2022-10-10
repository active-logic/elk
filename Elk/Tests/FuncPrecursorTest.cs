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
            new Identifier("func"),
            new Identifier("main"),
            new Operator("("), new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

    [Test] public void Test_1_Arg(){
        var seq = new Sequence(
            new Identifier("func"),
            new Identifier("main"),
            new Operator("("),
            new Identifier("a"),
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

    [Test] public void Test_2_Arg(){
        var seq = new Sequence(
            new Identifier("func"),
            new Identifier("main"),
            new Operator("("),
            new Identifier("a"),
            new Operator(","),
            new Identifier("b"),
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

    [Test] public void Test_3_Arg(){
        var seq = new Sequence(
            new Identifier("func"),
            new Identifier("main"),
            new Operator("("),
            new Identifier("a"),
            new Operator(","),
            new Identifier("b"),
            new Operator(","),
            new Identifier("c"),
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is FuncDef);
    }

}}
