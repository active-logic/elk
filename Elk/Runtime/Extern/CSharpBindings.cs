using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Ex = System.Exception;
using Elk.Util;

namespace Elk.Bindings.CSharp{
public static class CSharpBindings{

    public static bool Invoke(object arg, string func,
                              object[] @params,
                              out object @out){
        var type = arg.GetType();
        // NOTE when an argument is null, in general setting
        // 'object' as type is likely to return no compatible
        // function - reflect harder? #25
        var typeArray = (
            from e in @params
            select e?.GetType() ?? typeof(object)  // (1)
        ).ToArray();
        //ebug.Log($"Check {arg} for function {func}{typeArray.Format()}");
        var method = type.GetMethod(func, typeArray);
        if(method == null){
            @out = null;
            return false;
        }
        @out = method.Invoke(arg, @params);
        return true;
    }

    public static object Invoke(object arg, string func,
                                object[] @params){
        var type = arg.GetType();
        var typeArray = (
            from e in @params
            select e?.GetType() ?? typeof(object)
        ).ToArray();
        var method = type.GetMethod(func, typeArray);
        if(method == null){
            throw new Ex($"Method: {func} not in {arg}");
        }
        return method.Invoke(arg, @params);
    }

    public static object Fetch(IEnumerable<object> scope, string label)
    {
        foreach(var obj in scope){
            //nityEngine.Debug.Log($"Find {label} in {obj}");
            var @out = Fetch(obj, label, out bool didMatch);
            if(didMatch) return @out;
        }
        throw new Ex($"Field or property not in {scope}: {label}");
    }

    public static object Fetch(object arg, string label, out bool didMatch)
    {
        var type = arg.GetType();
        var field = type.GetField(label);
        if(field != null){
            didMatch = true;
            return field.GetValue(arg);
        }
        var prop = type.GetProperty(label);
        if(prop != null){
            didMatch = true;
            return prop.GetValue(arg);
        }
        //nityEngine.Debug.Log($"Not in {arg}: {label}");
        didMatch = false;
        return null;
    }

}}
