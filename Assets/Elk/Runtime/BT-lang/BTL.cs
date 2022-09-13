using UnityEngine;
using Elk;

namespace Activ.Script{
public class BTL : MonoBehaviour{

    public string path;
    object π;
    Interpreter ι;
    public string output;

    void Update() => output
        = interpreter.runner.Run(program, gameObject)?.ToString();

    object Parse(string path){
        var src = Resources.Load<TextAsset>(path).text;
        if(src.StartsWith(BTLScriptChecker.Shebang))
            src = src.Substring(5);
        return interpreter.Parse(src);
    }

    object program => π != null ? π : (π = Parse(path));

    Interpreter interpreter
    => ι != null ? ι : (ι = NewInterpreter);

    public static Interpreter NewInterpreter => new Interpreter(
        new Elk.Basic.Tokenizer(),
        new Elk.Basic.Parser(),
        new Elk.Basic.Runner()
    );

}}
