using NUnit.Framework;
using System;
using UnityEngine;
using Elk.Bindings.CSharp;

namespace Elk_Test.CSharpBindings{
public class MethodInfoExtTest{

    static object[] noArgs = new object[]{};

    [Test] public void TestMatch(){
        var typeArgs = new Type[]{typeof(string)};
        var m = typeof(Cog).GetMethod("F1", typeArgs);
        Assert.IsTrue( m.Matches("F1", typeArgs) );
    }

    [Test] public void TestMismatch(){
        var typeArgs1 = new Type[]{typeof(string)};
        var typeArgs2 = new Type[]{typeof(object)};
        var m = typeof(Cog).GetMethod("F1", typeArgs1);
        Assert.IsFalse( m.Matches("F1", typeArgs2) );
    }

    [Test] public void TestMatchWildCard_1(){
        var typeArgs1 = new Type[]{typeof(string)};
        var typeArgs2 = new Type[]{null};
        var m = typeof(Cog).GetMethod("F1", typeArgs1);
        Assert.IsTrue( m.Matches("F1", typeArgs2) );
    }

    [Test] public void TestMatchWildCard_2(){
        var typeArgs1 = new Type[]{typeof(int?)};
        var typeArgs2 = new Type[]{null};
        var m = typeof(Cog).GetMethod("F2b", typeArgs1);
        Assert.IsTrue( m.Matches("F2b", typeArgs2) );
    }

    class Cog{
        public void F0(){}
        public void F1(string arg){}
        public void F2a(string arg){}
        public void F2b(int? arg){}
    }

}}
