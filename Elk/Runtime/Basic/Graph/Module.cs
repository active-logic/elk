using Ex = System.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
//using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class Module{

    public Include[] includes;
    public FuncDef[] functions;

    public Module(Include[] includes, FuncDef[] functions){
        this.includes = includes;
        this.functions = functions;
    }

    public void AddIncludes(IEnumerable<string> args, string except){
        var list = includes?.ToList() ?? new List<Include>();
        foreach(var x in args){
            if(x == except) continue;
            list.Add(new Include(x));
        }
        includes = list.ToArray();
    }

    public void Merge(Module other, string path){
        if(other == null) return;
        foreach(var fdef in other.functions){
            if(Array.Find(
                functions,
                x => x.MatchesSignatureOf(fdef)) !=null
            ){
                throw new Ex($"{path} already contains {fdef}");
            }
        }
        functions = functions.Concat(other.functions).ToArray();
    }

    int includeCount => includes?.Length ?? 0;
    int functionCount => functions?.Length ?? 0;

    override public string ToString()
    => $"(includes: {includeCount}, functions: {functionCount})";

}}
