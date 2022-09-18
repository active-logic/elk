using S = System.String;
using Elk.Util;
using Invocation = Elk.Basic.Graph.Invocation;

namespace Elk{

public class Interpreter<Cx>{

    public Sequence.Transformer typecaster;
    public Runner<Cx> runner;
    public string entry = "Main";
    protected Tokenizer tokenizer;
    protected Parser parser;

    public Interpreter(){}

    public Interpreter(Tokenizer t, Parser p, Runner<Cx> r){
        tokenizer = t; parser = p; runner = r;
    }

    public object this[S arg, Cx context]
    => runner.Eval(Parse(arg), context);

    public object Run(Cx context)
    => runner.Eval(new Invocation(entry), context);

    public object Parse(string arg){
        var tokens = new Sequence(tokenizer.Tokenize(arg));
        typecaster?.Transform(tokens);
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
    object Eval(object program, Cx context);
}

}