using S = System.String;
using System.Collections.Generic;
using System.Linq;

namespace Elk{
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

}}
