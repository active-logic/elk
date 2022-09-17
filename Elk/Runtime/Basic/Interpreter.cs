namespace Elk.Basic{
public class Interpreter : Interpreter<Context>{

    public Interpreter(string funcKeyword){
        tokenizer = new Elk.Basic.Tokenizer();
        parser = new Elk.Basic.Parser(funcKeyword);
        runner = new Elk.Basic.Runner();
        typecaster = new TypeCaster();
    }

}}
