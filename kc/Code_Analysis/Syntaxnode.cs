using System.Collections.Generic;

namespace Kusanagi.Code_Analysis
{
    public abstract class Syntaxnode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<Syntaxnode> GetChildren();
    }
}