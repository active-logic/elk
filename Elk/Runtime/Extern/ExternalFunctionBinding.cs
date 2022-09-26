using System.Reflection;
using Elk.Util;
using Elk.Basic;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Bindings.CSharp{
public class ExternalFunctionBinding : InvocationBinding{

    readonly object target;
    readonly MethodInfo method;

    public ExternalFunctionBinding(object target, MethodInfo method){
        this.target = target;
        this.method = method;
    }

    override protected object Invoke(
        object[] values, Runner<Context> ρ, Context cx
    ) => method.Invoke(target, values);

}}
