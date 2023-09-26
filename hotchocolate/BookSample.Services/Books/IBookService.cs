using BookSample.Data.Models;

namespace BookSample.Services.Books;
public interface IBookService
{
    IQueryable<Book> GetBooksQueryable();
    IQueryable<Book> GetBookQueryable(long id);
    Task CreateBookAsync(Book book, CancellationToken cancellationToken = default);
    Task DeleteBookAsync(long bookId, CancellationToken cancellationToken = default);
    Task UpdateBookAsync(long bookId, Book book, CancellationToken cancellationToken = default);
}