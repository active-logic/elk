using System.Reflection;
using Elk.Basic.Runtime;
using System.Linq;

namespace Elk.Bindings.CSharp{
public static class ObjectBindings{

    public static MethodInfo Bind(
        this object self, string func, object[] args, bool debug
    ) => self.GetType().LookupMethod(func, args, debug);

    public static PropertyBinding Bind(
        this object self, string label
    ){
        var type = self.GetType();
        var field = type.GetField(label);
        if(field != null){
            return new ExternalFieldBinding(self, field);
        }
        var prop = type.GetProperty(label);
        if(prop != null){
            return new ExternalPropertyBinding(self, prop);
        }
        return null;
    }

    public static bool Eval(
        this object self, string label, out object @out
    ){
        var type = self.GetType();
        var field = type.GetField(label);
        if(field != null){
            @out = field.GetValue(self);
            return true;
        }
        var prop = type.GetProperty(label);
        if(prop != null){
            @out = prop.GetValue(self);
            return true;
        }
        @out = null;
        return false;
    }

    public static bool Invoke(
        this object self, string func, object[] args, out object @out
    ){
        var method = self.GetType().LookupMethod(func, args, debug: false);
        @out = method?.Invoke(self, args);
        return (method != null);
    }

    public static ExternalFieldBinding[] LookupFields<T>(this object self){
        var type = self.GetType();
        var fields = type.GetFields();
        var @out = from field in fields
            where field.FieldType.IsAssignableFrom(typeof(T))
            select new ExternalFieldBinding(self, field);
        return @out.ToArray();
    }

}}
