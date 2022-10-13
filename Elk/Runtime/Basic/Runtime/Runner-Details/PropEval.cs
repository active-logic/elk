using Ex = System.Exception;
using Elk.Util;
using Elk.Basic.Graph;
using Elk.Bindings.CSharp;

namespace Elk.Basic.Runtime{
public class PropEval{

    public object Eval(Identifier prop, Context cx){
        if(prop.binding == null)
            prop.binding = cx.BindProperty(prop, cx);
        var binding = prop.binding as PropertyBinding;
        return binding != null
             ? binding.value
             : throw new Ex($"`{prop.value}` not found");
    }

}}
