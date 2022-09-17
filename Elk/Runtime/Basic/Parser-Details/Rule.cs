using Elk.Util;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public abstract class Rule{

    public abstract void Process(Sequence tokens);

}}}
