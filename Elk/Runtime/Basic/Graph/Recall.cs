using S = System.String;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class Recall : Expression{

    public readonly Expression action;
    public readonly Expression since;
    public readonly bool strict;

    public Recall(Expression action, Expression since, bool strict){
        this.action = action;
        this.since = since;
        this.strict = strict;
    }

    override public S ToString()
    => since != null ? $"(did [{action}] since [{since}])"
                     : $"(did [{action}])";

}}
