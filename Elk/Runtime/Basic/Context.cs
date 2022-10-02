using System;
using System.Collections.Generic;
using FuncDef = Elk.Basic.Graph.FuncDef;
using ArgMap = System.Collections.Generic
                     .Dictionary<string, object>;

namespace Elk.Basic{
public class Context{

    public Stack<ArgMap> argumentStack;
    public Func<string, object> domain;
    public IEnumerable<FuncDef[]> modules;
    public IEnumerable<object> externals;
    public CallGraph graph;
    public Elk.Memory.Record record;
    public Elk.Memory.Cog cog;

    public Context(){
        argumentStack = new Stack<ArgMap>();
        graph = new CallGraph();
    }

    public object this[string key]
    => argumentStack.Peek()[key];

    public bool HasKey(string name){
        if(argumentStack.Count == 0) return false;
        var map = argumentStack.Peek();
        return map.ContainsKey(name);
    }

    public void StackPush(string arg) => graph.Push(arg);

    public void StackPop(object value){
        var callInfo = graph.Peek();
        cog.Commit(callInfo, value, record);
        graph.Pop(value);
    }

    public void PushArguments(string[] parameters, object[] arguments){
        var map = new ArgMap();
        var len = parameters?.Length ?? 0;
        for(int i = 0; i < len; i++){
            map[parameters[i]] = arguments[i];
        }
        argumentStack.Push(map);
    }

    public void PopArguments() => argumentStack.Pop();

}}
