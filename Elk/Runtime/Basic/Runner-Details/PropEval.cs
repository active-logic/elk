using Ex = System.Exception;
using Elk.Bindings.CSharp;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class PropEval{

    public object Eval(Identifier id, Context cx){
        var label = id.value;
        // Check internals (ELK function arguments)
        if(cx.HasKey(label)){
            return cx[label];
        }
        // Check externals
        var @out = cx.externals.Eval(label, out bool found);
        if(found){
            return @out;
        }else if(cx.domain == null){
            throw new Ex($"Property not found: [{label}]");
        }
        // Check scene domain if set
        return cx.domain.Invoke(label);
    }

}}
