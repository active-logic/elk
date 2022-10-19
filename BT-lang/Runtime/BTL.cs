using System;
using System.Linq;
using UnityEngine;
using Elk; using Elk.Basic; using Elk.Basic.Runtime;
using Cx = Elk.Basic.Runtime.Context;
using Record = Elk.Memory.Record;
using Active.Core; using static Active.Status;
using History = Active.Core.Details.History;

namespace Activ.BTL{
public partial class BTL : MonoBehaviour, LogSource{

    public string path;
    public Component[] @import;
    public string[] requirements;
    public bool useHistory = true;
    public bool recordIntents = false;
    public bool sparse = true;
    [Header("Debugging")]
    public bool pauseOnErrors = false;
    public bool logErrors = true;
    //
    public Record record;
    public BTLCog cognition;
    //
    [NonSerialized] public bool hasValidPath = true;
    [NonSerialized] public string exceptionMessage;
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

    // -------------------------------------------------------------

    public Elk.Stack stack => context?.callStack;

    // -------------------------------------------------------------

    public action Wait(float duration){
        this.enabled = false;
        Invoke("Resume", duration);
        return @void();
    }

    public void Resume() => this.enabled = true;

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
    public string RecordEvent(string action, string args)
    => cognition.CommitEvent(action, args, done(), record);

    public string RecordEvent(string action, string args, status @out)
    => cognition.CommitEvent(action, args, @out, record);

    // -------------------------------------------------------------

    void Start() => EvalExternals();

    // NOTE: public for testing
    public void Awake(){
        record = new Record(gameObject.name);
        cognition = new BTLCog(this);
    }

    void Update(){
        if(suspend) return;
        if(string.IsNullOrEmpty(path)) return;
        try{
            EvalProgram();
            if(program == null) return;
            context = contextFactory.Create(
                this, program, useScene, externals
            );
            output = interpreter.Run(context)?.ToString();
            exceptionMessage = null;
        }catch(Exception ex){
            exceptionMessage = ex.Message;
            if(pauseOnErrors) Debug.Break();
            if(logErrors) throw;
        }finally{
            log = context.graph.Format();
            if(useHistory) history.Log(log, transform);
            context = null;
        }
    }

    void OnValidate(){
        if(path == null) return;
        if(path.EndsWith(".txt")){
            path = path.Substring(0, path.Length-4);
        }
        IsValidPath(path);
    }

    void OnDisable() => log = "DISABLED";

    void EvalProgram(){
        if(loadedFrom != path && IsValidPath(path)){
            program    = Build(path);
            loadedFrom = path;
        }
    }

    bool IsValidPath(string path)
    => hasValidPath = Resources.Load<TextAsset>(path) != null;

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
