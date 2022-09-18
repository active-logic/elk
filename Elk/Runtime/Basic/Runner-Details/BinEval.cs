using Ex = System.Exception;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Basic;
using Elk.Basic.Graph;

namespace Elk.Basic.Runtime{
public class BinEval{

    public object Eval(BinaryExp operation,
                       Runner ρ, Context cx){
        var left = ρ.Eval(operation.arg0, cx);
        // TODO - CS binding calls should be in 'Extern'
        var type = left.GetType();
        var method = type.GetMethod(
            operation.binding, BF.Static | BF.Public
        );
        if(method != null){
            // NOTE - leaving here until proven
            UnityEngine.Debug.Log(
                 $"Call C# native overload: "
               + $"{operation.binding} ({left}, ?)"
            );
            var right = ρ.Eval(operation.arg1, cx);
            return method.Invoke(null, new object[]{left, right} );
        }else{
            return DumbEval(left, operation.arg1,
                            (string)operation.op, ρ, cx);
        }
    }

    // NOTE - C# native ops need a dumb eval because they're not
    // exposed via reflection (say 2 + 2); without 'dynamic' this
    // is a pain (also, short-circuit operators cause not *directly*
    // overloaded)
    protected virtual object DumbEval(object X, object right,
                                      string op, Runner ρ,
                                      Context cx){
        switch(X){
            case float floatVal: return EvalFloat(
                op, floatVal, (float)ρ.Eval(right, cx)
            );
            case int intVal: return EvalInt(
                op, intVal, (int)ρ.Eval(right, cx)
            );
        }
        throw new Ex($"Unsupported operation: {X} {op} ? ({X.GetType()})");
    }

    object EvalFloat(string op, float X, float Y){
        switch(op){
            case "*": return X * Y;
            case "/": return X / Y;
            case "%": return X % Y;
            //
            case "+": return X + Y;
            case "-": return X - Y;
            //
            case "==": return X == Y;
            case "!=": return X != Y;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

    object EvalInt(string op, int X, int Y){
        switch(op){
            case "*": return X * Y;
            case "/": return X / Y;
            case "%": return X % Y;
            //
            case "+": return X + Y;
            case "-": return X - Y;
            //
            case "==": return X == Y;
            case "!=": return X != Y;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

}}
