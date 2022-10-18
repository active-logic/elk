using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Activ.DPE;

namespace Activ.DPE.Editor{
public static class PerceivedObjectsFormatter{

    static Dictionary<string, int> counts
        = new Dictionary<string, int>();

    public static string Format(Solver arg){
        var elems = arg.GetPerceivedObjects();
        if(elems == null) return "(no perceived objects)";
        //
        counts.Clear();
        foreach(var e in elems){
            var key = FormatElement(e);
            if (counts.ContainsKey(key)){
                counts[key] ++;
            }else{
                counts[key] = 1;
            }
        }
        var formatted = from key in counts.Keys
            orderby key ascending
            select FormatWithCount(key, counts[key]);
        return string.Join(", ", formatted);
    }

    static string FormatElement(object arg){
        switch(arg){
            case Transform t:
                return t.gameObject.name;
            case null:
                return "null";
            default:
                return arg.ToString();
        }
    }

    static string FormatWithCount(string arg, int count)
    => count == 1 ? arg : $"{arg} ({count})";

}}
