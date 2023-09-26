namespace BookSample.ReviewAPI.Data.Models;

public readonly record struct Rating(
    long BookId,
    double AverageStars,
    int NumberOf1Star,
    int NumberOf2Stars,
    int NumberOf3Stars,
    int NumberOf4Stars,
    int NumberOf5Stars
);