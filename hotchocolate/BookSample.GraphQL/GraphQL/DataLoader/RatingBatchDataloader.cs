using BookSample.ReviewAPIClient.Models;
using BookSample.ReviewAPIClient;

namespace BookSample.GraphQL.GraphQL.DataLoader;

public class RatingBatchDataloader : BatchDataLoader<long, Rating>
{
    private static readonly Dictionary<long, Rating> s_emptyRatingList = new();

    private readonly ReviewsClient _reviewsClient;

    private readonly ILogger _logger;

    public RatingBatchDataloader(ReviewsClient reviewsClient, ILogger<RatingBatchDataloader> logger, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _reviewsClient = reviewsClient;
        _logger = logger;
    }

    protected override async Task<IReadOnlyDictionary<long, Rating>> LoadBatchAsync(IReadOnlyList<long> keys, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Making HTTP call to ReviewAPI to get rating for {keys}.", keys);

        var result = await _reviewsClient.Api
            .Ratings
            .GetAsync(x => x.QueryParameters.BookIds = keys.ToStrings(), cancellationToken);

        return result?
            .Where(x => x.BookId.HasValue)
            .ToDictionary(x => x.BookId!.Value)
            ?? s_emptyRatingList;
    }
}