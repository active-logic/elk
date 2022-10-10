using Elk.Util;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public abstract class LocalRule{

    public abstract void Process(Sequence tokens, int index);

}}}
