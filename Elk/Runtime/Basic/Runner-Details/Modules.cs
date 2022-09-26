using System.Collections.Generic;
using Array = System.Array;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public static class Modules{

    public static InvocationBinding Bind(
        this IEnumerable<FuncDef[]> self, Invocation ι,
        Runner ρ, Context cx
    ){
        foreach(var module in self){
            var fdef = Array.Find(
                module, x => x.Matches(ι.name, ι.argumentCount)
            );
            if(fdef != null){
                 return new InternalFunctionBinding(null, fdef);
            }
        }
        return null;
    }

}}
