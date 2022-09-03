using S = System.String;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elk{
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

    // NOTE if we were supporting parentheses, or maybe some other
    // case, every time didReplace is true we have to go back to
    // prec level 0, unless we just processed prec 0... or maybe
    // whenever parens has collapsed?
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

}}
