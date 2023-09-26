using BookSample.Data.Models;

namespace BookSample.GraphQL.GraphQL.Types;

public class PublisherType : ObjectType<Publisher>
{
    protected override void Configure(IObjectTypeDescriptor<Publisher> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}
