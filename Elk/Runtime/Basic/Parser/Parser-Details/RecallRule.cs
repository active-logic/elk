using Elk.Util;
using Elk.Basic.Graph;
using UnityEngine;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class RecallRule : LocalRule{

    override public void Process(Sequence vec, int i){
        var i0 = i;
        var op0  = vec.Get<Identifier>(i++);
        var arg0 = vec.Get<Expression>(i++);
        if(!ValidateOperand(arg0)) return;
        if(op0?.value != "did" || arg0 == null){
            //ebug.Log("'did' not found");
            return;
        }
        var op1 = vec.Get<Identifier>(i);
        if(op1?.value != "since"){
            //ebug.Log("Short form because 'since' not found");
            vec.Replace(
                i0, i - i0,
                new Recall(arg0, null, strict: false),
                this
            );
        }else{
            //ebug.Log($"Parsing long form (size now {i - i0})");
            i++; // step over 'since'
            var arg1 = vec.Get<Expression>(i++);
            if(!ValidateOperand(arg1)) return;
            var op2 = vec.AsString(i);
            bool strict = false;
            if( op2 == "!"){
                strict = true; i++;
            }
            var n = i - i0;
            //ebug.Log($"Long form; replace {n} elements at {i}");
            vec.Replace(i0, i - i0,
                        new Recall(arg0, arg1, strict), this);
        }
    }

    bool ValidateOperand(Expression arg){
        switch(arg){
            case Invocation:
                return true;
            case BinaryExp exp when exp.op == ".":
                return true;
            case null:
                return false;
            default:
                return false;
        }
    }

    override public string ToString()
    => $"RecallRule";

}}}
