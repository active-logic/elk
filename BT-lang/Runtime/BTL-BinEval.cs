using Ex = System.Exception;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Basic;
using Elk.Basic.Graph;
using Active.Core; using static Active.Raw;

namespace Activ.BTL.Imp{
public class BinEval : Elk.Basic.Runtime.BinEval{

    override protected void CastOperands(ref object x, ref object y){
        if(x is status && y is bool Y){
            y = (status)Y;
        }
        if(x is bool X && y is status){
            x = (status)X;
        }
    }

    override protected object DumbEval(object X, object right, string op,
                                       Runner ρ, Context cx){
        switch(X){
            case float f: return EvalFloat(
                op, f, (float)ρ.Eval(right, cx)
            );
            case int i: return EvalInt(
                op, i, (int)ρ.Eval(right, cx)
            );
            case bool b: return EvalBool(
                op, b, right, ρ, cx
            );
            case status s: return EvalStatus(
                op, s, right, ρ, cx
            );
            case null: return EvalStatus(  // TODO #25
                op, fail, right, ρ, cx
            );
        }
        throw new Ex($"Unsupported operation: {X} {op} ? ({X.GetType()})");
    }

    // NOTE - only && and || are not directly overloaded, therefore
    // other operators are supported via reflection
    object EvalBool(string op, bool X, object right,
                               Runner ρ, Context cx){
        switch(op){
            case "||": return !X ? ToStatus(ρ.Eval(right, cx)) : done;
            case "&&": return  X ? ToStatus(ρ.Eval(right, cx)) : fail;
        }
        throw new Ex($"Unimplemented op {op}");
    }

    // NOTE - only && and || are not directly overloaded, therefore
    // other operators are supported via reflection
    object EvalStatus(string op, status X, object right,
                             Runner ρ, Context cx){
        switch(op){
            case "||": return X.failing  ? ToStatus(ρ.Eval(right, cx)) : X;
            case "&&": return X.complete ? ToStatus(ρ.Eval(right, cx)) : X;
        }
        throw new Ex($"Unimplemented op {op}");
    }

    status ToStatus(object arg)
    => arg is status s ? s : arg is bool b ? (status)b
    : arg is null ? fail  // TODO #25
    : throw new Ex($"Don't know how to convert {arg} to status");

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
