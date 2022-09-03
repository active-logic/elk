using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Elk.Test{
public class RunnerTest{

    [Test] public void Test_1_PLUS_1(){
        var add = new BinaryOp("1", "+", "1");
        var runner = new Runner0();
        Assert.That( runner[add], Is.EqualTo(2)  );
    }

    [Test] public void Test_2_TIMES_3(){
        var mul = new BinaryOp("2", "*", "3");
        var runner = new Runner0();
        Assert.That( runner[mul], Is.EqualTo(6)  );
    }

    [Test] public void Test_COMPLEX_EXP(){
        var leaf = new BinaryOp("1", "+", "1");
        var root = new BinaryOp(leaf, "*", "3");
        var runner = new Runner0();
        Assert.That( runner[root], Is.EqualTo(6)  );
    }

}}
