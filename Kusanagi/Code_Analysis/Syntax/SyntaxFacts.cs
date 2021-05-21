/*

     *
    / \
   -   2
   |
   1

*/
using System;

namespace Kusanagi.Code_Analysis.Syntax
{
    internal static class SyntaxFacts // this maintains the correct sought after precedence (this will go through changes over development)
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.ExclamationToken:
                    return 6; 

                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
        
        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind) // Maintains PEMDAS through returns (precedence). This mainly works since the operator tokens we're working on isn't too large. Although this is a "brute force" tactic.
        {
            switch(kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 5;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                case SyntaxKind.EqualityToken:
                case SyntaxKind.NotEqualityToken:
                    return 3;

                case SyntaxKind.LogicalANDToken:
                    return 2;

                case SyntaxKind.LogicalORToken:
                    return 1;

                default:
                    return 0;
            }
        }
    }
}