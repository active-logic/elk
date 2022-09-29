using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class IncludeRule : Frame<string, Include>{

    public IncludeRule() : base(
        "with", ";",
        x => new Include(x)
    ){}

}}}
