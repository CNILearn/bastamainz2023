using System.Text.Json;
using System.Text.Json.Serialization;

Book b = new("Professional C#", "Wiley", "3443");
string json = JsonSerializer.Serialize(b, typeof(Book), BooksContext.Default);
Console.WriteLine(json);

public class Book(string title, string? publisher = default, string? isbn = default)
{
    public string _isbn = isbn ?? string.Empty;
    public string Title { get; set; } = title;
    [JsonPropertyName("vendor")]
    public string? Publisher { get; set; } = publisher;
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