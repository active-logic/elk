using System;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Elk.Util{
public static class ArrayExt{

    public static Type[] Types(this object[] arr, bool nullIsObj
    ) => (
        from x in arr select x?.GetType()
        ?? (nullIsObj ? typeof(object) : null)
    ).ToArray();

    // NOTE: concise but not so clean
    // TODO if no good remove it
    public static string Format(this object[] arr)
    => "(" + string.Join(", ", arr) + ")";

    // TODO clarity? this is mainly for
    // rendering arguments in ELK memory record
    public static string NeatFormat(this Array arr){
        var o = new StringBuilder();
        o.Append("(");
        var len = arr.Length;
        for(int i = 0; i < len; i++){
            o.Append(ToCleanString(arr.GetValue(i)));
            if(i < len -1) o.Append(", ");
        }
        o.Append(")");
        return o.ToString();
    }

    // TODO clarity? this is mainly for
    // rendering arguments in ELK memory record
    public static string ToCleanString(object arg){
        switch(arg){
            case GameObject go: return go.name;
            case Component c:   return c.gameObject.name;
            case null: return "null";
            default: return arg.ToString();
        }
    }

}}
