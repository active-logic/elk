using Ex = System.Exception;

namespace Elk.Basic.Runtime{
public readonly struct Call{

    readonly string func;
    readonly object[] args;

    public Call(string func, object[] args){
        this.func = func;
        this.args = args;
    }

}}
