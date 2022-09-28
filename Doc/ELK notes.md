# ELK notes

## Adding a construct

In general you can achieve new things in Elk without implementing constructs from scratch. For example you can use `BinaryRule` to implement a new binary operator. Still, in these notes I walk through the whole process, from designing a new parsing rule to fitting the interpreter with an evaluator (a function that knows how to interpret a bit of program).

As an example let's have a look at the work needed to add the strong import construct.

### (1) Parsing test

In general adding a test first is a good idea; head over to `ParserTest` and add the test.

```cs
[Test] public void Test_With() => Assert.AreEqual(
    // formatted graph node (output)
    "(with encounters;)",
    // manually tokenized input
    p["with", "encounters", ";"]?.ToString()
);
```

### (2) Rule test

This is more definite than a parser test:

```cs
[Test] public void Test_StrongImport(){
    Assert.Fail();
    var seq = new Sequence("with", "encounters", ";");
    rule.Process(seq, 0);
    Assert.AreEqual(1, seq.size);
    Assert.That(seq[0] is StrongImport);
}
```

### (3) Stub the rule and resulting graph node

Rule stub
```cs
using Elk.Util; using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class StrongImportRule : LocalRule{

    override public void Process(Sequence vec, int i){
        // stub
    }

    override public string ToString() => $"StrongImportRule";

}}}

```

Graph node; often little more than a typed data holder
```cs
namespace Elk.Basic.Graph{
public class StrongImport{

    public readonly string module;

    public StrongImport(string module) => this.module = module;

    override public string ToString() => $"(with {module};)";

}}
```

### (4) Implement the rule

Local rules are provided a sequence with an index; the rule is used to find a match in the sequence at the specified index; if the rule matches, used up nodes are replaced with a new branch in the parse tree. `Sequence` is really just a list of nodes with convenience accessors.

As an example, `AsString(index)` checks for a string object at the specified index. If the entry at 'index' is not a string (or overflow) null is returned.

```cs
public class StrongImportRule : LocalRule{

    override public void Process(Sequence vec, int i){
        var i0 = i;
        if(vec.AsString(i++) != "with") return;
        var arg = vec.AsString(i++);
        if(arg == null) return;
        if(vec.AsString(i++) != ";") return;
        vec.Replace(i0, i - i0, new StrongImport(arg), this);
    }

    override public string ToString() => $"StrongImportRule";

}
```

After implementing the rule, if correct the rule test will pass. The parser test will pass if the rule is added to the default parser (`Parser.cs`).

```cs
public Parser(string funcPreamble) => rules = new Rule[]{
    Rst( new StrongImportRule() ),
    // more rules
    // ...
    Una("! ~ ++ -- + -"),
    Bin("|| &&"),
    Rst( new TypedSeqRule<FuncDef>() )
};
```

Parsing rules are grouped; within a group all rules have the same
precedence (leftmost rules in a group still evaluate first!)
The `Rst`, `Una` and `Bin` forms are shorthands to concisely add rules. For example "Una" expands to a rule group including only unary operators.

The final test is probably just modifying a source file and trying things with out; with the strong import rule, originally this didn't pan out:

```
#!btl
with encounters;  # error at line 2

task Step()
=> Encounter(player) || Support(player) || Roam();
```

Elk error reporting is not smart; the only thing it does understand is unification errors. In the above case, parsing is successful - short of the parser knowing what to do with a `{StrongImport, Funcdef}` sequence. Since parsing does not end with exactly one node at the root, parsing has failed.

Ordinarily the solution is to represent the new construct via an interface. For example "Invocation" and "BinaryExp" both implement "Expression"; the consequence is that parsing allows expression trees involving invocations and binary expressions.

For strong import, however, this ends up modifying the structure of elk programs. I solved this by introducing the "Module" node:



### (6) Implement an evaluator
