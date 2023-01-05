using Ex = System.Exception;

namespace Elk.Memory{
public readonly struct Occurence{

    public readonly string subject, verb, @object;
    readonly int hashcode;

    public Occurence(string subject, string verb, string @object){
        this.subject = subject;
        this.verb    = verb;
        this.@object = @object;
        hashcode = (subject + verb + @object).GetHashCode();
    }

    // -------------------------------------------------------------

    override public bool Equals(object arg){
        if(arg == null) return false;
        if(object.ReferenceEquals(this, arg)) return true;
        if(!(arg is Occurence)) return false;
        var that = (Occurence)arg;
        return this.subject == that.subject
            && this.verb    == that.verb
            && this.@object == that.@object;
    }

    override public int GetHashCode() => hashcode;

    override public string ToString()
    => $"[{subject} {verb} {@object}](svo)";

    public static Occurence FromCallInfo(string arg)
    => throw new Ex($"From call info {arg}");

    // Operators ---------------------------------------------------

    public static implicit operator Occurence(
        (string subject, string verb, string @object) arg
    ) => new Occurence(arg.subject, arg.verb, arg.@object);

    public static bool operator == (Occurence x, Occurence y){
        if(x == null) return y == null;
        return x.Equals(y);
    }

    public static bool operator != (Occurence x, Occurence y)
    => ! (x == y);

}}
