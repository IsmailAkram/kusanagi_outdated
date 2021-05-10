using System.Collections.Generic;
using System.Linq;

namespace Kusanagi.Code_Analysis
{
    sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagonostics, ExpressionSyntax root, SyntaxToken eOFToken)
        {
            Diagonostics = diagonostics.ToArray(); // to deal with garbage values
            Root = root;
            EOFToken = eOFToken;
        }

        public IReadOnlyList<string> Diagonostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFToken { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}