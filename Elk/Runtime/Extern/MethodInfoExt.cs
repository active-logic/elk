using System;
using System.Reflection;
using UnityEngine;

namespace Elk.Bindings.CSharp{
public static class MethodInfoExt{

    public static bool Matches(
        this MethodInfo method, string func, Type[] argTypes, bool debug
    ){
        if(method.Name != func){
            //if(debug) Log($"Method name {func} does not match {method.Name}");
            return false;
        }
        var parameters        = method.GetParameters();
        var argCount          = argTypes.Length;
        var maxParamCount = parameters.Length;
        if(argCount > maxParamCount){
            if(debug) Warn(
                  $"C# '{func}' matches but arg count ({argCount})"
                + $" > max param count ({maxParamCount})"
            );
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
                Warn($"C# [{i}]: {p.ParameterType} is not assignable from {argTypes[i]}");
                return false;
            }
        }
        //ebug.Log("! did match");
        return true;
    }

    static void Warn(string arg) => Debug.Log(arg);

}}
