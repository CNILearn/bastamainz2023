using BookSample.Data.Models;
using BookSample.GraphQL.GraphQL.Mutations.Inputs;

namespace BookSample.GraphQL.Mapping;

internal static class BookMapping
{
    public static Book ToModel(this SaveBookInput input) =>
        new()
        {
            Title = input.Title,
            Description = input.Description,
            ISBN = input.ISBN,
            AuthorId = input.AuthorId,
            PublishedAt = input.PublishedAt,
            GenreId = input.GenreId,
            PublisherId = input.PublisherId,
        };
}
