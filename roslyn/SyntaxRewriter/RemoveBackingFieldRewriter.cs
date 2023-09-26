using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxRewriter;

class RemoveBackingFieldRewriter(SemanticModel semanticModel, params string[] fieldsToRemove) : CSharpSyntaxRewriter
{
    private readonly IEnumerable<string> _fieldsToRemove = fieldsToRemove;
    private readonly SemanticModel _semanticModel = semanticModel;

    public override SyntaxNode? VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        if (_fieldsToRemove.Contains(node.GetText().ToString()))
        {
            return null;
        }
        return base.VisitFieldDeclaration(node);
    }
}
