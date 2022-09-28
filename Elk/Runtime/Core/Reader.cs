using Elk.Util;

namespace Elk{
public class Reader{

    public Tokenizer            tokenizer;
    public Sequence.Transformer typecaster;
    public Parser               parser;
    public Validator            validator;

    public Reader(){}

    public Reader(Tokenizer t, Parser p){
        tokenizer = t; parser = p;
    }

    public object Parse(string arg){
        var tokens = new Sequence(tokenizer.Tokenize(arg));
        typecaster?.Transform(tokens);
        var @out = parser.Parse(tokens);
        validator?.Validate(@out);
        return @out;
    }

}}
