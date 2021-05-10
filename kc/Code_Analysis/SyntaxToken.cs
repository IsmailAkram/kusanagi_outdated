using System.Collections.Generic;
using System.Linq;

namespace Kusanagi.Code_Analysis
{
    public sealed class SyntaxToken : Syntaxnode             // pretend syntax tokens are the leaves in our tree
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value) // object for strings/floats for later
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<Syntaxnode> GetChildren()
        {
            return Enumerable.Empty<Syntaxnode>();
        }
    }
}