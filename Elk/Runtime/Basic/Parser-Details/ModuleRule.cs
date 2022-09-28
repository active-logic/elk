using Elk.Util; using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class ModuleRule : LocalRule{

    override public void Process(Sequence vec, int i){
        var i0 = i;
        var imports  = vec.ReadSeveral<StrongImport>(ref i);
        var funcdefs = vec.ReadSeveral<FuncDef>(ref i);
        if(imports == null && funcdefs == null) return;
        vec.Replace(i0, i - i0, new Module(imports, funcdefs), this);
    }

    override public string ToString() => $"ModuleRule";

}}}
