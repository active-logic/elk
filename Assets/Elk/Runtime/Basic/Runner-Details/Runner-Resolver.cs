using Array = System.Array;
using UnityEngine;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;

namespace Elk.Basic{
// Function + Properties resolver
public partial class Runner{  // -Resolver

    public object Invoke(Invocation inv, Context cx){
        if(TryInvokeInternalFunction(inv.name, inv.arguments, cx,
                                     out object @out)){
            return @out;
        }
        return CSharpBindings.Invoke(cx, inv.name, inv.arguments);
    }

    public object EvalProperty(string name, Context cx)
    => cx.HasKey(name) ? cx[name]
                       : CSharpBindings.Fetch(cx.externals, name);

    public bool TryInvokeInternalFunction(string name,
                                          object[] args,
                                          Context cx,
                                          out object @out){
        Debug.Log($"Try invoke internal: {name}");
        foreach(var module in cx.modules){
            Debug.Log($"Check module {module}");
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
        Debug.Log($"Check fdef {fdef}");
        if(fdef.name == null){
            Debug.LogError("fdef name is null");
            return false;
        }
        return fdef.name == name && fdef.paramCount == argLength;
    }

    public object InvokeInternalFunction(FuncDef func,
                                         object[] args,
                                         Context cx){
        var len = args?.Length ?? 0;
        var @in = new object[len];
        for(int i = 0; i < len; i++){
            @in[i] = this.Run(args[i], cx);
        }
        cx.Push(func.parameters, @in);
        var @out = this.Run(func.body, cx);
        cx.Pop();
        return @out;
    }

}}
