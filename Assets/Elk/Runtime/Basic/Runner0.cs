using S = System.String;
using Ex = System.Exception;
using System.Linq;
using UnityEngine;

namespace Elk{
public class Runner0{

    public object this[object arg]{ get{
        if(arg is BinaryOp){
            var op = (BinaryOp) arg;
            var left = (int)this[op.arg0];
            var right = (int)this[op.arg1];
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
    }}

}}
