using System.Collections.Generic;
using UnityEngine;

namespace Elk.Memory{
// Captures actions observed by an agent, including those performed
// by the agent itself and other agents
public class Record{

    public string name;
    List<Frame> events = new List<Frame>(256);

    public Record(string name) => this.name = name;

    public bool Contains(string arg, string since, bool strict){
        if(events.Count == 0) return false;
        if(since == null) return Contains(arg);
        //ebug.Log($"Check {arg} since [{since}, strict: {strict}]");
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

    bool Contains(string @event){
        //ebug.Log($"Check {@event} / ({events.Count})");
        for(int i = events.Count - 1; i >= 0; i--){
            if(events[i].Matches(@event)){
                //ebug.Log($"Did match {@event}");
                return true;
            }
        }
        return false;
    }

    public void Append(string @event, float time){
        events.Add(new Frame(@event, time));
    }

}}
