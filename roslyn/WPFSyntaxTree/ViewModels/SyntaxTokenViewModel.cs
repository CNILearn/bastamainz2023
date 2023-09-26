using Microsoft.CodeAnalysis;

namespace WPFSyntaxTree.ViewModels;

public class SyntaxTokenViewModel(SyntaxToken syntaxToken)
{
    public SyntaxToken SyntaxToken { get; } = syntaxToken;

    public string TypeName => SyntaxToken.GetType().Name;

    public override string ToString() => SyntaxToken.ToString();
}
