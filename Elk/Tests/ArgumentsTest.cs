using System;
using UnityEngine;
using NUnit.Framework;
using Elk.Bindings.CSharp;

namespace Elk_Test{
public class ArgumentsTest{

    [Test] public void TestParameterTypes(
        [Values(false, true)] bool nullIsObj
    ){
        var arr = new object[]{};
        var types = arr.ParameterTypes(nullIsObj);
        Assert.AreEqual(types.Length, 0);
    }

    [Test] public void TestParameterTypes_FromNullArray_Throws(){
        object[] arr = null;
        Assert.Throws<ArgumentNullException>(
            () => arr.ParameterTypes(nullIsObj: false)
        );
    }

}}
