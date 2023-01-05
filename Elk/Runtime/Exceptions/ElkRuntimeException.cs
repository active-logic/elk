using System;

namespace Elk{
[Serializable] public class ElkRuntimeException : Exception{

    public ElkRuntimeException(){}

    public ElkRuntimeException(string msg)
    : base(msg){}

    public ElkRuntimeException(string msg, Exception inner)
    : base(msg, inner) {}

}}
