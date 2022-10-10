using Ex = System.Exception;

namespace Elk.Basic.Runtime{

public class InvalidPropertyBinding : PropertyBinding{

    readonly string name;

    public InvalidPropertyBinding(string name)
    => this.name = name;

    public object value
    => throw new Ex($"No such property: {name}");

    public bool exists
    => false;

}}
