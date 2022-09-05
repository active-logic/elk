using NUnit.Framework;
using Elk.Basic.Graph;

namespace Elk.Test{
public class RunnerTest{

    Elk.Basic.Runner runner;

    [SetUp] public void Setup() => runner = new Elk.Basic.Runner();

    [Test] public void Test_1_PLUS_1(){
        var add = new BinaryOp("1", "+", "1");
        Assert.That( Run(add), Is.EqualTo(2)  );
    }

    [Test] public void Test_2_TIMES_3(){
        var mul = new BinaryOp("2", "*", "3");
        Assert.That( Run(mul), Is.EqualTo(6)  );
    }

    [Test] public void Test_COMPLEX_EXP(){
        var leaf = new BinaryOp("1", "+", "1");
        var root = new BinaryOp(leaf, "*", "3");
        Assert.That( Run(root), Is.EqualTo(6)  );
    }

    object Run(object arg) => runner.Run(arg, null);

}}
