using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class PostfixUnaryRule : LocalRule{

    public string op;

    public PostfixUnaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vec, int i){
        // TODO should Get<Exp> but that doesn't allow numbers
        var arg  = vec.Get(i);
        if(arg == null || arg is Operator) return;
        var op   = vec.Get<Operator>(i + 1);
        if(op == null || !op.Matches(this.op)) return;
        vec.Replace(i, 2,
            new UnaryExp( arg, op.value, postfix: true),
            this
        );
    }

    override public string ToString()
    => $"UnaryRule({op})";

}}}
