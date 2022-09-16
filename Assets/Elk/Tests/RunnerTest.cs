using NUnit.Framework;
using Elk.Basic.Graph;

namespace Elk.Test{
public class RunnerTest{

    Elk.Basic.Runner runner;

    [SetUp] public void Setup() => runner = new Elk.Basic.Runner();

    [Test] public void Test_1_PLUS_1(){
        var add = new BinaryOp(1, "+", 1);
        Assert.That( Eval(add), Is.EqualTo(2)  );
    }

    [Test] public void Test_2_TIMES_3(){
        var mul = new BinaryOp(2, "*", 3);
        Assert.That( Eval(mul), Is.EqualTo(6)  );
    }

    [Test] public void Test_COMPLEX_EXP(){
        var leaf = new BinaryOp(1, "+", 1);
        var root = new BinaryOp(leaf, "*", 3);
        Assert.That( Eval(root), Is.EqualTo(6)  );
    }

    object Eval(object arg) => runner.Eval(arg, null);

}}
