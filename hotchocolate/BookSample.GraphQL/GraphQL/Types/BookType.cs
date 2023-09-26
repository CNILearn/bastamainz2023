using BookSample.Data.Models;

namespace BookSample.GraphQL.GraphQL.Types;

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
        descriptor.Field(x => x.PublisherId).Ignore();
        descriptor.Field(x => x.AuthorId).Ignore();
        descriptor.Field(x => x.GenreId).Ignore();

        // Sample for Relay node resolver
        //descriptor.ImplementsNode()
        //    .ResolveNode(async (IResolverContext context, long id) => 
        //        await context.Service<IBookService>().GetBookQueryable(id).FirstOrDefaultAsync());
    }
}
