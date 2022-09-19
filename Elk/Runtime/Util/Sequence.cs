using System.Collections.Generic;
using System.Linq;

namespace Elk.Util{
public class Sequence{

    List<object> elements;
    public bool dirty{ get; private set; }
    public System.Action<object> log;

    public Sequence(params string[] args){
        elements = (from x in args select (object)x).ToList();
    }

    public static implicit operator Sequence(string[] args)
    => new Sequence(args);

    // -------------------------------------------------------------

    public bool didChange => dirty;

    public bool isSingleton => size == 1;

    public int lastIndex => size - 1;

    public int size => elements.Count;

    public object this[int index]{
        get => elements[index];
        set => elements[index] = value;
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

    public string AsWord(int index){
        var str = AsString(index);
        if(str == null) return null;
        return char.IsLetter(str[0]) ? str : null;
    }

    public object Get(int index)
    => (index > lastIndex || index < 0) ? null : this[index];

    public T Get<T>(int index) where T : class
    => (index > lastIndex || index < 0) ? null : this[index] as T;

    // -------------------------------------------------------------

    // NOTE last arg here only for logging purposes
    public void Replace(int i, int count, object arg, object src){
        log?.Invoke($"Replace [{count}] tokens at index {i} via {src}");
        elements.RemoveRange(i, count);
        elements.Insert(i, arg);
        dirty = true;
    }

    public string Format(){
        var builder = new System.Text.StringBuilder();
        for(int i = 0; i < size; i++){
            builder.Append( $"|| {this[i].ToString()} ({this[i].GetType().Name})\n");
        }
        return builder.ToString();
    }

    // =============================================================

    public interface Transformer{
        void Transform(Sequence vec);
    }

}}
