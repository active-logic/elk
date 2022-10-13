using NUnit.Framework;
using Elk;
using BinaryExp = Elk.Basic.Graph.BinaryExp;
using Context = Elk.Basic.Context;
using Activ.BTL; using Active.Core;

namespace BTL_Test{
public class BTL_InterpreterTest{

    Interpreter<Context> i;

    [SetUp] public void Setup(){
        i = BTLInterpreterFactory.Create();
        i.reader.validator = null;
    }

    [Test] public void TestInvocation()
    => Parse("Attack()");

    [Test] public void TestLogicalOr_2()
    => Parse("Attack() || Roam");

    [Test] public void TestLogicalOr_1()
    => Parse("Attack() || Roam()");

    [Test] public void TestFunc_A()
    => Parse("task Main() => Attack() || Roam();");

    [Test] public void TestLiterals_1(){
        var root = Parse("true || false") as BinaryExp;
        Assert.That( root.arg0 is bool );
        Assert.That( root.arg1 is bool );
    }

    [Test] public void TestLiterals_2(){
        var root = Parse("done || fail") as BinaryExp;
        Assert.That( root.arg0 is status );
        Assert.That( root.arg1 is status );
    }

    [Test] public void TestScript1()
    => Parse(script1);

    [Test] public void TestScript2()
    => Parse(script2);

    object Parse(string arg){
        var debug = new System.Collections.Generic.List<string>();
        try{
            var @out = i.Parse(arg, debug);
            return @out;
        }catch(ElkParsingException ex){
            Log("PARSING FAILED; DEBUG INFO...");
            //foreach(var k in debug) Log(k);
            Log(string.Join("\n", debug));
            Log("END OF DEBUG INFO");
            throw ex;
        }
    }

    void Log(string arg) => UnityEngine.Debug.Log(arg);

// ------------------------------------------------------------------

    const string script1 =
@"
task Step()
=> Encounter(player!) || Support(player!) || Roam();

task Encounter(target)
=> Heal(player!) || (!did Greet(target) && !foeNearby && GoGreet(target));
";

    const string script2 =
@"
task Support(target)
=> did foe.Attack(target) && Attack();
";

}}
