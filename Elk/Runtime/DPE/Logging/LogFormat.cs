using System.Collections.Generic;
using System.Text;

namespace Activ.DPE{
public static class LogFormat{

    public static string Format(
        List<LogEntry> log, bool passOnly, float minRating, int offBy
    ){
        StringBuilder @out = new StringBuilder();
        string role = null;
        foreach(var e in log){
            if(!e.Satisfies(minRating, offBy) || (passOnly && !e.pass)){
                continue;
            }
            if(e.role != role){
                role = e.role;
                @out.Append("\n" + role + ":\n");
            }
            @out.Append( FormatLogEntry(e, passOnly) );
        }
        return @out.ToString();
    }

    public static string FormatLogEntry(LogEntry e, bool passOnly)
    => (e.pass ? "✓ " : "✗ ") + e.entityName
                               + " :: " + e.ToString() + '\n';


}}
