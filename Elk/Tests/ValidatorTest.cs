using System;
using NUnit.Framework;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Elk.Basic;

namespace Elk_Test{
public class ValidatorTest{

    Validator v = new Validator();

    // Allows simple parsing tests to pass
    [Test] public void Test_allowSubprograms(){
        var program = "not a module";
        // Nothing to assert, ensure no throw
        v.Validate(program, allowSubPrograms: true);
    }

    [Test] public void Test_disallowSubprograms(){
        var program = "not a module";
        // Nothing to assert, ensure no throw
        Assert.Throws<NullReferenceException>(
            () => v.Validate(program, allowSubPrograms: false)
        );
    }

    [Test] public void Test_withDistinctNames(){
        var program = new FuncDef[]{
            new FuncDef("A", new string[]{"p0", "p1"}, null),
            new FuncDef("B", new string[]{"p0", "p1"}, null)
        };
        // Nothing to assert, ensure no throw
        v.Validate(program);
    }

    [Test] public void Test_withDistinctParamCount(){
        var program = new FuncDef[]{
            new FuncDef("A", new string[]{"p0", "p1"}, null),
            new FuncDef("A", new string[]{"p0", "p1", "p2"}, null)
        };
        // Nothing to assert, ensure no throw
        v.Validate(program);
    }

    [Test] public void Test_disallowDupeFunctionDef(){
        var program = new FuncDef[]{
            new FuncDef("A", new string[]{"p0", "p1"}, null),
            new FuncDef("A", new string[]{"r0", "r1"}, null)
        };
        Assert.Throws<Exception>( () => v.Validate(program) );
    }

}}
