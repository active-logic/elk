using NUnit.Framework;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk_Test{
public class RecallRuleTest{

    Elk.Basic.Parser.RecallRule rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.RecallRule();

    [Test] public void Test_did(){
        var inv = new Invocation("foo", 0f);
        var seq = new Sequence("did", inv);
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Recall);
    }

    [Test] public void Test_did_since(){
        var inv0 = new Invocation("foo", 0f);
        var inv1 = new Invocation("bar", 0f);
        var seq = new Sequence("did", inv0, "since", inv1);
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Recall);
        var rec = (Recall)seq[0];
        Assert.That(!rec.strict);
    }

    [Test] public void Test_did_since_strict(){
        var inv0 = new Invocation("foo", 0f);
        var inv1 = new Invocation("bar", 0f);
        var seq = new Sequence("did", inv0, "since", inv1, "!");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Recall);
        var rec = (Recall)seq[0];
        Assert.That(rec.strict);
    }

}}
