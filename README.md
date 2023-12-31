![Footer](./Hero.jpg)

# BASTA! Mainz 2023

## Don't miss a Beat with C# 11 and 12

Dienstag, 26. September 2023, 10:45 - 11:45, Gutenbergsaal 1

Christian Nagel

C# 11 ist released, C# 12 steht vor der Tür. Seit vielen C#-Versionen gibt es Erweiterungen beim Pattern Matching - so auch mit C# 11. Raw string literals, required members, abstrakte statische Members in Interfaces sind Features, die mit C# 11 verfügbar sind. Für C# 12 sind u. a. primary constructors, semi-auto properties und roles geplant. Diese Session bietet einen Überblick über die neuen Features und zeigt Ihnen, wie Sie diese in Ihren Projekten einsetzen können.

[Slides](slides/CSharp11-12.pdf)

### Samples

* [UTF 8 and raw string literals](csharp/01-Strings/)
* [Alias any type](csharp/02a-Alias/)
* [Required modifier](csharp/02b-RequiredModifier/)
* [Primary constructors](csharp/02c-PrimaryConstructors/)
* [Static members in interfaces](csharp/03a-ParsableSample/)
* [Static members in interfaces](csharp/03b-MathSample/)
* [Collection literals](csharp/04a-CollectionLiterals/)
* [List pattern matching](csharp/04b-ListPatternMatching/)
* [List pattern matching](csharp/04c-ListPatterns/)
* [Inline arrays](csharp/04d-InlineArrays/)
* [Unsafe accessors](csharp/05a-UnsafeAccessors/)
* [Interceptors](csharp/05b-Interceptors/)
* [Native AOT](csharp/05c_NativeAOT/)

## Creating Source Code with Source Generators

Dienstag, 26. September 2023, 17:00 - 18:00, Gutenbergsaal 1

Christian Nagel

Source Generators sind eine neue Möglichkeit, Code zu generieren. Der Code wird dabei schon während des Tippens im Editor erzeugt. Damit steht Intellisense auch gleich zur Verfügung. In dieser Session lernen Sie die Grundlagen der Incremental Source Generators, und wie Sie Source Generators, die bereits mit .NET mitgeliefert werden, in Ihren Applikationen verwenden können, darunter z. B. Logging, JSON Serialisierung, Regular Expressions, JavaScript Interop und mehr.

[Slides - Creating Source Code with Source Generators](slides/SourceGenerators.pdf)

### Samples

#### Roslyn

* [Syntax Query](roslyn/SyntaxQuery/)
* [Syntax Walker](roslyn/SyntaxWalker/)
* [Syntax Rewriter](roslyn/SyntaxRewriter/)
* [Semantics Compilation](roslyn/SemanticsCompilation/)
* [Refactoring](roslyn/PropertyCodeRefactoring/)
* [WPF Syntax Tree](roslyn/WpfSyntaxTree/)

#### Using Source Generators

* [Regular Expressions](sourcegenerators/usingsourcegenerators/RegularExpressionSample/)
* [PInvoke](sourcegenerators/usingsourcegenerators/PInvoke/)
* [Logging](sourcegenerators/usingsourcegenerators/Logging/)
* [JSON Serialization](sourcegenerators/usingsourcegenerators/JsonSerialization/)
* [JavaScript Interop](sourcegenerators/usingsourcegenerators/BlazorWasmSample/)
* [Native AOT](sourcegenerators/usingsourcegenerators/NativeAOT/)

#### Creating Source Generators

* [Hello World](sourcegenerators/sourcegenerator/01-hello/)
* [Using attributes](sourcegenerators/sourcegenerator/02-Attribute/)
* [Chaining generators](sourcegenerators/sourcegenerator/03-MoreGeneric/)

[How to - creating a simple source generator](sourcegenerators/sourcegenerator/createsimplesourcegenerator.md)

### Links

* [Incremental Source Generators](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md​)
* [Source Generators](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.md​)
* [Source Generator Cookbook](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md)

## HotChocolate: ein Heißgetränk oder ein GraphQL-Backend für .NET EntwicklerInnen
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![GraphQL](https://img.shields.io/badge/GraphQl-E10098?style=for-the-badge&logo=graphql&logoColor=white)
![Postgres](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)

Mittwoch, 27. September 2023, 16:45 - 17:45, Watfordsaal

Sebastian Szvetecz

Alle Liebhaber:innen von heißer Schokolade bzw. Kakao muss ich jetzt leider enttäuschen. HotChocolate ist in diesem Kontext kein Heißgetränk, sondern ein GraphQL-Backend für .NET-Entwickler:innen. GraphQL ist eine Abfragesprache für APIs, gilt als DER Gegner von REST und wird unter anderem vom Facebook, GitHub und Pinterest verwendet. In dieser Session werden wir noch genauer aufarbeiten, was GraphQL ist und warum GraphQL ein würdiger Gegner von REST ist. Außerdem werden wir uns ansehen wie einfach man mit ASP.NET Core und HotChocolate ein GraphQL API umsetzen kann. Unser GraphQL API soll nicht nur mit Hilfe von Entity Framework Core auf eine Datenkbank zugreifen, sie soll dem Client auch ermöglichen, Daten zu filtern, zu sortieren und zu paginieren, um einen möglichst flexiblen Datenzugriff zu ermöglichen. Filtern, Sortieren und Paginieren – klingt aufwendig? Nein, nicht mit HotChocolate . Aber auch für fortgeschrittenere Szenarien wie z. B. langsame Teilabfragen ist man mit HotChocolate gerüstet. Mehr dazu in dieser Session...

### Sample

[Booksample](hotchocolate/)

### Links

* [GraphQL](https://graphql.org/)
* [HotChocolate-Docs](https://chillicream.com/docs/hotchocolate/v13)
* [HotChocolate-Github](https://github.com/ChilliCream/graphql-platform)