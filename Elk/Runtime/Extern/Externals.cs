using System.Collections;
using System.Reflection;
using Elk.Basic.Runtime;

namespace Elk.Bindings.CSharp{
public static class Externals{

    public static InvocationBinding Bind(
        this IEnumerable cx, string func, object[] args
    ){
        foreach(var obj in cx){
            var method = obj.Bind(func, args);
            if(method != null){
                return new ExternalFunctionBinding(obj, method);
            }
        }
        return null;
    }

    public static object Eval(
        this IEnumerable cx, string label, out bool found
    ){
        foreach(var e in cx){
            if(e.Eval(label, out object @out)){
                found = true; return @out;
            }
        }
        found = false; return null;
    }

}}
