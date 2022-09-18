using NUnit.Framework;
using Elk.Basic.Graph;

namespace Elk_Test{
public class RunnerTest{

    Elk.Basic.Runner runner;

    [SetUp] public void Setup() => runner = new Elk.Basic.Runner();

    [Test] public void Test_1_PLUS_1(){
        var add = new BinaryExp(1, "+", 1);
        Assert.That( Eval(add), Is.EqualTo(2)  );
    }

    [Test] public void Test_2_TIMES_3(){
        var mul = new BinaryExp(2, "*", 3);
        Assert.That( Eval(mul), Is.EqualTo(6)  );
    }

    [Test] public void Test_COMPLEX_EXP(){
        var leaf = new BinaryExp(1, "+", 1);
        var root = new BinaryExp(leaf, "*", 3);
        Assert.That( Eval(root), Is.EqualTo(6)  );
    }

    object Eval(object arg) => runner.Eval(arg, null);

}}
