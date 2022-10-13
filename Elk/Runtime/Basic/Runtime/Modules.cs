using System.Collections.Generic;
using Array = System.Array;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public static class Modules{

    // TODO diagnostics
    public static InvocationBinding Bind(
        this IEnumerable<FuncDef[]> self, Invocation ι, Context cx,
        bool debug
    ){
        foreach(var module in self){
            var fdef = Array.Find(
                module, x => x.Matches(ι.name, ι.argumentCount, debug)
            );
            if(fdef != null){
                 return new InternalFunctionBinding(null, fdef);
            }
        }
        return null;
    }

}}
