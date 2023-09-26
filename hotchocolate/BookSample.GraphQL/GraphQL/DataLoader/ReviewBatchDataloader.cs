using BookSample.ReviewAPIClient;
using BookSample.ReviewAPIClient.Models;

namespace BookSample.GraphQL.GraphQL.DataLoader;

public class ReviewBatchDataloader : BatchDataLoader<long, IEnumerable<Review>>
{
    private static readonly List<Review> s_emptyReviewList = new();

    private readonly ReviewsClient _reviewsClient;

    public ReviewBatchDataloader(ReviewsClient reviewsClient, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _reviewsClient = reviewsClient;
    }

    internal int? TakeParameter { get; set; }

    protected override async Task<IReadOnlyDictionary<long, IEnumerable<Review>>> LoadBatchAsync(IReadOnlyList<long> keys, CancellationToken cancellationToken)
    {
        var result = await _reviewsClient.Api
            .Reviews
            .GetAsync(config =>
            {
                config.QueryParameters.BookIds = keys.ToStrings();
                config.QueryParameters.Take = TakeParameter;
            }, cancellationToken)
            ?? s_emptyReviewList;
        return result
            .Where(x => x.BookId != default)
            .GroupBy(x => x.BookId!.Value)
            .ToDictionary(x => x.Key, x => x.AsEnumerable());
    }
}