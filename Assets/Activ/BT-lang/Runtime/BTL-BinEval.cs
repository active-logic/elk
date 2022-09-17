using Ex = System.Exception;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Basic;
using Elk.Basic.Graph;
using Active.Core;

namespace Activ.BTL.Imp{
public class BinEval : Elk.Basic.Runtime.BinEval{

    override protected object DumbEval(object X, object right, string op,
                                       Runner ρ, Context cx){
        switch(X){
            case status statusVal: return EvalStatus(
                op, statusVal, right, ρ, cx
            );
            case float floatVal: return EvalFloat(
                op, floatVal, (float)ρ.Eval(right, cx)
            );
            case int intVal: return EvalInt(
                op, intVal, (int)ρ.Eval(right, cx)
            );
        }
        throw new Ex($"Unsupported operation: {X} {op} ? ({X.GetType()})");
    }

    // NOTE - only && and || are not directly overloaded, therefore
    // other operators are supported via reflection
    object EvalStatus(string op, status X, object right,
                             Runner ρ, Context cx){
        switch(op){
            case "||": return X.failing  ? ρ.Eval(right, cx) : X;
            case "&&": return X.complete ? ρ.Eval(right, cx) : X;
        }
        throw new Ex($"Unimplemented op {op}");
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
