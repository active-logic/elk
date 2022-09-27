using System.Collections.Generic;
using System.Text;

namespace Elk.Util{
public static class IEnumerableExt{

    public static string Format(this IEnumerable<xchar> arg){
        var x = new StringBuilder();
        foreach(var c in arg) x.Append(c);
        return x.ToString();
    }

}}
