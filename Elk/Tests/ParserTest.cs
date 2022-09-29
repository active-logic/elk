using NUnit.Framework;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Module = Elk.Basic.Graph.Module;

namespace Elk_Test{
public class ParserTest{

    Elk.Basic.Parser p;

    [SetUp] public void Setup(){
        p = new Elk.Basic.Parser("func");
        // NOTE enable for additional debug info
        // p.log = UnityEngine.Debug.Log;
    }

    [Test] public void Test_FuncDef_1() => Assert.AreEqual(
        "main → {body}",
        ((Module)(
            p["func", "main", "(", ")", "=>", "body", ";"]
        )).functions[0].ToString()
    );

    [Test] public void Test_FuncDef_2() => Assert.AreEqual(
        "(main(arg) → {(attack|flee)})",
        ((Module)(
            p["func", "main", "(", "arg", ")", "=>", "attack", "|", "flee", ";"]
        )).functions[0].ToString()
    );

    [Test] public void Test_NOT() => Assert.AreEqual(
        "(!true)",
        p["!", "true"]?.ToString()
    );

    [Test] public void Test_Module() => Assert.AreEqual(
        "(includes: 1, functions: 1)",
        p["with", "mod", ";",
        "func", "main", "(", ")", "=>", "body", ";"]?.ToString()
    );

    [Test] public void Test_MUL() => Assert.AreEqual(
        "(1*2)",
        p["1", "*", "2"]?.ToString()
    );

    [Test] public void Test_MUL_PLUS_ops() => Assert.AreEqual(
        "((1*2)+3)",
        p["1", "*", "2", "+", "3"]?.ToString()
    );

    [Test] public void Test_PLUS() => Assert.AreEqual(
        "(1+2)",
        p["1", "+", "2"]?.ToString()
    );

    [Test] public void Test_PLUS_MUL() => Assert.AreEqual(
        "(1+(2*3))",
        p["1", "+", "2", "*", "3"]?.ToString()
    );

    [Test] public void Test_PLUS_MUL_parens() => Assert.AreEqual(
        "(((1+2))*3)",
        p["(", "1", "+", "2", ")", "*", "3"]?.ToString()
    );

    [Test] public void Test_PLUS_PLUS() => Assert.AreEqual(
        "((1+2)+3)",
        p["1", "+", "2", "+", "3"]?.ToString()
    );

    [Test] public void Test_PLUS_MINUS_ops() => Assert.AreEqual(
        "((1+2)-3)",
        p["1", "+", "2", "-", "3"]?.ToString()
    );

    [Test] public void Test_PostfixUnary() => Assert.AreEqual(
        "Attack((player!))",
        p["Attack", "(", "player", "!", ")"]?.ToString()
    );

    [Test] public void Test_UNARY_PREC_quirk() => Assert.AreEqual(
        "((-2)+3)",
        p["-", "2", "+", "3"]?.ToString()
    );

    [Test] public void Test_Singleton() => Assert.AreEqual(
        "(5)",
        p["(", "5", ")"]?.ToString()
    );

    [Test] public void Test_With() => Assert.AreEqual(
        "(includes: 1, functions: 0)",
        p["with", "encounters", ";"]?.ToString()
    );

}}
