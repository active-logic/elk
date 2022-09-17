using NUnit.Framework;
using Elk.Basic;

namespace Elk_Test{
public class BasicTokenizerTest{

    protected Elk.Tokenizer t;

    [SetUp] public virtual void Setup()
    => t = new Elk.Basic.BasicTokenizer();

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
