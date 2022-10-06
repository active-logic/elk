public class Frame{
    
    float time;
    string @event;

    public Frame(string e, float t){
        time = t;
        @event = e;
    }

    public bool Matches(string arg) => @event == arg;

}
