using Elk;
using Elk.Basic;
using Elk.Basic.Runtime;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Active.Core;
using BTLBinEval    = Activ.BTL.Imp.BinEval;
using BTLTypeCaster = Activ.BTL.Imp.TypeCaster;

namespace Activ.BTL{
public static class BTLInterpreterFactory{

    public static Interpreter<Context> Create(){
        var interpreter = new Elk.Basic.Interpreter("task");
        interpreter.entry = "Step";
        interpreter.reader.typecaster = new BTLTypeCaster();
        var runner = new BTLRunner();
        interpreter.runner = runner;
        runner.bin = new BTLBinEval();
        return interpreter;
    }

}}
