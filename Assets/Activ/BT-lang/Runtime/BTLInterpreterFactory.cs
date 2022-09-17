using Elk;
using Elk.Basic;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Active.Core;
using BTLBinEval    = Activ.BTL.Imp.BinEval;
using BTLTypeCaster = Activ.BTL.Imp.TypeCaster;

namespace Activ.BTL{
public static class BTLInterpreterFactory{

    public static Interpreter<Context> Create(){
        var interpreter = new Elk.Basic.Interpreter();
        interpreter.typecaster = new BTLTypeCaster();
        var runner = (Elk.Basic.Runner) interpreter.runner;
        runner.valuable =
            x => x is bool || x is int || x is status;
        runner.binEval = new BTLBinEval();
        return interpreter;
    }

}}
