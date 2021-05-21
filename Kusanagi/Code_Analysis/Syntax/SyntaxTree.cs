using System.Collections.Generic;
using System.Linq;

namespace Kusanagi.Code_Analysis.Syntax
{
    public sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken eOFToken)
        {
            Diagnostics = diagnostics.ToArray(); // to deal with garbage values
            Root = root;
            EOFToken = eOFToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFToken { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}