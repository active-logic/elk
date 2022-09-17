using ArgEx = System.ArgumentException;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action = System.Action<object>;
using Elk.Util;
using FuncDef = Elk.Basic.Graph.FuncDef;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{

    Rule[] rules;
    public Action logFunc;

    public Parser(Rule[] rules) => this.rules = rules;

    public Parser() => rules = new Rule[]{
        new RuleSet( new FuncRule(), new FuncPrecursor() ),
        new RuleSet( new InvocationRule() ),
        new RuleSet("."),
        new RuleSet("*", "/", "%"),
        new RuleSet("+", "-"),
        new RuleSet("==", "!="),
        new RuleSet("|", "&"),
        new RuleSet("||", "&&"),
        new RuleSet( new TypedSeqRule<FuncDef>() )
    };

    public object Parse(Sequence vector){
        for(int prec = 0; prec < rules.Length; prec++){
            Log($"Apply prec level {prec}");
            rules[prec].Process(vector);
            if(vector.Check()){
                Log("Reset prec");
                prec = -1;
            }
        }
        return vector.isSingleton
            ? vector[0]
            : throw new ArgEx(
                $"Irreducible (count: {vector.size})\n"
                + vector.Format()
            );
    }

    public object this[params string[] tokens] => Parse(tokens);

    void Log(object arg) => logFunc?.Invoke(arg);

}}
