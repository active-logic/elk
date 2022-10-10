using System;
using NUnit.Framework;
using Elk.Util; using Elk.Basic.Graph;

namespace Elk_Test{
public class TypeCasterTest{

    Elk.Basic.TypeCaster typecaster;

    [SetUp] public void Setup() => typecaster = new Elk.Basic.TypeCaster();

    [Test] public void Test(){
        var s = new Sequence("5", "5.4f", "foo", "true", "false", "done", "cont", "fail", "null");
        typecaster.Transform(s);
        Assert.That( s[0], Is.EqualTo(5)  );
        Assert.That( s[1], Is.EqualTo(5.4f).Within(0.001f) );
        Assert.That( s[2].ToString(), Is.EqualTo("(id:foo)")  );
        Assert.That( s[3], Is.EqualTo(true)  );
        Assert.That( s[4], Is.EqualTo(false)  );
        Assert.That( s[5].ToString(), Is.EqualTo("(id:done)")  );
        Assert.That( s[6].ToString(), Is.EqualTo("(id:cont)")  );
        Assert.That( s[7].ToString(), Is.EqualTo("(id:fail)")  );
        Assert.That( s[8], Is.EqualTo(null)  );
    }

    [Test] public void Test_TransformToken_int(){
        var x = typecaster.TransformToken("5");
        Assert.That( x, Is.EqualTo(5)  );
        Assert.That( x.GetType(), Is.EqualTo(typeof(int))  );
    }

    [Test] public void Test_TransformToken_float(){
        var x = typecaster.TransformToken("5f");
        Assert.That( x, Is.EqualTo(5)  );
        Assert.That( x.GetType(), Is.EqualTo(typeof(float))  );
    }

    [Test] public void Test_No_Doubles(){
        var s = new Sequence("5.5");
        Assert.Throws<FormatException>(
            () => typecaster.Transform(s)
        );
    }

}}
