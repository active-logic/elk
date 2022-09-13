using System.Collections.Generic;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
// Form:
// OneLineFunc => BODY ;
public class OneLineFuncRule : LocalRule{

    string arrow = "=>";
    string terminal = ";";

    override public void Process(Sequence vec, int i){
        var i0 = i;
        var func = vec.Get<OneLineFunc>(i++);
        if(func == null)                    return;
        if(vec.AsString(i++) != arrow)      return;
        if(vec.AsString(i + 1) != terminal) return;
        func.body = vec[ i++ ];
        ////ebug.Log("Body set to ")
        var repCount = i + 1 - i0;
        //ebug.Log($"rep count {repCount}");
        vec.Replace(i0, repCount, func, this);
    }

    override public string ToString() => "OneLineFuncRule";

}}}
