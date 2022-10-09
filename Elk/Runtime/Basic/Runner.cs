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

    public Runner(){
        bin = new BinEval();
        una = new UnaEval();
        prp = new PropEval();
        inv = new InvocationEval();
        rec = new RecallEval();
    }

    public object Invoke(string func, Context cx)
    => Eval(new Invocation(func), cx);

    public object Eval(object arg, Context cx){
        switch(arg){
            case BinaryExp     op: return bin.Eval(op, this, cx);
            case UnaryExp      op: return una.Eval(op, this, cx);
            case Identifier label: return prp.Eval(label, cx);
            case Invocation ι:
                var pass = Intercept(ι, cx);
                if(pass.i){
                    return inv.Bypass(ι, pass.e, pass.r, cx);
                }else{
                    return inv.Eval(ι, pass.e, this, cx);
                }
            case Recall     r: return rec.Eval(r, this, cx);
            case Singleton  s: return Eval(s.content, cx);
            case object val when IsLiteral(val): return val;
            default: throw new Ex($"Cannot evaluate {arg} of type {arg.GetType()}");
        }
    }

    public void EvalArgs(object[] args, object[] @out, Context cx){
        var len = args?.Length ?? 0;
        for(int i = 0; i < len; i++){
            @out[i] = this.Eval(args[i], cx);
        }
    }

    // Override to intercept invocations; ref 'Pass.cs'
    virtual protected Pass Intercept(Invocation ι, Context cx)
    => new Pass();

    // Whereas unknown types will cause an error, traversed literals
    // are returned 'as is'
    virtual protected bool IsLiteral(object arg)
    => arg is bool || arg is int || arg is float || arg is null;

}}
