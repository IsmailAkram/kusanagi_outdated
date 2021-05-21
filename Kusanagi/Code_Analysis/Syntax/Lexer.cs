using System.Collections.Generic;

namespace Kusanagi.Code_Analysis.Syntax
{
    internal sealed class Lexer                                 // responsible for reading into the actual value. Internal because Syntax parse
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>(); // for Error handling

        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnositcs => _diagnostics;

        private char Current => Peek(0);
        private char Lookahead => Peek(1);      // So we can keep track of our position

        private char Peek(int offset) 
        {
            var index = _position + offset; 

            if (index >= _text.Length)          // if the position is outside the bounds of the text;
                return '\0';                    // then return 0 terminator

            return _text[index];
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()                // find next word from current position in file, take that and return, the keep going
        {
            // Expression evaluators (words we need)
            // <numbers>
            // + - * / ()
            // <whitespace>

            if (_position >= _text.Length)      // EOF (End of File). Notifies the reader that they've reached EOF.
                return new SyntaxToken(SyntaxKind.EOFtoken, _position, "\0", null); // Virtual token that doesn't exist in the input file.

            if (char.IsDigit(Current))
            {
                var start = _position;
                
                while (char.IsDigit(Current))
                    Next();
                
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                    _diagnostics.Add($"The number {_text} is not a valid Int32.");

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value); // for later phases, I'm just setting this up now.
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                
                while (char.IsWhiteSpace(Current))
                    Next();
                
                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                var start = _position;
                
                while (char.IsLetter(Current))
                    Next();
                
                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch (Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
                
                // adding cases to account for NOT, AND, and OR.    
                case '&':
                    if (Lookahead == '&')
                        return new SyntaxToken(SyntaxKind.LogicalANDToken, _position += 2, "&&", null);
                    break;
                case '|':
                    if (Lookahead == '|')
                        return new SyntaxToken(SyntaxKind.LogicalORToken, _position += 2, "||", null);
                    break;
                case '=':
                    if (Lookahead == '=')
                        return new SyntaxToken(SyntaxKind.EqualityToken, _position += 2, "==", null);
                    break;
                case '!':
                    if (Lookahead == '=')
                        return new SyntaxToken(SyntaxKind.NotEqualityToken, _position += 2, "!=", null);
                    else
                        return new SyntaxToken(SyntaxKind.ExclamationToken, _position++, "!", null);
            }

            _diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);  // position has already been incremented       
        }
    }
}