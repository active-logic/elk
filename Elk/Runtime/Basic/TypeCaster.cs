using Elk.Util;
using UnityEngine;

namespace Elk.Basic{
public class TypeCaster : Sequence.Transformer{

    public void Transform(Sequence vec){
        for(int i = 0; i < vec.size; i++){
            vec[i] = TransformToken((string)vec[i]);
        }
    }

    object TransformToken(string arg){
        if(char.IsDigit(arg[0])) return int.Parse(arg);
        switch(arg){
            case "true" : return true;
            case "false" : return false;
//            case "fail": return fail;
//            case "cont": return cont;
//            case "done": return done;
            case "null": return null;
            default: return arg;
        }
    }

}}
