using System;
using UnityEngine;
using NUnit.Framework;
using Elk.Util;

namespace Elk_Test{
public class ArrayExtTest{

    [Test] public void TestTypes([Values(false, true)] bool nullIsObj){
        var arr = new object[]{};
        var types = arr.Types(nullIsObj);
        Assert.AreEqual(types.Length, 0);
    }

    [Test] public void TestTypes_FromNullArray_Throws(){
        object[] arr = null;
        Assert.Throws<ArgumentNullException>(
            () => arr.Types(nullIsObj: false)
        );
    }

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
