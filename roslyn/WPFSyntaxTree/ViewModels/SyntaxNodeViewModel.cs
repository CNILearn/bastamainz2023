﻿using Microsoft.CodeAnalysis;

namespace WPFSyntaxTree.ViewModels;

public class SyntaxNodeViewModel(SyntaxNode syntaxNode)
{
    public SyntaxNode SyntaxNode { get; } = syntaxNode;

    public IEnumerable<SyntaxNodeViewModel> Children => SyntaxNode.ChildNodes()
        .Select(n => new SyntaxNodeViewModel(n));
    public IEnumerable<SyntaxTokenViewModel> Tokens => SyntaxNode.ChildTokens()
        .Select(t => new SyntaxTokenViewModel(t));

    public string TypeName => SyntaxNode.GetType().Name;

    public IEnumerable<SyntaxTriviaViewModel> Trivia
    {
        get
        {
            var leadingTrivia = SyntaxNode.GetLeadingTrivia().Select(t => new SyntaxTriviaViewModel(TriviaKind.Leading, t));
            var trailingTrivia = SyntaxNode.GetTrailingTrivia().Select(t => new SyntaxTriviaViewModel(TriviaKind.Trailing, t));
            return leadingTrivia.Union(trailingTrivia);
        }
    }
}
