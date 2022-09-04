using NUnit.Framework;

namespace Elk.Test{
public class ParserTest{

    Elk.Basic.Parser p;

    [SetUp] public void Setup() => p = new Elk.Basic.Parser();

    [Test] public void Test_PLUS(){
        Assert.AreEqual(
            "(1+2)",
            p["1", "+", "2"]?.ToString()
        );
    }

    [Test] public void Test_MUL(){
        Assert.AreEqual(
            "(1*2)",
            p["1", "*", "2"]?.ToString()
        );
    }

    [Test] public void Test_PLUS_PLUS(){
        Assert.AreEqual(
            "((1+2)+3)",
            p["1", "+", "2", "+", "3"]?.ToString()
        );
    }

    [Test] public void Test_PLUS_MINUS_ops(){
        Assert.AreEqual(
            "((1+2)-3)",
            p["1", "+", "2", "-", "3"]?.ToString()
        );
    }

    [Test] public void Test_MUL_PLUS_ops(){
        Assert.AreEqual(
            "((1*2)+3)",
            p["1", "*", "2", "+", "3"]?.ToString()
        );
    }

    [Test] public void Test_PLUS_MUL_ops(){
        Assert.AreEqual(
            "(1+(2*3))",
            p["1", "+", "2", "*", "3"]?.ToString()
        );
    }

    [Test] public void Test_FuncDef_1(){
        Assert.AreEqual(
            "main → {body}",
            p["func", "main", "(", ")", "=>", "body", ";"].ToString()
        );
    }

    [Test] public void Test_FuncDef_2(){
        Assert.AreEqual(
            "main(arg) → {(attack|flee)}",
            p["func", "main", "(", "arg", ")", "=>", "attack", "|", "flee", ";"].ToString()
        );
    }

}}
