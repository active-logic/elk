using S = System.String;
using Elk.Util;

namespace Elk{

public class Interpreter<Cx>{

    protected Tokenizer tokenizer;
    protected Parser parser;
    public Runner<Cx> runner;

    public Interpreter(){}

    public Interpreter(Tokenizer t, Parser p, Runner<Cx> r){
        tokenizer = t; parser = p; runner = r;
    }

    public object this[S arg, Cx context]
    => runner.Run(Parse(arg), context);

    public object Parse(string arg){
        var tokens = tokenizer.Tokenize(arg);
        return parser.Parse(tokens);
    }

}

public interface Tokenizer{
    string[] Tokenize(string arg);
}

public interface Parser{
    object Parse(Sequence tokens);
}

public interface Runner<Cx>{
    object Run(object program, Cx context);
}

}
