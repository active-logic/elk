using Elk;
using Elk.Basic;
using Elk.Basic.Graph;
using Active.Core;
using UnityEngine;

namespace Activ.BTL{
public class BTLContextFactory{

    ArgumentStack argStack = new ArgumentStack();

    public Context Create(
        BTL owner, object program, bool useScene,
        params object[] externals
    ){
        var module = (Module) program;
        //ebug.Log($"Module: {module}, owner:{owner}");
        var cx = new Context(argStack){
            modules   = new FuncDef[][]{ module.functions },
            externals = externals,
            domain    = useScene ? owner.findInScene : null,
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

    public static object FindInScene(string arg)
    => GameObject.Find(arg)?.transform;

}}
