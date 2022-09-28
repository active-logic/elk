using System;
using Ex = System.Exception;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public class Runner : Elk.Runner<Context>{

    public UnaEval una;
    public BinEval bin;
    public PropEval prp;
    public InvocationEval inv;
    public RecallEval rec;
    public Func<object, bool> literal;

    public Runner(){
        bin = new BinEval();
        una = new UnaEval();
        prp = new PropEval();
        inv = new InvocationEval();
        rec = new RecallEval();
        literal = IsLiteral;
    }

    public object Invoke(string func, Context cx)
    => Eval(new Invocation(func), cx);

    public object Eval(object arg, Context cx){
        switch(arg){
            case BinaryExp op: return bin.Eval(op, this, cx);
            case UnaryExp  op: return una.Eval(op, this, cx);
            case string label: return prp.Eval(label, cx);
            case Invocation ι: return inv.Eval(ι, this, cx);
            case Recall     r: return rec.Eval(r, this, cx);
            case object val when literal(val): return val;
            case null: return arg;
            default: throw new Ex($"Cannot evaluate {arg}");
        }
    }

    public void EvalArgs(object[] args, object[] @out, Context cx){
        var len = args?.Length ?? 0;
        for(int i = 0; i < len; i++){
            @out[i] = this.Eval(args[i], cx);
        }
    }

    static bool IsLiteral(object arg) => arg is bool || arg is int;

}}
