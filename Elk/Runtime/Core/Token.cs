namespace Elk{
public class Token{

    public int line;
    public int offset;
    public float id;
    public object content;

    public Token(object content, int line, int offset){
        this.content = content;
        this.line    = line;
        this.offset  = offset;
        this.id      = float.Parse($"{line}.{offset}");
    }

    public static explicit operator string(Token self)
    => (string)self.content;

}}
