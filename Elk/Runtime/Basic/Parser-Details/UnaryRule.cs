using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class UnaryRule : LocalRule{

    public string op;

    public UnaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vec, int i){
        var op  = vec.AsString(i);
        var arg = vec.Get(i + 1);
        if(op != this.op) return;
        vec.Replace(i, 2, new UnaryExp( vec[i+1], op), this);
    }

    override public string ToString()
    => $"UnaryRule({op})";

}}}
