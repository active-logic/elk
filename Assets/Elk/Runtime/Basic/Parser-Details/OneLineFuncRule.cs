using System.Collections.Generic;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
// Something like:
// func Attack(a) => Strike(a) || Reach(a)
public class OneLineFuncRule : LocalRule{

    string preamble;
    string arrow = "=>";
    string terminal = ";";

    public OneLineFuncRule()
    => preamble = "func";

    public OneLineFuncRule(string preamble)
    => this.preamble = preamble;

    override public void Process(Sequence vec, int i){
        var i0 = i;
        // First, check preamble and opening parens
        if(vec.AsString(i) != preamble){
            //ebug.Log($"Missing [{preamble}] preamble");
            return;
        }
        if(vec.AsChar(i + 2) != '('){
            //ebug.Log("missing '(' parens");
            return;
        }
        var funcName = vec.AsWord(i + 1);
        if( funcName == null){
            //ebug.Log($"no func name found: [{funcName}]");
            return;
        }
        i += 3;
        // Read arguments or bail out
        List<object> arguments = null;
        while(i < vec.size && vec.AsChar(i) != ')'){
            if(vec.AsChar( i + 1 ) == ')'){
                if(arguments == null) arguments = new List<object>(3);
                arguments.Add(vec[i]);
                i += 2;
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
        // if no args, head is at ) and we move it to hopefully =>
        if(arguments == null) i += 1;
        // Read => body ;
        //ebug.Log($"Done parsing args ({i0} => {i})");
        if(vec.AsString(i) != arrow){
            //ebug.Log($"Missing {arrow} at index {i} (found {vec.Get(i)})");
            return;
        }
        if(vec.AsString(i + 2) != terminal){
            //ebug.Log($"Missing terminal {terminal} @{i+2}");
            return;
        }
        var body = vec[++i];
        ////ebug.Log("Body set to ")
        var repCount = i + 2 - i0;
        //ebug.Log($"rep count {repCount}");
        vec.Replace(i0, repCount, new OneLineFunc(
            funcName, arguments, body
        ));
    }

}}}
