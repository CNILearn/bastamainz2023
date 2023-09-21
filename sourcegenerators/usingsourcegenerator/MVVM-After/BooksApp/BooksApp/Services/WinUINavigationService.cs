using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace BooksApp.Services;

public class WinUINavigationService : ObservableObject, INavigationService, IRecipient<NavigationMessage>
{
    private readonly WinUIInitializeNavigationService _initializeNavigation;

    public WinUINavigationService(WinUIInitializeNavigationService initializeNavigationService)
    {
        _initializeNavigation = initializeNavigationService;
        WeakReferenceMessenger.Default.Register<NavigationMessage>(this);
    }

    private bool _useNavigation;
    public bool UseNavigation
    {
        get => _useNavigation;
        set => SetProperty(ref _useNavigation, value);
    }
    public string CurrentPage { get; private set; } = string.Empty;

    private Frame? _frame;
    private Frame Frame => _frame ??= _initializeNavigation.Frame;

    private Dictionary<string, Type>? _pages;
    private Dictionary<string, Type> Pages => _pages ??= _initializeNavigation.Pages;

    public Task GoBackAsync()
    {
        PageStackEntry stackEntry = Frame.BackStack.Last();
        Type backPageType = stackEntry.SourcePageType;
        var pageEntry = Pages.FirstOrDefault(pair => pair.Value == backPageType);
        CurrentPage = pageEntry.Key;

        Frame.GoBack();
        return Task.CompletedTask;
    }

    public Task NavigateToAsync(string pageName)
    {
        CurrentPage = pageName;
        Frame.Navigate(Pages[pageName]);
        return Task.CompletedTask;
    }

    public void Receive(NavigationMessage message)
    {
        UseNavigation = message.Value.UseNavigation;
    }
}
