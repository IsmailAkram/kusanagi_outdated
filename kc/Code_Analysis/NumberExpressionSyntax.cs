using System.Collections.Generic;

namespace Kusanagi.Code_Analysis
{
    sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken NumberToken { get; }

        public override IEnumerable<Syntaxnode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}