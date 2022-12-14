using Cx = Elk.Basic.Runtime.Context;

namespace Elk.Memory{
public interface Cog{

    // Return true if an agent have performed the specified action
    // since 'since' has occured
    bool Did(Occurence arg, Occurence? since, bool strict, Cx cx);

    // Name a subject (used when committing actions to memory)
    string NameOf(object agent, bool allowDefault);

    // Called by the runtime when a function has evaluated;
    // in this case, either record or discard the call
    void CommitCall(string func, string arg, object returnValue, Record record);

    void CommitEvent(Occurence arg, object returnValue, Record record);

}}
