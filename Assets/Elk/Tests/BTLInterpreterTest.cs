using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Activ.BTL;
using Active.Core;
using Elk.Basic.Graph;

namespace Elk.Test{
public class BTLInterpreterTest{

    [Test] public void TestInvocation(){
        var i = BTL.NewInterpreter;
        i.Parse("Attack()");
    }

    [Test] public void TestLogicalOr_2(){
        var i = BTL.NewInterpreter;
        i.Parse("Attack() || Roam");
    }

    [Test] public void TestLogicalOr_1(){
        var i = BTL.NewInterpreter;
        i.Parse("Attack() || Roam()");
    }

    [Test] public void TestFunc_A(){
        var i = BTL.NewInterpreter;
        i.Parse("func Main() => Attack() || Roam();");
    }

    [Test] public void TestLiterals_1(){
        var i = BTL.NewInterpreter;
        var root = i.Parse("true || false") as BinaryOp;
        Assert.That( root.arg0 is bool );
        Assert.That( root.arg1 is bool );
    }

    [Test] public void TestLiterals_2(){
        var i = BTL.NewInterpreter;
        var root = i.Parse("done || fail") as BinaryOp;
        Assert.That( root.arg0 is status );
        Assert.That( root.arg1 is status );
    }

}}
