using Bogus;
using BookSample.ReviewAPI.Data.Models;
using System.Collections.Frozen;

namespace BookSample.ReviewAPI.Data.Repositories;

public class ReviewRepository
{
    private static readonly Faker<Review> s_reviewFaker = new Faker<Review>("en_US")
        .RuleFor(x => x.Id, (faker) => faker.IndexFaker)
        .RuleFor(x => x.BookId, faker => faker.PickRandom(Enumerable.Range(1, 100)))
        .RuleFor(x => x.Username, (faker) => faker.Internet.UserName())
        .RuleFor(x => x.Text, (faker) => faker.Lorem.Paragraph(3))
        .RuleFor(x => x.Stars, (faker) => faker.Random.Number(1, 5));

    private static readonly IDictionary<long, IEnumerable<Review>> s_demoReviews = s_reviewFaker.GenerateForever()
        .Take(1000)
        .GroupBy(x => x.BookId)
        .ToFrozenDictionary(x => x.Key, x => x.AsEnumerable());

    private static readonly IDictionary<long, Rating> s_demoRatings = s_demoReviews
        .Select(reviews => new Rating(
                reviews.Key,
                reviews.Value.Average(x => x.Stars),
                reviews.Value.Count(x => x.Stars == 1),
                reviews.Value.Count(x => x.Stars == 2),
                reviews.Value.Count(x => x.Stars == 3),
                reviews.Value.Count(x => x.Stars == 4),
                reviews.Value.Count(x => x.Stars == 5)
            ))
        .ToFrozenDictionary(x => x.BookId, x => x);

    public IEnumerable<Review> GetReviews(IEnumerable<long> bookIds, int? take) =>
        bookIds.SelectMany(id =>
            s_demoReviews.TryGetValue(id, out var reviews)
                ? take.HasValue
                    ? reviews.Take(take.Value)
                    : reviews
                : Enumerable.Empty<Review>());

    public Rating? GetRating(long bookId) =>
        s_demoRatings.TryGetValue(bookId, out var rating)
            ? rating
            : null;

    public IEnumerable<Rating> GetRatings(IEnumerable<long> bookIds) =>
        bookIds
            .Where(s_demoRatings.ContainsKey)
            .Select(id => s_demoRatings[id]);
}
