using System;

namespace Elk.Basic.Runtime{
public class DelegatedPropertyBinding<T> : PropertyBinding{

    string @var;
    Func<string, T> func;

    public DelegatedPropertyBinding(
        string @var, Func<string, T> func
    ){
        this.@var = @var;
        this.func = func;
    }

    public object value  => func(@var);
    public bool   exists => true;

}}
