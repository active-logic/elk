using System;
using System.Collections.Generic;
using FuncDef = Elk.Basic.Graph.FuncDef;
//using ArgMap = ArgumentStack;
//using ArgMap = System.Collections.Generic
//                     .Dictionary<string, object>;

namespace Elk.Basic{
public class Context{

    public ArgumentStack argumentStack;
    public Func<string, object> domain;
    public IEnumerable<FuncDef[]> modules;
    public IEnumerable<object> externals;
    public CallGraph graph;
    public Elk.Memory.Record record;
    public Elk.Memory.Cog cog;

    public Context(ArgumentStack argstack){
        argstack.Clear();
        this.argumentStack = argstack;
        graph = new CallGraph();
    }

    public Elk.Stack callStack => graph.CallStack(cog, record);

    public object this[string key]
    => argumentStack.Peek()[key];

    public bool HasKey(string name){
        if(argumentStack.empty) return false;
        return argumentStack.Peek().ContainsKey(name);
    }

    public void StackPush(string arg, ulong id)
    => graph.Push(arg, id);

    public void StackPop(object value){
        var callInfo = graph.Peek();
        cog.CommitCall(callInfo, value, record);
        graph.Pop(value);
    }

    public void PushArguments(string[] parameters, object[] arguments)
    => argumentStack.Push(parameters, arguments);

    public void PopArguments() => argumentStack.Pop();

}}
