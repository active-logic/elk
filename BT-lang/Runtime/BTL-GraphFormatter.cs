using Active.Core;
using Elk.Basic.Runtime;

namespace Activ.BTL{
public partial class BTL{

public class GraphFormatter : CallGraph.Formatter{

    string CallGraph.Formatter.Property(string name, object value){
        switch(value){
            case status:
                return ReturnValue(value) + " " + name;
            case bool:
                return ReturnValue(value) + " " + name;
            default:
                return name + " : " + value.ToString();
        }
    }

    public string ReturnValue(object arg){
        switch(arg){
            case status s when s.complete:
                return "✓";
            case status s when s.running:
                return "→";
            case status s when s.failing:
                return "✗";
            case bool b:
                return b ? "[✓]" : "[✗]";
            case null:
                return "∅";
            default:
                return arg.ToString();
        }
    }

}

}}
