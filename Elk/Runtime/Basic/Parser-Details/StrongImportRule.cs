using Elk.Util; using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class StrongImportRule : LocalRule{

    override public void Process(Sequence vec, int i){
        var i0 = i;
        if(vec.AsString(i++) != "with") return;
        var arg = vec.AsString(i++);
        if(arg == null) return;
        if(vec.AsString(i++) != ";") return;
        vec.Replace(i0, i - i0, new StrongImport(arg), this);
    }

    override public string ToString() => $"StrongImportRule";

}}}
