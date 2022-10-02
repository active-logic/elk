using System;
using System.Reflection;
using UnityEngine;

namespace Elk.Bindings.CSharp{
public static class MethodInfoExt{

    public static bool Matches(
        this MethodInfo method, string func, Type[] argTypes
    ){
        if(method.Name != func) return false;
        var parameters        = method.GetParameters();
        var argCount          = argTypes.Length;
        var maxParamCount = parameters.Length;
        if(argCount > maxParamCount){
            //ebug.Log("- arg count is over max param count");
            return false;
        }
        for(int i = 0; i < maxParamCount; i++){
            var p = parameters[i];
            // When parameter index runs beyond argument count,
            // either we're missing optional parameters (fine)
            // or we are missing required parameters (mismatch)
            if(i >= argCount){
                return p.IsOptional ? true : false;
            }
            if(argTypes[i] == null){
                //ebug.Log($"- [{i}]: {p.ParameterType} matches through wildcard");
                continue;
            }
            if(!p.ParameterType.IsAssignableFrom(argTypes[i])){
                //ebug.Log($"- [{i}]: {p.ParameterType} not assignable from {argTypes[i]}");
                return false;
            }
        }
        //ebug.Log("! did match");
        return true;
    }

}}
