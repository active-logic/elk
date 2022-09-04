using NUnit.Framework;
using Elk.Basic;

namespace Elk.Test{
public class TokenizerTest{

    Elk.Basic.Tokenizer t;

    [SetUp] public void Setup() => t = new Elk.Basic.Tokenizer();

    [Test] public void Test_T0_Pass(){
        Assert.AreEqual(
            new string[]{"big", "time"},
            t.Tokenize("big time")
        );
    }

    [Test] public void Test_T0_Pass_withExtraSpace(){
        Assert.AreEqual(
            new string[]{"big", "time"},
            t.Tokenize("big          time  ")
        );
    }

    [Test] public void Test_T0_Pass_withSpecialWhitespace(){
        Assert.AreEqual(
            new string[]{"big", "time"},
            t.Tokenize("big\n\rtime")
        );
    }

}}
