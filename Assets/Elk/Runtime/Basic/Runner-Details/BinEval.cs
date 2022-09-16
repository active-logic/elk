using Ex = System.Exception;
using O = System.Object;
using Elk.Basic;
using Elk.Basic.Graph;
using Active.Core;

namespace Elk.Basic.Runtime{
public static class BinEval{

    public static object Eval(BinaryOp operation,
                              Runner ρ, Context cx){
        object X = ρ.Eval(operation.arg0, cx);
        object Y = ρ.Eval(operation.arg1, cx);
        string op = (string)operation.op;
        if(X is status && Y is status){
            return EvalStatus(op, (status)X, (status)Y);
        }
        if(X is float && Y is float){
            return EvalFloat(op, (float)X, (float)Y);
        }
        if(X is int && Y is int){
            return EvalInt(op, (int)X, (int)Y);
        }
        throw new Ex($"Unsupported operands: {X}, {Y} ({X.GetType()}, {Y.GetType()})");
    }

    public static object EvalStatus(string op, status X, status Y){
        switch(op){
            case "*": return X * Y;
            //case "/": return X / Y;
            case "%": return X % Y;
            //
            case "+": return X + Y;
            //case "-": return X - Y;
            //
            case "==": return X == Y;
            case "!=": return X != Y;
            //
            //case "|": return X | Y;
            //case "&": return X & Y;
            //
            case "||": return X || Y;
            case "&&": return X && Y;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

    public static object EvalFloat(string op, float X, float Y){
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
            //case "|": return X | Y;
            //case "&": return X & Y;
            //
            //case "||": return X || Y;
            //case "&%": return X && Y;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

    public static object EvalInt(string op, int X, int Y){
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
            //case "|": return X | Y;
            //case "&": return X & Y;
            //
            //case "||": return X || Y;
            //case "&%": return X && Y;
            //
            default: throw new Ex($"Unimplemented op {op}");
        }
    }

}}
