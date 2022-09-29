using Elk.Util;
using UnityEngine;
using Active.Core; using static Active.Raw;
using Sequence = Elk.Util.Sequence;

namespace Activ.BTL.Imp{
public class TypeCaster : Elk.Basic.TypeCaster{

    override public object TransformToken(string arg){
        object obj = base.TransformToken(arg);
        switch(obj){
            case "fail": return fail;
            case "cont": return cont;
            case "done": return done;
            default: return obj;
        }
    }

}}
