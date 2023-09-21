using BooksLib.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using GenericViewModels.Services;

using System.Collections.ObjectModel;

namespace BooksLib.Services;

public class BooksService(IBooksRepository repository) : ObservableObject, IItemsService<Book>
{
    private readonly IBooksRepository _booksRepository = repository;

    public event EventHandler<Book>? SelectedItemChanged;

    public ObservableCollection<Book> Items { get; } = new();

    private Book? _selectedItem;
    public Book? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value is not null && SetProperty(ref _selectedItem, value) && _selectedItem is not null)
            {
                SelectedItemChanged?.Invoke(this, _selectedItem);
            }
        }
    }

    public async Task<Book> AddOrUpdateAsync(Book book)
    {
        if (book.BookId == 0)
        {
            return await _booksRepository.AddAsync(book);
        }
        else
        {
            return await _booksRepository.UpdateAsync(book);
        }
    }

    public Task DeleteAsync(Book book) =>
        _booksRepository.DeleteAsync(book.BookId);

    public async Task RefreshAsync()
    {
        IEnumerable<Book> books = await _booksRepository.GetItemsAsync();
        Items.Clear();
        foreach (var book in books)
        {
            Items.Add(book);
        }
        SelectedItem = Items.FirstOrDefault();
    }
}
