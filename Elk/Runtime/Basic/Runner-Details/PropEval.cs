using Ex = System.Exception;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Runtime{
public class PropEval{

    public object Eval(string label, Context cx){
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
