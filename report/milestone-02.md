# Episode 2

[Commits Branch](https://github.com/IsmailAkram/kusanagi/commits/milestone2-unary-operators) |
[Previous](milestone-01.md) |
[Next]()

## Completed tasks

* Generalized parsing (using precedences)
* Supports unary operators (such as `+1` and `-1`)
* Supports Boolean literals (`false`, `true`)
* Supports conditions (such as `1 == 1 && 1 != 2 || true`)
* Type checking internal representation (`Binder`, and `BoundNode`)

## Key highlights

### Generalized parsing precedence

In [milestone 1](milestone-01.md), I created a basic foundation in the form of a rdp.  This recursive descent parser parses addititive and multiplicative expressions correctly. This was achieved by using `ParseTerm` for `+` and `-` and `ParseFactor` for `*` and `/`.
However, for the long term, this doesn't scale well with large numbers of operators. So I modified it into a [unified method][precedence-parsing]

[precedence-parsing]: https://github.com/IsmailAkram/kusanagi/blob/milestone2-unary-operators/kc/Code_Analysis/Syntax/Parser.cs#L94-L121

### Bound tree

The first version of the evaluator was just walking the syntax tree directly. 
But it doesn't have any *semantic* information (it doesn't
know which types an expression will be evaluating to). This makes complex features nigh on impossible (having operators that
depend on the input types).

So I've implemented the *bound tree*. The [Binder][binder] creates the bound tree
by traversing the syntax tree and *binding* the nodes to symbolic information.
The binder represents the semantic analysis of
our compiler. It looks up variable names in scope,
performs type checks, and enforces correctness rules.

This is shown in [Binder.BindBinaryExpression][bind-binary] which
binds `BinaryExpressionSyntax` to a [BoundBinaryExpression][bound-binary]. The
operator is searched by using the types of the left and right expressions in
[BoundBinaryOperator.Bind][bind-binary-op].

[binder]: https://github.com/IsmailAkram/kusanagi/blob/milestone2-unary-operators/kc/Code_Analysis/Syntax/Binding/Binder.cs
[bind-binary]: https://github.com/IsmailAkram/kusanagi/blob/milestone2-unary-operators/kc/Code_Analysis/Syntax/Binding/Binder.cs#L48-L61
[bound-binary]: https://github.com/IsmailAkram/kusanagi/blob/milestone2-unary-operators/kc/Code_Analysis/Syntax/Binding/BoundBinaryExpression.cs#L5-L18
[bind-binary-op]: https://github.com/IsmailAkram/kusanagi/blob/milestone2-unary-operators/kc/Code_Analysis/Syntax/Binding/BoundBinaryOperator.cs#L52-L61
