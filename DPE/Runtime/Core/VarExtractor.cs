using Elk.Bindings.CSharp;

namespace Activ.DPE{
public static class VarExtractor{

public static ExternalFieldBinding[] LookupFields(object arg)
=> arg.LookupFields<Set>();

}}
