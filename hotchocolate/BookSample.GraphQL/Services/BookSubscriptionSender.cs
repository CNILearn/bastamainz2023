using BookSample.Data.Models;
using BookSample.GraphQL.GraphQL.Subscriptions;
using BookSample.Services.Books;
using HotChocolate.Subscriptions;

namespace BookSample.GraphQL.Services;

internal class BookSubscriptionSender(ITopicEventSender sender) : IBookEventPublisher
{
    public ValueTask FireBookAddedAsync(Book book, CancellationToken cancellationToken) =>
        sender.SendAsync(nameof(BookSubscriptions.BookAdded), book, cancellationToken);

    public ValueTask FireBookUpdatedAsync(Book book, CancellationToken cancellationToken) =>
        sender.SendAsync(nameof(BookSubscriptions.BookUpdated), book, cancellationToken);

    public ValueTask FireBookDeletedAsync(long bookId, CancellationToken cancellationToken) =>
        sender.SendAsync(nameof(BookSubscriptions.BookDeleted), bookId, cancellationToken);
}
