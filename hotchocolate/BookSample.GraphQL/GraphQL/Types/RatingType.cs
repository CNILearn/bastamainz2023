using BookSample.ReviewAPIClient.Models;

namespace BookSample.GraphQL.GraphQL.Types;

public class RatingType : ObjectType<Rating>
{
    protected override void Configure(IObjectTypeDescriptor<Rating> descriptor)
    {
        descriptor.Field(x => x.GetFieldDeserializers()).Ignore();
    }
}
