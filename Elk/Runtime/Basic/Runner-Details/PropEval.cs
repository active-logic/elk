using Ex = System.Exception;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Runtime{
public class PropEval{

    public object Eval(string label, Context cx)
    => cx.HasKey(label) ? cx[label]
    : cx.domain?.Invoke(label) ??  cx.externals.Eval(label);

}}
