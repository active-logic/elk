using System.Collections.Generic;
using T = UnityEngine.Transform;

namespace Activ.DPE{
public interface Solver{

    object Resolve(Set arg, string label);
    List<LogEntry> GetLog();

}}
