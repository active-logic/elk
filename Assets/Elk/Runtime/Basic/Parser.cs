using ArgEx = System.ArgumentException;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elk.Util;
using UnityEngine;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{

    Rule[] rules;

    public Parser(Rule[] rules) => this.rules = rules;

    public Parser() => rules = new Rule[]{
        new RuleSet( new OneLineFuncRule() ),
        new RuleSet( new InvocationRule() ),
        new RuleSet(".", "*", "/"),
        new RuleSet("*", "/", "%"),
        new RuleSet("+", "-"),
        new RuleSet("==", "!="),
        new RuleSet("|", "&"),
        new RuleSet("||", "&&"),
    };

    public object Parse(Sequence vector){
        for(int prec = 0; prec < rules.Length; prec++){
            Debug.Log($"Apply prec level {prec}");
            rules[prec].Process(vector);
            if(vector.Check()){
                Debug.Log("Reset prec");
                prec = -1;
            }
        }
        return vector.isSingleton
            ? vector[0]
            : throw new ArgEx(
                $"Irreducible (count: {vector.size})\n"
                + Format(vector)
            );
    }

    string Format(Sequence arg){
        var builder = new StringBuilder();
        for(int i = 0; i < arg.size; i++){
            builder.Append( "|| " + arg[i].ToString() + "\n" );
        }
        return builder.ToString();
    }

    public object this[params string[] tokens] => Parse(tokens);

}}
