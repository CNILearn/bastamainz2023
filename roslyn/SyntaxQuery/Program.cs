using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxQuery;

// Use LINQ queries accessing the syntax tree
// to find public methods and properties with lowercase first character

// Not using top-level statements because access modifiers are used that are queried

class Program
{
    static async Task Main()
    {
        await CheckLowercaseMembers();
    }

    static async Task CheckLowercaseMembers()
    {
        string code = File.ReadAllText("Program.cs");
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        SyntaxNode root = await tree.GetRootAsync();
        
        var methods = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(m => char.IsLower(m.Identifier.ValueText.First()))
            .Where(m => m.Modifiers.Select(t => t.Value).Contains("public"));
        Console.WriteLine("Public methods with lowercase first character:");
        foreach (var m in methods)
        {
            Console.WriteLine(m.Identifier.ValueText);             
        }

        var properties = root.DescendantNodes()
            .OfType<PropertyDeclarationSyntax>()
            .Where(p => char.IsLower(p.Identifier.ValueText.First()))
            .Where(p => p.Modifiers.Select(t => t.Value).Contains("public"));

        Console.WriteLine("Public properties with lowercase first character:");
        foreach (var p in properties)
        {
            Console.WriteLine(p.Identifier.ValueText);
        }
    }

    public void foo()
    {

    }

    private void foobar()
    {

    }

    public int bar { get; set; }
}
