using UnityEngine;

namespace Elk.Memory{
public class Frame{

    public Occurence @event;
    public readonly float time;

    public Frame(Occurence arg, float t){
        this.@event = arg;
        time = t;
    }

    public bool Matches(Occurence arg) => this.@event == arg;

    override public string ToString()
    => $"[{@event.subject}.{@event.verb}({@event.@object})/{time:0.0}]";

}}
