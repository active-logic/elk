namespace Elk{
public class Token{

    public int line;
    public object content;

    public Token(object content, int line){
        this.content = content;
        this.line    = line;
    }

    public static explicit operator string(Token self)
    => (string)self.content;

}}
