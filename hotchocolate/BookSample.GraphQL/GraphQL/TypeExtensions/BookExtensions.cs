using BookSample.Data.Models;
using BookSample.GraphQL.GraphQL.DataLoader;
using BookSample.ReviewAPIClient;
using BookSample.ReviewAPIClient.Models;

namespace BookSample.GraphQL.GraphQL.TypeExtensions;

[ExtendObjectType<Book>]
public class BookExtensions
{
    private static readonly Dictionary<long, Rating> s_emptyRatingList = new();

    public async Task<IEnumerable<Review>> GetReviews([Parent] Book book, [Argument] int? take, ReviewBatchDataloader reviewBatchDataloader, CancellationToken cancellationToken)
    {
        reviewBatchDataloader.TakeParameter = take;
        return await reviewBatchDataloader.LoadAsync(book.Id, cancellationToken);
    }

    //public async Task<Rating?> GetRating([Parent] Book book, RatingBatchDataloader ratingBatchDataloader, CancellationToken cancellationToken) =>
    //    await ratingBatchDataloader.LoadAsync(book.Id, cancellationToken);

    [DataLoader(AccessModifier = DataLoaderAccessModifier.PublicInterface)]
    internal static async Task<IReadOnlyDictionary<long, Rating>> RatingById(IReadOnlyList<long> bookIds, ReviewsClient reviewsClient, CancellationToken cancellationToken)
    {
        var result = await reviewsClient.Api
            .Ratings
            .GetAsync(x => x.QueryParameters.BookIds = bookIds.ToStrings(), cancellationToken);

        return result?
            .Where(x => x.BookId.HasValue)
            .ToDictionary(x => x.BookId!.Value)
            ?? s_emptyRatingList;
    }

    public async Task<Rating> GetRatingAsync([Parent] Book parent, IRatingByIdDataLoader dataloader, CancellationToken cancellationToken) =>
        await dataloader.LoadAsync(parent.Id, cancellationToken);

    //[UseDataLoader(typeof(RatingBatchDataloader))]
    //public long GetRating([Parent] Book book) =>
    //    book.Id;
}
