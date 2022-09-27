using System.Collections.Generic;
using System.Linq;
using Elk.Util;

namespace Elk.Basic{
public class CommentParser{

    const char n = '\n';
    char κ = '#';

    public CommentParser(){}

    public CommentParser(char commentChar) => κ = commentChar;

    public IEnumerable<xchar> Parse(string arg){
        var characters = xchar.ToXChar(arg);
        bool mute = false;
        return from c in characters where !Mute(c, ref mute) select c;
    }

    bool Mute(char c, ref bool mute)
    => c == κ ? (mute = true ) :
       c == n ? (mute = false) : mute;

}}
