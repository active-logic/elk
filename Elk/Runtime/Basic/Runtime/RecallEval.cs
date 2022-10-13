using Ex = System.Exception;
using Elk.Util;
using Elk.Basic.Graph;
using Elk.Memory;

namespace Elk.Basic.Runtime{
public class RecallEval{

    public bool Eval(Recall r, Runner ρ, Context cx){
        var action = EvalEvent(r.action, ρ, cx);
        var since  = r.since == null
                     ? null : EvalEvent(r.since, ρ, cx);
        return cx.cog.Did(action, since, r.strict, cx);
    }

    Call EvalEvent(object e, Runner ρ, Context cx){
        switch(e){
            case BinaryExp exp when exp.op == ".":
                var subject = ρ.Eval(exp.arg0, cx);
                var call = ρ.inv.Recall((Invocation)exp.arg1, ρ, cx);
                return new Call(
                    cx.cog.NameOf(subject, allowDefault: false),
                    call
                );
            case Invocation ι:
                return new Call("self", ρ.inv.Recall(ι, ρ, cx));
            default:
                throw new Ex(
                    $"Cannot eval {e} of type {e?.GetType().Name}"
                );
        }
    }

}}
