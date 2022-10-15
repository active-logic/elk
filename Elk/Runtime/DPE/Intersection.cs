using System.Linq;
using T = UnityEngine.Transform;

namespace Activ.DPE{
public class Intersection: BinarySet{

    public Intersection(Set x, Set y) : base(x, y){}

    override public bool Contains(T arg)
    => x.Contains(arg) && y.Contains(arg);

    override public bool Contains(T arg, Logger lg){
        lg("(");
        var X = x.Contains(arg, lg);
        lg(" ∩ ");
        var Y = y.Contains(arg, lg);
        lg(")");
        return X && Y;
    }

}}
