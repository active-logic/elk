using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class ParensRule : Frame<object, Singleton>{

    public ParensRule() : base(
        "(", ")",
        x => new Singleton(x)
    ){}

}}}
