// This source snippet breaks characters into 'words' for the parser to work with in creating sentences.
// Lexer produces tokens ("leaves in a tree")
// Parser produces actual sentences ("trees")

using System;
using System.Collections.Generic;
using System.Linq;
using Kusanagi.Code_Analysis;
using Kusanagi.Code_Analysis.Syntax;

namespace Kusanagi
{

    internal static class Program
    {
        private static void Main()
        {
            var showTree = false;

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                // psuedocommands
                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees");
                    continue;
                }
                else if (line == "#clear")
                {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!syntaxTree.Diagonostics.Any())
                {
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnositcs in syntaxTree.Diagonostics)
                        Console.WriteLine(diagnositcs);

                    Console.ResetColor();
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool IsLast = true)
        {
            // for presentation (appropriated from https://en.wikipedia.org/wiki/Tree_(command))
            // │
            // ├──
            // └──

            var marker = IsLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(' ');
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += IsLast ? "   " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault(); // I kept crashing, LastorDefault saved it


            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}