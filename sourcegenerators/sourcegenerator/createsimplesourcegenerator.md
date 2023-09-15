# Create and use a Source Generator

## Hello World Source Generator

Create a source generator to return a Hello, World app

.NET Standard 2.0 Library

Packages:

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis" Version="4.4.0" PrivateAssets="all" />
    <PackageReference Include="Microsoft.CodeAnalysis.Analyzers" Version="3.3.4" PrivateAssets="all" />
    <PackageReference Include="Microsoft.CodeAnalysis.CSharp.Workspaces" Version="4.4.0" PrivateAssets="all" />
  </ItemGroup>
```

C# Version

```xml
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <LangVersion>latest</LangVersion>
    <Version>1.0.1</Version>
    <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  </PropertyGroup>
```

More settings

```xml
  <PropertyGroup>
    <EnforceExtendedAnalyzerRules>true</EnforceExtendedAnalyzerRules>
    <IncludeBuildOutput>false</IncludeBuildOutput> <!-- Do not include as a lib dependency -->
    <DevelopmentDependency>true</DevelopmentDependency>
    <PackageProjectUrl>https://github.com/cnilearn/bastaspring2023</PackageProjectUrl>
    <RepositoryUrl>https://github.com/cnilearn/bastaspring2023</RepositoryUrl>
  </PropertyGroup>
```

Analyzer:

```xml
  <ItemGroup>
    <None Include="$(OutputPath)\$(AssemblyName).dll" Pack="true" PackagePath="analyzers/dotnet/cs" Visible="false" />
    <None Include="_._" Pack="true" PackagePath="lib/netstandard2.0" />
    <!-- https://docs.microsoft.com/en-us/nuget/reference/errors-and-warnings/nu5128#scenario-2 -->
  </ItemGroup>
```

Initialize method

```csharp
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context =>
        {
            context.AddSource("hello.g.cs", SourceText.From(helloWorld, Encoding.UTF8));
        });
    }
```	

Hello, World


```csharp
    private const string helloWorld = """
        // <generated />

        using System;

        namespace Sample;
        public static class Hello
        {
            public static void World() => Console.WriteLine("Hello BASTA!");
        }
        """;
```

Build the package, copy, create client app using it!

## Test Project

1. Xunit test project
2. package updates and additional packages (including Verify.SourceGenerators)

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.Analyzers" Version="3.3.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.4.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.4.1" />
    <PackageReference Include="Verify.SourceGenerators" Version="2.1.0" />
    <PackageReference Include="Verify.Xunit" Version="19.10.0" />
    <PackageReference Include="xunit" Version="2.4.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="coverlet.collector" Version="3.2.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>
```

3. One-time initializer for Snapshot testing

```csharp
public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
```

Create and run the generator

```csharp

internal class TestHelperHello
{
    public static SettingsTask VerifyAsync(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree });


        // Create an instance of our EnumGenerator incremental source generator
        HelloWorldGenerator generator = new();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier.Verify(driver);
    }
}
```

Unit test

```csharp
[UsesVerify]
public class HelloWorldGeneratorTests
{
    [Fact]
    public Task TestHelloWorldGenerated()
    {
        var source = """
            namespace Test;
            public class TestClass
            {
                public static void Main()
                {
                    Console.WriteLine("Test");
                }
            }
            """;
        return TestHelperHello.VerifyAsync(source);
    }
}
```