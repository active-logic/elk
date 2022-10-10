using Ex = System.Exception;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Basic;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class UnaEval{

    public object Eval(UnaryExp operation,
                       Runner ρ, Context cx){
        var operand = ρ.Eval(operation.arg, cx);
        // TODO - CS binding calls should be in 'extern'
        if(operand == null) throw new Ex(
            $"Null arg to unary '{operation.op}' is not allowed."
        );
        var type = operand.GetType();
        var method = type.GetMethod(
            operation.binding, BF.Static | BF.Public
        );
        if(method != null){
            return method.Invoke(null, new object[]{operand} );
        }else{
            return DumbEval(operand, operation.op, ρ, cx);
        }
    }

    // NOTE - C# native ops need a dumb eval because they're not
    // exposed via reflection (say 2 + 2); without 'dynamic' this
    // is a pain (also, short-circuit operators cause not *directly*
    // overloaded)
    protected virtual object DumbEval(object X, string op, Runner ρ,
                                      Context cx){
        switch(X){
            case bool boolVal: return EvalBool(op, boolVal);
            case int  intVal:  return EvalInt(op, intVal);
        }
        throw new Ex($"Unsupported operation: {X}{op} ({X.GetType()})");
    }

    object EvalBool(string op, bool X){
        switch(op){
            case "!": return !X;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

    object EvalInt(string op, int X){
        switch(op){
            case "++": return X + 1;
            case "--": return X - 1;
            case "-": return -X;
            case "+": return +X;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

}}
