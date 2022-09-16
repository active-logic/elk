using S = System.String;
using Ex = System.Exception;
using System;
using UnityEngine;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public partial class Runner : Elk.Runner<Context>{

    public string mainFuncName = "Main";
    bool didReportMainNotFound;

    public object Eval(object arg, Context cx){
        if(arg is BinaryOp){
            return BinEval.Eval((BinaryOp)arg, this, cx);
        }else if(arg is string){
            var str = arg as string;
            if(cx == null)
                throw new Ex($"Var {arg} has no meaning out of context");
            else
                return EvalProperty(str, cx);
        }else if(arg is FuncDef[]){
            var main = Array.Find((FuncDef[])arg, x => x.name == mainFuncName);
            if(main == null){
                Err($"{mainFuncName} function not found",
                    ref didReportMainNotFound);
                return null;
            }
            return Eval(main.body, cx);
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
