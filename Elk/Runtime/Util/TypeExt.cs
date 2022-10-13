using System;

namespace Elk.Util{
public static class TypeExt{

    public static string Format(this Type self)
    => self == null ? "?" : self.Name;

}}
