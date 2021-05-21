# Milestone 1

[Branch Commits](https://github.com/IsmailAkram/kusanagi/commits/milestone1-lexer-and-parser) | [Next](milestone-02.md)

## Completed tasks

* Basic REPL (read-eval-print-loop) for expression evaluator
* Lexer, parser, and evaluator added
* `+`, `-`, `*`, `/`, `(`, `)`, handling
* Print out Syntax Trees

## Key highlights

### Operator precendence (PEMDAS)

When parsing the expression `1 + 2 * 3`, we parse it into a tree structure with a precedence of `*` over `+`:

```
└──BinaryExpression
    ├──NumberExpression
    │   └──NumberToken 1
    ├──PlusToken
    └──BinaryExpression
        ├──NumberExpression
        │   └──NumberToken 2
        ├──StarToken
        └──NumberExpression
            └──NumberToken 3
```
![](report/images/milestone2-1.PNG)

Having a correct priority of operators is vital as it ensures the correct output is achieved. (`*` binds stronger than `+`).

I used a [recursive descent parser][rdp] which is highlight here [structuring call methods][parsing]

[rdp]: https://en.wikipedia.org/wiki/Recursive_descent_parser
[parsing]: https://github.com/IsmailAkram/kusanagi/blob/8ea9ff5dfb09a5c32251c1a47fb068d04fd7c1cf/kc/Code_Analysis/Parser.cs#L85-L114

### Token fabrication

In certain cases, like parsing:
`(` and an `<expression>`, and a `)`, 
it will assert that a token follows.

If the current token doesn't match the expression expectation, then it will create a [new matching fabricated token][match].

This is for later parts of the compiler that go through the tree and avoids cases where it must assume anything could be null.

[match]: https://github.com/IsmailAkram/kusanagi/blob/8ea9ff5dfb09a5c32251c1a47fb068d04fd7c1cf/kc/Code_Analysis/Parser.cs#L65-L71
