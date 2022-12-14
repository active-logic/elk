using T = UnityEngine.Transform;

namespace Activ.DPE{
public class AntiSet: Set{

    Set x;

    public AntiSet(Set arg) => x = arg;

    override public bool Contains(T arg)
    => !x.Contains(arg);

    override public bool Contains(T arg, Logger lg)
    => lg["!", this] && lg[!x.Contains(arg, lg)];

}}
