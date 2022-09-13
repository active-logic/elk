using S = System.String;
using Ex = System.Exception;
using System;
//using System.Linq;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public class Runner : Elk.Runner{

    public string mainFuncName = "Main";
    bool didReportMainNotFound;

    public object Run(object arg, object cx){
        if(arg is BinaryOp){
            var op = (BinaryOp) arg;
            var left  = (int)Run(op.arg0, cx);
            var right = (int)Run(op.arg1, cx);
            switch(op.op){
                case "*": return left * right;
                case "/": return left / right;
                case "+": return left + right;
                case "-": return left - right;
                default: throw new Ex($"Unknown op {op.op}");
            }
        }else if(arg is string){
            return int.Parse((string)arg);
        }else if(arg is FuncDef[]){
            var main = Array.Find((FuncDef[])arg, x => x.name == mainFuncName);
            if(main == null){
                Err($"{mainFuncName} function not found",
                    ref didReportMainNotFound);
                return null;
            }
            return Run(main.body, cx);
        }else{
            Debug.LogError($"{arg} cannot be interpreted");
            return null;
        }
    }

    void Err(string err, ref bool flag){
        if(flag) return;
        Debug.LogError(err);
        flag = true;
    }

}}
