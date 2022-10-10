using Ex = System.Exception;

namespace Elk.Basic.Runtime{

public class InvalidPropertyBinding : PropertyBinding{

    public object value  => throw new Ex("No such property");
    public bool   exists => false;

}}
