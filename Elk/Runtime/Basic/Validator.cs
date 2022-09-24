using Ex = System.Exception;
using FuncDef = Elk.Basic.Graph.FuncDef;

namespace Elk.Basic{
public class Validator : Elk.Validator{

    public void Validate(object program, bool allowSubPrograms=true){
        var F = program as FuncDef[];
        if(F == null && allowSubPrograms) return;
        for(int i = 0; i < F.Length; i++){
            for(int j = i + 1; j < F.Length; j++){
                if(F[i].MatchesSignatureOf(F[j])){
                    throw new Ex($"Duplicate function: {F[i]}");
                }
            }
        }
    }

}}
