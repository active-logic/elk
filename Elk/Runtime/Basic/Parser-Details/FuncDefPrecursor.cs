using System.Collections.Generic;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
// Form
// func NAME([arg0, ...])
// Precursor avoids NAME([arg0, ...]) getting parsed
// as an invocation; could also 'promote' said invocation, which
// would be more concise but perhaps inconvenient later
// for syntax err reporting
public class FuncPrecursor : LocalRule{

    string preamble;

    public FuncPrecursor()
    => preamble = "func";

    public FuncPrecursor(string preamble)
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
        List<string> arguments = null;
        while(i < vec.size && vec.AsChar(i) != ')'){
            if(vec.AsChar( i + 1 ) == ')'){
                if(arguments == null) arguments = new List<string>(3);
                arguments.Add((string)vec[i]);
                i += 2;
                //ebug.Log($"Did read final arg {arguments.Count}");
                break;
            }else if(vec.AsChar( i + 1 ) == ','){
                if(arguments == null) arguments = new List<string>(3);
                arguments.Add((string)vec[i]);
                //ebug.Log($"Did read arg {arguments.Count}");
            }else{
                //ebug.Log($"no ',' for arg {i-i0-2} (found {vec.Get(i+1)})");
                return;
            }
            i += 2;
        }
        // if no args, head is at ) and we move it to hopefully =>
        if(arguments == null) i += 1;
        var repCount = i - i0;
        //ebug.Log($"rep count {repCount}");
        vec.Replace(i0, repCount, new FuncDef(
            funcName, arguments, null
        ), this);
    }

    override public string ToString() => "FuncPrecursor";

}}}
