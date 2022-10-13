using System;
using RtEx = Elk.ElkRuntimeException;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Util; using Elk.Basic;
using Elk.Basic.Graph;
using UnityEngine;

namespace Elk.Basic.Runtime{
public class BinEval{

    public object Eval(BinaryExp operation, Runner ρ, Context cx){
        var left = ρ.Eval(operation.arg0, cx);
        // TODO - CS binding calls should be in 'Extern'
        var binding = operation.binding;
        if(binding != null){
            var right = ρ.Eval(operation.arg1, cx);
            CastOperands(ref left, ref right);
            var type = left.GetType();
            var method = type.GetMethod(
                binding, BF.Static | BF.Public
            );
            //ebug.Log($"METHOD: {method}");
            if(method != null){
                return method.Invoke(null, new object[]{left, right} );
            }
        }
        return Eval_obj_x_obj(left, operation.arg1,
                              (string)operation.op, ρ, cx);
    }

    protected virtual void CastOperands(ref object x, ref object y){}

    // NOTE - afaik until C#6 support for concise handling of
    // numeric types is weak.
    public virtual object Eval_obj_x_obj(object X, object right,
                                   string op, Runner ρ,
                                   Context cx){
        return Eval_obj_x_obj(X, op, ρ.Eval(right, cx));
    }

    // NOTE - can convert Y to the desired type via IConvertible or
    // Convert.To... but on its own too powerful, will round numbers
    public virtual object Eval_obj_x_obj(object X, string op, object Y){
        switch(X){
            case float floatVal: return EvalFloat(
                op, floatVal, (float)Y
            );
            case int intVal: return EvalInt(
                op, intVal,  (int)Y
            );
        }
        var Tx = X?.GetType();
        var Ty = Y?.GetType();
        throw new RtEx(
              $"Unsupported: {X.Format()} {op} {Y.Format()}"
            + $" ({Tx.Format()}, {Ty.Format()})"
        );
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
            case "<": return X < Y;
            case ">": return X > Y;
            case "<=": return X <= Y;
            case ">=": return X >= Y;
            //
            case "==": return X == Y;
            case "!=": return X != Y;
            //
            default: throw new RtEx($"Unimplemented op {op}");
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
            case "<": return X < Y;
            case ">": return X > Y;
            case "<=": return X <= Y;
            case ">=": return X >= Y;
            //
            case "==": return X == Y;
            case "!=": return X != Y;
            //
            default: throw new RtEx($"Unimplemented op {op}");
        }
    }

}}
