using System;
using Ex = System.Exception;
using System.Reflection;
using UnityEngine;
using Elk.Util;

namespace Elk.Bindings.CSharp{
public static class TypeExt{

    static Type[] NoArgs = new Type[]{};

    public static MethodInfo GetMethod(
        this Type self, string func, object[] args
    ){
        var method = self.GetMethod(
            func,
            args?.ParameterTypes(nullIsObj : true) ?? NoArgs
        );
        if(method != null){
            return method;
        }
        return self.FindMethod(
            func,
            args?.ParameterTypes(nullIsObj : false) ?? NoArgs
        );
    }

    static MethodInfo FindMethod(
        this Type self, string func, Type[] @params
    ){
        var methods = self.GetMethods();
        MethodInfo sel = null;
        foreach(var m in methods){
            //ebug.Log($"Check {m} / {@params.NeatFormat()}");
            if(!m.Matches(func, @params)) continue;
            if(sel != null) throw new Ex(BindingConflictInfo(m, sel));
            sel = m;
        }
        return sel;
    }

    static string BindingConflictInfo(MethodInfo a, MethodInfo b){
        return $"Ambiguous binding {a} and {b}";
    }

}}
