namespace Kusanagi.Code_Analysis
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
        BinaryExpression,
        ParenthesizedExpression
    }
}