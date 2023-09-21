using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft;
using System.Text;

using VerifyCS = InitializeSample.Tests.CSharpSourceGeneratorVerifier<InitializeSample.InitializeGenerator>;


namespace InitializeSample.Tests;
public class UnitTest1
{
    [Fact]
    public async Task TestA()
    {
        var code1 = """
            [assembly: CNinnovation.Samples.Initialize]
            """;

        var code2 = """
            using CNinnovation.Samples;
            
            namespace Foo1Customeer;
            
            public class Foo1 : IInitialize
            {
                public void Init()
                {
                    Console.WriteLine("Foo1.Init");
                }
            }
            """;

        var code3 = """
            using CNinnovation.Samples;
            
            namespace Foo2Customeer;
            
            public class Foo2 : IInitialize
            {
                public void Init()
                {
                    Console.WriteLine("Foo2.Init");
                }
            }
            """;

        var generatedAttribtue = """
            // generated code
            namespace CNinnovation.Samples;
            
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class InitializeAttribute : Attribute
            {
            }
            """;
        
        var generatedInitialize = """
            // generated code
            namespace CNinnovation.Samples;
            
            public static partial class Initialize
            {
                public static void Init()
                {
                    Foo1 foo1 = new();
                    foo1.Init();
                    
                    Foo2 foo2 = new();
                    foo2.Init();
                }
            }
            """;

        await new VerifyCS.TestX
        {
            TestState =
            {
                Sources = { code1, code2, code3 },
                GeneratedSources =
                {
                    (typeof(InitializeGenerator), "Init.g.cs", SourceText.From(generatedInitialize, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                    (typeof(InitializeGenerator), "InitializeAttribute.g.cs", SourceText.From(generatedAttribtue, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }
}