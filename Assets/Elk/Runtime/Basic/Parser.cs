using ArgEx = System.ArgumentException;
using System.Collections.Generic;
using System.Linq;
using Elk.Util;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{

    Rule[] rules;

    public Parser(Rule[] rules) => this.rules = rules;

    public Parser() => rules = new Rule[]{
        new RuleSet("*/"),
        new RuleSet("+-"),
        new RuleSet("|&"),
        new RuleSet( new OneLineFuncRule() ),
        new RuleSet( new InvocationRule() ),
    };

    public object Parse(Sequence vector){
        for(int prec = 0; prec < rules.Length; prec++){
            rules[prec].Process(vector);
            if(vector.Check()) prec = 0;
        }
        return vector.isSingleton
            ? vector[0]
            : throw new ArgEx($"Irreducible (c: {vector.size})");
    }

    public object this[params string[] tokens] => Parse(tokens);

}}
