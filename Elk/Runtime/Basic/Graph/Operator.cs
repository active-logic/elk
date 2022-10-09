namespace Elk.Basic.Graph{
public class Operator{

    public readonly string value;

    public Operator(string value)
    => this.value = value;

    override public string ToString() => $"(op{value})";

    public bool Matches(string op) => value == op;

}}
