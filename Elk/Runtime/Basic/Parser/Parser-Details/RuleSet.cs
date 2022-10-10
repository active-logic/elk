using System.Collections.Generic;
using System.Linq;
using Action = System.Action<object>;
using Elk.Util;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class RuleSet : Rule{

    LocalRule[] rules;

    public RuleSet(params LocalRule[] rules) => this.rules = rules;

    public RuleSet(IEnumerable<LocalRule> rules) => this.rules = (
        from arg in rules select (LocalRule)arg
    ).ToArray();

    override public void Process(Sequence vector, List<string> debug){
        for(int i = 0; i < vector.size; i++){
            foreach(var rule in rules){
                rule.Process(vector, i);
                if(vector.didChange){
                    debug?.Add(rule.GetType().Name + " :: " + vector.lastInsert);
                    return;
                }
            }
        }
    }

    public static RuleSet Rst(params object[] args) => new RuleSet(
        from arg in args select MakeRule(arg)
    );

    public static RuleSet Una(string operators) => new RuleSet(
        from arg in operators.Split() select new UnaryRule(arg)
    );

    public static RuleSet PostUna(string operators) => new RuleSet(
        from arg in operators.Split() select new PostfixUnaryRule(arg)
    );

    public static RuleSet Bin(string operators) => new RuleSet(
        from arg in operators.Split() select new BinaryRule(arg)
    );

    public static LocalRule MakeRule(object arg){
        switch(arg){
            case LocalRule rule:
                return rule;
            case string op when op.EndsWith("u"):
                return new UnaryRule(op.Substring(op.Length-1));
            case string op when op.StartsWith("u"):
                return new PostfixUnaryRule(op.Substring(1, op.Length-1));
            default:
                return new BinaryRule((string)arg);
        }
    }

}}}
