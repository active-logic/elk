using System.Linq;
using UnityEngine;
using Elk;
using Cx = Elk.Basic.Context;
using Active.Core;
using History = Active.Core.Details.History;

namespace Activ.BTL{
public class BTL : MonoBehaviour, LogSource{

    public string path;
    public Component[] @import;
    string output; string log; object π;
    Interpreter<Cx> ι;
    History _history;
    public bool useHistory = true;

    void Update(){
        var π  = program;
        var cx = BTLContextFactory.Create(π, Untype(@import));
        output = interpreter.Run(cx)?.ToString();
        log = cx.graph.Format();
        if(useHistory) history.Log(log, transform);
    }

    void OnValidate(){
        if(path.EndsWith(".txt")){
            path = path.Substring(0, path.Length-4);
        }
    }

    void OnDisable(){
        log = "DISABLED";
    }

    object Parse(string path){
        var src = Resources.Load<TextAsset>(path).text;
        if(src.StartsWith(BTLScriptChecker.Shebang))
            src = src.Substring(5);
        return interpreter.Parse(src);
    }

    object program => π != null ? π : (π = Parse(path));

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
