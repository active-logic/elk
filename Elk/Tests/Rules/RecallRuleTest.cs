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
        var inv = new Invocation("foo");
        var seq = new Sequence(new Identifier("did"), inv);
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Recall);
    }

    [Test] public void Test_did_since(){
        var inv0 = new Invocation("foo");
        var inv1 = new Invocation("bar");
        var seq = new Sequence(new Identifier("did"), inv0, new Identifier("since"), inv1);
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Recall);
        var rec = (Recall)seq[0];
        Assert.That(!rec.strict);
    }

    [Test] public void Test_did_since_strict(){
        var inv0 = new Invocation("foo");
        var inv1 = new Invocation("bar");
        var seq = new Sequence(new Identifier("did"), inv0, new Identifier("since"), inv1, "!");
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Recall);
        var rec = (Recall)seq[0];
        Assert.That(rec.strict);
    }

}}
