using NUnit.Framework;

namespace Elk.Test{
public class ParserTest{

    [Test] public void Test_1_op(){
        var p = new Parser0();
        Assert.AreEqual(
            "(1+2)",
            p["1", "+", "2"]?.ToString()
        );
    }

    [Test] public void Test_2_ops(){
        var p = new Parser0();
        Assert.AreEqual(
            "((1+2)+3)",
            p["1", "+", "2", "+", "3"]?.ToString()
        );
    }

}}
