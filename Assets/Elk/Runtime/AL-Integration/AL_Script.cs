using UnityEngine;
using Elk;

namespace Activ.Script{
public class AL_Script : MonoBehaviour{

    public string path;
    object π;
    Interpreter ι;
    public string output;

    void Update() => output
        = interpreter.runner.Run(program, gameObject).ToString();

    object Parse(string path){
        var src = Resources.Load<TextAsset>(path).text;
        return interpreter.Parse(src);
    }

    object program => π != null ? π : (π = Parse(path));

    Interpreter interpreter => ι != null ? ι : (ι = new Interpreter(
        new Elk.Basic.Tokenizer(),
        new Elk.Basic.Parser(),
        new AcRunner()
    ));

}}
