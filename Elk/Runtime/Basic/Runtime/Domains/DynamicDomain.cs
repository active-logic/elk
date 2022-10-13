using System;
using Elk.Basic;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class DynamicDomain<T> : Domain{

    Func<string, T> finder;

    public DynamicDomain(Func<string, T> finder)
    => this.finder = finder;

    public PropertyBinding Bind(Identifier id, Context cx)
    => new DelegatedPropertyBinding<T>(id.value, finder);

    public InvocationBinding Bind(Invocation inv, Context cx, bool debug)
    => null;

}}
