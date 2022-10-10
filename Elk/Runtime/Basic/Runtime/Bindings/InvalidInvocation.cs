using Elk.Basic.Graph;
using Ex = System.Exception;

namespace Elk.Basic.Runtime{
public class InvalidInvocation : InvocationBinding{

    readonly string name;

    public InvalidInvocation(string name)
    => this.name = name;

    override protected object Invoke(
        object[] values, Runner<Context> ρ, Context cx
    ) => throw new Ex($"Function not found: {name}");

}}
