using UnityEngine;
using Elk; using Elk.Basic;

namespace Activ.BTL{
public class BTLPathHandler : PathHandler{

    public bool useScene;

    public string Load(string path){
        if(path == "scene"){
            useScene = true;
            return null;
        }else{
            var src = Resources.Load<TextAsset>(path)?.text;
            return src ??
                   throw new ParsingException($"Invalid path {path}");
        }
    }

}}
