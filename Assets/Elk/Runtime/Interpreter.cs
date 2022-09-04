using S = System.String;
using Elk.Util;

namespace Elk{

public class Interpreter{

    Tokenizer tokenizer;
    Parser parser;
    Runner runner;

    public Interpreter(){
        tokenizer = new Elk.Basic.Tokenizer();
        parser = new Elk.Basic.Parser();
        runner = new Elk.Basic.Runner();
    }

    public object this[S arg]{ get{
        var tokens  = tokenizer.Tokenize(arg);
        var graph   = parser.Parse(tokens);
        return runner.Run(graph);
    }}

}

public interface Tokenizer{
    string[] Tokenize(string arg);
}

public interface Parser{
    object Parse(Sequence tokens);
}

public interface Runner{
    object Run(object program);
}

}
