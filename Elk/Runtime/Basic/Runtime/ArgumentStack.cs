using Ex = System.Exception;

public class ArgumentStack{

    int index = -1;
    ArgMap[] stack = new ArgMap[64];

    public bool empty => index == -1;

    public ArgumentStack(){
        for(int i = 0; i < stack.Length; i++){
            stack[i] = new ArgMap();
        }
    }

    public void Clear() => index = -1;

    public void Pop() => index--;

    public void Push(string[] parameters, object[] arguments){
        stack[++index].Set(parameters, arguments);
    }

    public ArgMap Peek() => stack[index];

    // -------------------------------------------------------------

    public class ArgMap{

        string[] parameters;
        object[] arguments;

        public void Set(string[] p, object[] args){
            parameters = p;
            arguments = args;
        }

        public bool ContainsKey(string arg){
            if(parameters == null) return false;
            foreach(var p in parameters) if(p == arg) return true;
            return false;
        }

        public object this[string key]{ get{
            for(var i = 0; i < parameters.Length; i++){
                if(parameters[i] == key) return arguments[i];
            }
            throw new Ex($"Not an argument: {key}");
        }}

    }

}
