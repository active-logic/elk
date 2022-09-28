using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Elk.Basic.Graph{
public class FuncDef{

    public readonly string name;
    public readonly string[] parameters;
    public object body;

    public FuncDef(
        string name, IEnumerable<string> @params, object body
    ){
        this.name = name;
        this.parameters = @params?.ToArray() ?? null;
        this.body = body;
    }

    public int paramCount => parameters?.Length ?? 0;

    public bool MatchesSignatureOf(FuncDef that)
    => Matches(that.name, that.paramCount);

    public bool Matches(string name, int paramCount)
    => name == this.name && paramCount == this.paramCount;

    bool FuncMatches(FuncDef fdef, string name, int argLength){
        if(fdef == null){
            Debug.LogError("fdef is null");
            return false;
        }
        if(fdef.name == null){
            Debug.LogError("fdef name is null");
            return false;
        }
        return fdef.name == name && fdef.paramCount == argLength;
    }

    override public string ToString(){
        if(parameters == null) return name + " → {" + body + "}";
        var @out = new StringBuilder();
        @out.Append(name + "(");
        for(int i = 0; i < parameters.Length; i++){
            @out.Append(
                $"{parameters[i]}" +
                ((i < parameters.Length - 1) ? ',' : ')')
            );
        }
        return "(" + @out.ToString() + " → {" + body + "})";
    }

}}
