namespace Elk.Basic.Runtime{
public class InternalPropertyBinding : PropertyBinding{

    string @var;
    ArgumentStack argstack;

    public InternalPropertyBinding(string @var, ArgumentStack stk){
        this.@var     = @var;
        this.argstack = stk;
    }

    public object value  => argstack.Peek()[@var];
    public bool   exists => true;

}}
