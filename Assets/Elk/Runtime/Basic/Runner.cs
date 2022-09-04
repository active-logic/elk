using S = System.String;
using Ex = System.Exception;
using System.Linq;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public class Runner : Elk.Runner{

    public object Run(object arg){
        if(arg is BinaryOp){
            var op = (BinaryOp) arg;
            var left  = (int)Run(op.arg0);
            var right = (int)Run(op.arg1);
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
