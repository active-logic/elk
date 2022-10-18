using System;
using System.Collections.Generic;
using System.Text;

namespace Activ.DPE{
public static class LogFormat{

    public static string FormatBestMatch(List<LogEntry> log){
        if(log == null) return "-";
        LogEntry sel = null;
        foreach(var e in log){
            if(sel == null || e.rating > sel.rating){
                sel = e;
            }
        }
        return FormatLogEntry(sel);
    }

    public static string Format(
        List<LogEntry> log, Predicate<LogEntry> cond
    ){
        if(log == null) return "-";
        StringBuilder @out = new StringBuilder();
        foreach(var e in log) if(cond(e)){
            @out.Append( FormatLogEntry(e) + '\n');
        }
        return @out.ToString();
    }

    public static string FormatLogEntry(LogEntry e)
    => e == null ? "null"
    : (e.pass ? "✓ " : "✗ ") + e.entityName
                              + " :: " + e.ToString();


}}
