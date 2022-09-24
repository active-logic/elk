using UnityEditor;
using UnityEngine;
using System.IO;
using Elk;
using Cx = Elk.Basic.Context;
using Interpreter = Elk.Basic.Interpreter;

namespace Activ.BTL{
public class BTLScriptChecker : AssetPostprocessor{

    public const string Shebang = "#!btl";
    Interpreter<Cx> interpreter = BTLInterpreterFactory.Create();

    void OnPreprocessAsset()
    {
        if(!assetPath.Contains("Resources")) return;
        if(!assetPath.EndsWith(".txt"))      return;
        if(!Match(assetPath, Shebang))       return;
        //ebug.Log($"Process BTL file {assetPath}");
        var content = File.ReadAllText(assetPath).Substring(5);
        var obj = interpreter.Parse(content);
        foreach(var bt in Object.FindObjectsOfType<BTL>()){
            if(assetPath.Contains(bt.path)){
                bt.program = obj;
            }
        }
    }

    public static bool Match(string filename, string contentPrefix){
        using (var stream = File.OpenRead(filename))
        using (var reader = new StreamReader(stream)){
            var count = contentPrefix.Length;
            var buffer = new char[count];
            int n = reader.ReadBlock(buffer, 0, count);
            if(n < count) return false;
            for(int i = 0; i < count; i++){
                if(contentPrefix[i] != buffer[i]) return false;
            }
            return true;
        }
    }

}}
