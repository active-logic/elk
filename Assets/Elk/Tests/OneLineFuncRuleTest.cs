using NUnit.Framework;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Test{
public class OneLineFuncRuleTest{

    Elk.Basic.Parser.OneLineFuncRule rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.OneLineFuncRule();

    [Test] public void Test_0_Arg(){
        var seq = new Sequence(
            "func", "main", "(", ")", "=>", "noop", ";");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is OneLineFunc);
    }

    [Test] public void Test_1_Arg(){
        var seq = new Sequence(
            "func", "main", "(", "3", ")", "=>", "noop", ";");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is OneLineFunc);
    }

    [Test] public void Test_2_Arg(){
        var seq = new Sequence(
            "func", "main", "(", "1", ",", "2", ")", "=>", "noop", ";");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is OneLineFunc);
    }

    [Test] public void Test_3_Arg(){
        var seq = new Sequence(
            "func", "main", "(", "1", ",", "2", ",", "3", ")", "=>", "noop", ";");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is OneLineFunc);
    }

}}
