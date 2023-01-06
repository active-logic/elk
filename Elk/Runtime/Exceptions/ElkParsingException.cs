using System;

namespace Elk{
[Serializable] public class ElkParsingException : Exception{

    public ElkParsingException(){}

    public ElkParsingException(string msg)
    : base(msg){}

    public ElkParsingException(string msg, Exception inner)
    : base(msg, inner) {}

}}
