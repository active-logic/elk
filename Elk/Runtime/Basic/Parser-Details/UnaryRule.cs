using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class UnaryRule : LocalRule{

    public string op;

    public UnaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vec, int i){
        var prev = vec.Get(i - 1);
        // NOTE unary/binary op prec isn't consistent
        // -2 + 3 ------- evals to 1, not -(2 + 3)
        //  2 + 3 ------- evals to 6, not an error
        if(prev != null && IsOperand(prev)) return;
        var op  = vec.Get<Operator>(i);
        if(op == null || !op.Matches(this.op)) return;
        // TODO should Get<Exp> but that doesn't allow numbers
        var arg = vec.Get(i + 1);
        if(!IsOperand(arg)) return;
        vec.Replace(i, 2, new UnaryExp( vec[i+1], op.value), this);
    }

    override public string ToString()
    => $"UnaryRule({op}x)";

    bool IsOperand(object arg){
        switch(arg){
            // Note patch; 'did' should be defined as a keyword
            // (turning off cause apparently not needed)
            // case Identifier id when id.value == "did":
            //    return false;
            case Operator:
                return false;
            default:
                return true;
        }
    }

}}}
