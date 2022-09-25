using System;

namespace Elk{
[Serializable] public class ParsingException : Exception{

    public ParsingException(){}

    public ParsingException(string msg)
    : base(msg){}

    public ParsingException(string msg, Exception inner)
    : base(msg, inner) {}

}}
