using System.Linq;
using UnityEngine;
using Elk;
using Cx = Elk.Basic.Context;

namespace Activ.BTL{
public class BTL : MonoBehaviour{

    public string path;
    public Component[] @import;
    public string output;
    object π;
    Interpreter<Cx> ι;

    void Update(){
        var π  = program;
        var cx = BTLContext.Create(π, Untype(@import));
        output = interpreter.runner.Run(π, cx)?.ToString();
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
    => ι != null ? ι : (ι = NewInterpreter);

    public static Interpreter<Cx> NewInterpreter
    => new Interpreter<Cx>(
        new Elk.Basic.Tokenizer(),
        new Elk.Basic.Parser(),
        new Elk.Basic.Runner()
    );

}}
