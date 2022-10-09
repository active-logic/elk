using System.Collections.Generic;
using Elk.Util;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
// Form
// ```
// preamble NAME([arg0, ...])
// ```
// Precursor avoids NAME([arg0, ...]) getting parsed
// as an invocation; could also 'promote' said invocation, which
// would be more concise but perhaps inconvenient later
// for syntax err reporting
// TODO: should be FuncPrecursorRule
public class FuncPrecursor : LocalRule{

    string preamble = "func";

    public FuncPrecursor(){}

    public FuncPrecursor(string preamble)
    => this.preamble = preamble;

    override public void Process(Sequence vec, int i){
        var i0 = i;
        // First, check preamble and opening parens
        if(vec.StringValue(i) != preamble){
            //ebug.Log($"Missing [{preamble}] preamble @{i} (found {vec[i]})");
            return;
        }
        if(vec.AsChar(i + 2) != '('){
            //ebug.Log("missing '(' parens");
            return;
        }
        var funcName = vec.Get<Identifier>(i + 1);
        if(funcName.value == null){
            //ebug.Log($"no func name found: [{funcName}]");
            return;
        }
        i += 3;
        // Read arguments or bail out
        List<string> arguments = null;
        while(i < vec.size && vec.AsChar(i) != ')'){
            if(vec.AsChar( i + 1 ) == ')'){
                if(arguments == null) arguments = new List<string>(3);
                arguments.Add(vec.Get<Identifier>(i).value);
                i += 2;
                //ebug.Log($"Did read final arg {arguments.Count}");
                break;
            }else if(vec.AsChar( i + 1 ) == ','){
                if(arguments == null) arguments = new List<string>(3);
                arguments.Add(vec.Get<Identifier>(i).value);
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
            funcName.value, arguments, null
        ), this);
    }

    override public string ToString() => "FuncPrecursor";

}}}
