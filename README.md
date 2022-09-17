[DEVELOPMENT IN PROGRESS - STATUS: UNSTABLE]

*ELK and BT-Lang are under development; do not use in production*

**Working alpha: October 15 2022**

# ELK / BT-Lang

This Unity package includes the following:
- BT-Lang ("BTL", pronounced 'beetle' or 'behtel') is a scripted language for stateless behavior trees
- ELK is a toolkit for building interpreters

## Requirements

While this package does not significantly rely on the Unity platform, currently a little prepping would be needed for standalone deployment.

BT-Lang requires Active Logic or Active LT
ELK has no requirements

## Get started with BTL

Add your BTL programs under *Assets/Resources* or any resources directory. The extension is *.txt*.

BTL behavior trees are expressed as modules defining several functions. The entry point is the `Step()` function.

**Sample-BT.txt**

```
#!btl

status Step() => Attack() || Roam();

status Attack() => Reach(target) && Strike(target);

status Roam() => Move(rdir);

status Reach(target) => fail;

status Strike(target) => fail;
```

Add a BTL program to a game object via the BTL component.
Import functions, fields and properties from other components via the BTL component's *Import* attribute.
In the above example, `Move` and `target` are external; they are defined in separate components; if name conflict, components higher up the list have priority.

Multiline statements are supported; multi-statement functions are not supported. Parentheses (for scoping) are not supported. Refer to **issues** (or file an issue) for planned features.

## Get started with ELK

ELK will be documented at a later date.
