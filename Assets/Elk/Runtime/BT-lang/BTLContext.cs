using Elk.Basic;
using FuncDef = Elk.Basic.Graph.FuncDef;

namespace Activ.BTL{
public static class BTLContext{

    public static Context Create(object program,
                                 params object[] externals)
    => new Context(){
        modules = new FuncDef[][]{ (FuncDef[])program },
        externals = externals
    };

}}
