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
        if(id.binding == null){
            id.binding = cx.externals.Bind(label);
        }
        var binding = id.binding as PropertyBinding;
        if(binding.exists){
            return binding.value;
        }else if(cx.domain == null){
            throw new Ex($"Property not found: [{label}]");
        }
        // Check scene domain if set
        return cx.domain.Invoke(label);
    }

}}
