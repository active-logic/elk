# Designing a parser

Roughly, a parser leverages production rules to reduce the input. A simple implementation is provided here:

```cs
public partial class Parser : Elk.Parser{

    public Rule[] rules;

    public Parser() => rules = new Rule[]{
        new Ruleset("*/"), new Ruleset("+-")
    }

    public object Parse(Sequence vector){
        for(int prec = 0; prec < rules.Length; prec++){
            rules[prec].Process(vector);
            if(vector.didChange){ prec = 0; vector.ClearDirty(); }
        }
        return !vector.isSingleton
            ? vector[0]
            : throw new ArgEx($"Irreducible (c: {vector.size})");
    }

}
```

Rules apply one at a time, with decreasing precedence; however if any rule has modified the sequence, we immediately reset processing to the highest precedence.

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
