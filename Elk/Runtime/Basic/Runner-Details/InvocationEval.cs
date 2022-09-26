using Ex = System.Exception;
using Elk.Util;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public class InvocationEval{

    public object Eval(Invocation ι, Runner ρ, Context cx){
        ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        cx.StackPush(ι.name + ι.values.NeatFormat());
        var @out = DoEval(ι, ρ, cx);
        cx.StackPop(@out);
        return @out;
    }

    // PROVISIONAL - evaluate invocation arguments, then return
    // the matching stack entry, without a return value
    public string Recall(Invocation ι, Runner ρ, Context cx){
        if(ι == null) return null;
        ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        return ι.name + ι.values.NeatFormat();
    }

    public object DoEval(Invocation ι, Runner ρ, Context cx){
        if(ι.binding == null){
            ι.binding =  cx.modules  .Bind(ι, ρ, cx)
                      ?? cx.externals.Bind(ι.name, ι.values);
        }
        var binding = (InvocationBinding)ι.binding;
        if(binding == null) throw new Ex($"`{ι}` not found");
        return ((InvocationBinding)ι.binding).Eval(ι, ρ, cx);
    }

}}
