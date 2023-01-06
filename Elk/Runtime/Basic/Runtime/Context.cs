using System;
using System.Collections.Generic;
using FuncDef = Elk.Basic.Graph.FuncDef;
using Elk.Basic.Runtime;
using Elk.Basic.Graph;
using Occurence = Elk.Memory.Occurence;

namespace Elk.Basic.Runtime{
public class Context{

    public ArgumentStack argumentStack;
    public IEnumerable<FuncDef[]> modules;
    public List<Domain> domains = new List<Domain>(5);
    public CallGraph graph;
    public Func<object, object> caster;
    // NOTE: used to decide whether to output props to the stack
    // or not; guesswork
    public bool propsToStack = true;
    public Elk.Memory.Record record;
    public Elk.Memory.Cog cog;
    //public Activ.DPE.Solver rs;

    public Context(ArgumentStack argstack){
        argstack.Clear();
        this.argumentStack = argstack;
        graph = new CallGraph();
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

    public bool HasKey(string name)
    => argumentStack.Exists(name);

    public void StackPush(string arg, ulong id)
    => graph.Push(arg, id);

    // NOTE: bypass indicates the function was not called
    public void StackPop(object value, bool bypass){
        var callInfo = graph.Peek();
        //
        var details = ParseCallInfo(callInfo);
        //
        if(!bypass){
            cog.CommitCall(details.verb, details.obj, value, record);
        }
        graph.Pop(value);
    }

    // TODO this should not be needed
    public static (string verb, string obj) ParseCallInfo(string arg){
        int i = arg.IndexOf("(");
        var verb = arg.Substring(0, i);
        var obj = arg.Substring(i + 1);
        obj = obj.Substring(0, obj.Length-1);
        return (verb, obj);
    }

    public void StackPushPopProp(string name, object value){
        var callInfo = graph.Peek();
        if(propsToStack) graph.PushPopProp(name, value);
    }

    public void PushArguments(string[] parameters, object[] arguments)
    => argumentStack.Push(parameters, arguments);

    public void PopArguments() => argumentStack.Pop();

}}
