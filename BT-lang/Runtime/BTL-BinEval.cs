using Ex = System.Exception;
using O = System.Object;
using BF = System.Reflection.BindingFlags;
using Elk.Util;
using Elk.Basic;
using Elk.Basic.Graph;
using Elk.Basic.Runtime;
using RtEx = Elk.ElkRuntimeException;
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

    override public object Eval_obj_x_obj(
        object X, object right, string op, Runner ρ, Context cx
    ){
        switch(X){
            case bool b: return EvalBool(
                op, b, right, ρ, cx
            );
            case status s: return EvalStatus(
                op, s, right, ρ, cx
            );
            default: return base.Eval_obj_x_obj(
                X, right, op, ρ, cx
            );
        }
        throw new RtEx($"Unsupported operation: {X} {op} ? ({X.GetType()})");
    }

    // NOTE - only && and || are not directly overloaded, therefore
    // other operators are supported via reflection
    object EvalBool(string op, bool X, object right,
                               Runner ρ, Context cx){
        switch(op){
            case "||": return !X ? ToStatus(ρ.Eval(right, cx)) : done;
            case "&&": return  X ? ToStatus(ρ.Eval(right, cx)) : fail;
        }
        throw new RtEx($"Unimplemented op {op}");
    }

    // NOTE - only && and || are not directly overloaded, therefore
    // other operators are supported via reflection
    object EvalStatus(string op, status X, object right,
                             Runner ρ, Context cx){
        switch(op){
            case "||": return X.failing  ? ToStatus(ρ.Eval(right, cx)) : X;
            case "&&": return X.complete ? ToStatus(ρ.Eval(right, cx)) : X;
        }
        throw new RtEx($"Unimplemented op {op}");
    }

    status ToStatus(object arg)
    => arg is status s ? s : arg is bool b ? (status)b
    : throw new RtEx($"Don't know how to convert {arg.Format()} to status");

}}
