using System.Collections.Generic;
using T = UnityEngine.Transform;

namespace Activ.DPE{
public abstract class Set{

    public T value;
    public List<LogEntry> log = new List<LogEntry>(0);

    // Evaluate a set
    abstract public bool Contains(T arg);

    abstract public bool Contains(T arg, Logger lg);

    // intersection of two sets
    public static Set operator * (Set x, Set y)
    => new Intersection(x, y);

    // logical or (if x is non empty return x, otherwise return y)
    public static Set operator | (Set x, Set y)
    => new Selector(x, y);

    // logical or (if x is non empty return x, otherwise return y)
    public static Set operator ! (Set x)
    => new AntiSet(x);

}}
