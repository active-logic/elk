using S = System.String;
using Elk.Util;
using Invocation = Elk.Basic.Graph.Invocation;

namespace Elk{

public class Interpreter<Cx>{

    public string entry = "Main";
    //
    protected Tokenizer            tokenizer;
    public    Sequence.Transformer typecaster;
    protected Parser               parser;
    protected Validator            validator;
    public    Runner<Cx>           runner;
    //
    public bool allowSubPrograms;

    // Constructors ------------------------------------------------

    public Interpreter(){}

    public Interpreter(Tokenizer t, Parser p, Runner<Cx> r){
        tokenizer = t; parser = p; runner = r;
    }

    // Props -------------------------------------------------------

    public object this[S arg, Cx context]
    => runner.Eval(Parse(arg), context);

    public object Run(Cx context)
    => runner.Eval(new Invocation(entry), context);

    public object Parse(string arg){
        var tokens = new Sequence(tokenizer.Tokenize(arg));
        typecaster?.Transform(tokens);
        var @out = parser.Parse(tokens);
        validator.Validate(@out, allowSubPrograms);
        return @out;
    }

}

public interface Tokenizer{
    Token[] Tokenize(string arg);
}

public interface Parser{
    object Parse(Sequence tokens);
}

public interface History{
    bool Did(string action);
    bool Did(string action, string since, bool strict);
}

public interface Validator{
    void Validate(object program, bool allowSubPrograms);
}

public interface Runner<Cx>{
    object Eval(object program, Cx context);
}

}
