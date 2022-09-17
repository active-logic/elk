using S = System.String;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class BinaryOp{

    public readonly object arg0, arg1, op;
    public readonly string binding;

    // TODO probably should be binary exp?
    public BinaryOp(object arg0, object op, object arg1){
        this.arg0 = arg0;
        this.arg1 = arg1;
        this.op   = op;
        this.binding = CSharpOps.Binary[(string)op];
    }

    override public S ToString() => $"({arg0}{op}{arg1})";

}}
