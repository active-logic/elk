using UnityEngine;

namespace Elk.Basic.Runtime{
public class ArgumentBinding : PropertyBinding{

    string @var;
    ArgumentStack argstack;

    public ArgumentBinding(string @var, ArgumentStack stk){
        this.@var     = @var;
        this.argstack = stk;
    }

    public object value => argstack[ @var ];

    public bool exists => true;

}}
