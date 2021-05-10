using System.Collections.Generic;

namespace Kusanagi.Code_Analysis
{
    public sealed class LiteralExpressionToken : ExpressionSyntax
    {
        public LiteralExpressionToken(SyntaxToken literalToken)
        {
            LiteralToken = literalToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken LiteralToken { get; }

        public override IEnumerable<Syntaxnode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}