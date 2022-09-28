using System.Linq;
using UnityEngine;
using Elk; using Elk.Basic;
using Cx = Elk.Basic.Context;
using Record = Elk.Memory.Record;
using Active.Core;
using History = Active.Core.Details.History;

namespace Activ.BTL{
public class BTL : MonoBehaviour, LogSource{

    public string path;
    public Component[] @import;
    public bool useHistory = true;
    public Record record;
    public BTLCog cognition;
    //
    string log, output, loadedFrom;
    object π;
    Interpreter<Cx> ι;
    History _history;

    void Awake(){
        record = new Record(gameObject.name);
        cognition = new BTLCog(this);
    }

    void Update(){
        if(path != loadedFrom && IsValidPath(path)) π = null;
        var p  = program;
        var cx = BTLContextFactory.Create(this, p, Untype(@import));
        output = interpreter.Run(cx)?.ToString();
        log = cx.graph.Format();
        if(useHistory) history.Log(log, transform);
    }

    void OnValidate(){
        if(path.EndsWith(".txt")){
            path = path.Substring(0, path.Length-4);
        }
    }

    void OnDisable() => log = "DISABLED";

    bool IsValidPath(string path)
    => Resources.Load<TextAsset>(path) != null;

    object Build(string path) => new Builder(
        interpreter.reader, BTLScriptChecker.Shebang
    ).Build(path);

    public object program{
        private get => π != null ? π : (π = Build(path));
        set => π = value;
    }

    // TODO not so great
    object[] Untype(Component[] arg)
    => (from c in @import select (object)c).ToArray();

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
