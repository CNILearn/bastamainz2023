using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace SyntaxRewriter;

class AutoPropertyRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;


    public AutoPropertyRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    private readonly List<string> _fieldsToRemove = new();
    public IEnumerable<string> FieldsToRemove => _fieldsToRemove;

    public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        return base.VisitClassDeclaration(node);
    }

    public override SyntaxNode? VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        return base.VisitFieldDeclaration(node);
    }

    public override SyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        if (HasBothAccessors(node))
        {
            AccessorDeclarationSyntax? accessorDeclaration = node.AccessorList?.Accessors.Single(
                ad => ad.Kind() == SyntaxKind.GetAccessorDeclaration);
            if (accessorDeclaration is null) return null;

            IFieldSymbol? backingField = GetBackingFieldFromGetter(accessorDeclaration);
            if (backingField is null) return null;

            SyntaxNode? fieldDeclaration = backingField.DeclaringSyntaxReferences
                .First()
                .GetSyntax()
                .Ancestors()
                .Where(a => a is FieldDeclarationSyntax)
                .FirstOrDefault();

            if (fieldDeclaration is FieldDeclarationSyntax fieldDeclarationSyntax)
            {
                string fieldToRemove = fieldDeclarationSyntax.GetText().ToString();
                _fieldsToRemove.Add(fieldToRemove);
            }
            PropertyDeclarationSyntax property = ConvertToAutoProperty(node).WithAdditionalAnnotations(Formatter.Annotation);
            return property;
        }
        return node;
    }

    private static bool HasBothAccessors(BasePropertyDeclarationSyntax property)
    {
        var accessors = property.AccessorList?.Accessors;
        if (accessors is null) return false;
        
        var getter = accessors?.FirstOrDefault(
            ad => ad.Kind() == SyntaxKind.GetAccessorDeclaration);
        var setter = accessors?.FirstOrDefault(
            ad => ad.Kind() == SyntaxKind.SetAccessorDeclaration);

        return getter?.Body?.Statements.Count == 1 && setter?.Body?.Statements.Count == 1;
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        return base.VisitNamespaceDeclaration(node);
    }

    private static PropertyDeclarationSyntax ConvertToAutoProperty(PropertyDeclarationSyntax propertyDeclaration)
    {
        var newProperty = propertyDeclaration
            .WithAccessorList(
                SyntaxFactory.AccessorList(
                    SyntaxFactory.List(new[]
                        {
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)).WithAdditionalAnnotations(Formatter.Annotation),
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        })));

        return newProperty;
    }

    private IFieldSymbol? GetBackingFieldFromGetter(AccessorDeclarationSyntax getter)
    {
        if (getter.Body?.Statements.Count != 1) return null;

        var statement = getter.Body.Statements.Single() as ReturnStatementSyntax;
        if (statement?.Expression == null) return null;

        return _semanticModel.GetSymbolInfo(statement.Expression).Symbol as IFieldSymbol;
    }
}
