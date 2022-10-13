using System.Collections.Generic;
using Ex = System.Exception;
using UnityEngine;
using Active.Core;
using Elk.Util; using Elk.Memory;
using Context = Elk.Basic.Runtime.Context;

namespace Activ.BTL{
public class BTLCog : Cog{

    BTL owner;
    public static List<BTLCog> instances = new List<BTLCog>(512);

    public BTLCog(BTL owner){
        this.owner = owner;
        instances.Add(this);
    }

    public bool Did(
        Call @event, Call since, bool strict, Context cx
    ) => cx.record.Contains(@event, since, strict);

    // Called by elk runtime to submit function calls
    public void CommitCall(string call, object output, Record record){
        if(recordIntents) DoCommit(call, output, record);
    }

    public void CommitAction(string call, object output, Record record){
        DoCommit(call, output, record);
    }

    // Called by clients (via BTL.CommitAction) when a memorable
    // event or action has occurred
    public string CommitEvent(
        string action, string args, status @out, Record record
    )
    => DoCommit($"{action}({args})", @out, record);

    public static string ArgsToString(object args){
        switch(args){
            case System.Array array:
                return array.NeatFormat();
            default:
                return args.Format();
        }
    }

    // NOTE may want to be more generous in what we record here;
    // negative results (and others) can be useful
    public string DoCommit(string call, object output, Record record){
        bool complete = (output is status task && task.complete)
                     || (output is bool flag && flag);
        string @out = null;
        if(complete){
            var time = Time.time;
            @out = record.Append("self." + call, Time.time);
            foreach(var other in instances){
                if(other == this) continue;
                other.Notify(owner, call, time);
            }
        }
        return @out;
    }

    // NOTE - this just adds anything done by another agent to our
    // record; normally filtering would be added for perception
    void Notify(object agent, string call, float time){
        owner.record.Append(
            NameOf(agent, allowDefault: false) + "." + call,
            time
        );
    }

    public string NameOf(object agent, bool allowDefault){
        switch(agent){
            case GameObject go:
                return go.name;
            case Component co:
                return co.gameObject.name;
            case null:
                return "self";
            default:
                return allowDefault ? null
                : throw new Ex($"Name of {agent} is undefined");
        }
    }

    bool recordIntents => owner.recordIntents;

}}
