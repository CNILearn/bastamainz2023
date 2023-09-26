namespace BookSample.GraphQL.GraphQL.Mutations.Inputs;

public abstract class SaveBookInput
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string ISBN { get; set; }

    public long AuthorId { get; set; }

    public long PublisherId { get; set; }

    public required string GenreId { get; set; }

    public DateOnly PublishedAt { get; set; }
}
