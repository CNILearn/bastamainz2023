using BookSample.Data.Models;

namespace BookSample.GraphQL.GraphQL.Types;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}
