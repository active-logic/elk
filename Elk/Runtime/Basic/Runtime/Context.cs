using System;
using System.Collections.Generic;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Elk.Basic.Runtime;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class Context{

    public ArgumentStack argumentStack;
    public IEnumerable<FuncDef[]> modules;
    public List<Domain> domains = new List<Domain>(5);
    public CallGraph graph;
    // NOTE: used to decide whether to output props to the stack
    // or not; guesswork
    public bool propsToStack = true;
    public Elk.Memory.Record record;
    public Elk.Memory.Cog cog;
    public Activ.DPE.Solver rs;

    public Context(ArgumentStack argstack, Activ.DPE.Solver rs){
        argstack.Clear();
        this.argumentStack = argstack;
        graph = new CallGraph();
        this.rs = rs;
    }

    public Elk.Stack callStack => graph.CallStack(cog, record);

    public InvocationBinding BindMethod(
        Invocation ι, Runner ρ, Context cx
    ){
        foreach(var domain in domains){
            var binding = domain.Bind(ι, cx, debug: false);
            if(binding != null) return binding;
        }
        // diagnostic level
        foreach(var domain in domains){
            var binding = domain.Bind(ι, cx, debug: true);
            if(binding != null) return binding;
        }
        return new InvalidInvocation(ι.name);
    }

    public PropertyBinding BindProperty(
        Identifier prop, Context cx
    ){
        foreach(var domain in domains){
            var binding = domain.Bind(prop, cx);
            if(binding != null) return binding;
        }
        return new InvalidPropertyBinding(prop.value);
    }

    public object ResolveRole(Activ.DPE.Set s, string label)
    => rs.Resolve(s, label);

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

    public void StackPushPopProp(string name, object value){
        var callInfo = graph.Peek();
        if(propsToStack) graph.PushPopProp(name, value);
    }

    public void PushArguments(string[] parameters, object[] arguments)
    => argumentStack.Push(parameters, arguments);

    public void PopArguments() => argumentStack.Pop();

}}