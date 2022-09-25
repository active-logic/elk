using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elk.Util{
public static class ListExt{

    public static void Add(
        this List<Token> self, StringBuilder s, int line
    ){
        self.Add(new Token(s.ToString(), line));
    }

    public static string[] ToStringArray(this IEnumerable<Token> self)
    => (from t in self select t.content.ToString()).ToArray();

    public static int Line(this List<Token> self, int index)
    => self[index].line;

}}
