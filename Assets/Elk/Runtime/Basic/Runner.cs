using Ex = System.Exception;
using status = Active.Core.status;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;

namespace Elk.Basic{
public partial class Runner : Elk.Runner<Context>{

    public object Eval(object arg, Context cx){
        switch(arg){
            case BinaryOp op:
                return BinEval.Eval(op, this, cx);
            case string label:
                return PropEval.Eval(label, cx);
            case Invocation ι:
                cx.graph.Push(ι.name);
                var @out = InvocationEval.Eval(ι, this, cx);
                cx.graph.Pop(@out);
                return @out;
            case object val when IsValue(val):
                return val;
            default:
                throw new Ex($"Cannot evaluate {arg}");
        }
    }

    static bool IsValue(object arg)
    => arg == null || arg is bool || arg is status || arg is int;

}}
