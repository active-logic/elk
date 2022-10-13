using Elk.Basic;
using Elk.Basic.Runtime;
using Elk.Basic.Graph;
using Active.Core; using static Active.Core.status;

namespace Activ.BTL{
public class BTLRunner : Elk.Basic.Runner{

    override protected bool IsLiteral(object x)
    => x is status || base.IsLiteral(x);

    override protected void Intercept(
        Invocation ι, Context cx, out Pass pass
    ){
        var args = ι.arguments;
        if(args == null){
            pass = new Pass();
            return;
        }
        var len = args.Length;
        for(int i = 0; i < len; i++){
            ι.values[i] = this.ProcessArg(
                args[i], this, cx,
                out status state,
                out bool stop
            );
            if(stop){
                ι.values[i] = state.complete ? "?" : "!";
                for(var j = i + 1; j < len; j++){
                    ι.values[j] = ".";
                }
                pass = new Pass(i: true, e: true, r: state);
                return;
            }
        }
        pass = new Pass(i: false, e: true, r: cont());
    }

    object ProcessArg(
        object arg, Runner ρ, Context cx, out status s, out bool stop
    ){
        var exp = arg as UnaryExp;
        if(exp == null){
            s = cont();
            stop = false;
            return ρ.Eval(arg, cx);
        }else if(!IsWard(exp)){
            s = cont();
            stop = false;
            return ρ.Eval(arg, cx);
        }else{
            var value = ρ.Eval(exp.arg, cx);
            if(value == null){
                if(exp.op == "?") s = done(); else s = fail();
                stop = true;
                return value;
            }else{
                stop = false;
                s = cont();
                return value;
            }
        }
    }

    bool IsWard(UnaryExp exp)
    => exp.postfix && (exp.op == "?" || exp.op == "!");

}}
