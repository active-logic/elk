using S = System.String;

namespace Elk{
public class BinaryOp : Node{

    object arg0, arg1, op;

    public BinaryOp(object arg0, object op, object arg1){
        this.arg0 = arg0;
        this.arg1 = arg1;
        this.op   = op;
    }

    override public S ToString()
    => $"({arg0}{op}{arg1})";

}}
