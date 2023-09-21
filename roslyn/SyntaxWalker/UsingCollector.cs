using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxWalker;

// CSharpSyntaxWalker - just override all VisitXX methods you're interested in,
// and invoke the Visit method with the root node of the tree (SyntaxTree) you want to walk.
class UsingCollector : CSharpSyntaxWalker
{
    private readonly List<UsingDirectiveSyntax> _usingDirectives = new();
    public IEnumerable<UsingDirectiveSyntax> UsingDirectives => _usingDirectives;
    public override void VisitUsingDirective(UsingDirectiveSyntax node)
    {
        _usingDirectives.Add(node);
    }
}
