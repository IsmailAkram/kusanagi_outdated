# Milestone 3

[Branch Commits](https://github.com/IsmailAkram/kusanagi/commits/Milestone3-assignments-and-variables) |
[Previous](milestone-02.md) |
[Next](milestone-04.md)

## Completed tasks

* Extracted compiler into its own seperate library (for future scalability)
* Error diagnositics implemented which exposes span said diagnostics (indicates location of error)
* Assignments and variables support implemented

## Key highlights

### Compilation API

I've added a `Compilation` type that holds onto the entire program state.
I plan on using it to expose declared symbols and house all
compiler operations (emitting code). Currently, it exposes an
`Evaluate` API that interprets the expression:

```C#
var syntaxTree = SyntaxTree.Parse(line);
var compilation = new Compilation(syntaxTree);
var result = compilation.Evaluate();
Console.WriteLine(result.Value);
```

### Assignments as expressions

C language treats assignments as expressions, rather than isolated top-level statements. This
allows for writing code in this manner:

```C#
a = b = 1
```

However thinking about assigments as binary operators will result in an awkward parsing strategy.
For example: the parse tree for the
expression `a + b + 1` looks like this:

```
    +
   / \
  +   1
 / \
a   b
```

This tree shape isn't ideal for assignments. Instead, we need this:

```
  =
 / \
a   =
   / \
  b   1
```

which means that `=` is *right associative*. First `b` is assigned the value `5` and *then* `a` is assigned
the value `1`.

For the left-hand-side (LHS), I'm only allowing variable names (as a [single token][token]) for now. Which is also great as it allows for ["peeking" ahead][peek] for easier parsing.

Later, I need to deal with array indexes, qualified names. Most compilers will represent them as an expression. But we can represent *every* expression, like literals (`1`).
These are called *L-values*.

[token]: https://github.com/IsmailAkram/kusanagi/blob/Milestone3-assignments-and-variables/Kusanagi/Code_Analysis/Syntax/AssignmentExpressionSyntax.cs
[peek]: https://github.com/IsmailAkram/kusanagi/blob/Milestone3-assignments-and-variables/Kusanagi/Code_Analysis/Syntax/Parser.cs#L94-L121

Note: AssignmentExpressionSyntax.cs does not exist yet. This is where I stopped.
