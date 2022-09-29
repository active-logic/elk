namespace Elk.Basic.Graph{
public class Singleton{

    public readonly object content;

    public Singleton(object content) => this.content = content;

    override public string ToString() => $"({content})";

}}
