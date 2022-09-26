using Elk.Basic.Graph;
using UnityEngine;

namespace Elk.Basic{
public class RecallEval{

    public bool Eval(Recall r, Runner ρ, Context cx){
        var action = "✓ " + ρ.inv.Recall(r.action, ρ, cx);
        var since = ρ.inv.Recall(r.since, ρ, cx);
        if(!string.IsNullOrEmpty(since)){
            since = "✓ " + since;
        }
        return cx.history.Did(action, since, r.strict);
    }

}}
