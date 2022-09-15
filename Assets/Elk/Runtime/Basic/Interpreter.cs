namespace Elk.Basic{
public class Interpreter : Interpreter<Context>{

    public Interpreter(){
        tokenizer = new Elk.Basic.Tokenizer();
        parser = new Elk.Basic.Parser();
        runner = new Elk.Basic.Runner();
    }

}}
