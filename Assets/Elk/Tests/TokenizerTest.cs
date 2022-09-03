using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Elk.Test{
public class TokenizerTest{

    [Test] public void Test_T0_Pass(){
        var t = new Tokenizer0();
        Assert.AreEqual(
            new string[]{"big", "time"},
            t["big time"]
        );
    }

    [Test] public void Test_T0_Pass_withExtraSpace(){
        var t = new Tokenizer0();
        Assert.AreEqual(
            new string[]{"big", "time"},
            t["big          time  "]
        );
    }

    [Test] public void Test_T0_Pass_withSpecialWhitespace(){
        var t = new Tokenizer0();
        Assert.AreEqual(
            new string[]{"big", "time"},
            t["big\n\rtime"]
        );
    }

}}
