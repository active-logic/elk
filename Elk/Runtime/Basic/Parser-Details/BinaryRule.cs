using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class BinaryRule : LocalRule{

    public string op;

    public BinaryRule(object op) => this.op = op.ToString();

    override public void Process(Sequence vector, int i){
        if(vector.AsString(i + 1) != op) return;
        vector.Replace(i, 3, new BinaryOp(
            vector[i], vector[i+1], vector[i+2]
        ), this);
    }

    override public string ToString()
    => $"BinaryRule({op})";

}}}
