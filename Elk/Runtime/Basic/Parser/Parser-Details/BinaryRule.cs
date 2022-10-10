using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class BinaryRule : LocalRule{

    public string op;

    public BinaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vec, int i){
        if(!Validate(vec, i)) return;
        var x  = vec.Get(i);
        var op = vec.Get<Operator>(i + 1);
        if(op == null) return;
        var y  = vec.Get(i + 2);
        // TODO need for this signals lack of typing in tokens
        if(IsParens(x) || IsParens(y)) return;
        // TODO possible error since "null" converts to null
        if(!op.Matches(this.op) || y == null) return;
        vec.Replace(i, 3, new BinaryExp( vec[i], op.value, vec[i+2]), this);
    }

    bool IsParens(object arg){
        if(arg is Operator op) return op.value == "(" || op.value == ")";
        else return false;
    }

    protected virtual bool Validate(Sequence vec, int i) => true;

    override public string ToString()
    => $"BinaryRule({op})";

}}}
