using System.Collections;
using Ex = System.Exception;

namespace Elk.Bindings.CSharp{
public static class IEnumerableExt{

    public static object Invoke(
        this IEnumerable cx, string func, object[] args
    ){
        foreach(var e in cx)
            if(e.Invoke(func, args, out object @out))
                return @out;
        throw new Ex($"Not found ({func})");
    }

    public static object Eval(
        this IEnumerable cx, string label
    ){
        foreach(var e in cx)
            if(e.Eval(label, out object @out))
                return @out;
        throw new Ex($"Not found ({label})");
    }

}}
