using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class BinaryRule : LocalRule{

    public string op;

    public BinaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vec, int i){
        var x  = vec.Get(i);
        var op = vec.AsString(i + 1);
        var y  = vec.Get(i + 2);
        // TODO possible error since "null" converts to null
        if(op != this.op || y == null) return;
        vec.Replace(i, 3, new BinaryExp( vec[i], op, vec[i+2]), this);
    }

    override public string ToString()
    => $"BinaryRule({op})";

}}}
