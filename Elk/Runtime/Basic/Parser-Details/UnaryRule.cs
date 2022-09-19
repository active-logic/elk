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
        if(IsOperand(prev)) return;
        var op  = vec.AsString(i);
        var arg = vec.Get(i + 1);
        if(op != this.op) return;
        vec.Replace(i, 2, new UnaryExp( vec[i+1], op), this);
    }

    override public string ToString()
    => $"UnaryRule({op}x)";

    bool IsOperand(object arg){
        switch(arg){
            case Expression:
                return true;
            case string str when char.IsLetterOrDigit(str[0]):
                return true;
            case object obj when obj.IsNumber():
                return true;
            default:
                return false;
        }
    }

}}}
