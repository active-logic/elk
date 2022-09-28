namespace Elk.Memory{
public class Call{

    public string subject;
    public string call;

    public Call(string subject, string call){
        this.subject = subject;
        this.call = call;
    }

    public static implicit operator string(Call self)
    => self?.ToString();

    override public string ToString()
    => $"{subject}.{call}";

}}
