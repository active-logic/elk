using S = System.String;

namespace Elk{

public class Interpreter{

    Tokenizer tokenizer;
    Parser parser;
    Transform transform;
    Model model;

    public Interpreter(){
        tokenizer = new Tokenizer();
        parser = new Parser();
        transform = new Transform();
    }

    public object this[S arg]{ get{
        var tokens  = tokenizer[arg];
        var graph   = parser[tokens];
        var program = transform[graph, model];
        return program.Run();
    }}

}

// ----------------------------------------------------------------

    public class Tokenizer{ public S[] this[S arg] => null; }

    public class Parser{ public Node this[S[] tokens] => null; }

    public class Node{}

    public class Transform{
        public Program this[Node source, Model model] => new Program();
    }

    public class Model{}

    public class Program{ public object Run() => null; }

}
