using Elk.Memory;

namespace Elk.Basic{
public readonly struct StackTrace : Elk.Stack{

    readonly CallGraph.Node tail;
    readonly Cog client;
    readonly Record record;

    public StackTrace(CallGraph.Node tail, Cog client, Record record){
        this.tail = tail;
        this.client = client;
        this.record = record;
    }

    public void Commit(object result){
        var node = tail;
        while(node != null){
            var msg = node.info.Substring(2);
            client.CommitAction(msg, result, record);
            node = node.parent;
        }
    }

}}
