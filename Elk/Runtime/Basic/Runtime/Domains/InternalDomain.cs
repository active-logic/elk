using Elk.Basic;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class InternalDomain : Domain{

    Context context;

    public InternalDomain(Context cx)
    => this.context = cx;

    public PropertyBinding Bind(Identifier id, Context cx)
    => cx.HasKey(id.value)
        ? new InternalPropertyBinding(id.value, cx.argumentStack)
        : null;

    public InvocationBinding Bind(Invocation inv, Context cx)
    => cx.modules.Bind(inv, cx);

}}
