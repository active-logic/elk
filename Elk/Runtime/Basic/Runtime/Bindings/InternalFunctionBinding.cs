using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class InternalFunctionBinding : InvocationBinding{

    protected object target;  // NOTE unused; for instance func
    protected FuncDef function;

    public InternalFunctionBinding(object target, FuncDef function){
        this.target = target;
        this.function = function;
    }

    override protected object Invoke(
        object[] values, Runner<Context> ρ, Context cx
    ){
        cx.PushArguments(function.parameters, values);
        var @out = ρ.Eval(function.body, cx);
        cx.PopArguments();
        return @out;
    }

}}
