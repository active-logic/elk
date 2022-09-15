using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Activ.BTL;

namespace Elk.Test{
public class BTLParserTest{

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

}}
