using NUnit.Framework;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk_Test{
public class InvocationRuleTest{

    Elk.Basic.Parser.InvocationRule rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.InvocationRule();

    [Test] public void Test_0_Arg(){
        var seq = new Sequence(
            new Identifier("main"),
            new Operator("("),
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

    [Test] public void Test_1_Arg(){
        var seq = new Sequence(
            new Identifier("main"),
            new Operator("("),
            3,
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

    [Test] public void Test_2_Arg(){
        var seq = new Sequence(
            new Identifier("main"),
            new Operator("("),
            1, ",", 5f,
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

    [Test] public void Test_3_Arg(){
        var seq = new Sequence(
            new Identifier("main"),
            new Operator("("),
            1, ",",
            null, ",",
            new Identifier("abc"),
            new Operator(")")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

}}
