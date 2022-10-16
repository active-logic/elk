using System.Text;

namespace Activ.DPE{
public class Logger{

    StringBuilder builder = new StringBuilder();
    public int score, sum;
    public readonly bool critsOnly;

    public Logger(bool critsOnly) => this.critsOnly = critsOnly;

    public void Log(string arg, Set src){
        if(!critsOnly || src is Crit) builder.Append(arg);
    }

    public bool this[string arg, Set src]{ get{
        if(!critsOnly || src is Crit){
            builder.Append(arg);
        }
        return true;
    }}

    public bool this[bool arg]{ get{
        sum++;
        if(arg) score++;
        return arg;
    }}

    public string message => builder.ToString();

}}
