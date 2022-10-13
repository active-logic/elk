using System;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Elk.Util{
public static class ArrayExt{

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
            o.Append( arr.GetValue(i).Format() );
            if(i < len -1) o.Append(", ");
        }
        o.Append(")");
        return o.ToString();
    }

}}
