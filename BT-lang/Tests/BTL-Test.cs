using NUnit.Framework;
using UnityEngine;
using BTL = Activ.BTL.BTL;
using Active.Core; using static Active.Status;

namespace BTL_Test{
public class BTL_Test{

    BTL btl;
    GameObject temp;

    [Test] public void TestRecordEvent(){
        btl.RecordEvent(("dog", "Attack", "target"), done());
        Assert.That(
            btl.record.Contains(("dog", "Attack", "target")),
            Is.True);
    }

    [SetUp] public void Setup(){
        temp = new GameObject("Temp");
        btl = temp.AddComponent<BTL>();
    }

    [TearDown] public void Teardown(){
        Object.DestroyImmediate(temp);
    }

}}
