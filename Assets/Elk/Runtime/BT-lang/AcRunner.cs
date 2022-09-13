using S = System.String;
using Ex = System.Exception;
using System.Linq;
using UnityEngine;
using Elk.Basic.Graph;

namespace Activ.Script{
public class AcRunner : Elk.Runner{

    public object Run(object arg, object context){
        if(arg is BinaryOp){
            var op = (BinaryOp) arg;
            var left  = (int)Run(op.arg0, context);
            var right = (int)Run(op.arg1, context);
            switch(op.op){
                case "*": return left * right;
                case "/": return left / right;
                case "+": return left + right;
                case "-": return left - right;
                default: throw new Ex($"Unknown op {op.op}");
            }
        }else if(arg is string){
            return int.Parse((string)arg);
        }else{
            Debug.LogError("Suspicious indeed");
            return null;
        }
    }

}}
