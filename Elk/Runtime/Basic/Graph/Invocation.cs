using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Elk.Basic.Graph{
public class Invocation : Expression{

    public static ulong nextId = 0;
    public readonly ulong id;
    public readonly string name;
    public readonly object[] arguments;
    public readonly object[] values;
    public object binding;

    public Invocation(string name, IEnumerable<object> arguments=null){
        this.id = nextId ++;
        this.name = name;
        this.arguments = arguments?.ToArray() ?? null;
        this.values = this.arguments != null
            ? new object[this.arguments.Length] : new object[]{};
    }

    public int argumentCount => arguments?.Length ?? 0;

    override public string ToString(){
        if(arguments == null) return $"{name}()";
        var @out = new StringBuilder();
        @out.Append(name);
        @out.Append("(");
        for(int i = 0; i < arguments.Length; i++){
            @out.Append(
                $"{arguments[i]}" +
                ((i < arguments.Length - 1) ? ',' : ')')
            );
        }
        return @out.ToString();
    }

}}
