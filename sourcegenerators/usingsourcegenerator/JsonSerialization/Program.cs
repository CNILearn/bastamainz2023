// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Text.Json.Serialization;

Book b = new("Professional C#", "Wiley", "3443");
var json = JsonSerializer.Serialize(b, typeof(Book), BooksContext.Default);
Console.WriteLine(json);


public class Book
{
    public Book(string title, string? publisher = default, string? isbn = default)
    {
        Title = title;
        Publisher = publisher;
        _isbn = isbn ?? string.Empty;
    }
    public string _isbn = string.Empty;
    public string Title { get; set; }
    [JsonPropertyName("vendor")]
    public string? Publisher { get; set; }
    public override string ToString()
    {
        return $"{Title}, {Publisher}, {_isbn}";
    }
}

[JsonSourceGenerationOptions(
    WriteIndented = true, 
    IncludeFields = false, 
    IgnoreReadOnlyProperties = false,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Book))]
internal partial class BooksContext : JsonSerializerContext
{

}