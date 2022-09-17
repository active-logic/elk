using System;
using Ex = System.Exception;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public partial class Runner : Elk.Runner<Context>{

    public BinEval binEval;
    public PropEval propEval;
    public InvocationEval invocationEval;
    public Func<object, bool> valuable;

    public Runner(){
        binEval = new BinEval();
        propEval = new PropEval();
        invocationEval = new InvocationEval();
        valuable = IsValue;
    }

    public object Eval(object arg, Context cx){
        switch(arg){
            case BinaryOp op:
                return binEval.Eval(op, this, cx);
            case string label:
                return propEval.Eval(label, cx);
            case Invocation ι:
                return invocationEval.Eval(ι, this, cx);
            case object val when valuable(val):
                return val;
            case null:
                return arg;
            default:
                throw new Ex($"Cannot evaluate {arg}");
        }
    }

    static bool IsValue(object arg)
    => arg is bool || arg is int;

    //static bool IsValue(object arg)
    //=> arg == null || arg is bool || arg is status || arg is int;

}}
