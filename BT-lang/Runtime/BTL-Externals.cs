using System;
using System.Collections.Generic;
using Ex = System.Exception;
using Elk.Util;
using UnityEngine;

namespace Activ.BTL{
public partial class BTL{

    void EvalExternals(){
        List<object> ext = new List<object>();
        if(@import != null) foreach(var c in @import) ext.Add(c);
        foreach(var type in requirements){
            if(string.IsNullOrEmpty(type)) continue;
            ext.Add(Req(type));
        }
        externals = ext.ToArray();
    }

    object Req(string typeName){
        var type = TypeFinder.GetType(typeName);
        if(type == null)throw new Ex(
            $"Cannot find type [{typeName}]"
        );
        var c = GetComponent(type);
        if(c) return c;
        c = gameObject.AddComponent(type);
        if(!c) throw new Ex(
            $"Cannot add a component of type [{type}]"
        );
        return c;
    }

}}
