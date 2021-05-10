namespace Kusanagi.Code_Analysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EOFtoken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        
        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        
    }
}