using Elk.Util;
using System.Collections.Generic;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
// NOTE used to express containers, such as types or modules
// for example, in a module all elements are funcs
public class TypedSeqRule<T> : LocalRule where T : class{

    const int DefaultCapacity = 5;

    // NOTE inefficient could just size the array and fill it vs
    // using a temp list
    override public void Process(Sequence vec, int i0){
        List<T> @out = null; int i;
        for(i = i0; i < vec.size; i++){
            var obj = vec.Get<T>(i);
            if(obj == null) break;
            if(@out == null) @out = new List<T>(DefaultCapacity);
            @out.Add(obj);
        }
        if(@out == null) return;
        vec.Replace(i0, i - i0, @out.ToArray(), this);
    }

    override public string ToString() => $"TypedSeqRule";

}}}
