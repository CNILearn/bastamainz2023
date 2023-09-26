using BookSample.Data.Database;
using BookSample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSample.Data.Repositories;

public class BookRepository(IDbContextFactory<BookDbContext> dbContextFactory) : IBookRepository, IAsyncDisposable
{
    private readonly BookDbContext _dbContext = dbContextFactory.CreateDbContext();

    public IQueryable<Book> GetBooksQueryable() => _dbContext.Books;

    public IQueryable<Book> GetBookQueryable(long id) => GetBooksQueryable().Where(x => x.Id == id);

    public async Task CreateBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        _dbContext.Books.Add(book);
        book.CreatedAt = DateTime.UtcNow;
        book.LastModifiedAt = book.CreatedAt;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateBookAsync(long bookId, Book book, CancellationToken cancellationToken = default)
    {
        book.Id = bookId;
        _dbContext.Books.Update(book);
        book.LastModifiedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBookAsync(long bookId, CancellationToken cancellationToken = default)
    {
        await _dbContext.Books.Where(x => x.Id == bookId).ExecuteDeleteAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync() =>
        await _dbContext.DisposeAsync();
}
