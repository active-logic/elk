using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class RecallRule : LocalRule{

    override public void Process(Sequence vec, int i){
        var op0  = vec.AsString(i);
        var arg0 = vec.Get<Invocation>(i + 1);
        if(op0 != "did" || arg0 == null) return;
        var op1 = vec.AsString(i + 2);
        if(op1 != "since"){
            vec.Replace(i, 2,
                        new Recall(arg0, null, strict: false), this);
        }else{
            var arg1 = vec.Get<Invocation>(i + 3);
            if(arg1 == null) return;
            var op2 = vec.AsString(i + 4);
            var n = 4;
            bool strict = false;
            if( op2 == "!"){ strict = true; n = 5; }
            vec.Replace(i, n,
                        new Recall(arg0, arg1, strict), this);
        }
    }

    override public string ToString()
    => $"RecallRule";

}}}
