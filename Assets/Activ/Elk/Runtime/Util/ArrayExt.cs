using System;
using System.Text;

namespace Elk.Util{
public static class ArrayExt{

    public static string Format(this object[] arr){
        var o = new StringBuilder();
        o.Append("(");
        var len = arr.Length;
        for(int i = 0; i < len; i++){
            var elem = arr[i];
            o.Append(elem != null ? elem : "null");
            if(i < len -1) o.Append(", ");
        }
        o.Append(")");
        return o.ToString();
    }

}}
