using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class AccessRule : BinaryRule{

    public AccessRule() : base(".") {}

    // NOTE: checks for an opening parens, otherwise (x.y)
    // may conflict with invocation exp
    // 0   1  2  3
    // foo . bar (
    override protected bool Validate(Sequence vec, int i)
    => vec.Get<Operator>(i + 3)?.value != "(";

}}}
