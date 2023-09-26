using BookSample.Data.Models;

namespace BookSample.GraphQL.GraphQL.Subscriptions;

[ExtendObjectType<Subscription>]
public class BookSubscriptions
{
    [Subscribe]
    public Book BookAdded([EventMessage] Book book) => book;

    [Subscribe]
    public Book BookUpdated([EventMessage] Book book) => book;

    [Subscribe]
    public long BookDeleted([EventMessage] long bookId) => bookId;
}
