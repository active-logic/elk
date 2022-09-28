using NUnit.Framework;
using Elk;
using BinaryExp = Elk.Basic.Graph.BinaryExp;
using Context = Elk.Basic.Context;
using Activ.BTL; using Active.Core;

namespace BTL_Test{
public class BTL_InterpreterTest{

    Interpreter<Context> i;

    [SetUp] public void Setup(){
        i = BTLInterpreterFactory.Create();
        i.reader.validator = null;
    }

    [Test] public void TestInvocation()
    => i.Parse("Attack()");

    [Test] public void TestLogicalOr_2()
    => i.Parse("Attack() || Roam");

    [Test] public void TestLogicalOr_1()
    => i.Parse("Attack() || Roam()");

    [Test] public void TestFunc_A()
    => i.Parse("task Main() => Attack() || Roam();");

    [Test] public void TestLiterals_1(){
        var root = i.Parse("true || false") as BinaryExp;
        Assert.That( root.arg0 is bool );
        Assert.That( root.arg1 is bool );
    }

    [Test] public void TestLiterals_2(){
        var root = i.Parse("done || fail") as BinaryExp;
        Assert.That( root.arg0 is status );
        Assert.That( root.arg1 is status );
    }

}}
