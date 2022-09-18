using Array = System.Array;
using Ex = System.Exception;
using UnityEngine;
using Elk.Util;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;

namespace Elk.Basic{
public class InvocationEval{

    public object Eval(Invocation ι, Runner ρ, Context cx){
        var values = EvalArgs(ι.arguments, ρ, cx);
        cx.graph.Push(ι.name + values.Format());
        var @out = DoEval(ι, values, ρ, cx);
        cx.graph.Pop(@out);
        return @out;
    }

    public object DoEval(Invocation inv, object[] values,
                                Runner ρ, Context cx){
        object @out;
        if(Invoke(inv.name, inv.arguments, values, ρ, cx, out @out)){
            return @out;
        }
        foreach(var target in cx.externals){
            if(CSharpBindings.Invoke(target, inv.name, values,
                                     out @out)){
                return @out;
            }
        }
        throw new Ex($"Function not in scope: {inv.name}");
    }

    public bool Invoke(string name, object[] args,
                       object[] values, Runner ρ,
                       Context cx, out object @out){
        foreach(var module in cx.modules){
            var fdef = Array.Find(
                module,
                x => FuncMatches(x, name, args?.Length ?? 0)
            );
            if(fdef != null){
                @out = InvokeInternalFunction(fdef, values, ρ, cx);
                return true;
            }
        }
        @out = null;
        return false;
    }

    bool FuncMatches(FuncDef fdef, string name, int argLength){
        if(fdef == null){
            Debug.LogError("fdef is null");
            return false;
        }
        if(fdef.name == null){
            Debug.LogError("fdef name is null");
            return false;
        }
        return fdef.name == name && fdef.paramCount == argLength;
    }

    object InvokeInternalFunction(FuncDef func,
                                                object[] values,
                                                Runner ρ,
                                                Context cx){
        cx.Push(func.parameters, values);
        var @out = ρ.Eval(func.body, cx);
        cx.Pop();
        return @out;
    }

    object[] EvalArgs(object[] args, Runner ρ, Context cx){
        var len = args?.Length ?? 0;
        var @out = new object[len];
        for(int i = 0; i < len; i++){
            @out[i] = ρ.Eval(args[i], cx);
        }
        return @out;
    }

}}