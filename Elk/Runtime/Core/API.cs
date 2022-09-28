using Elk.Util;

namespace Elk{

public interface Tokenizer{
    Token[] Tokenize(string arg);
}

public interface Parser{
    object Parse(Sequence tokens);
}

public interface Validator{
    void Validate(object program);
}

public interface Runner<Cx>{

    // Run a function (entry point) against a context
    object Invoke(string func, Cx cx);

    // Run the specified program (function body/expression)
    object Eval(object program, Cx context);

}

}
