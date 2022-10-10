using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class IncludeRule : Frame<Identifier, Include>{

    // TODO this should expand subgraph, but without evaluation
    public IncludeRule() : base(
        "with", ";",
        x => new Include(x.value)
    ){}

}}}
