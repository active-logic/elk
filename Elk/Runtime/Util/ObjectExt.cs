using UnityEngine;

namespace Elk.Util{
public static class ObjectExt{

    // NOTE - used to format args in elk memory record
    public static string Format(this object self){
        switch(self){
            case GameObject go: return go.name;
            case Component c:   return c.gameObject.name;
            case null: return "null";
            default: return self.ToString();
        }
    }

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
