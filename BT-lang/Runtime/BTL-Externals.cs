using System;
using System.Collections.Generic;
using Ex = System.Exception;
using Elk.Util;
using UnityEngine;

namespace Activ.BTL{
public partial class BTL{

    void EvalExternals(){
        var ext = new List<object>();
        var errors = new List<string>();
        if(@import != null) foreach(var c in @import) ext.Add(c);
        foreach(var typeName in requirements){
            ResolveExternal(typeName, ext, errors);
        }
        externals = ext.ToArray();
        if(errors.Count > 0){
            throw new Ex(
                $"BTL: {Time.frameCount} - {this.gameObject.name} encountered errors while resolving externals. "
                + string.Join(", ", errors)
            );
        }
    }

    void ResolveExternal(
         string typeName, List<object> @out, List<string> errors
    ){
        if(string.IsNullOrEmpty(typeName)){
            return;
        }
        string error = null;
        var type = Req(typeName, ref error);
        if(type == null){
            errors.Add(error);
        }else{
            @out.Add(type);
        }
    }

    object Req(string typeName, ref string error){
        var type = TypeFinder.GetType(typeName);
        if(type == null){
            error = $"cannot find type [{typeName}]";
            return null;
        }
        var c = GetComponent(type);
        if(c) return c;
        c = gameObject.AddComponent(type);
        if(!c){
            error = $"cannot add a component of type [{type}]";
            return null;
        }
        return c;
    }

}}
