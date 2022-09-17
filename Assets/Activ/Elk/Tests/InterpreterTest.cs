using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Elk_Test{
public class InterpreterTest{

    [Test] public void Test_SimpleScript(){
        var i = new Elk.Basic.Interpreter();
        Assert.That( (int) i["2 + 2", context: null] == 4 );
    }

}}
