using NUnit.Framework;
using Active.Core; using static Active.Raw;
using Activ.BTL.Imp;
using Activ.BTL;
using Elk;
using Elk.Basic.Graph;
using Context = Elk.Basic.Context;
using UnaEval = Elk.Basic.Runtime.UnaEval;

namespace BTL_Test{
public class BTL_UnaEvalTest{

    Elk.Basic.Runner ρ; UnaEval una;

    [SetUp] public void Setup(){
        var ι = BTLInterpreterFactory.Create();
        ρ = (Elk.Basic.Runner)ι.runner;
        una = new UnaEval();
    }

    // Status/status ================================================

    // inverter, promoter, demoter, condoner
    [Test] public void TestInverter([Range(-1, 1)] int arg)
    => Assert.That( ε("!", arg), Is.EqualTo(!S(arg)) );

    [Test] public void TestPromoter([Range(-1, 1)] int arg)
    => Assert.That( ε("+", arg), Is.EqualTo(+S(arg)) );

    [Test] public void TestDemoter([Range(-1, 1)] int arg)
    => Assert.That( ε("-", arg), Is.EqualTo(-S(arg)) );

    [Test] public void TestCondoner([Range(-1, 1)] int arg)
    => Assert.That( ε("~", arg), Is.EqualTo(~S(arg)) );

    // ==============================================================

    object ε(string op, int arg){
        status operand = S(arg);
        var exp = new UnaryExp(operand, op);
        return una.Eval(exp, ρ, null);
    }

    status S(int i) => i == -1 ? fail : i == 0 ? cont : done;


}}
