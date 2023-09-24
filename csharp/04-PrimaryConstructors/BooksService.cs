using Microsoft.Extensions.Logging;

namespace PrimaryConstructors;
public class BooksService(IBooksRepository booksRepository, ILogger<BooksService> logger)
{
    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        logger.LogInformation("Getting books");
        return await booksRepository.GetBooksAsync();
    }
}


public interface IBooksRepository
{
    Task<IEnumerable<Book>> GetBooksAsync();
}