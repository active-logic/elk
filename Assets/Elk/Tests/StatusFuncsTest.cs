using BF = System.Reflection.BindingFlags;
using NUnit.Framework;
using UnityEngine;
using Active.Core;

namespace Elk.Test{
public class StatusFuncsTest{

    [Test] public void GetStatusFunctions(){
        var type = typeof(status);
        var methods = type.GetMethods(BF.Static | BF.Public);
        //foreach(var m in methods){
        //    Debug.Log(m);
        //}
    }

    [Test] public void GetIntFunctions(){
        var type = typeof(int);
        var methods = type.GetMethods(BF.Static | BF.Public);
        //foreach(var m in methods){
        //    Debug.Log(m);
        //}
    }

    [Test] public void GetNumberFunctions(){
        var type = typeof(int);
        var methods = type.GetMethods(BF.Static | BF.Public);
        //foreach(var m in methods){
        //    Debug.Log(m);
        //}
    }

}}
