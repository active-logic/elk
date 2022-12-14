using NUnit.Framework;
using Elk.Util; using Elk.Basic.Graph;

namespace Elk_Test{
public class IncludeRuleTest{

    Elk.Basic.Parser.IncludeRule rule;

    [SetUp] public void Setup()
    => rule = new Elk.Basic.Parser.IncludeRule();

    [Test] public void Test_Include(){
        var seq = new Sequence(
            new Identifier("with"),
            new Identifier("encounters"),
            new Operator(";")
        );
        rule.Process(seq, 0);
        Assert.AreEqual(1, seq.size);
        Assert.That(seq[0] is Include);
    }

}}
