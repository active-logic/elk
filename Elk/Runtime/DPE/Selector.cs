using System.Linq;
using T = UnityEngine.Transform;

namespace Activ.DPE{
public class Selector: BinarySet{

    public Selector(Set x, Set y) : base(x, y){}

    override public bool Contains(T arg)
    => x.Contains(arg) || y.Contains(arg);

}}
