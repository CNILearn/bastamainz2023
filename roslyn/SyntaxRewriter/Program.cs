using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SyntaxRewriter;

class Program
{
    static async Task  Main()
    {
        string code = File.ReadAllText("Sample.cs");
        await ProcessAsync(code);
    }

    static async Task ProcessAsync(string code)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("Sample")
            .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
            .AddSyntaxTrees(tree);

        SemanticModel semanticModel = compilation.GetSemanticModel(tree);

        AutoPropertyRewriter propertyRewriter = new(semanticModel);

        SyntaxNode root = await tree.GetRootAsync().ConfigureAwait(false);
        SyntaxNode rootWithAutoProperties = propertyRewriter.Visit(root);

        compilation = compilation.RemoveAllSyntaxTrees().AddSyntaxTrees(rootWithAutoProperties.SyntaxTree);
        semanticModel = compilation.GetSemanticModel(rootWithAutoProperties.SyntaxTree);
        RemoveBackingFieldRewriter fieldRewriter = new(semanticModel, propertyRewriter.FieldsToRemove.ToArray());
        SyntaxNode rootWithFieldsRemoved = fieldRewriter.Visit(rootWithAutoProperties);
        Console.WriteLine(rootWithFieldsRemoved);
    }
}
