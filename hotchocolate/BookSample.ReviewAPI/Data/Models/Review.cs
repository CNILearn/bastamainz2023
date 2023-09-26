namespace BookSample.ReviewAPI.Data.Models;

public class Review
{
    public long Id { get; set; }

    public long BookId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public int Stars { get; set; }
}
