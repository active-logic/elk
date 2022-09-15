using System.Linq;
using System.Collections.Generic;
using Ex = System.Exception;

namespace Elk.Bindings.CSharp{
public static class CSharpBindings{

    public static object Invoke(object arg, string func,
                                object[] @params){
        var type = arg.GetType();
        var typeArray = (
            from e in @params
            select e?.GetType() ?? typeof(object)
        ).ToArray();
        var method = type.GetMethod(func, typeArray);
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
