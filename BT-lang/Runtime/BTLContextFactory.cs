using Elk;
using Elk.Basic;
using Elk.Basic.Graph;
using Active.Core;

namespace Activ.BTL{
public static class BTLContextFactory{

    public static Context Create(
        BTL owner, object program, params object[] externals
    ){
        var module = (Module) program;
        var cx = new Context(){
            modules   = new FuncDef[][]{ module.functions },
            externals = externals,
            record    = owner.record,
            cog      = owner.cognition
        };
        cx.graph.returnValueFormatter = x => {
            var str = x?.ToString() ?? "∅";
            if(x is Active.Core.status){
                var s = (Active.Core.status) x;
                if(s.failing) str = "✗";
                if(s.running) str = "→";
                if(s.complete) str = "✓";
            };
            return str;
        };
        return cx;
    }

}}
