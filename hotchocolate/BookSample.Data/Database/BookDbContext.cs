using BookSample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSample.Data.Database;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Publisher> Publishers => Set<Publisher>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
