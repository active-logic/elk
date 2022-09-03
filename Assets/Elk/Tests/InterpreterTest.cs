using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Elk.Test{
public class InterpreterTest{

    [Test] public void Test(){
        var i = new Interpreter();
        Assert.That( i["abc"], Is.Null );
    }

}}
