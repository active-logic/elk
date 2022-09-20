using Elk.Basic;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Active.Core;

namespace Activ.BTL{
public static class BTLContextFactory{

    public static Context Create(object program,
                                 params object[] externals){
        var cx = new Context(){
            modules = new FuncDef[][]{ (FuncDef[])program },
            externals = externals,
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
