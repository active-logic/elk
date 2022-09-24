using System.Collections.Generic;
using System.Text;

namespace Elk.Basic{
public class Tokenizer : Elk.Tokenizer{

    public char decimalDot = '.';
    public string doubleSymbols = "+-&|=";

    public string[] Tokenize(string arg){
        StringBuilder word = new StringBuilder();
        List<string> tokens = new List<string>();
        foreach(char c in arg){
            if(char.IsWhiteSpace(c) || char.IsControl(c)){
                if(word.Length > 0){
                    tokens.Add(word.ToString());
                    word = new StringBuilder();
                }
            }else if (char.IsLetterOrDigit(c)){
                if(word.Length > 0 && IsBreaking(word[word.Length-1])){
                    tokens.Add(word.ToString());
                    word = new StringBuilder();
                }
                word.Append(c);
            }else{
                // TODO in general I think one operator followed by another
                // should be an error, however may want to support +=, -=, ...
                if(!(Double(c, word) || IsDecimalDot(c, word) || IsArrowHead(c, word))){
                    tokens.Add(word.ToString());
                    word = new StringBuilder();
                }
                word.Append(c);
            }
        }
        var lastWord = word.ToString();
        if(lastWord.Length > 0) tokens.Add(lastWord);
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
