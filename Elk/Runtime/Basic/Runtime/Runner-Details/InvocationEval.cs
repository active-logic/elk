using Ex = System.Exception;
using Elk.Util;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public class InvocationEval{

    public object Eval(
        Invocation ι, Runner ρ, Context cx
    ){
        // Process args or intercept
        cx.propsToStack = false;
        ρ.Intercept(ι, cx, out Pass pass);
        if(!pass.e && pass.i){
            ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        }
        cx.propsToStack = true;
        // Eval or bypass
        if(pass.i){
            return Bypass(ι, pass.e, pass.r, cx);
        }else{
            return EvalBody(ι, pass.e, ρ, cx);
        }
    }

    object EvalBody(Invocation ι, bool didEvalArgs,
                       Runner ρ, Context cx){
        cx.propsToStack = true;
        cx.StackPush(ι.name + ι.values.NeatFormat(), ι.id);
        var @out = DoEval(ι, ρ, cx);
        cx.StackPop(@out, bypass: false);
        return @out;
    }

    object Bypass(
        Invocation ι, bool didEvalArgs, object sub, Context cx
    ){
        cx.StackPush(
            ι.name + (didEvalArgs ? ι.values.NeatFormat() : "(-)"),
            ι.id
        );
        cx.StackPop(sub, bypass: true);
        return sub;
    }

    // PROVISIONAL - evaluate invocation arguments, then return
    // the matching stack entry, without a return value
    public (string verb, object[] args) Recall(
        Invocation ι, Runner ρ, Context cx
    ){
        if(ι == null) return (null, null);
        ρ.EvalArgs(ι.arguments, @out: ι.values, cx);
        return (ι.name, ι.values);
    }

    object DoEval(Invocation ι, Runner ρ, Context cx){
        if(ι.binding == null)
            ι.binding = cx.BindMethod(ι, ρ, cx);
        var binding = ι.binding as InvocationBinding;
        return binding != null
             ? binding.Eval(ι, ρ, cx)
             : throw new Ex($"`{ι}` not found");
    }

}}
