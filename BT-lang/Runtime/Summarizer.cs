using System.Collections.Generic;

public class Summarizer{

    List<Entry> entries = new List<Entry>();

    public string AddAll(List<string> arg){
        Entry sel = null;
        string selValue = null;
        foreach(var str in arg){
            if(str.Contains("✗")) continue;
            var e = Add(str);
            if(sel == null || sel.count > e.count){
                sel = e;
                selValue = str;
            }
        }
        return selValue;
    }


    Entry Add(string arg){
        arg = Strip(arg);
        var e = entries.Find( x => x.value == arg);
        if(e == null){
            e = new Entry(1, arg);
            entries.Add(e);
            return e;
        }else{
            e.count ++;
            return e;
        }
    }

    string Strip(string arg){
        int i = arg.IndexOf("(");
        if(i >= 0) return arg.Substring(0, i);
        else return arg;
    }

    class Entry{
        public int count;
        public string value;
        public Entry(int count, string value){
            this.count = count;
            this.value = value;
        }
    }

}
