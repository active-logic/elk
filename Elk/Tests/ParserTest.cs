using System.Collections.Generic;
using NUnit.Framework;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Module = Elk.Basic.Graph.Module;
using Elk.Basic.Graph;

namespace Elk_Test{
public class ParserTest{

    Elk.Basic.Parser p;

    [SetUp] public void Setup(){
        p = new Elk.Basic.Parser("func");
        // NOTE enable for additional debug info
        // p.log = UnityEngine.Debug.Log;
    }

    [Test] public void Test_FuncDef_1() => Assert.AreEqual(
        "main → {(id:body)}",
        ((Module)(
            Parse("func", "main", "(", ")", "=>", "body", ";")
        )).functions[0].ToString()
    );

    [Test] public void Test_FuncDef_2() => Assert.AreEqual(
        "(main(arg) → {((id:attack)|(id:flee))});",
        ((Module)(
            Parse("func", "main", "(", "arg", ")", "=>", "attack", "|", "flee", ";")
        )).functions[0].ToString()
    );

    [Test] public void Test_NOT_identifier() => Assert.AreEqual(
        "(!foo)",
        p[new Operator("!"), "foo"]?.ToString()
    );

    [Test] public void Test_Invocation_1() => Assert.AreEqual(
        "Attack((id:player))",
        Parse("Attack", "(", "player", ")")?.ToString()
    );

    [Test] public void Test_NOT_bool_1() => Assert.AreEqual(
        "(!True)",
        p[new Operator("!"), true]?.ToString()
    );

    [Test] public void Test_NOT_bool_2() => Assert.AreEqual(
        "(!True)",
        Parse("!", "true")?.ToString()
    );

    [Test] public void Test_NOT_float() => Assert.AreEqual(
        "(!2.5)",
        p[new Operator("!"), 2.5f]?.ToString()
    );

    [Test] public void Test_Module() => Assert.AreEqual(
        "(includes: 1, functions: 1)",
        Parse("with", "mod", ";",
        "func", "main", "(", ")", "=>", "body", ";")?.ToString()
    );

    [Test] public void Test_MUL() => Assert.AreEqual(
        "(1*2)",
        Parse("1", "*", "2")?.ToString()
    );

    [Test] public void Test_MUL_PLUS_ops() => Assert.AreEqual(
        "((1*2)+3)",
        Parse("1", "*", "2", "+", "3")?.ToString()
    );

    [Test] public void Test_PLUS() => Assert.AreEqual(
        "(1+2)",
        Parse("1", "+", "2")?.ToString()
    );

    [Test] public void Test_PLUS_MUL() => Assert.AreEqual(
        "(1+(2*3))",
        Parse("1", "+", "2", "*", "3")?.ToString()
    );

    [Test] public void Test_PLUS_MUL_parens() => Assert.AreEqual(
        "(((1+2))*3)",
        Parse("(", "1", "+", "2", ")", "*", "3")?.ToString()
    );

    [Test] public void Test_PLUS_PLUS() => Assert.AreEqual(
        "((1+2)+3)",
        Parse("1", "+", "2", "+", "3")?.ToString()
    );

    [Test] public void Test_PLUS_MINUS_ops() => Assert.AreEqual(
        "((1+2)-3)",
        Parse("1", "+", "2", "-", "3")?.ToString()
    );

    [Test] public void Test_PostfixUnary_1() => Assert.AreEqual(
        "((id:player)!)",
        Parse("player", "!")?.ToString()
    );

    [Test] public void Test_PostfixUnary_2() => Assert.AreEqual(
        "Attack(((id:player)!))",
        Parse("Attack", "(", "player", "!", ")")?.ToString()
    );

    [Test] public void Test_UNARY_PREC_quirk() => Assert.AreEqual(
        "((-2)+3)",
        Parse("-", "2", "+", "3")?.ToString()
    );

    [Test] public void Test_Singleton() => Assert.AreEqual(
        "(5)",
        Parse("(", "5", ")")?.ToString()
    );

    [Test] public void Test_With() => Assert.AreEqual(
        "(includes: 1, functions: 0)",
        Parse("with", "encounters", ";")?.ToString()
    );

    object Parse(params string[] tokens){
        var caster = new Elk.Basic.TypeCaster();
        var seq = new Elk.Util.Sequence(tokens);
        caster.Transform(seq);
        var debug = new List<string>();
        var @out = p.Parse(seq, debug);
        UnityEngine.Debug.Log($"OUTPUT: {@out}");
        return @out;
    }

}}
