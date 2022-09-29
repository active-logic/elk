using Elk.Util;
using UnityEngine;

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
            default: return arg;
        }
    }



}}
