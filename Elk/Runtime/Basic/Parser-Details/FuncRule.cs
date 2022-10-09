using System.Collections.Generic;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
// Form:
// FuncDef => BODY ;
public class FuncRule : LocalRule{

    string arrow = "=>";
    string terminal = ";";

    override public void Process(Sequence vec, int i){
        var i0 = i;
        var func = vec.Get<FuncDef>(i++);
        if(func == null)                    return;
        var arrowCd = vec.Get<Operator>(i++);
        if(arrowCd == null) return;
        if(!arrowCd.Matches(arrow))      return;
        var terminalCd = vec.Get<Operator>(i + 1);
        if(terminalCd == null) return;
        if(!terminalCd.Matches(terminal)) return;
        func.body = vec[ i++ ];
        ////ebug.Log("Body set to ")
        var repCount = i + 1 - i0;
        //ebug.Log($"rep count {repCount}");
        vec.Replace(i0, repCount, func, this);
    }

    override public string ToString() => "FuncRule";

}}}
