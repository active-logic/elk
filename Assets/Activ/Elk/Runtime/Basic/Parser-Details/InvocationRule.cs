using System.Collections.Generic;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class InvocationRule : LocalRule{

    override public void Process(Sequence vec, int i){
        var i0 = i;
        // First, check function name and opening parens
        if(vec.AsChar(i + 1) != '('){
            //ebug.Log("missing '(' parens");
            return;
        }
        var funcName = vec.AsWord(i);
        if( funcName == null){
            //ebug.Log($"no func name found: [{funcName}]");
            return;
        }
        i += 2;
        // Read arguments or bail out
        List<object> arguments = null;
        while(i < vec.size && vec.AsChar(i) != ')'){
            if(vec.AsChar( i + 1 ) == ')'){
                if(arguments == null) arguments = new List<object>(3);
                arguments.Add(vec[i]);
                //ebug.Log($"Did read final arg {arguments.Count}");
                break;
            }else if(vec.AsChar( i + 1 ) == ','){
                if(arguments == null) arguments = new List<object>(3);
                arguments.Add(vec[i]);
                //ebug.Log($"Did read arg {arguments.Count}");
            }else{
                //ebug.Log($"no ',' for arg {i-i0-2} (found {vec.Get(i+1)})");
                return;
            }
            i += 2;
        }
        //ebug.Log($"Done parsing args ({i0} => {i})");
        var repCount = 0;
        if(arguments == null){
            repCount = 3;
        }else{
            repCount = 2 + arguments.Count * 2;
        }
        //ebug.Log($"rep count {repCount}");
        vec.Replace(i0, repCount, new Invocation(
            funcName, arguments
        ), this);
    }

    override public string ToString() => "InvocationRule";

}}}
