using System.Collections.Generic;
using System.Linq;

namespace Elk.Util{
public class Sequence{

    List<object> elements;
    public bool dirty{ get; private set; }

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

    public object this[int index] => elements[index];

    // -------------------------------------------------------------

    public bool Check()
    { var @out = dirty; dirty = false; return @out; }

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

    // NOTE - specifically use if you wish the getter to default vs
    // raising when out of bounds
    public object Get(int index)
    => (index > lastIndex || index < 0) ? null : this[index];

    public void Replace(int i, int count, object arg){
        elements.RemoveRange(i, count);
        elements.Insert(i, arg);
        dirty = true;
    }

}}
