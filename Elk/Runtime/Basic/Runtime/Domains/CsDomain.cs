using Elk.Basic.Graph;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Runtime{
public class CsDomain : Domain{

    object[] objects;

    public CsDomain(object[] objects)
    => this.objects = objects;

    public PropertyBinding Bind(Identifier id, Context cx)
    => objects.Bind(id.value);

    public InvocationBinding Bind(Invocation inv, Context cx)
    => objects.Bind(inv.name, inv.values);

}}
