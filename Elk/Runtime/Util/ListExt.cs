using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elk.Util{
public static class ListExt{

    public static void Add(
        this List<Token> self, StringBuilder s, int line, int offset
    ){
        self.Add(new Token(s.ToString(), line, offset));
    }

    public static string[] ToStringArray(this IEnumerable<Token> self)
    => (from t in self select t.content.ToString()).ToArray();

    public static int LineNumber(this List<Token> self, int index)
    => self[index].line;

    public static int LineOffset(this List<Token> self, int index)
    => self[index].offset;

}}
