using T = UnityEngine.Transform;

namespace Activ.DPE{
public class Union: BinarySet{

    public Union(Set x, Set y) : base(x, y){}

    override public bool Contains(T arg)
    => x.Contains(arg) || y.Contains(arg);

    override public bool Contains(T arg, Logger lg){
        lg.Log("(", this);
        var X = x.Contains(arg, lg);
        lg.Log(" ∪ ", this);
        var Y = y.Contains(arg, lg);
        lg.Log(")", this);
        return lg[X && Y];
    }

}}
