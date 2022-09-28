namespace Elk.Basic.Graph{
public class StrongImport{

    public readonly string module;

    public StrongImport(string module) => this.module = module;

    override public string ToString() => $"(with {module};)";

}}
