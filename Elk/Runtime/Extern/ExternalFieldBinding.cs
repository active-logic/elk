using System.Reflection;
using Elk.Basic.Runtime;
using UnityEngine;

namespace Elk.Bindings.CSharp{
public class ExternalFieldBinding : CsPropBinding<FieldInfo>,
                                    PropertyBinding{

    public ExternalFieldBinding(object target, FieldInfo prop)
    : base(target, prop){}

    // NOTE: explicit nulls possible after hot reloading
    public object value{ get{
        var val = property.GetValue(target);
        return val == null || val.Equals(null) ? null : val;
    }}

    public string name => property.Name;

}}
