using Array = System.Array;
using Ex = System.Exception;
using UnityEngine;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;

namespace Elk.Basic{
public static class InvocationEval{

    public static object Eval(Invocation inv, Runner ρ, Context cx){
        if(cx == null)
            throw new Ex($"Cannot invoke {inv.name} out of context");
        object @out;
        if(TryInvokeInternalFunction(inv.name, inv.arguments, ρ, cx,
                                     out @out)){
            return @out;
        }
        var arguments = EvalArgs(inv.arguments, ρ, cx);
        foreach(var target in cx.externals){
            if(CSharpBindings.Invoke(target, inv.name, @arguments,
                                     out @out)){
                return @out;
            }
        }
        throw new Ex($"Function not in scope: {inv.name}");
    }

    public static bool TryInvokeInternalFunction(string name,
                                          object[] args,
                                          Runner ρ,
                                          Context cx,
                                          out object @out){
        //ebug.Log($"Try invoke internal: {name}");
        foreach(var module in cx.modules){
            //ebug.Log($"Check module {module}");
            var fdef = Array.Find(
                module,
                x => FuncMatches(x, name, args?.Length ?? 0)
            );
            if(fdef != null){
                @out = InvokeInternalFunction(fdef, args, ρ, cx);
                return true;
            }
        }
        @out = null;
        return false;
    }

    static bool FuncMatches(FuncDef fdef, string name, int argLength){
        if(fdef == null){
            Debug.LogError("fdef is null");
            return false;
        }
        //ebug.Log($"Check fdef {fdef}");
        if(fdef.name == null){
            Debug.LogError("fdef name is null");
            return false;
        }
        return fdef.name == name && fdef.paramCount == argLength;
    }

    public static object InvokeInternalFunction(FuncDef func,
                                                object[] args,
                                                Runner ρ,
                                                Context cx){
        var @in = EvalArgs(args, ρ, cx);
        cx.Push(func.parameters, @in);
        var @out = ρ.Eval(func.body, cx);
        cx.Pop();
        return @out;
    }

    static object[] EvalArgs(object[] args, Runner ρ, Context cx){
        var len = args?.Length ?? 0;
        var @out = new object[len];
        for(int i = 0; i < len; i++){
            @out[i] = ρ.Eval(args[i], cx);
        }
        return @out;
    }

}}
