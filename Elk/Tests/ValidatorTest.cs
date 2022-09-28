using System;
using NUnit.Framework;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Module = Elk.Basic.Graph.Module;
using Elk.Basic;

namespace Elk_Test{
public class ValidatorTest{

    Validator v = new Validator();

    [Test] public void Test_NonModule(){
        var program = "program";
        Assert.Throws<Exception>( () => v.Validate(program) );
    }

    [Test] public void Test_withDistinctNames(){
        var program = Create(
            new FuncDef("A", new string[]{"p0", "p1"}, null),
            new FuncDef("B", new string[]{"p0", "p1"}, null)
        );
        // Nothing to assert, ensure no throw
        v.Validate(program);
    }

    [Test] public void Test_withDistinctParamCount(){
        var program = Create(
            new FuncDef("A", new string[]{"p0", "p1"}, null),
            new FuncDef("A", new string[]{"p0", "p1", "p2"}, null)
        );
        // Nothing to assert, ensure no throw
        v.Validate(program);
    }

    [Test] public void Test_disallowDupeFunctionDef(){
        var program = Create(
            new FuncDef("A", new string[]{"p0", "p1"}, null),
            new FuncDef("A", new string[]{"r0", "r1"}, null)
        );
        Assert.Throws<Exception>( () => v.Validate(program) );
    }

    Module Create(params FuncDef[] args)
    => new Module(includes: null, functions: args);

}}
