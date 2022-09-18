using System;
using Ex = System.Exception;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public partial class Runner : Elk.Runner<Context>{

    public UnaEval una;
    public BinEval bin;
    public PropEval prp;
    public InvocationEval inv;
    public Func<object, bool> literal;

    public Runner(){
        bin = new BinEval();
        una = new UnaEval();
        prp = new PropEval();
        inv = new InvocationEval();
        literal = IsLiteral;
    }

    public object Eval(object arg, Context cx){
        switch(arg){
            case BinaryExp op: return bin.Eval(op, this, cx);
            case UnaryExp  op: return una.Eval(op, this, cx);
            case string label: return prp.Eval(label, cx);
            case Invocation ι: return inv.Eval(ι, this, cx);
            case object val when literal(val): return val;
            case null: return arg;
            default: throw new Ex($"Cannot evaluate {arg}");
        }
    }

    static bool IsLiteral(object arg) => arg is bool || arg is int;

}}
