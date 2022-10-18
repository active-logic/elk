using System.Reflection;
using Ex = System.Exception;
// TODO unwanted dep
using Elk.Basic.Runtime;

namespace Elk.Bindings.CSharp{

public abstract class CsPropBinding<T>{

    readonly protected object target;
    readonly protected T property;

    public CsPropBinding(object target, T prop){
        this.target = target; this.property = prop;
    }

    public bool exists => true;

}

// -----------------------------------------------------------------

public class ExternalPropertyBinding : CsPropBinding<PropertyInfo>,
                                       PropertyBinding{

    public ExternalPropertyBinding(object target, PropertyInfo prop)
    : base(target, prop){}

    public object value => property.GetValue(target);

}

}
