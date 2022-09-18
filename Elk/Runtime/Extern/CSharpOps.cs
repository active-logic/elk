
using Map = System.Collections.Generic.Dictionary<string, string>;

// REF https://stackoverflow.com/questions/11113259/
// how-to-call-custom-operator-with-reflection
namespace Elk.Bindings.CSharp{
public static class CSharpOps{

    public static Map Unary = new Map()
    {
        {"+", "op_UnaryPlus" },
        {"-", "op_UnaryNegation" },
        {"++", "op_Increment" },
        {"--", "op_Decrement" },
        {"!", "op_LogicalNot" },
        {"~", "op_OnesComplement" },
    };

    public static Map Binary = new Map()
    {
        {"+", "op_Addition" },
        {"-", "op_Subtraction" },
        {"*", "op_Multiply" },
        {"/", "op_Division" },
        {"&", "op_BitwiseAnd" },
        {"|", "op_BitwiseOr" },
        {"^", "op_ExclusiveOr" },
        {"==", "op_Equality" },
        {"!=", "op_Inequality" },
        {"<", "op_LessThan" },
        {">", "op_GreaterThan" },
        {"<=", "op_LesserThanOrEqual" },
        {">=", "op_GreaterThanOrEqual" },
        {"<<", "op_LeftShift" },
        {">>", "op_RightShift" },
        {"%", "op_Modulus" },
    };

}}
