using CommunityToolkit.Mvvm.ComponentModel;

namespace BooksLib.Models;

public partial class Book : ObservableObject
{
    public Book(string? title = null, string? publisher = null, int id = 0)
    {
        BookId = id;
        _title = title ?? string.Empty;
        _publisher = publisher ?? string.Empty;
    }
    public int BookId { get; set; }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _publisher;

    public override string ToString() => Title;
}
