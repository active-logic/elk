using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Elk.Util;

namespace Elk.Memory{
// Captures actions observed by an agent, including those performed
// by the agent itself and other agents
public class Record{

    public delegate void FrameEvent(object frame);

    public string name;
    List<Frame> events = new List<Frame>(256);
    public event FrameEvent OnEvent;

    // -------------------------------------------------------------

    public Record(string name) => this.name = name;

    public int count => events.Count;

    public Frame this[Predicate<Frame> cond]
    => events.Find(cond);

    public Frame this[int index, bool fromEnd]
    => fromEnd ? events[count - index - 1] : events[index];

    // -------------------------------------------------------------

    public List<Frame> All(Predicate<Frame> cond)
    => events.FindAll(cond);

    public void Append(Occurence arg, float time){
        Frame frame = new Frame(arg, time);
        events.Add(frame);
        OnEvent?.Invoke(frame);
    }

    public void Append(Frame arg){
        if(events.Contains(arg)) return;
        events.Add(arg);
        OnEvent?.Invoke(arg);
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
