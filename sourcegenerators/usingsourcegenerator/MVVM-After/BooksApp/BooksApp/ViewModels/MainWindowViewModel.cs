﻿namespace BooksApp.ViewModels;

public class MainWindowViewModel(INavigationService navigationService, WinUIInitializeNavigationService initializeNavigationService) : ViewModelBase
{
    private readonly Dictionary<string, Type> _pages = new()
    {
        [PageNames.BooksPage] = typeof(BooksPage),
        [PageNames.BookDetailPage] = typeof(BookDetailPage)
    };

    private readonly INavigationService _navigationService = navigationService;
    private readonly WinUIInitializeNavigationService _initializeNavigationService = initializeNavigationService;

    public void SetNavigationFrame(Frame frame) => _initializeNavigationService.Initialize(frame, _pages);

    public void UseNavigation(bool navigation)
    {
        _navigationService.UseNavigation = navigation;
    }

    public void OnNavigationSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem navigationItem)
        {
            switch (navigationItem.Tag)
            {
                case "books":
                    _navigationService.NavigateToAsync(PageNames.BooksPage);
                    break;
                default:
                    break;
            }
        }
    }
}
