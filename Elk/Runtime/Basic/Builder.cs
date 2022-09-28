using System.Collections.Generic;
using Elk;
using Elk.Basic.Graph;
using UnityEngine;

namespace Elk.Basic{
public class Builder{

    Reader reader;
    string shebang;

    public Builder(Reader reader, string shebang=null){
        this.reader = reader;
        this.shebang = shebang;
    }

    public Module Build(string path, List<string> paths=null){
        if(paths == null) paths = new List<string>();
        paths.Add(path);
        var module = Parse(path);
        var includes = module.includes;
        if(includes != null) foreach(var inc in includes){
            if(paths.Contains(inc.module)) continue;
            module.Merge(Build(inc.module, paths), inc.module);
        }
        return module;
    }

    Module Parse(string path){
        var src = Resources.Load<TextAsset>(path)?.text;
        if(src == null)
            throw new ParsingException($"Invalid path {path}");
        if(shebang != null && src.StartsWith(shebang))
            src = src.Substring(shebang.Length);
        try{
            return (Module)reader.Parse(src);
        }catch(ParsingException ex){
            throw new ParsingException
                ( ex.Message + $" in {path}.txt", ex );
        }
    }

}}
