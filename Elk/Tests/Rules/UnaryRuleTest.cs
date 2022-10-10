using NUnit.Framework;
using Elk.Util; using Elk.Basic.Graph;

namespace Elk_Test{
public class UnaryRuleTest{

    Elk.Basic.Parser.UnaryRule rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.UnaryRule("!");

    [Test] public void Test_Include(){
        var seq = new Sequence(
            new Operator("!"),
            new Identifier("foo")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is UnaryExp);
    }

}}
