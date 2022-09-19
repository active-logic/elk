namespace Elk.Util{
public static class ObjectExt{

    public static bool IsNumber(this object value)
     => value is sbyte
     || value is byte
     || value is short
     || value is ushort
     || value is int
     || value is uint
     || value is long
     || value is ulong
     || value is float
     || value is double
     || value is decimal;

}}
