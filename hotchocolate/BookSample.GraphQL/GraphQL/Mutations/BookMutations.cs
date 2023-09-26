using BookSample.Data.Models;
using BookSample.GraphQL.GraphQL.Mutations.Inputs;
using BookSample.GraphQL.Mapping;
using BookSample.Services.Books;

namespace BookSample.GraphQL.GraphQL.Mutations;

[ExtendObjectType<Mutation>]
public class BookMutations
{
    [UseMutationConvention(PayloadFieldName = "createdBook")]
    [UseFirstOrDefault]
    [UseProjection]
    public async Task<IQueryable<Book>> CreateBookAsync([Service] IBookService bookService, [Argument] CreateBookInput input, CancellationToken cancellationToken)
    {
        Book book = input.ToModel();
        await bookService.CreateBookAsync(book, cancellationToken);
        return bookService.GetBookQueryable(book.Id);
    }

    [UseMutationConvention(PayloadFieldName = "updatedBook")]
    [UseFirstOrDefault]
    [UseProjection]
    public async Task<IQueryable<Book>> UpdateBookAsync([Service] IBookService bookService, [Argument] UpdateBookInput input, CancellationToken cancellationToken)
    {
        Book book = input.ToModel();
        await bookService.UpdateBookAsync(input.BookId, book, cancellationToken);
        return bookService.GetBookQueryable(book.Id);
    }

    [UseMutationConvention(PayloadFieldName = "deletedBookId")]
    public async Task<long> DeleteBookAsync([Service] IBookService bookService, [Argument][ID] long bookId, CancellationToken cancellationToken)
    {
        await bookService.DeleteBookAsync(bookId, cancellationToken);
        return bookId;
    }
}
