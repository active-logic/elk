using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Elk.Basic.Graph{
public class OneLineFunc{

    public readonly string   name;
    public readonly object[] parameters;
    public object   body;

    public OneLineFunc(string name, IEnumerable<object> @params, object body){
        this.name = name;
        this.parameters = @params?.ToArray() ?? null;
        this.body = body;
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
        return @out.ToString() + " → {" + body + "}";
    }

}}
