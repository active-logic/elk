using Elk.Util;
using System.Linq;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class RuleSet : Rule{

    LocalRule[] rules;

    public RuleSet(params LocalRule[] rules) => this.rules = rules;

    public RuleSet(string operators) => rules = (
        from op in operators select (LocalRule) new BinaryRule(op)
    ).ToArray();

    override public void Process(Sequence vector){
        for(int i = 0; i < vector.size; i++){
            foreach(var rule in rules){
                rule.Process(vector, i);
                if(vector.didChange) return;
            }
        }
    }

}}}
