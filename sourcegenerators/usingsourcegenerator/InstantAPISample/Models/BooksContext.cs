using Microsoft.EntityFrameworkCore;

namespace InstantAPISample.Models;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {      
    }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var books = Enumerable.Range(1, 10)
            .Select(
                n => new Book { Id = n, Title = $"sample title {n}", Publisher = "sample pub" });   
    }
}
