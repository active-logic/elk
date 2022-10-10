using System.Collections.Generic;
using Elk.Util;

namespace Elk{

public interface Tokenizer{
    Token[] Tokenize(string arg);
}

public interface Parser{

    // Parse the specified sequence; accumulate info in 'debug'
    // (leave null if no debug info is needed)
    object Parse(Sequence tokens, List<string> debug);

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

// For deferred/client side tracking and recording
public interface Stack{
    void Commit(object result);
    ulong Id();
}

}
