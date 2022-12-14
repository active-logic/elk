using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class BinaryExp : Expression{

    public readonly object arg0, arg1;
    public readonly string op;
    public readonly string binding;

    public BinaryExp(object arg0, string op, object arg1){
        this.arg0 = arg0;
        this.arg1 = arg1;
        this.op   = op;
        CSharpOps.Binary.TryGetValue(op, out binding);
    }

    override public string ToString() => $"({arg0}{op}{arg1})";

}}
