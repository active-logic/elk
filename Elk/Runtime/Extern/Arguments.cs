using System; using System.Linq;

namespace Elk.Bindings.CSharp{
// Array exts for getting implied parameter types from arguments
public static class Arguments{

    public static Type[] ParameterTypes(
        this object[] args, bool nullIsObj
    ) => (
        from x in args select x?.GetType()
        ?? (nullIsObj ? typeof(object) : null)
    ).ToArray();

}}
