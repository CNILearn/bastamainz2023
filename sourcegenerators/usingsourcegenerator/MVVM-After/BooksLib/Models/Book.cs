using CommunityToolkit.Mvvm.ComponentModel;

namespace BooksLib.Models;

public partial class Book(string? title = null, string? publisher = null, int id = 0) : ObservableObject
{
    public int BookId { get; set; } = id;

    [ObservableProperty]
    private string _title = title ?? string.Empty;

    [ObservableProperty]
    private string _publisher = publisher ?? string.Empty;

    public override string ToString() => Title;
}
