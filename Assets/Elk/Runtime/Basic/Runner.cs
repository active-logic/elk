using S = System.String;
using Ex = System.Exception;
using System;
using UnityEngine;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Runner : Elk.Runner<Context>{

    public string mainFuncName = "Main";
    bool didReportMainNotFound;

    public object Run(object arg, Context cx){
        if(arg is BinaryOp){
            var op = (BinaryOp) arg;
            var left  = (int) Run(op.arg0, cx);
            var right = (int) Run(op.arg1, cx);
            switch(op.op){
                case "*": return left * right;
                case "/": return left / right;
                case "+": return left + right;
                case "-": return left - right;
                default: throw new Ex($"Unknown op {op.op}");
            }
        }else if(arg is string){
            var str = arg as string;
            if(char.IsDigit(str[0])){ // TODO resolve upstream
                return int.Parse(str);
            }else{
                if(cx == null) return null;
                return EvalProperty(str, cx);
            }
        }else if(arg is FuncDef[]){
            var main = Array.Find((FuncDef[])arg, x => x.name == mainFuncName);
            if(main == null){
                Err($"{mainFuncName} function not found",
                    ref didReportMainNotFound);
                return null;
            }
            return Run(main.body, cx);
        }if(arg is Invocation){
            if(cx == null) return null;
            return Invoke((Invocation)arg, cx);
        }else if(arg is bool || arg is Active.Core.status || arg is int || arg == null){
            return arg;
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
