using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace SemanticsCompilation;

class Program
{
    static async Task Main()
    {
        await ProcessAsync();
    }

    static async Task ProcessAsync()
    {
        string source = File.ReadAllText("../../../HelloWorld.cs");
        SyntaxTree tree = CSharpSyntaxTree.ParseText(source);
        if (await tree.GetRootAsync() is CompilationUnitSyntax root)
        {
            // get Hello method
            MethodDeclarationSyntax? helloMethod = root
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Identifier.ValueText == "Hello")
                .FirstOrDefault();

            // get hello variable
            VariableDeclaratorSyntax? helloVariable = root
                .DescendantNodes()
                .OfType<VariableDeclaratorSyntax>()
                .Where(v => v.Identifier.ValueText == "hello")
                .FirstOrDefault();

            var compilation = CSharpCompilation.Create("HelloWorld")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree);

            EmitResult result = compilation.Emit("HelloWorld.exe");

            ISymbol? helloVariableSymbol1 = compilation.GetSymbolsWithName(name => name == "Hello").FirstOrDefault();

            SemanticModel model = compilation.GetSemanticModel(tree);
           
            if (helloVariable != null && helloMethod != null)
            {
                ISymbol helloVariableSymbol = model.GetDeclaredSymbol(helloVariable) ?? throw new InvalidOperationException("no symbol");
                IMethodSymbol helloMethodSymbol = model.GetDeclaredSymbol(helloMethod) ?? throw new InvalidOperationException("not a symbol");

                ShowSymbol(helloVariableSymbol);
                ShowSymbol(helloMethodSymbol);
            }
        }
    }

    private static void ShowSymbol(ISymbol symbol)
    {
        Console.WriteLine(symbol.Name);
        Console.WriteLine(symbol.Kind);
        Console.WriteLine(symbol.ContainingSymbol);
        Console.WriteLine(symbol.ContainingType);
        Console.WriteLine((symbol as IMethodSymbol)?.MethodKind);
        Console.WriteLine();
    }
}
