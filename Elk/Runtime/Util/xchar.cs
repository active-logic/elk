using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elk.Util{
public readonly struct xchar{

    readonly char character;
    public readonly int line;
    public readonly int offset;

    public xchar(char c, int line, int offset){
        this.character = c;
        this.line = line;
        this.offset = offset;
    }

    public static implicit operator char(xchar self)
    => self.character;

    public static IEnumerable<xchar> ToXChar(string arg){
        int line = 1, offset = 1;
        return from c in arg
               select CreateXChar(c, ref line, ref offset);
    }

    static xchar CreateXChar(char c, ref int line, ref int offset){
        var @out = new xchar(c, line, offset);
        if(c.IsNewLine()) { line++; offset = 1; }
        else              { offset++; }
        return @out;
    }

}}
