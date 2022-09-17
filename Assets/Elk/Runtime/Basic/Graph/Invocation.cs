using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Elk.Basic.Graph{
public class Invocation{

    public readonly string name;
    public readonly object[] arguments;

    public Invocation(string name, IEnumerable<object> arguments=null){
        this.name = name;
        this.arguments = arguments?.ToArray() ?? null;
    }

    override public string ToString(){
        if(arguments == null) return $"{name}()";
        var @out = new StringBuilder();
        @out.Append(name);
        for(int i = 0; i < arguments.Length; i++){
            @out.Append(
                $"{arguments[i]}" +
                ((i < arguments.Length - 1) ? ',' : ')')
            );
        }
        return @out.ToString();
    }

}}
