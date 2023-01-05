using Elk; using Elk.Basic; using Elk.Basic.Runtime;
using Cx = Elk.Basic.Runtime.Context;
using History = Active.Core.Details.History;
using Record = Elk.Memory.Record;

namespace Activ.BTL{
public partial class BTL{
public class Vars{

    public Interpreter<Cx> ι;
    public BTLCog cognition;
    public Elk.Stack stack => context?.callStack;
    //
    public int frame = 0;
    public string log, output, loadedFrom;
    public object program;
    public bool useScene = false;
    public bool suspend = false;
    public History history;
    public BTLContextFactory contextFactory;
    public Context context;
    public bool hasValidPath = true;
    public string exceptionMessage;
    public Record record;

}}}
