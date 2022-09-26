using Active.Core.Details;

namespace Activ.BTL{
public readonly struct BTLHistory : Elk.History{

    readonly History history;

    public BTLHistory(History history)
    => this.history = history;

    public bool Did(string action)
    => history.Contains(action);

    public bool Did(string action, string since, bool strict)
    => history.Contains(action, since, strict);

}}
