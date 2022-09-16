using Array = System.Array;
using Ex = System.Exception;
using UnityEngine;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;

namespace Elk.Basic{
// Function + Properties resolver
public partial class Runner{  // -Resolver

    public object Invoke(Invocation inv, Context cx){
        object @out;
        if(TryInvokeInternalFunction(inv.name, inv.arguments, cx,
                                     out @out)){
            return @out;
        }
        var arguments = EvalArgs(inv.arguments, cx);
        foreach(var target in cx.externals){
            if(CSharpBindings.Invoke(target, inv.name, @arguments, out @out)){
                return @out;
            }
        }
        throw new Ex($"Function not in scope: {inv.name}");
    }

    public object EvalProperty(string name, Context cx)
    => cx.HasKey(name) ? cx[name]
                       : CSharpBindings.Fetch(cx.externals, name);

    public bool TryInvokeInternalFunction(string name,
                                          object[] args,
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
                @out = InvokeInternalFunction(fdef, args, cx);
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
        //ebug.Log($"Check fdef {fdef}");
        if(fdef.name == null){
            Debug.LogError("fdef name is null");
            return false;
        }
        return fdef.name == name && fdef.paramCount == argLength;
    }

    public object InvokeInternalFunction(FuncDef func,
                                         object[] args,
                                         Context cx){
        var @in = EvalArgs(args, cx);
        cx.Push(func.parameters, @in);
        var @out = this.Eval(func.body, cx);
        cx.Pop();
        return @out;
    }

    object[] EvalArgs(object[] args, Context cx){
        var len = args?.Length ?? 0;
        var @out = new object[len];
        for(int i = 0; i < len; i++){
            @out[i] = this.Eval(args[i], cx);
        }
        return @out;
    }

}}
