using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System.Text;

using static InitializeSample.InitializeGenerator;

namespace InitializeSample;

[Generator]
public class InitializeGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        static IEnumerable<INamespaceSymbol> GetAllNamespaces(INamespaceSymbol root)
        {
            yield return root;
            foreach (var child in root.GetNamespaceMembers())
            {
                foreach (var next in GetAllNamespaces(child))
                {
                    yield return next;
                }
            }
        }

        static IEnumerable<INamedTypeSymbol> AllNestedTypesAndSelf(INamedTypeSymbol type)
        {
            yield return type;
            foreach (var typeMember in type.GetTypeMembers())
            {
                foreach (var nestedType in AllNestedTypesAndSelf(type))
                {
                    yield return nestedType;
                }
            }
        }

        List<INamedTypeSymbol> referencedTypes = new();
        foreach (var reference in context.Compilation.References)
        {
            var assemblySymbol = context.Compilation.GetAssemblyOrModuleSymbol(reference) as IAssemblySymbol;
            
            if (assemblySymbol is null) continue;
            var namespaces = GetAllNamespaces(assemblySymbol.GlobalNamespace).ToList();
            var types = namespaces.SelectMany(ns => ns.GetTypeMembers()).ToList();
            // var types2 = types.SelectMany(t => AllNestedTypesAndSelf(t)).ToList();
            var types3 = types.Where(t => t is
            {
                TypeKind: TypeKind.Class,
                DeclaredAccessibility: Accessibility.Public
            }).ToList();

            var types4 = types3.Where(
                t => t.Interfaces
                    .Any(its => its.Name == "IInitialize" || its.Name == "ISerializable")).ToList();
        }
        if (context.SyntaxContextReceiver is SyntaxReceiver syntaxReceiver)
        {
            context.AddSource("Init.g.cs", SourceText.From(GetInitClassSource(syntaxReceiver.ClassesToInvoke, context, referencedTypes), Encoding.UTF8, SourceHashAlgorithm.Sha256));
        }
    }

    private string GetInitClassSource(IEnumerable<ClassDeclarationSyntax> classes, GeneratorExecutionContext context, IEnumerable<INamedTypeSymbol> referencedTypes)
    {
        StringBuilder initializeClass = new();
        const string initializeClassPre = $$"""
            // generated code
            namespace CNinnovation.Samples;

            public static partial class Initialize
            {
                public static void Init()
                {                    
        
            """;
        initializeClass.Append(initializeClassPre);

        foreach (var item in classes)
        {
            SemanticModel model = context.Compilation.GetSemanticModel(item.SyntaxTree);
            var typeSymbol = model.GetDeclaredSymbol(item);
            if (typeSymbol is null) throw new InvalidOperationException();
            var ns = typeSymbol.ContainingNamespace.ToDisplayString();
            if (ns == "<global namespace>") ns = null;
            string typeName = ns == null ? typeSymbol.Name : ns + "." + typeSymbol.Name;

            string invocationCode = $$"""
                    {{ typeName }} {{ typeSymbol.Name.ToLower() }} = new();
                    {{ typeSymbol.Name.ToLower() }}.Init();
                        
            """;
            initializeClass.Append(invocationCode);
        }

        foreach (var referencedType in referencedTypes)
        {
            string invocationCode = $$"""
                    {{ referencedType.ContainingNamespace }}.{{ referencedType.Name }} {{ referencedType.Name.ToLower()}} = new();
                    {{ referencedType.Name.ToLower() }}.Init();
            """;
            initializeClass.Append(invocationCode);
        }

        const string initializeClassPost = """
                }
            }
            """;
        initializeClass.Append(initializeClassPost);

        return initializeClass.ToString();
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization(init =>
        {
            const string initializeAttribute = """
            // generated code
            namespace CNinnovation.Samples;
            
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class InitializeAttribute : Attribute
            {
            }
            """;
            init.AddSource("InitializeAttribute.g.cs", SourceText.From(initializeAttribute, Encoding.UTF8, SourceHashAlgorithm.Sha256));
        });
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    internal class SyntaxReceiver : ISyntaxContextReceiver
    {
        public List<ClassDeclarationSyntax> ClassesToInvoke { get; } = new();
        // public bool CreateInit { get; set; } = false;
        //public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        //{
        //    //if (!CreateInit)
        //    //{
        //    //    CreateInit = syntaxNode is AttributeListSyntax als &&
        //    //        als.Attributes.Any(a => a.Name.ToString() == "Initialize" || a.Name.ToString() == "InitializeAttribute");
        //    //}

        //    if (syntaxNode is ClassDeclarationSyntax classDeclaration)
        //    {
        //        if (classDeclaration.BaseList?
        //        if (classDeclaration.Ancestors()
        //            .Any(a => a is InterfaceDeclarationSyntax interfaceDeclaration && interfaceDeclaration.GetType().Name == "IInitialize"))
        //        {
        //            ClassesToInvoke.Add(classDeclaration);
        //        }
        //    }
        //}

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax classDeclaration)
            {
                if (classDeclaration.BaseList is null) return;
                foreach (var n in classDeclaration.BaseList.Types)
                {
                    if (n.ToString() == "IInitialize")
                    {
                        ClassesToInvoke.Add(classDeclaration);
                    }
                } 
            }
        }
    }
}
