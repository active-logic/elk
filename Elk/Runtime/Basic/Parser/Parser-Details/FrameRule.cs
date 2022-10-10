using System;
using Ex = System.Exception;
using Elk.Util;
using Elk.Basic.Graph;

namespace Elk.Basic{
public partial class Parser : Elk.Parser{
public class Frame<T, Q> : LocalRule where T : class{

    readonly string left, right;
    readonly Func<T, Q> generator;

    public Frame(string left, string right, Func<T, Q> generator){
        this.left = left;
        this.right = right;
        this.generator = generator;
    }

    override public void Process(Sequence vec, int i){
        var a = vec.StringValue(i);
        var b = vec.StringValue(i+2);
        if(a != left || b != right) return;
        var arg = vec.Get<T>(i + 1);
        if(arg == null) return;
        vec.Replace(i, 3, generator(arg), this);
    }

    override public string ToString()
    => throw new Ex("ToString() must be overriden in Frame<T>");

}}}
