using System;
using Ex = System.Exception;
using System.Reflection;
using UnityEngine;
using Elk.Util;
using BF = System.Reflection.BindingFlags;

namespace Elk.Bindings.CSharp{
public static class TypeBindings{

    static Type[] NoArgs = new Type[]{};

    public static MethodInfo LookupMethod(
        this Type self, string func, object[] args, bool debug
    ){
        var method = self.GetMethod(
            func,
            args?.ParameterTypes(nullIsObj : true) ?? NoArgs
        );
        if(method != null){
            return method;
        }else{
            if(debug) Warn($"{self}.{func}{args.NeatFormat()} not found via GetMethod()");
        }
        return self.FindMethod(
            func,
            args?.ParameterTypes(nullIsObj : false) ?? NoArgs,
            debug
        );
    }

    static MethodInfo FindMethod(
        this Type self, string func, Type[] @params, bool debug
    ){
        var methods = self.GetMethods();
        MethodInfo sel = null;
        foreach(var m in methods){
            //ebug.Log($"Check {m} / {@params.NeatFormat()}");
            if(!m.Matches(func, @params, debug)) continue;
            if(sel != null) throw new Ex(BindingConflictInfo(m, sel));
            sel = m;
        }
        return sel;
    }

    static string BindingConflictInfo(MethodInfo a, MethodInfo b){
        return $"Ambiguous binding {a} and {b}";
    }

    static void Warn(string arg) => Debug.Log(arg);

}}
