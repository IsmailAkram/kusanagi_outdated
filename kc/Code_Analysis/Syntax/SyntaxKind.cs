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

        // Logical gates
        ExclamationToken,
        LogicalANDToken,
        LogicalORToken,
        EqualityToken,
        NotEqualityToken,

        OpenParenthesisToken,
        CloseParenthesisToken,
        IdentifierToken,

        // Keywords
        FalseKeyword,
        TrueKeyword,

        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
    }
}