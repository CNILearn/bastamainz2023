using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxRewriter;

class RemoveBackingFieldRewriter : CSharpSyntaxRewriter
{
    private readonly IEnumerable<string> _fieldsToRemove;
    private readonly SemanticModel _semanticModel;
    public RemoveBackingFieldRewriter(SemanticModel semanticModel, params string[] fieldsToRemove)
    {
        _semanticModel = semanticModel;
        _fieldsToRemove = fieldsToRemove;
    }

    public override SyntaxNode? VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        if (_fieldsToRemove.Contains(node.GetText().ToString()))
        {
            return null;
        }
        return base.VisitFieldDeclaration(node);
    }
}
