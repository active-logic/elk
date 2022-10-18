using System.Collections.Generic;
using System.Text;
using T = UnityEngine.Transform;

namespace Activ.DPE{
public class LogEntry{

    public readonly string role;
    public readonly T entity;
    public readonly string entityName;
    public readonly string msg;
    public readonly bool pass;
    public readonly int score, sum;

    public LogEntry(
        string role, T entity, string msg, bool pass, int score,
        int sum
    ){
        this.role   = role;
        this.entity = entity;
        this.entityName = entity.gameObject.name;
        this.msg    = msg;
        this.pass   = pass;
        this.score  = score;
        this.sum    = sum;
    }

    public float rating => score/(float)sum;

    public bool Satisfies(float r, int offBy){
        if(offBy != -1 && sum - score > offBy)
            return false;
        if(rating < r)
            return false;
        return true;
    }

    override public string ToString(){
        var @out = msg;
        var len = msg.Length;
        if(len > 0 && msg[len-1] != ' ') @out += ' ';
        return $"{@out}{rating:P0} ({score}/{sum})";
    }

}}
