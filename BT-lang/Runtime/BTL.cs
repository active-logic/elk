using System;
using System.Linq;
using UnityEngine;
using Elk; using Elk.Basic; using Elk.Basic.Runtime;
using Cx = Elk.Basic.Runtime.Context;
using Record = Elk.Memory.Record;
using Active.Core; using static Active.Status;
using History = Active.Core.Details.History;
using Occurence = Elk.Memory.Occurence;
#if UNITY_EDITOR
using Ed = UnityEditor.EditorApplication;
#endif

namespace Activ.BTL{
public partial class BTL : MonoBehaviour, LogSource{

    public string path;
    public Component[] @import;
    public object[] externals;
    public string[] requirements;
    public string[] submodules;
    public bool useHistory = true;
    public bool recordIntents = false;
    public bool sparse = true;
    public int postroll = 1;  // postpone running by N frames
    [Header("Debugging")]
    public bool pauseOnErrors = false;
    public bool logErrors = true;
    //
    Vars _vars;

    // -------------------------------------------------------------

    public object program
    { get => vars.program; set => vars.program = value; }

    public Record record => vars.record;
    public BTLCog cognition => vars.cognition;
    public string exceptionMessage => vars.exceptionMessage;
    public bool hasValidPath => vars.hasValidPath;
    public Elk.Stack stack => vars.stack;

    // -------------------------------------------------------------

    public action Wait(float duration){
        this.enabled = false;
        Invoke("Resume", duration);
        return @void();
    }

    Vars Init(){
        var self = new Vars();
        self.contextFactory = new BTLContextFactory();
        self.record = new Record(gameObject.name);
        self.cognition = new BTLCog(this);
        EvalExternals();
        return self;
    }

    public void Resume() => this.enabled = true;

    public void Hold(bool flag, object src){
        if(!sparse){ vars.suspend = false; return; }
        if(flag){
            vars.suspend = true;
        }else{
            vars.suspend = false;
        }
    }

    public void RecordEvent(Occurence arg, status @out)
    => vars.cognition.CommitEvent(arg, @out, record);

    // -------------------------------------------------------------

    void Update(){
        #if UNITY_EDITOR
        // NOTE BTL while compiling may cause an arg stack error
        if(Ed.isCompiling) return;
        #endif
        if(vars.frame++ < postroll) return;
        if(vars.suspend) return;
        if(string.IsNullOrEmpty(path)) return;
        try{
            EvalProgram();
            if(vars.program == null) return;
            vars.context = vars.contextFactory.Create(
                this, vars.program, vars.useScene, externals
            );
            vars.output = interpreter.Run(vars.context)?.ToString();
            vars.exceptionMessage = null;
            vars.log = vars.context.graph.Format();
            if(useHistory) history.Log(vars.log, transform);
            vars.context = null;
        }catch(Exception ex){
            vars.exceptionMessage = ex.Message;
            vars.log = "ERROR";
            vars.context = null;
            if(pauseOnErrors) Debug.Break();
            if(logErrors) throw;
        }
    }

    void OnValidate(){
        if(path == null) return;
        if(path.EndsWith(".txt")){
            path = path.Substring(0, path.Length-4);
        }
        IsValidPath(path);
    }

    void EvalProgram(){
        if(vars.loadedFrom != path && IsValidPath(path)){
            vars.program = Build(path);
            vars.loadedFrom = path;
        }
    }

    bool IsValidPath(string path)
    => vars.hasValidPath = Resources.Load<TextAsset>(path) != null;

    object Build(string path){
        var ph      = new BTLPathHandler();
        var builder = new Builder(
            interpreter.reader, ph, BTLScriptChecker.Shebang
        );
        var program = builder.Build(path, submodules);
        vars.useScene = ph.useScene;
        return program;
    }

    public System.Func<string, Transform> findInScene{ get{
        var finder = GetComponent<Finder>();
        return finder != null ? finder.FindInScene : null;
    }}

    Interpreter<Cx> interpreter
    => vars.ι != null
        ? vars.ι : (vars.ι = BTLInterpreterFactory.Create());

    // History related ---------------------------------------------

    public History history => useHistory
        ? vars.history ?? (vars.history = new History())
        : vars.history = null;

    // <LogSource> =================================================

    Vars vars => _vars ?? (_vars = Init());

    History LogSource.GetHistory()
    => (_vars == null) ? null : history;

    bool LogSource.IsLogging() => true;

    // NOTE - history window may not cause BTL to instantiate.
    string LogSource.GetLog()
    => (_vars == null) ? null : vars.log;

}}
