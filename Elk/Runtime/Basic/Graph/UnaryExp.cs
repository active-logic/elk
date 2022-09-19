using S = System.String;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class UnaryExp : Expression{

    public readonly string op;
    public readonly object arg;
    public readonly string binding;
    public readonly bool postfix;

    public UnaryExp(object arg, string op, bool postfix=false){
        this.op      = op;
        this.arg     = arg;
        this.postfix = postfix;
        CSharpOps.Unary.TryGetValue(op, out binding);
    }

    override public S ToString() => $"({op}{arg})";

}}
