using NUnit.Framework;
using System;
using UnityEngine;
using Elk.Bindings.CSharp;

namespace Elk_Test.CSharpBindings{
public class TypeExtTest{

    static object[] noArgs = new object[]{};

    [Test] public void TestF0_NullArgs(){
        Type t = typeof(Cog);
        t.GetMethod("F0", (object[])null);
    }

    [Test] public void TestF0_NoArgs(){
        Type t = typeof(Cog);
        var m = t.GetMethod("F0", noArgs);
    }

    [Test] public void TestF1(){
        Type t = typeof(Cog);
        var m = t.GetMethod("F1", new object[]{"str"});
        Assert.IsTrue( m != null );
    }

    [Test] public void TestF1_argIsNull(){
        Type t = typeof(Cog);
        var m = t.GetMethod("F1", new object[]{null});
        Assert.IsTrue( m != null );
    }

    [Test] public void TestF2_ambiguousBinding_throws(){
        Type t = typeof(Cog);
        Assert.Throws<Exception>(
            () => t.GetMethod("F2a", new object[]{null})
        );
    }

    class Cog{
        public void F0(){}
        public void F1(string arg){}
        public void F2a(string arg){}
        public void F2a(int? arg){}
    }

}}
