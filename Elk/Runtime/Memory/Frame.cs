using UnityEngine;

namespace Elk.Memory{
public class Frame{

    public Occurence @event;
    public readonly float time;

    public Frame(Occurence arg, float t){
        this.@event = arg;
        time = t;
    }

    public string subject => @event.subject;
    public string verb => @event.verb;
    public string @object => @event.@object;

    public static bool operator < (Frame x, float time)
    => x.time < time;

    public static bool operator > (Frame x, float time)
    => x.time > time;

    public bool Matches(Occurence arg) => this.@event == arg;

    override public string ToString()
    => $"{@event.subject}.{@event.verb}( {@event.@object} ) / {time:0.0}";

}}
