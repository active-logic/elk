using Ex = System.Exception;
using Elk.Util;
using Elk.Basic.Graph;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Runtime{
public class PropEval{

    virtual public object Eval(Identifier prop, Context cx){
        if(prop.binding == null)
            prop.binding = cx.BindProperty(prop, cx);
        var binding = prop.binding as PropertyBinding;
        var @out = binding != null
             ? binding.value
             : throw new Ex($"`{prop.value}` not found");
        cx.StackPushPopProp(prop.value, @out);
        return @out;
    }

}}
