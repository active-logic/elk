using System.Collections.Generic;
using System.Text;
using Elk.Util;

namespace Elk.Basic{
public class Tokenizer : Elk.Tokenizer{

    public char decimalDot = '.';
    public string doubleSymbols = "+-&|=:";
    CommentParser commentParser = new CommentParser();

    public Token[] Tokenize(string arg){
        StringBuilder word = new StringBuilder();
        List<Token> tokens = new List<Token>();
        var xchars = commentParser.Parse(arg);
        // NOTE - when a word is formed its line number is the same
        // as the next character's.
        int line = 1;
        foreach(xchar c in xchars){
            line = c.line;
            if(char.IsWhiteSpace(c) || char.IsControl(c)){
                if(word.Length > 0){
                    tokens.Add(word, line);
                    word = new StringBuilder();
                }
            }else if (char.IsLetterOrDigit(c)){
                if(word.Length > 0 && IsBreaking(word[word.Length-1])){
                    tokens.Add(word, line);
                    word = new StringBuilder();
                }
                word.Append(c);
            }else{
                // TODO sometimes one operator followed by another
                // is an error, however... +=, -=, ...
                if(!(Double(c, word) || IsDecimalDot(c, word)
                                     || IsArrowHead(c, word))){
                    tokens.Add(word, line);
                    word = new StringBuilder();
                }
                word.Append(c);
            }
        }
        if(word.Length > 0) tokens.Add(word, line);
        return tokens.ToArray();
    }

    bool IsBreaking(char c) => !char.IsLetterOrDigit(c);

    // TODO generalize through list of two-char operators
    bool IsArrowHead(char c, StringBuilder word)
        => word.Length == 1
        && word[0]     == '='
        && c           == '>';

    bool Double(char c, StringBuilder word)
        => word.Length == 1
        && word[0] == c
        && doubleSymbols.Contains(c);

    // TODO allows something like 15.156.12
    bool IsDecimalDot(char c, StringBuilder word)
        => word.Length == 0
        || (c == decimalDot && char.IsDigit(word[word.Length-1]));

}}
