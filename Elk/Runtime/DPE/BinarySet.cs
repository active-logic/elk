using T = UnityEngine.Transform;

namespace Activ.DPE{
public abstract class BinarySet: Set{

    protected Set x, y;

    public BinarySet(Set x, Set y){
        this.x = x; this.y = y;
    }

}}
