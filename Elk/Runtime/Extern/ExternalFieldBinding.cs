using System.Reflection;
using Elk.Basic.Runtime;

namespace Elk.Bindings.CSharp{
public class ExternalFieldBinding : CsPropBinding<FieldInfo>,
                                    PropertyBinding{

    public ExternalFieldBinding(object target, FieldInfo prop)
    : base(target, prop){}

    public object value => property.GetValue(target);
    public string name => property.Name;

}}
