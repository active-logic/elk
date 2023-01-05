using Ex = System.Exception;
using Elk.Util;
using Elk.Basic.Graph;
using Elk.Memory;

namespace Elk.Basic.Runtime{
public class RecallEval{

    public bool Eval(Recall r, Runner ρ, Context cx){
        Occurence action = EvalEvent(r.action, ρ, cx);
        Occurence? since = (r.since == null)
            ? null : EvalEvent(r.since, ρ, cx);
        return cx.cog.Did(action, since, r.strict, cx);
    }

    Occurence EvalEvent(object e, Runner ρ, Context cx){
        switch(e){
            case BinaryExp exp when exp.op == ".":
                var subject = ρ.Eval(exp.arg0, cx);
                var c0 = ρ.inv.Recall((Invocation)exp.arg1, ρ, cx);
                return (
                    cx.cog.NameOf(subject, allowDefault: false),
                    c0.verb,
                    Object(c0, cx)
                );
            case Invocation ι:
                var c1 = ρ.inv.Recall(ι, ρ, cx);
                return (
                    "self",
                    c1.verb,
                    Object(c1, cx)
                );
            default:
                throw new Ex(
                    $"Cannot eval {e} of type {e?.GetType().Name}"
                );
        }
    }

    string Object((string verb, object[] args) call, Context cx){
        var obj = call.args.Length == 0 ? null : call.args[0];
        return cx.cog.NameOf(obj, allowDefault: true);
    }

}}
