using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using static System.Console;

namespace SyntaxWalker;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 1)
        {
            ShowUsage();
            return;
        }

        string path = args[0];
        if (!Directory.Exists(path))
        {
            ShowUsage();
            return;
        }

        await ProcessUsingsAsync(path);
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage: SyntaxWalker directory");
    }

    static async Task ProcessUsingsAsync(string path)
    {
        const string searchPattern = "*.cs";
        UsingCollector collector = new();

        IEnumerable<string> fileNames = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories).Where(fileName => !fileName.EndsWith(".g.i.cs") && !fileName.EndsWith(".g.cs"));
        foreach (var fileName in fileNames)
        {
            string code = File.ReadAllText(fileName);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            SyntaxNode root = await tree.GetRootAsync();
            collector.Visit(root);
        }

        var usings = collector.UsingDirectives;
        var usingStatics = usings
            .Select(n => n.ToString())
            .Distinct()
            .Where(u => u.StartsWith("using static"))
            .OrderBy(u => u);
        var orderedUsings = usings
            .Select(n => n.ToString())
            .Distinct()
            .Except(usingStatics)
            .OrderBy(u => u[..^1]);
        foreach (var item in orderedUsings.Union(usingStatics))
        {
            WriteLine(item);
        }
    }
}
