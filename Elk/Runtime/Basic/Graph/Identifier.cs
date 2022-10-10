namespace Elk.Basic.Graph{
public class Identifier : Expression{

    public readonly string value;
    public object binding;

    public Identifier(string value)
    => this.value = value;

    override public string ToString() => $"(id:{value})";

}}
