using Ex = System.Exception;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Runtime{
public static class PropEval{

    public static object Eval(string label, Context cx){
        if(cx == null){
            throw new Ex($"Var {label} has no meaning out of context");
        }else if(cx.HasKey(label)){
            return cx[label];
        }else{
            return CSharpBindings.Fetch(cx.externals, label);
        }
    }

}}
