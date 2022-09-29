using Ex = System.Exception;
using System;
using System.Linq;
//using Elk.Bindings.CSharp;

namespace Elk.Basic.Graph{
public class Module{

    public readonly Include[] includes;
    public FuncDef[] functions;

    public Module(Include[] includes, FuncDef[] functions){
        this.includes = includes;
        this.functions = functions;
    }

    public void Merge(Module other, string path){
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
