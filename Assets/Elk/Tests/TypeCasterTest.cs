using NUnit.Framework;
using Active.Core; using static Active.Raw;
using Elk.Util; using Elk.Basic.Graph;

namespace Elk.Test{
public class TypeCasterTest{

    Elk.Basic.TypeCaster typecaster;

    [SetUp] public void Setup() => typecaster = new Elk.Basic.TypeCaster();

    [Test] public void Test(){
        var s = new Sequence("5", "foo", "true", "false", "done", "cont", "fail", "null");
        typecaster.Transform(s);
        Assert.That( s[0], Is.EqualTo(5)  );
        Assert.That( s[1], Is.EqualTo("foo")  );
        Assert.That( s[2], Is.EqualTo(true)  );
        Assert.That( s[3], Is.EqualTo(false)  );
        Assert.That( s[4], Is.EqualTo(done)  );
        Assert.That( s[5], Is.EqualTo(cont)  );
        Assert.That( s[6], Is.EqualTo(fail)  );
        Assert.That( s[7], Is.EqualTo(null)  );
    }

}}
