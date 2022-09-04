using NUnit.Framework;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Test{
public class InvocationRuleTest{

    Elk.Basic.Parser.InvocationRule rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.InvocationRule();

    [Test] public void Test_0_Arg(){
        var seq = new Sequence("main", "(", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

    [Test] public void Test_1_Arg(){
        var seq = new Sequence("main", "(", "3", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

    [Test] public void Test_2_Arg(){
        var seq = new Sequence("main", "(", "1", "," ,"2", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

    [Test] public void Test_3_Arg(){
        var seq = new Sequence("main", "(", "1", "," ,"2", ",", "3", ")");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Invocation);
    }

}}
