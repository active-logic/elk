namespace Elk.Util{
public abstract class Pattern{

    Sequence sequence;
    int firstIndex;
    int index;

    public abstract bool Eval();
    public abstract object Instantiate();

    protected bool Match(string arg) => false;
    protected bool Read(string @var) => false;
    protected bool Read(string prefix, string @var) => false;
    protected bool Read<T>(string @var) => false;

    int Match(Sequence s, int index){
        this.sequence = s;
        this.index = firstIndex = index;
        bool didMatch = Eval();
        if(didMatch){
            var node = Instantiate();
            return index - firstIndex;
        }else{
            return 0;
        }
    }

}}
