using System.Collections.Generic;
using Elk.Util;

namespace Elk{
public class Interpreter<Cx>{

    public string entry = "Main";
    //
    public Reader reader;
    public Runner<Cx> runner;

    public Interpreter(){}

    public object this[string arg, Cx context]
    => runner.Eval(Parse(arg, debug: null), context);

    public object Run(Cx context)
    => runner.Invoke(entry, context);

    public object Parse(string arg, List<string> debug)
    => reader.Parse(arg, debug);

}}
