using Microsoft.Extensions.Logging;

namespace PrimaryConstructors;
public class BooksService
{
    private readonly ILogger<BooksService> _logger;
    private readonly IBooksRepository _booksRepository;
    public BooksService(IBooksRepository booksRepository, ILogger<BooksService> logger)
    {
        _logger = logger;
        _booksRepository = booksRepository;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        _logger.LogInformation("Getting books");
        return await _booksRepository.GetBooksAsync();
    }
}

public interface IBooksRepository
{
    Task<IEnumerable<Book>> GetBooksAsync();
}