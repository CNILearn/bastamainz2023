using BookSample.Data.Models;

namespace BookSample.GraphQL.GraphQL.Types;

public class GenreType : ObjectType<Genre>
{
    protected override void Configure(IObjectTypeDescriptor<Genre> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}