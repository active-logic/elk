using S = System.String;
using System.Linq;

namespace Elk.Basic{
public class BasicTokenizer : Elk.Tokenizer{

    public string[] Tokenize(string arg)
    => (from x in arg.Split() where x.Length > 0 select x).ToArray();

}}
