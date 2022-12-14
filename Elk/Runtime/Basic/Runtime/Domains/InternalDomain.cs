using Elk.Basic;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class InternalDomain : Domain{

    Context context;

    public InternalDomain(Context cx)
    => this.context = cx;

    public PropertyBinding Bind(Identifier id, Context cx)
    => cx.HasKey(id.value)
        ? new ArgumentBinding(id.value, cx.argumentStack)
        : null;

    public InvocationBinding Bind(Invocation inv, Context cx, bool debug)
    => cx.modules.Bind(inv, cx, debug);

}}
