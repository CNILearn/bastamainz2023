using BookSample.Data.Models;
using BookSample.Data.Repositories;

namespace BookSample.Services.Books;

public class BookService(IBookRepository bookRepository, IBookEventPublisher bookEventPublisher) : IBookService
{
    public IQueryable<Book> GetBooksQueryable() => bookRepository.GetBooksQueryable();

    public IQueryable<Book> GetBookQueryable(long id) => bookRepository.GetBookQueryable(id);

    public async Task CreateBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        await bookRepository.CreateBookAsync(book, cancellationToken);
        await bookEventPublisher.FireBookAddedAsync(book, cancellationToken);
    }

    public async Task UpdateBookAsync(long bookId, Book book, CancellationToken cancellationToken = default)
    {
        await bookRepository.UpdateBookAsync(bookId, book, cancellationToken);
        await bookEventPublisher.FireBookUpdatedAsync(book, cancellationToken);
    }

    public async Task DeleteBookAsync(long bookId, CancellationToken cancellationToken = default)
    {
        await bookRepository.DeleteBookAsync(bookId, cancellationToken);
        await bookEventPublisher.FireBookDeletedAsync(bookId, cancellationToken);
    }
}
