using Ex = System.Exception;
using UnityEngine;
using Elk.Util;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public class InvocationEval{

    public object Eval(Invocation ι, Runner ρ, Context cx){
        ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        cx.graph.Push(ι.name + ι.values.NeatFormat());
        var @out = DoEval(ι, ρ, cx);
        cx.graph.Pop(@out);
        return @out;
    }

    public object DoEval(Invocation ι, Runner ρ, Context cx){
        if(ι.binding == null){
            ι.binding =  cx.modules  .Bind(ι, ρ, cx)
                      ?? cx.externals.Bind(ι.name, ι.values);
        }
        return ((InvocationBinding)ι.binding).Eval(ι, ρ, cx);
    }

}}
