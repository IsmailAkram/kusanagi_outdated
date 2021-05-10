using System.Collections.Generic;

/*  Tree structure
        1 + 2 * 3


            +           ... binary operator node
           / \
          1   *
             / \
            2   3       ... token nodes in the input file

    */

namespace Kusanagi.Code_Analysis
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;

        private List<string> _diagnostics = new List<string>();
        private int _position; // curren token

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();

                if (token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EOFtoken); // EOF is part of the token list

            _tokens = tokens.ToArray(); // readonly tokens
            _diagnostics.AddRange(lexer.Diagnositcs); // lexer report Errors
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset) // "peek ahead", lex everything at once and then look ahead.
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

                return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

         private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();
            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected: <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var eOFToken = MatchToken(SyntaxKind.EOFtoken);
            return new SyntaxTree(_diagnostics, expression, eOFToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        private ExpressionSyntax ParseTerm()         // logically: first parse the "leaves" at the bottom. then build the method structures on top as you go.
        {
            var left = ParseFactor(); // ParsePrimaryExpression (split to handle PEMDAS)

            // recursive descent parser to handle mathematics correctly (PEMDAS); forming the tree structure.
            while (Current.Kind == SyntaxKind.PlusToken || 
                   Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()         // correct term for "multiplicative expression"
        {
            var left = ParsePrimaryExpression();

            // recursive descent parser to handle mathematics correctly (PEMDAS); forming the tree structure.
            while (Current.Kind == SyntaxKind.StarToken ||
                   Current.Kind == SyntaxKind.SlashToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }
    }
}