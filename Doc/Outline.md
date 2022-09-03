# Implementing a simple interpreter

For a language we need the following (1) a tokenizer, (2) a parser, (3) a model and (4) a graph transform.
Therefore, here is the code for a simple interpreter:

```cs
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

    public object this[S arg]{ get{  // s = String
        var tokens  = tokenizer[arg];
        var graph   = parser[tokens];
        var program = transform[graph, model];
        return program.Run();
    }}

}
```

For the most part, the types are implied. We start with a string (probably!) and output an object

## Simple tokenizer

The most simple tokenizer we can come up with is probably one which uses whitespace as delimiters, here's one:

```cs
public class Tokenizer0{

    public S[] this[S arg]
    => (from x in arg.Split() where x.Length > 0 select x).ToArray();

}
```

We quickly confirm that unusual whitespaces (new line, carriage return) are correctly handled (being "white space")

## Simple parser

Once we have tokenized the input, an actual parser will transform the list of tokens into a tree-like structure. Let's write a simple parser. As an example let's write a parser that's going to generate branches, assuming only the '+' and '-' binary operators

```cs
public class Parser0{

    public string[] Operators;

    public Parser0() => Operators = new string[]{"+", "-"};

    public Node this[params S[] args]{ get{
        var E = ( from e in args select (object)e ).ToList();
        for(var i = 1; i < E.Count - 1; i++){
            foreach(var op in Operators){
                if(E[i] as S != op) continue;
                var node = new BinaryOp(E[i-1], E[i], E[i+1]);
                E.RemoveRange(i - 1, 3);
                E.Insert(i - 1, node);
                i--;
            }
        }
        return E[0] as Node;
    }}

}
```

Now let's add support for the * and / operators; this needs a little more effort:

```cs
public class Parser0{

    public string[][] Operators;

    public Parser0() => Operators = new string[][]{
        new string[]{"*", "/"},
        new string[]{"+", "-"}
    };

    public Node this[params S[] args]{ get{
        var E = ( from e in args select (object)e ).ToList();
        return Parse(E);
    }}

    public Node Parse(List<object> args){
        for(int i = 0; i < Operators.Length; i++){
            Parse(args, Operators[i], out bool didReplace);
        }
        return args[0] as Node;
    }

    public void Parse(List<object> args, string[] ops,
                      out bool didReplace){
        didReplace = false;
        for(var i = 1; i < args.Count - 1; i++){
            foreach(var op in ops){
                if(args[i] as S != op) continue;
                var node = new BinaryOp(args[i-1], args[i], args[i+1]);
                args.RemoveRange(i - 1, 3);
                args.Insert(i - 1, node);
                i--;
                didReplace = true;
            }
        }
    }

}
```

.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
.
