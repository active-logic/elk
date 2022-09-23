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
    public static string Format(this object[] arr)
    => "(" + string.Join(", ", arr) + ")";

    public static string NeatFormat(this object[] arr){
        var o = new StringBuilder();
        o.Append("(");
        var len = arr.Length;
        for(int i = 0; i < len; i++){
            o.Append(ToCleanString(arr[i]));
            if(i < len -1) o.Append(", ");
        }
        o.Append(")");
        return o.ToString();
    }

    static string ToCleanString(object arg){
        switch(arg){
            case GameObject go: return go.name;
            case Transform t:   return t.gameObject.name;
            case null: return "null";
            default: return arg.ToString();
        }
    }
}}
