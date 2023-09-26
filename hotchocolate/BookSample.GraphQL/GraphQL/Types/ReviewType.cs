using BookSample.ReviewAPIClient.Models;

namespace BookSample.GraphQL.GraphQL.Types;

public class ReviewType : ObjectType<Review>
{
    protected override void Configure(IObjectTypeDescriptor<Review> descriptor)
    {
        descriptor.Field(x => x.GetFieldDeserializers()).Ignore();
    }
}
