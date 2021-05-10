using System.Collections.Generic;

namespace Kusanagi.Code_Analysis
{
    sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closedParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            ClosedParenthesisToken = closedParenthesisToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
        public SyntaxToken OpenParenthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken ClosedParenthesisToken { get; }

        public override IEnumerable<Syntaxnode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return ClosedParenthesisToken;
        }
    }
}