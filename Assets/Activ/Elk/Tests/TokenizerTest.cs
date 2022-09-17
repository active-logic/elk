using NUnit.Framework;
using Elk.Basic;

namespace Elk_Test{
public class TokenizerTest : BasicTokenizerTest{

    public override void Setup()
    => t = new Elk.Basic.Tokenizer();

    [Test] public void Test_Parens_Pass(){
        Assert.AreEqual(
            new string[]{"(", ")"},
            t.Tokenize("()")
        );
    }

    [Test] public void Test_SymbolIsBreakingAfterLetter(){
        Assert.AreEqual(
            new string[]{"foo", "("},
            t.Tokenize("foo(")
        );
    }

    [Test] public void Test_SymbolIsBreakingBeforeLetter(){
        Assert.AreEqual(
            new string[]{"+", "foo"},
            t.Tokenize("+foo")
        );
    }

    [Test] public void Test_DoubleChar_Pass(){
        Assert.AreEqual(
            new string[]{"++", "5"},
            t.Tokenize("++5")
        );
    }

    [Test] public void Test_DoubleChar_Ignore(){
        Assert.AreEqual(
            new string[]{"*", "*", "5"},
            t.Tokenize("**5")
        );
    }

    [Test] public void Test_Arrow_Op(){
        Assert.AreEqual(
            new string[]{"foo", "=>", "5"},
            t.Tokenize("foo=>5")
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
