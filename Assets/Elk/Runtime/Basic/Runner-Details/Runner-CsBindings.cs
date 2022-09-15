using UnityEngine;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;

namespace Elk.Basic{  // C# bindings (externals)
public partial class Runner : Elk.Runner<Context>{

    // TODO problem here is we are not wanting to bind
    // just the parent game object; in general we're wanting
    // to bind a composite context.
    // For example BTL is going to have imported methods (from some
    // components) and imported properties (other components!)
    // But also the context needs to include a hash of internal
    // methods.

    public object CsInvoke(Invocation inv, Context cx)
    => CSharpBindings.Invoke(cx, inv.name, inv.arguments);

    //public bool CsEval(string name, Context cx, ref object @out){
    //    @out = CSharpBindings.Fetch(cx, name);
    //    return true;
    //}

}}
