using Elk.Basic;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public interface Domain{

    PropertyBinding Bind(Identifier id, Context cx);

    InvocationBinding Bind(Invocation inv, Context cx);

}}
