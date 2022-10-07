using System;
using UnityEngine;
using NUnit.Framework;
using Elk.Util;

namespace Elk_Test{
public class ArrayExtTest{

    [Test] public void TestFormat(){
        var x = new object[]{ "a", 10, null };
        Assert.AreEqual("(a, 10, )", x.Format());
    }

    [Test] public void TestNeatFormat(){
        var go = new GameObject("Jim");
        var x = new object[]{ "a", 10, null, go, go.transform };
        Assert.AreEqual("(a, 10, null, Jim, Jim)", x.NeatFormat());
        UnityEngine.Object.DestroyImmediate(go);
    }

}}
