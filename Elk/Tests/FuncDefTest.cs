using System;
using NUnit.Framework;
using FuncDef = Elk.Basic.Graph.FuncDef;

namespace Elk_Test{
public class FuncDefTest{

    [Test] public void Test_SignatureMatch(){
        var f0 = new FuncDef("A", new string[]{"p0", "p1"}, null);
        var f1 = new FuncDef("A", new string[]{"p0", "p1"}, null);
        Assert.That(f0.MatchesSignatureOf(f1), Is.True);
    }

    [Test] public void Test_NameMismatch(){
        var f0 = new FuncDef("A", new string[]{"p0", "p1"}, null);
        var f1 = new FuncDef("B", new string[]{"p0", "p1"}, null);
        Assert.That(f0.MatchesSignatureOf(f1), Is.False);
    }

    [Test] public void Test_ParamCountMismatch(){
        var f0 = new FuncDef("A", new string[]{"p0"}      , null);
        var f1 = new FuncDef("A", new string[]{"p0", "p1"}, null);
        Assert.That(f0.MatchesSignatureOf(f1), Is.False);
    }

}}
