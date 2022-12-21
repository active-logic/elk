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

    // NOTE used by Elk.Runtime when matching reified recalls
    public bool Did(
        Occurence @event, Occurence? since, bool strict, Context cx
    ){
        return since.HasValue
            ? cx.record.Contains(@event, since.Value, strict)
            : cx.record.Contains(@event);
    }

    // Called by elk runtime to submit function calls; "self"
    // is implied as subject
    public void CommitCall(
        string func, string arg, object output, Record record
    ){
        if(recordIntents){
            var caller = owner.gameObject.name;
            DoCommit((caller, func, arg), output, record);
        }
    }

    public void CommitEvent(
        Occurence @event, object output, Record record
    ){
        Debug.Log($"Commit event {@event} -> {output}");
        DoCommit(@event, output, record);
    }

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
    public void DoCommit(
        Occurence @event, object output, Record record
    ){
        bool complete = (output is status task && task.complete)
                     || (output is bool flag && flag);
        //string @out = null;
        if(complete){
            var time = Time.time;
            //@out =
            record.Append(@event, Time.time);
            //ebug.Log($"notify {instances.Count} of {call} in {owner.gameObject.name}");
            foreach(var other in instances){
                if(other == this) continue;
                other.Notify(@event, time);
            }
        }
        //return @out;
    }

    // TODO - adds anything done by another agent to our record;
    // perception filtering should be used
    public void Notify(Occurence @event, float time)
    => owner.record.Append(@event, time);

    public string NameOf(object agent, bool allowDefault){
        switch(agent){
            case GameObject go:
                return go.name;
            case Component co:
                return co.gameObject.name;
            case string str:
                return str;
            case null:
                return "self";
            default:
                return allowDefault ? null
                : throw new Ex($"Name of {agent} is undefined");
        }
    }

    bool recordIntents => owner.recordIntents;

}}
