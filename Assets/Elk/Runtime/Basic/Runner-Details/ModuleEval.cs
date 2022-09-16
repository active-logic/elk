using Ex = System.Exception;
using Array = System.Array;
using FuncDef = Elk.Basic.Graph.FuncDef;

namespace Elk.Basic.Runtime{
public static class ModuleEval{

    public static object Eval(FuncDef[] module, Runner ρ, Context cx){
        var main = Array.Find(module, x => x.name == "Main");
        return main != null
            ? ρ.Eval(main.body, cx)
            : throw new Ex("No Main function");
    }

}}
