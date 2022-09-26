using System.Collections.Generic;
using Ex = System.Exception;
using UnityEngine;
using Active.Core;
using Elk.Memory;
using Context = Elk.Basic.Context;

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

    public void Commit(string call, object output, Record record){
        if(output is status task && task.complete){
            var time = Time.time;
            record.Append("self." + call, Time.time);
            foreach(var other in instances){
                if(other == this) continue;
                other.Notify(owner, call, time);
            }
        }
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

}}
