using S = System.String;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class Recall : Expression{

    public readonly Invocation action;
    public readonly Invocation since;
    public readonly bool strict;

    public Recall(Invocation action, Invocation since, bool strict){
        if(action == null) throw new ParsingException("action is undefined");
        this.action = action;
        this.since = since;
        this.strict = strict;
    }

    override public S ToString() => $"(did [{action}] since [{since}])";

}}
