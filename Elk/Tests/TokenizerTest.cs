using NUnit.Framework;
using Elk.Basic;
using Elk.Util;

namespace Elk_Test{
public class TokenizerTest{

    protected Elk.Tokenizer t;

    [SetUp] public void Setup()
    => t = new Elk.Basic.Tokenizer();

    // -------------------------------------------------------------

    [Test] public void Test_T0_Pass(){
        Assert.AreEqual(
            new string[]{"big", "time"},
            t.Tokenize("big time").ToStringArray()
        );
    }

    [Test] public void Test_Tokenize_Num_With_Suffix(){
        Assert.AreEqual(
            new string[]{"big", "5f"},
            t.Tokenize("big 5f").ToStringArray()
        );
    }

    [Test] public void Test_T0_Pass_withExtraSpace(){
        Assert.AreEqual(
            new string[]{"big", "time"},
            t.Tokenize("big          time  ").ToStringArray()
        );
    }

    [Test] public void Test_T0_Pass_withSpecialWhitespace(){
        Assert.AreEqual(
            new string[]{"big", "time"},
            t.Tokenize("big\n\rtime").ToStringArray()
        );
    }

    // -------------------------------------------------------------

    [Test] public void Test_Parens_Pass(){
        Assert.AreEqual(
            new string[]{"(", ")"},
            t.Tokenize("()").ToStringArray()
        );
    }

    [Test] public void Test_SymbolIsBreakingAfterLetter(){
        Assert.AreEqual(
            new string[]{"foo", "("},
            t.Tokenize("foo(").ToStringArray()
        );
    }

    [Test] public void Test_SymbolIsBreakingBeforeLetter(){
        Assert.AreEqual(
            new string[]{"+", "foo"},
            t.Tokenize("+foo").ToStringArray()
        );
    }

    [Test] public void Test_DoubleChar_Pass(){
        Assert.AreEqual(
            new string[]{"++", "5"},
            t.Tokenize("++5").ToStringArray()
        );
    }

    [Test] public void Test_DoubleChar_Ignore(){
        Assert.AreEqual(
            new string[]{"*", "*", "5"},
            t.Tokenize("**5").ToStringArray()
        );
    }

    [Test] public void Test_Arrow_Op(){
        Assert.AreEqual(
            new string[]{"foo", "=>", "5"},
            t.Tokenize("foo=>5").ToStringArray()
        );
    }

    // Additional tests, mainly to help understand how characters
    // classify

    [Test] public void Test_Type()
    => Assert.That( t is Elk.Basic.Tokenizer );

    [Test] public void Test_Symbol()
    => Assert.That( char.IsSymbol('+') );

    [Test] public void Test_NonSymbol()
    => Assert.That( !char.IsSymbol('(') );

}}
