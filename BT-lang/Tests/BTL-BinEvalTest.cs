using NUnit.Framework;
using Active.Core; using static Active.Raw;
using Activ.BTL.Imp;
using Activ.BTL;
using Elk;
using Elk.Basic.Graph;
using Context = Elk.Basic.Context;

namespace BTL_Test{
public class BTL_BinEvalTest{

    Elk.Basic.Runner ρ; BinEval bin;

    [SetUp] public void Setup(){
        var ι = BTLInterpreterFactory.Create();
        ρ = (Elk.Basic.Runner)ι.runner;
        bin = new BinEval();
    }

    // Status/status ================================================

    [Test] public void TestLogicalOr([Range(-1, 1)] int l,
                                     [Range(-1, 1)] int r)
    => Assert.That(ε(l, "||", r), Is.EqualTo(S(l) || S(r)) );

    [Test] public void TestLogicalAnd([Range(-1, 1)] int l,
                                      [Range(-1, 1)] int r)
    => Assert.That( ε(l, "&&", r), Is.EqualTo(S(l) && S(r)) );

    [Test] public void TestLenientCombinator([Range(-1, 1)] int l,
                                             [Range(-1, 1)] int r)
    => Assert.That( ε(l, "+", r), Is.EqualTo(S(l) + S(r)) );

    [Test] public void TestStrictCombinator([Range(-1, 1)] int l,
                                            [Range(-1, 1)] int r)
    => Assert.That( ε(l, "*", r), Is.EqualTo(S(l) * S(r)) );

    /*
    [Test] public void TestNeutralCombinator([Range(-1, 1)] int l,
                                             [Range(-1, 1)] int r)
    => Assert.That( ε(l, "%", r), Is.EqualTo(S(l) % S(r)) );
    */

    // Status/bool ==================================================

    [Test] public void TestLogicalOr([Range(-1, 1)] int l,
                                     [Values(false, true)] bool r)
    => Assert.That(ε(l, "||", r), Is.EqualTo(S(l) || r) );

    [Test] public void TestLogicalAnd([Range(-1, 1)] int l,
                                     [Values(false, true)] bool r)
    => Assert.That( ε(l, "&&", r), Is.EqualTo(S(l) && r) );

    [Test] public void TestLenientCombinator([Range(-1, 1)] int l,
                                             [Values(false, true)] bool r)
    => Assert.That( ε(l, "+", r), Is.EqualTo(S(l) + r) );

    [Test] public void TestStrictCombinator([Range(-1, 1)] int l,
                                            [Values(false, true)] bool r)
    => Assert.That( ε(l, "*", r), Is.EqualTo(S(l) * r) );

    /*
    [Test] public void TestNeutralCombinator([Range(-1, 1)] int l,
                                             [Values(false, true)] bool r)
    => Assert.That( ε(l, "%", r), Is.EqualTo(S(l) % r) );
    */

    // bool/status ==================================================

    [Test] public void TestLogicalOr([Values(false, true)] bool l,
                                     [Range(-1, 1)] int r)
    => Assert.That(ε(l, "||", r), Is.EqualTo(l || S(r)) );

    [Test] public void TestLogicalAnd([Values(false, true)] bool l,
                                      [Range(-1, 1)] int r)
    => Assert.That( ε(l, "&&", r), Is.EqualTo(l && S(r)) );

    [Test] public void TestLenientCombinator([Values(false, true)] bool l,
                                             [Range(-1, 1)] int r)
    => Assert.That( ε(l, "+", r), Is.EqualTo(l + S(r)) );

    [Test] public void TestStrictCombinator([Values(false, true)] bool l,
                                            [Range(-1, 1)] int r)
    => Assert.That( ε(l, "*", r), Is.EqualTo(l * S(r)) );

    /*
    [Test] public void TestNeutralCombinator([Values(false, true)] bool l,
                                             [Range(-1, 1)] int r)
    => Assert.That( ε(l, "%", r), Is.EqualTo(l % S(r)) );
    */

    // ==============================================================

    object ε(int l, string op, int r){
        status left = S(l), right = S(r);
        var exp = new BinaryExp(left, op, right);
        return bin.Eval(exp, ρ, null);
    }

    object ε(bool l, string op, int r){
        bool left = l;
        status right = S(r);
        var exp = new BinaryExp(left, op, right);
        return bin.Eval(exp, ρ, null);
    }

    object ε(int l, string op, bool r){
        status left = S(l);
        bool right = r;
        var exp = new BinaryExp(left, op, right);
        return bin.Eval(exp, ρ, null);
    }

    status S(int i) => i == -1 ? fail : i == 0 ? cont : done;

}}
