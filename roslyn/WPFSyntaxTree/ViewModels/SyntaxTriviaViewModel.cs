using Microsoft.CodeAnalysis;

namespace WPFSyntaxTree.ViewModels;

public enum TriviaKind
{
    Leading,
    Trailing,
    Structured,
    Annotated
}

public class SyntaxTriviaViewModel(TriviaKind kind, SyntaxTrivia syntaxTrivia)
{
    public SyntaxTrivia SyntaxTrivia { get; } = syntaxTrivia;
    public TriviaKind TriviaKind { get; } = kind;

    public override string ToString() => $"{TriviaKind}, Start: {SyntaxTrivia.Span.Start}, Length: {SyntaxTrivia.Span.Length} : {SyntaxTrivia}";

}
