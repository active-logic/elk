using Ex = System.Exception;
using Elk.Util;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public class InvocationEval{

    public object Eval(Invocation ι, bool didEvalArgs,
                       Runner ρ, Context cx){
        if(!didEvalArgs){
            ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        }
        cx.StackPush(ι.name + ι.values.NeatFormat(), ι.id);
        var @out = DoEval(ι, ρ, cx);
        cx.StackPop(@out);
        return @out;
    }

    public object Bypass(
        Invocation ι, bool didEvalArgs, object sub, Context cx
    ){
        cx.StackPush(
            ι.name + (didEvalArgs ? ι.values.NeatFormat() : "(-)"),
            ι.id
        );
        cx.StackPop(sub);
        return sub;
    }

    // PROVISIONAL - evaluate invocation arguments, then return
    // the matching stack entry, without a return value
    public string Recall(Invocation ι, Runner ρ, Context cx){
        if(ι == null) return null;
        ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        return ι.name + ι.values.NeatFormat();
    }

    public object DoEval(Invocation ι, Runner ρ, Context cx){
        if(ι.binding == null)
            ι.binding = cx.BindMethod(ι, ρ, cx);
        var binding = ι.binding as InvocationBinding;
        return binding != null
             ? binding.Eval(ι, ρ, cx)
             : throw new Ex($"`{ι}` not found");
    }

}}
