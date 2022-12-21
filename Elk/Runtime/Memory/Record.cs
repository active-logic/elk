using System;
using System.Collections.Generic;
using UnityEngine;
using Elk.Util;

namespace Elk.Memory{
// Captures actions observed by an agent, including those performed
// by the agent itself and other agents
public class Record{

    public string name;
    List<Frame> events = new List<Frame>(256);

    // -------------------------------------------------------------

    public Record(string name) => this.name = name;

    public int count => events.Count;

    public Frame this[int index, bool fromEnd]
    => fromEnd ? events[count - index - 1] : events[index];

    // -------------------------------------------------------------

    public void Append(
        Occurence arg, float time
    ){
        events.Add(new Frame(arg, time));
        //return @event;
    }

    public void Append(Frame arg){
        if(!events.Contains(arg)){
            events.Add(arg);
        }
        //return arg.@event;
    }

    public bool Contains(Occurence arg, Occurence since, bool strict){
        if(events.Count == 0) return false;
        if(since == null) return Contains(arg);
        var found = false;
		for(int i = events.Count - 1; i >= 0; i--){
			if(events[i].Matches(arg)){
				if(!strict) return true;
				found = true;
			}
			if(events[i].Matches(since)) return found;
		}
		return false;
    }

    public bool Contains(Occurence arg){
        for(int i = events.Count - 1; i >= 0; i--){
            if(events[i].Matches(arg)){
                return true;
            }
        }
        return false;
    }

    public bool Did(string subj, string verb, string obj)
    => Contains((subj, verb, obj));

}}
