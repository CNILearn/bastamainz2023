using BookSample.Data.Models;

namespace BookSample.Services.Books;

public interface IBookEventPublisher
{
    ValueTask FireBookAddedAsync(Book book, CancellationToken cancellationToken);

    ValueTask FireBookUpdatedAsync(Book book, CancellationToken cancellationToken);

    ValueTask FireBookDeletedAsync(long bookId, CancellationToken cancellationToken);
}
