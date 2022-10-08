using System.Collections.Generic;
using System.Linq;
// TODO unwanted dep
using FuncDef = Elk.Basic.Graph.FuncDef;
using System.Text;
using Ex = System.Exception;

namespace Elk.Util{
public class Sequence{

    List<Token> elements;
    public bool dirty{ get; private set; }
    public System.Action<object> log;

    public Sequence(params Token[] args)
    => elements = args.ToList();

    // TODO minimally offset should increase
    public Sequence(params object[] args)
    => elements = (from x in args select new Token(x, 0, 0)).ToList();

    public Sequence(params string[] args)
    => elements = (from x in args select new Token(x, 0, 0)).ToList();

    public static implicit operator Sequence(string[] args)
    => new Sequence(args);

    // -------------------------------------------------------------

    public bool didChange => dirty;

    public bool isSingleton => size == 1;

    public int lastIndex => size - 1;

    public int size => elements.Count;

    public object this[int index]{
        get => elements[index].content;
        set{
            if(value is Token) throw new Ex("Pass content not token");
            elements[index].content = value;
        }
    }

    // -------------------------------------------------------------

    public bool Check()
    { var @out = dirty; dirty = false; return @out; }

    // -------------------------------------------------------------
    // NOTE: getters help parsing as they fail gracefully on bounds
    // and type constraining
    // -------------------------------------------------------------

    public char? AsChar(int index){
        var str = AsString(index);
        if(str == null || str.Length != 1) return null;
        return str[0];
    }

    public string AsString(int index) => Get(index) as string;

    public string AsWord(int index, out float id){
        id = Id(index);
        var str = AsString(index);
        if(str == null) return null;
        return char.IsLetter(str[0]) ? str : null;
    }

    public string AsWord(int index){
        var str = AsString(index);
        if(str == null) return null;
        return char.IsLetter(str[0]) ? str : null;
    }

    public float Id(int index){
        switch(Get(index)){
            case Token token: return token.id;
            default: return -1f;
        }
    }

    public object Get(int index)
    => (index > lastIndex || index < 0) ? null
       : this[index];

    public T Get<T>(int index) where T : class
    => (index > lastIndex || index < 0) ? null
       : this[index] as T;

    public int LineNumber(int index) => elements.LineNumber(index);

    public int LineOffset(int index) => elements.LineOffset(index);

    public T[] ReadSeveral<T>(ref int index) where T : class{
        List<T> @out = null;
        for(; index < this.size; index++){
            var elem = this.Get<T>(index);
            if(elem == null) break;
            if(@out == null) @out = new List<T>(8);
            @out.Add(elem);
        }
        return @out?.ToArray();
    }

    // -------------------------------------------------------------

    // NOTE last arg here only for logging purposes
    public void Replace(int i, int count, object arg, object src){
        log?.Invoke($"Replace [{count}] tokens at index {i} via {src}");
        var lineNum    = LineNumber(i);
        var lineOffset = LineOffset(i);
        elements.RemoveRange(i, count);
        elements.Insert(i, new Token(arg, lineNum, lineOffset));
        dirty = true;
    }

    public string Format(){
        var builder = new StringBuilder();
        for(int i = 0; i < size; i++)
            builder.Append(ElemToString(this[i]));
        return builder.ToString();
    }

    public string XFormat(){
        var builder = new StringBuilder();
        for(int i = 0; i < size; i++){
            var e = this[i];
            switch(e){
            case FuncDef[] functions:
                foreach(var f in functions){
                    builder.Append( $"|| {f.ToString()}\n");
                }
                break;
            default:
                builder.Append(ElemToString(e));
                break;
            }
        }
        return builder.ToString();
    }

    string ElemToString(object arg)
    => $"|| {arg.ToString()} ({arg.GetType().Name})\n";

    // =============================================================

    public interface Transformer{
        void Transform(Sequence vec);
    }

}}
