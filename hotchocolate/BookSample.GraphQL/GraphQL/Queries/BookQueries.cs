using BookSample.Data.Models;
using BookSample.Services.Books;

namespace BookSample.GraphQL.GraphQL.Queries;

[ExtendObjectType<Query>]
public class BookQueries
{
    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks([Service] IBookService bookService) =>
        bookService.GetBooksQueryable();

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Book> GetBookById([ID] long id, [Service] IBookService bookService) =>
        bookService.GetBookQueryable(id);
}
