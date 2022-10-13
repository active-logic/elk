namespace Elk.Basic.Runtime{
public partial class CallGraph{

public interface Formatter{

    string Property(string name, object value);
    string ReturnValue(object arg);

}

public class DefaultFormatter: Formatter{

    string Formatter.Property(string name, object value)
    => name + " : " + value?.ToString() ?? "null";

    string Formatter.ReturnValue(object arg)
    => arg?.ToString() ?? "null";

}

}}
