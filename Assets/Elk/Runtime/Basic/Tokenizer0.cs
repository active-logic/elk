using S = System.String;
using System.Linq;

namespace Elk{
public class Tokenizer0{

    public S[] this[S arg]
    => (from x in arg.Split() where x.Length > 0 select x).ToArray();

}}
