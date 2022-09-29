using System;
using NUnit.Framework;
using Elk.Basic.Runtime;
using Elk.Basic.Graph;

namespace Elk_Test{
public class BinEvalTest{

    BinEval f = new BinEval();

    // Supported by design -----------------------------------------

    [Test] public void Test_Less(){
        Assert.That( f.Eval_obj_x_obj(5, "<", 6), Is.True );
    }

    [Test] public void Test_LessOrEqual(){
        Assert.That( f.Eval_obj_x_obj(5, "<=", 6), Is.True );
    }

    // Only supporting float, int ----------------------------------

    [Test] public void TestAdd_b_b() => Assert.Throws<Exception>(
         () => f.Eval_obj_x_obj((byte)1, "+" ,(byte)5)
    );

    // No implicit conversions -------------------------------------

    [Test] public void TestAdd_f_i(){
        Assert.Throws<InvalidCastException>(
            () => f.Eval_obj_x_obj(1.5f, "+", 1)
        );
    }

}}
