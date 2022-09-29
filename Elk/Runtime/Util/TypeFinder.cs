using System;
using System.Reflection;
using UnityEngine;

namespace Elk.Util{
public static class TypeFinder{

    public static Type GetType(string typeName){
        var type = Type.GetType(typeName);
        if(type != null) return type;
        //
        var loaded = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach(var x in loaded){
            type = x.GetType(typeName);
            if(type != null) return type;
        }
        //
        var current = Assembly.GetExecutingAssembly();
        var referenced = current.GetReferencedAssemblies();
        foreach( var assemblyName in referenced ){
            var assembly = Assembly.Load( assemblyName );
            if( assembly != null ){
                type = assembly.GetType( typeName );
                if(type != null) return type;
            }
        }
        return null;
    }

}}
