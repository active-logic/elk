using Elk;
using Elk.Basic;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;
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
        var cx = new Context(argStack){
            modules   = new FuncDef[][]{ module.functions },
            record    = owner.record,
            cog      = owner.cognition
        };
        cx.domains.Add( new InternalDomain(cx) );
        cx.domains.Add( new CsDomain(externals) );
        if(useScene){
            cx.domains.Add(
                new DynamicDomain<Transform>(owner.findInScene)
            );
        }
        cx.graph.format = new BTL.GraphFormatter();
        return cx;
    }

    public static Transform FindInScene(string arg)
    => GameObject.Find(arg)?.transform;

}}
