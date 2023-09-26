namespace BookSample.GraphQL.GraphQL.Mutations.Inputs;

public class UpdateBookInput : SaveBookInput
{
    [ID]
    public required long BookId { get; set; }
}
