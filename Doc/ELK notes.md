# ELK notes

## Adding a construct [doc wip]

As an example let's have a look at the work needed to add unary operator support.

(1) parsing test

In general adding a test first is a good idea; head over
to ParserTest and add the test

(2) graph node

First, under Basic/Graph, define a node specifying what data the construct is holding. In our example, the graph node is a `UnaryExp` holding the operator name and an argument object.

(3) parsing rule

Next under Basic/Parser, define a rule specifying how the construct is parse. In our case a unary operator simply consists in an operator, followed by an operand
