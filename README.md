*ELK and BT-Lang are under development; do not use in production; this repository is shared for early feedback around BT-Lang development; planned alpha: Oct 15 2022.*

# ELK / BT-lang

This Unity package includes the following:
- BT-Lang ("BTL", pronounced *beetle* or *behtel*) is a scripting language for stateless behavior trees
- ELK is a toolkit for quickly building simple interpreters

## Requirements

Unity; for now.

- BT-Lang requires Active-Logic or Active-LT
- ELK has no requirements

## Get started with BTL

Add your BTL programs under *Assets/Resources* or any resources directory; the extension is `*.txt`; the `#!btl` shebang helps reporting syntax errors (in the Unity console, upon saving your BTL file).

In BTL a behavior tree is a module defining several functions; the entry point is the `Step()` function.

**Sample-BT.txt**

```
#!btl

task Step() => Attack() || Roam();

task Attack() => Reach(target) && Strike(target);

task Roam() => Move(rdir);

task Reach(target) => fail;

task Strike(target) => fail;
```

Add a BTL program to a game object via the BTL component.

- 'Path' is the path to the BTL source module, relative to `Resources`; for example if your BT program is under `Resources/Sample.txt`, then the path is `Sample` (without extension)
- Import functions, fields and properties from other components via the BTL component's *Import* attribute;
in the above example, `Move` and `target` are external; they are defined in separate components; if name conflict, components higher up the list have priority.
- 'Output' shows the current status of your BT (while running)
- 'Graph' shows the call tree for your BT (while running) along with parameters and return state for every traversed subtask.

The output of the sample program may be something like this:

```
→ Step()
  ✗ Attack()
    ✗ Reach(null)
  → Roam()
    → Move((0.78, 0.63, 0.00))
```

Multiline statements are supported; multi-statement functions are not supported; parentheses (for scoping) are not supported; comments are not supported yet; calculus (2+2) is supported but this may be dropped later; active logic unaries are not supported just yet; active logic binary ops *should be* supported.

Refer to **issues** (or file an issue) for planned features.

## Get started with ELK

ELK will be documented at a later date.
