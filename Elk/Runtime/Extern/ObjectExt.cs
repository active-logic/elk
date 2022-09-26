using System.Reflection;

namespace Elk.Bindings.CSharp{
public static class ObjectExt{

    public static MethodInfo Bind(
        this object self, string func, object[] args
    ) => self.GetType().GetMethod(func, args);

    public static bool Invoke(
        this object self, string func, object[] args, out object @out
    ){
        var method = self.GetType().GetMethod(func, args);
        @out = method?.Invoke(self, args);
        return (method != null);
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

}}
