using Elk.Util;
using Elk.Basic.Graph;
using UnityEngine;
using Ex = System.Exception;

namespace Elk.Basic{
public class TypeCaster : Sequence.Transformer{

    public void Transform(Sequence vec){
        for(int i = 0; i < vec.size; i++){
            vec[i] = TransformToken((string)vec[i]);
        }
    }

    public virtual object TransformToken(string arg){
        if(char.IsDigit(arg[0])){
            if(arg.EndsWith("f")){
                return float.Parse(arg.Substring(0, arg.Length-1));
            }else{
                return int.Parse(arg);
            }
        }
        switch(arg){
            case "true" : return true;
            case "false" : return false;
            case "null": return null;
            default:
                if(IsIdentifier(arg))
                    return new Identifier(arg);
                else if(IsOperator(arg))
                    return new Operator(arg);
                else
                    throw new Ex($"Unrecognized string: {arg}");
        }
    }

    bool IsIdentifier(string arg)
    => arg.Length > 0 && char.IsLetter(arg[0]);

    bool IsOperator(string arg)
    => arg.Length > 0 && arg.Length < 3
    && !char.IsLetterOrDigit(arg[0]);

}}
