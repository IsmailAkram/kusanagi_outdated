using System.Collections.Generic;

namespace Kusanagi.Code_Analysis.Syntax
{
    public sealed class BinaryExpressionSyntax : ExpressionSyntax // data structures in order (for now). Syntax nodes public for API
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Right { get; }

        public override IEnumerable<SyntaxNode> GetChildren() // create an Enumerable (array) where first item is "Left"
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}