# BTL notes

## Language

[PENDING]

## Integration

[PENDING]

## Customize memory and apperception

In BTL, the `did x` and `did x since y` forms can be used to test an agent's knowledge of past actions - including their own, or actions performed by other agents.

By *default* agents are omniscient - every agent knows everything other agents have done. In production this is not desirable. Therefore providing your own cognition model is recommended.

In general BTL takes care of the heavy lifting for you (all BT tasks are automatically recorded); a custom implementation hooks into automated task recording, and may accomplish any of the following and more:

- Apperception driven filtering - when an agent did X, which other agents became aware of it?
- How much do agents remember? For how long and how reliably? (simulate imperfect memory, both for optimization (storage) and realism)
- Naming: what are other agents remembered as?
- Plugging your own events into BTL records; such as apperception related (`cat.Spotted(mouse)`)
- Record sharing / transacting (simulate knowledge exchange between agents).

Two steps:

- (1) Extend [BTLCog][../BT-lang/Runtime/BTLCog.cs] or implement the [Cog](../Elk/Runtime/Memory/Cog.cs) interface (see inline doc for details)
- (2) Given a BTL component, assign `cognition`:

```cs
var cog = new MyCog();
this.GetComponent<BTL>().cognition = cog;
```
