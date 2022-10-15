using System.Linq;
using T = UnityEngine.Transform;

namespace Activ.DPE{
public class Union: BinarySet{

    public Union(Set x, Set y) : base(x, y){}

    override public bool Contains(T arg)
    => x.Contains(arg) || y.Contains(arg);

}}
