using System;

namespace Elk{
[Serializable] public class ElkRuntimeError : Exception{

    public ElkRuntimeError(){}

    public ElkRuntimeError(string msg)
    : base(msg){}

    public ElkRuntimeError(string msg, Exception inner)
    : base(msg, inner) {}

}}
