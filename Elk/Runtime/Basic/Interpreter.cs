namespace Elk.Basic{
public class Interpreter : Interpreter<Context>{

    public Interpreter(string funcKeyword){
        reader = new Reader(){
            tokenizer  = new Tokenizer(),
            typecaster = new TypeCaster(),
            parser     = new Parser(funcKeyword),
            validator  = new Validator()
        };
        runner     = new Runner();
    }

}}
