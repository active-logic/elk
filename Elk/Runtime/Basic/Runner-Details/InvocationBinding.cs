using System.Reflection;
using Elk.Basic.Graph;
using Elk.Util;

namespace Elk.Basic.Runtime{
public abstract class InvocationBinding{

    public object Eval(Invocation ι, Runner ρ, Context cx)
    =>  Invoke(ι.values, ρ, cx);

    protected abstract object Invoke(
        object[] values, Runner<Context> ρ, Context cx
    );

}}
