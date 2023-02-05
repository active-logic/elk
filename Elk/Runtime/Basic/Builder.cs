using System.Collections.Generic;
using Elk; using Elk.Basic.Graph;

namespace Elk.Basic{
public class Builder{

    Reader reader;
    string shebang;
    PathHandler pathHandler;

    public Builder(Reader reader, PathHandler pathHandler,
                   string shebang=null){
        this.reader = reader;
        this.pathHandler = pathHandler;
        this.shebang = shebang;
    }

    public Module Build(
        string path,
        IEnumerable<string> submodules,
        List<string> paths=null
    ){
        if(paths == null) paths = new List<string>();
        paths.Add(path);
        var module = Parse(path, submodules);
        if(module == null) return null;
        var includes = module.includes;
        if(includes != null) foreach(var inc in includes){
            if(paths.Contains(inc.module)) continue;
            module.Merge(Build(inc.module, submodules:null, paths), inc.module);
        }
        return module;
    }

    Module Parse(string path, IEnumerable<string> submodules){
        var src = pathHandler.Load(path);
        if(src == null) return null;
        if(shebang != null && src.StartsWith(shebang))
            src = src.Substring(shebang.Length);
            // TODO submodule injection will confuse line numbers
            if(submodules != null) foreach(var e in submodules){
                if(string.IsNullOrEmpty(e)) continue;
                src = "with " + e + ";\n" + src;
            }
        try{
            return (Module)reader.Parse(src, path, debug: null);
        }catch(ElkParsingException ex){
            throw new ElkParsingException(
                ex.Message + $" in {path}.txt", ex
            );
        }
    }

}}
