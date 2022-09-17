using System.Linq;
using UnityEngine;
using Elk;
using Cx = Elk.Basic.Context;
using Active.Core;

namespace Activ.BTL{
public class BTL : MonoBehaviour{

    public string path;
    public Component[] @import;
    public string output;
    [Multiline(6)]
    public string graph;
    object π;
    Interpreter<Cx> ι;

    void Update(){
        var π  = program;
        var cx = BTLContextFactory.Create(π, Untype(@import));
        output = interpreter.Run(cx)?.ToString();
        graph = cx.graph.Format();
    }

    void OnValidate(){
        if(path.EndsWith(".txt")){
            path = path.Substring(0, path.Length-4);
        }
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



}}
