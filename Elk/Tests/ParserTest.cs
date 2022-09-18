using NUnit.Framework;
using FuncDef = Elk.Basic.Graph.FuncDef;

namespace Elk_Test{
public class ParserTest{

    Elk.Basic.Parser p;

    [SetUp] public void Setup() => p = new Elk.Basic.Parser("func");

    [Test] public void Test_NOT() => Assert.AreEqual(
        "(!true)",
        p["!", "true"]?.ToString()
    );

    [Test] public void Test_PLUS() => Assert.AreEqual(
        "(1+2)",
        p["1", "+", "2"]?.ToString()
    );

    [Test] public void Test_MUL() => Assert.AreEqual(
        "(1*2)",
        p["1", "*", "2"]?.ToString()
    );

    [Test] public void Test_PLUS_PLUS() => Assert.AreEqual(
        "((1+2)+3)",
        p["1", "+", "2", "+", "3"]?.ToString()
    );

    [Test] public void Test_PLUS_MINUS_ops() => Assert.AreEqual(
        "((1+2)-3)",
        p["1", "+", "2", "-", "3"]?.ToString()
    );

    [Test] public void Test_MUL_PLUS_ops() => Assert.AreEqual(
        "((1*2)+3)",
        p["1", "*", "2", "+", "3"]?.ToString()
    );

    [Test] public void Test_PLUS_MUL_ops() => Assert.AreEqual(
        "(1+(2*3))",
        p["1", "+", "2", "*", "3"]?.ToString()
    );

    [Test] public void Test_FuncDef_1() => Assert.AreEqual(
        "main → {body}",
        ((FuncDef[])(
            p["func", "main", "(", ")", "=>", "body", ";"]
        ))[0].ToString()
    );

    [Test] public void Test_FuncDef_2() => Assert.AreEqual(
        "main(arg) → {(attack|flee)}",
        ((FuncDef[])(
            p["func", "main", "(", "arg", ")", "=>", "attack", "|", "flee", ";"]
        ))[0].ToString()
    );

}}
