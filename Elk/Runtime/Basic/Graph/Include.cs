namespace Elk.Basic.Graph{
public class Include{

    public readonly string module;

    public Include(string module) => this.module = module;

    override public string ToString() => $"(with {module};)";

}}
