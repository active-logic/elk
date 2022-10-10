using System.Linq;
using UnityEngine;
using Elk; using Elk.Basic;
using Cx = Elk.Basic.Context;
using Record = Elk.Memory.Record;
using Active.Core;
using History = Active.Core.Details.History;

namespace Activ.BTL{
public partial class BTL : MonoBehaviour, LogSource{

    public string path;
    public Component[] @import;
    public string[] requirements;
    public bool useHistory = true;
    public bool recordIntents = false;
    public bool sparse = true;
    public Record record;
    public BTLCog cognition;
    //
    string log, output, loadedFrom;
    // TODO don't see a point in making this public but see
    // BTL script checker
    public object program;
    object[] externals;
    Interpreter<Cx> ι;
    History _history;
    bool useScene = false;
    bool suspend = false;
    BTLContextFactory contextFactory = new BTLContextFactory();
    // refreshed at every iteration; keeping this handy
    // for clients to access the stack
    Context context;

    public Elk.Stack stack => context?.callStack;

    public void Hold(bool flag, object src){
        if(!sparse){ suspend = false; return; }
        if(flag){
            suspend = true;
        }else{
            suspend = false;
        }
    }

    // Use this function to record events; BTL takes care of
    // recording what agents are doing but doesn't keep track of
    // non-actions, such as snowfall, physics and apperception
    // (spotted "x")
    // NOTE convert args early via BTLCog.ArgsToString(object)
    public void RecordEvent(string action, string args, status @out)
    => cognition.CommitEvent(action, args, @out, record);

    // -------------------------------------------------------------

    void Start() => EvalExternals();

    void Awake(){
        record = new Record(gameObject.name);
        cognition = new BTLCog(this);
    }

    void Update(){
        if(suspend) return;
        if(string.IsNullOrEmpty(path)) return;
        EvalProgram();
        if(program == null) return;
        context = contextFactory.Create(
            this, program, useScene, externals
        );
        output = interpreter.Run(context)?.ToString();
        log = context.graph.Format();
        if(useHistory) history.Log(log, transform);
        context = null;
    }

    void OnValidate(){
        if(path == null) return;
        if(path.EndsWith(".txt")){
            path = path.Substring(0, path.Length-4);
        }
    }

    void OnDisable() => log = "DISABLED";

    void EvalProgram(){
        if(loadedFrom != path && IsValidPath(path)){
            program    = Build(path);
            loadedFrom = path;
        }
    }

    bool IsValidPath(string path)
    => Resources.Load<TextAsset>(path) != null;

    object Build(string path){
        var ph      = new BTLPathHandler();
        var builder = new Builder(
            interpreter.reader, ph, BTLScriptChecker.Shebang
        );
        var program = builder.Build(path);
        useScene = ph.useScene;
        return program;
    }

    public System.Func<string, Transform> findInScene{ get{
        var finder = GetComponent<Finder>();
        return finder != null ? finder.FindInScene : null;
    }}

    Interpreter<Cx> interpreter
    => ι != null ? ι : (ι = BTLInterpreterFactory.Create());

    // History related ---------------------------------------------

    public History history => useHistory
        ? _history ?? (_history = new History())
        : _history = null;

    // <LogSource> =================================================

    History LogSource.GetHistory() => history;
    bool    LogSource.IsLogging()  => true;
    string  LogSource.GetLog()     => log;

}}
