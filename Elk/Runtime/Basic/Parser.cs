using ArgEx = System.ArgumentException;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action = System.Action<object>;
using Elk.Util;
using FuncDef = Elk.Basic.Graph.FuncDef;
using static Elk.Basic.Parser.RuleSet;

namespace Elk.Basic{
// TODO separate parser vs default parser
public partial class Parser : Elk.Parser{

    public static bool showErrorDetails = true;
    Rule[] rules;
    public Action log;

    public Parser(Rule[] rules) => this.rules = rules;

    public Parser(string funcPreamble) => rules = new Rule[]{
        Rst( new IncludeRule() ),
        Rst( new FuncRule(), new FuncPrecursor(funcPreamble)),
        Rst( new RecallRule() ),
        Rst( new AccessRule(), new InvocationRule()),
        Una("! ~ ++ -- + -"),
        Bin("* / %"),
        Bin("+ -"),
        Bin("== !="),
        Bin("| &"),
        Bin("|| &&"),
        Rst( new ParensRule() ),
        Rst( new ModuleRule() )
    };

    public object Parse(Sequence vector){
        vector.log = this.log;
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
            : throw new ParsingException(
                $"{ErrorInfo(vector)}{ErrorDetails(vector)}"
            );
    }

    public string ErrorInfo(Sequence vector){
        for(var i = 0; i < vector.size; i++){
            var e = vector[i];
            if(e is FuncDef) continue;
            if(e is FuncDef[]) continue;
            return $"error at line {vector.LineNumber(i)}\n...";
        }
        return "Unknown error";
    }

    public string ErrorDetails(Sequence vector){
        if(!showErrorDetails) return null;
        return "\n" + vector.XFormat();
    }

    public object this[params string[] tokens] => Parse(tokens);

    void Log(object arg) => log?.Invoke(arg);

}}
