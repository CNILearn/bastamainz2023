namespace BookSample.GraphQL.GraphQL.Mutations.Inputs;

public class UpdateBookInput : SaveBookInput
{
    public required long BookId { get; set; }
}
