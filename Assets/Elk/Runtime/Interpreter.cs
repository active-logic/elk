using S = System.String;
using Elk.Util;

namespace Elk{

public class Interpreter{

    Tokenizer tokenizer;
    Parser parser;
    public Runner runner;

    public Interpreter(Tokenizer t, Parser p, Runner r){
        tokenizer = t; parser = p; runner = r;
    }

    public Interpreter(){
        tokenizer = new Elk.Basic.Tokenizer();
        parser = new Elk.Basic.Parser();
        runner = new Elk.Basic.Runner();
    }

    public object this[S arg] => runner.Run(Parse(arg), null);

    public object Parse(string arg){
        var tokens  = tokenizer.Tokenize(arg);
        return parser.Parse(tokens);
    }

}

public interface Tokenizer{
    string[] Tokenize(string arg);
}

public interface Parser{
    object Parse(Sequence tokens);
}

public interface Runner{
    object Run(object program, object context);
}

}
