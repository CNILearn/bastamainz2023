namespace BookSample.Data.Models;

public class Book
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public long AuthorId { get; set; }

    public Author? Author { get; set; }

    public long PublisherId { get; set; }

    public Publisher? Publisher { get; set; }

    public string GenreId { get; set; } = string.Empty;

    public Genre? Genre { get; set; }

    public DateOnly PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastModifiedAt { get; set; }
}
