using System;
using System.Linq;
using Kusanagi.Code_Analysis.Binding;
using Kusanagi.Code_Analysis.Syntax;

namespace Kusanagi.Code_Analysis
{
    public sealed class Compilation
    {
        public Compilation(SyntaxTree syntaxTree)
        {
            SyntaxTree = syntaxTree;
        }

        public SyntaxTree SyntaxTree { get; }

        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var boundExpression = binder.BindExpression(SyntaxTree.Root);

            var diagnostics = SyntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();
            if (diagnostics.Any())
            {
                return new EvaluationResult(diagnostics, null);
            }

            var evaluator = new Evaluator(boundExpression);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<string>(), value);
        }
    }
} 