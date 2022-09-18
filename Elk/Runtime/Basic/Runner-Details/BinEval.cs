using Ex = System.Exception;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Basic;
using Elk.Basic.Graph;
using UnityEngine;

namespace Elk.Basic.Runtime{
public class BinEval{

    public object Eval(BinaryExp operation,
                       Runner ρ, Context cx){
        var left = ρ.Eval(operation.arg0, cx);
        // TODO - CS binding calls should be in 'Extern'
        var binding = operation.binding;
        if(binding != null){
            var right = ρ.Eval(operation.arg1, cx);
            //ebug.Log($"Phase 0: {binding}({left}, {right})");
            CastOperands(ref left, ref right);
            var type = left.GetType();
            //ebug.Log($"Phase 1: {binding}({left}, {right})");
            var method = type.GetMethod(
                binding, BF.Static | BF.Public
            );
            //ebug.Log($"METHOD: {method}");
            if(method != null){
                return method.Invoke(null, new object[]{left, right} );
            }
        }
        return DumbEval(left, operation.arg1,
                        (string)operation.op, ρ, cx);
    }

    protected virtual void CastOperands(ref object x, ref object y){}

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
