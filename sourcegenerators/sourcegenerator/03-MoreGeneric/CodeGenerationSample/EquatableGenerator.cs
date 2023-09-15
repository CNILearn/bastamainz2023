// based on an implementation from https://andrewlock.net/

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.Collections.Immutable;
using System.Text;

namespace CodeGenerationSample;

[Generator(LanguageNames.CSharp)]
public class EquatableGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource("ImplementEquatable.g.cs", SourceText.From(attributeText, Encoding.UTF8)));

        // A simple filter for classes with attributes
        IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // is it a class with attributes?
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // select the class with the [ImplementEquatable] attribute
            .Where(static m => m is not null)!;

        // Combine the selected classes with the `Compilation`
        IncrementalValueProvider<(Compilation Compilation, ImmutableArray<ClassDeclarationSyntax> Classes)> compilationAndClasses
            = context.CompilationProvider.Combine(classDeclarations.Collect());

        // Generate the source using the compilation and enums
        context.RegisterSourceOutput(compilationAndClasses,
            static (sourceproductioncontext, source) => 
                Execute(source.Compilation, source.Classes, sourceproductioncontext));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node) =>
        node is ClassDeclarationSyntax { AttributeLists.Count: > 0 };

    private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax? classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                SymbolInfo symbolInfo = context.SemanticModel.GetSymbolInfo(attributeSyntax);

                ISymbol? attributeSymbol = symbolInfo.Symbol ?? symbolInfo.CandidateSymbols.FirstOrDefault();
                if (attributeSymbol is null) continue;

                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [ImplementEquatable] attribute?
                if (fullName == "CodeGenerationSample.ImplementEquatableAttribute")
                {
                    // return the class
                    return classDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        IEnumerable<ClassDeclarationSyntax> distinctClasses = classes.Distinct();

        // Convert each EnumDeclarationSyntax to an EnumToGenerate
        List<ClassToGenerateInfo> classesToGenerate = GetTypesToGenerate(compilation, distinctClasses, context.CancellationToken);

        foreach (var classToGenerate in classesToGenerate)
        {
            var source = SourceText.From(GenerateEquatableImlementation(classToGenerate), Encoding.UTF8);
            context.AddSource($"{classToGenerate.Name}.g.cs", source);
        }
    }

    static List<ClassToGenerateInfo> GetTypesToGenerate(Compilation compilation, IEnumerable<ClassDeclarationSyntax> classes, CancellationToken ct)
    {
        // Create a list to hold the output
        List<ClassToGenerateInfo> classesToGenerate = new();

        // Get the semantic representation of our marker attribute 
        INamedTypeSymbol? attributeSymbol = compilation.GetTypeByMetadataName("CodeGenerationSample.ImplementEquatableAttribute");

        if (attributeSymbol is null)
        {
            // If this is null, the compilation couldn't find the marker attribute type
            // which suggests there's something very wrong!
            return classesToGenerate;
        }

        foreach (ClassDeclarationSyntax classDeclarationSyntax in classes)
        {
            ct.ThrowIfCancellationRequested();

            // Get the semantic representation of the class syntax
            SemanticModel semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
            {
                // something went wrong
                continue;
            }

            // Get the class and namespace names
            string className = classSymbol.Name;
            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            // Create an EnumToGenerate for use in the generation phase
            classesToGenerate.Add(new ClassToGenerateInfo(className, namespaceName));
        }

        return classesToGenerate;
    }

    private const string attributeText = """
        // <generated />
        using System;
        namespace CodeGenerationSample;

        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
        sealed class ImplementEquatableAttribute : Attribute
        {
            public ImplementEquatableAttribute() { }
        }
        """;

    public static string GenerateEquatableImlementation(ClassToGenerateInfo classToGenerate)
    {
        string source = $$"""
            // <generated />

            #nullable enable
            using System;

            namespace {{classToGenerate.Namespace}};

            public partial class {{classToGenerate.Name}} : IEquatable<{{classToGenerate.Name}}>
            {
                private static partial bool IsTheSame({{classToGenerate.Name}}? left, {{classToGenerate.Name}}? right);

                public override bool Equals(object? obj) => this == obj as {{classToGenerate.Name}};

                public bool Equals({{classToGenerate.Name}}? other) => this == other;

                public static bool operator==({{classToGenerate.Name}}? left, {{classToGenerate.Name}}? right) => 
                    IsTheSame(left, right);

                public static bool operator!=({{classToGenerate.Name}}? left, {{classToGenerate.Name}}? right) =>
                    !(left == right);
            }

            #nullable restore
            """;
        return source;
    }
}

public readonly record struct ClassToGenerateInfo(string Name, string? Namespace = default);
