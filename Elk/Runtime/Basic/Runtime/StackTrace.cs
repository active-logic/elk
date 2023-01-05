using Elk.Memory;
using Ex = System.Exception;

namespace Elk.Basic.Runtime{
public readonly struct StackTrace : Elk.Stack{

    readonly CallGraph.Node tail;
    readonly Cog client;
    readonly Record record;

    public StackTrace(CallGraph.Node tail, Cog client, Record record){
        this.tail = tail;
        this.client = client;
        this.record = record;
    }

    public void Commit(string subject, object result){
        var node = tail;
        while(node != null){
            var msg = node.info.Substring(2);
            var info = Context.ParseCallInfo(msg);
            UnityEngine.Debug.Log($"{msg} ==> {info}");
            client.CommitEvent(
                (subject, info.verb, info.obj),
                result, record
            );
            node = node.parent;
        }
    }

    public ulong Id() => tail.id;

}}
