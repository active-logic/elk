using NUnit.Framework;
using UnityEngine;
using BTL = Activ.BTL.BTL;

namespace BTL_Test{
public class BTL_Test{

    BTL btl;
    GameObject temp;

    [Test] public void TestRecordEvent() => Assert.That(

       btl.RecordEvent("Attack", "target"),
       Is.EqualTo("self.Attack(target)")
    );

    [SetUp] public void Setup(){
        temp = new GameObject("Temp");
        btl = temp.AddComponent<BTL>();
        //btl.cognition = new BTLCog(btl);
        btl.Awake();
    }

    [TearDown] public void Teardown(){
        Object.DestroyImmediate(temp);
    }

}}
