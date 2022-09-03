using S = System.String;
using System.Collections.Generic;
using System.Linq;

namespace Elk{
public class Parser0{

    public Node this[params S[] args]{ get{
        var E = ( from e in args select (object)e ).ToList();
        for(int i = 1; i < E.Count - 1; i++){
            if(E[i] as string != "+") continue;
            var op = new BinaryOp(
                E[i-1], E[i], E[i+1]
            );
            E.RemoveRange(i - 1, 3);
            E.Insert(i - 1, op);
            i--;
        }
        return E[0] as Node;
    }}

}}
