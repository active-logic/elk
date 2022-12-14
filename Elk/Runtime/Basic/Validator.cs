using Ex = System.Exception;
using Elk.Basic.Graph;

namespace Elk.Basic{
public class Validator : Elk.Validator{

    public void Validate(object program, string path){
        if(!(program is Module)) throw new Ex(
            $"Excepted module, {program.GetType()} found");
        var module = (Module) program;
        FlagDupes(module.functions, path);
    }

    void FlagDupes(FuncDef[] F, string path){
        for(int i = 0; i < F.Length; i++){
            for(int j = i + 1; j < F.Length; j++){
                if(F[i].MatchesSignatureOf(F[j])){
                    throw new Ex($"Duplicate function: {F[i]} in {path}");
                }
            }
        }
    }

}}
