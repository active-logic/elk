using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class PostfixUnaryRule : LocalRule{

    public string op;

    public PostfixUnaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vec, int i){
        var arg  = vec.Get(i);
        var op   = vec.AsString(i + 1);
        if(op != this.op) return;
        vec.Replace(i, 2,
            new UnaryExp( vec[i+1], op, postfix: true),
            this
        );
    }

    override public string ToString()
    => $"UnaryRule({op})";

}}}
